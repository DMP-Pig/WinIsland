using System.Text.Json;
using System.Text.Json.Serialization;

using System.IO;

namespace WinIsland.Services;

public enum IslandPosition { Center, Right }
public enum ThemeMode { Auto, Light, Dark }
public enum MonitorSelection { Primary, All, Index }

/// <summary>Persisted user configuration. JSON at %APPDATA%\WinIsland\settings.json.</summary>
public sealed class AppSettings
{
    public int Version { get; set; } = 1;

    // ── Appearance ─────────────────────────────────────────────
    public string Language { get; set; } = "zh-CN";
    public ThemeMode Theme { get; set; } = ThemeMode.Auto;
    public string AccentColor { get; set; } = "#6C5CE7";
    public IslandPosition Position { get; set; } = IslandPosition.Center;
    public MonitorSelection Monitor { get; set; } = MonitorSelection.Primary;
    public int MonitorIndex { get; set; } = 0;
    public double OffsetX { get; set; } = 0;
    public double OffsetY { get; set; } = 16;
    public double Opacity { get; set; } = 0.92;

    // ── Behavior ───────────────────────────────────────────────
    public bool IsLocked { get; set; } = true;   // 上锁后不可拖动，解锁后可拖动
    public bool HideWhenNoMedia { get; set; } = true;
    public bool ShowWhenPaused { get; set; } = true;
    public bool StartWithWindows { get; set; } = false;
    public bool StartHidden { get; set; } = false;

    // ── Compact mode content ───────────────────────────────────
    public bool CompactShowArt { get; set; } = true;
    public bool CompactShowTitle { get; set; } = true;
    public bool CompactShowProgress { get; set; } = false;

    // ── Cider ──────────────────────────────────────────────────
    public bool CiderEnabled { get; set; } = true;
    public int CiderPort { get; set; } = 0;          // 0 = auto-detect (default 10767 then scan)
    public string CiderToken { get; set; } = string.Empty;

    // ── Lyrics ─────────────────────────────────────────────────
    public bool OnlineLyricsEnabled { get; set; } = true;    // 在线歌词（网易云非官方接口）；右键灵动岛可一键开关
    public string LyricsFolder { get; set; } = string.Empty; // extra .lrc folder; empty = auto (Music)
    public bool StandaloneLyricsWindow { get; set; } = false;
    public bool KaraokeHighlight { get; set; } = true;

    // ── Volume ─────────────────────────────────────────────────
    public bool UseSystemVolume { get; set; } = true;   // for non-Cider sources, control system volume

    public AppSettings Clone() => (AppSettings)MemberwiseClone();
}

/// <summary>Loads / saves <see cref="AppSettings"/> as JSON.</summary>
public sealed class SettingsService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() },
    };

    private readonly object _gate = new();
    private AppSettings _settings;

    public SettingsService()
    {
        AppPaths.EnsureDirectories();
        _settings = Load();
    }

    public AppSettings Current => _settings;

    public event EventHandler<AppSettings>? Changed;

    private AppSettings Load()
    {
        try
        {
            if (File.Exists(AppPaths.SettingsFile))
            {
                var json = File.ReadAllText(AppPaths.SettingsFile);
                var loaded = JsonSerializer.Deserialize<AppSettings>(json, JsonOptions);
                if (loaded is not null)
                {
                    // Recreate JSON members that were added after a user's old config file.
                    return loaded;
                }
            }
        }
        catch (Exception ex)
        {
            AppLogger.Error("Failed to read settings; using defaults", ex);
        }

        return new AppSettings();
    }

    public void Save()
    {
        lock (_gate)
        {
            try
            {
                AppPaths.EnsureDirectories();
                var json = JsonSerializer.Serialize(_settings, JsonOptions);
                var tmp = AppPaths.SettingsFile + ".tmp";
                File.WriteAllText(tmp, json);
                File.Move(tmp, AppPaths.SettingsFile, overwrite: true);
            }
            catch (Exception ex)
            {
                AppLogger.Error("Failed to save settings", ex);
            }
        }
    }

    /// <summary>Replace settings with <paramref name="next"/> (e.g. from the settings UI), save and notify.</summary>
    public void Apply(AppSettings next)
    {
        lock (_gate)
        {
            _settings = next;
            Save();
        }
        Changed?.Invoke(this, _settings);
    }

    public void Update(Action<AppSettings> mutate)
    {
        lock (_gate)
        {
            mutate(_settings);
            Save();
        }
        Changed?.Invoke(this, _settings);
    }

    /// <summary>Export current settings as JSON text.</summary>
    public string Export() => JsonSerializer.Serialize(_settings, JsonOptions);

    /// <summary>Import settings from JSON text. Returns false if invalid.</summary>
    public bool TryImport(string json)
    {
        try
        {
            var parsed = JsonSerializer.Deserialize<AppSettings>(json, JsonOptions);
            if (parsed is null) return false;
            Apply(parsed);
            return true;
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Import failed: {ex.Message}");
            return false;
        }
    }
}

