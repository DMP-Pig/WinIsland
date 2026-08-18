using System.Text.Json;
using System.Text.Json.Serialization;

using System.IO;

namespace WinIsland.Services;

public enum IslandPosition { Center, Right }

/// <summary>一个媒体程序的配置：是否启用 + 在列表中的位置即优先级。</summary>
public class MediaAppEntry
{
    public string Key { get; set; } = "";   // SMTC SourceAppUserModelId（如 Cider.exe、Spotify.exe）
    public bool Enabled { get; set; } = true;
}
public enum ThemeMode { Auto, Light, Dark }
public enum MonitorSelection { Primary, All, Index }

/// <summary>灵动岛组件开关：无歌曲播放时（Idle）与有歌曲播放时（Playing）可分别勾选。</summary>
public sealed class ComponentFlags
{
    public bool TimeWhenIdle { get; set; } = true;
    public bool TimeWhenPlaying { get; set; } = false;
    public bool WeatherWhenIdle { get; set; } = false;
    public bool WeatherWhenPlaying { get; set; } = false;
    public bool CoverWhenIdle { get; set; } = false;
    public bool CoverWhenPlaying { get; set; } = true;
    public bool TitleWhenIdle { get; set; } = false;
    public bool TitleWhenPlaying { get; set; } = true;
    public bool ArtistWhenIdle { get; set; } = false;
    public bool ArtistWhenPlaying { get; set; } = true;
    public bool LyricsWhenIdle { get; set; } = false;
    public bool LyricsWhenPlaying { get; set; } = true;
    public bool ProgressWhenIdle { get; set; } = false;
    public bool ProgressWhenPlaying { get; set; } = false;
}
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
    public bool IslandAlwaysVisible { get; set; } = false;   // 常驻：始终显示（无视媒体/暂停）
    public bool ShowMediaInfo { get; set; } = true;              // 是否显示媒体播放信息（歌名/封面/歌词等）
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

    // ── Island size ────────────────────────────────────────────
    public double CompactWidth { get; set; } = 360;
    public double CompactHeight { get; set; } = 72;
    public double ExpandedWidth { get; set; } = 400;
    public double MaxExpandedHeight { get; set; } = 384;

    // ── Idle widgets（无媒体时组件）──────────────────────────
    public bool ShowWidgetsWhenNoMedia { get; set; } = false; // 无媒体时显示组件（旧开关）
    public bool WidgetShowTime { get; set; } = true;
    public bool WidgetShowWeather { get; set; } = false;
    public string WeatherCity { get; set; } = "";             // 天气城市（Open-Meteo，需联网）

    // ── 组件（灵动岛显示内容，Idle/Playing 可分别勾选）──
    public ComponentFlags Components { get; set; } = new();
    public string WidgetOrder { get; set; } = "Time,Weather"; // 组件摆放顺序（逗号分隔的键）

    // ── 媒体程序选择与顺序（空列表 = 全部启用，按默认优先级）──
    public List<MediaAppEntry> MediaApps { get; set; } = new();
    // ── Notifications ──────────────────────────────────────────
    public bool BluetoothNotifyEnabled { get; set; } = false;   // 蓝牙设备连接/断开提示
    public bool NotificationTakeoverEnabled { get; set; } = false; // 接管 Windows 通知（尽力而为）
    public int NotificationTimeoutSeconds { get; set; } = 6;      // 横幅显示时长
    public string NotificationPosition { get; set; } = "TopRight"; // TopRight = 右上角

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
                    // 兼容旧配置：补齐新增字段
                    loaded.Components ??= new ComponentFlags();
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

