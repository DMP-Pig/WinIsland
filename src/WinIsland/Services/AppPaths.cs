using System.IO;

namespace WinIsland.Services;

/// <summary>Well-known file/directory locations used by the app.</summary>
public static class AppPaths
{
    /// <summary>
    /// %APPDATA%\WinIsland - config, logs, cache and lyrics live here.
    /// Tests can redirect via the WINISLAND_APPDATA environment variable.
    /// </summary>
    public static string AppDataDir
    {
        get
        {
            var overrideDir = Environment.GetEnvironmentVariable("WINISLAND_APPDATA");
            return string.IsNullOrWhiteSpace(overrideDir)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WinIsland")
                : overrideDir;
        }
    }

    // Note: computed (not cached) so tests/runtime can redirect via WINISLAND_APPDATA.
    public static string SettingsFile => Path.Combine(AppDataDir, "settings.json");

    public static string LogsDir => Path.Combine(AppDataDir, "logs");

    public static string ThumbCacheDir => Path.Combine(AppDataDir, "thumbcache");

    public static string LyricsDir => Path.Combine(AppDataDir, "Lyrics");

    public static string ExePath { get; } = Environment.ProcessPath ?? string.Empty;

    public static void EnsureDirectories()
    {
        Directory.CreateDirectory(AppDataDir);
        Directory.CreateDirectory(LogsDir);
        Directory.CreateDirectory(ThumbCacheDir);
        Directory.CreateDirectory(LyricsDir);
    }
}

