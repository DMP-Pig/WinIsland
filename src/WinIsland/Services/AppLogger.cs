using System.IO;

using System.Text;

namespace WinIsland.Services;

/// <summary>
/// Minimal, dependency-free file logger. Writes one line per entry to
/// %APPDATA%\WinIsland\logs\app-yyyyMMdd.log. Never throws.
/// </summary>
public static class AppLogger
{
    private static readonly object Gate = new();
    private static string _currentDay = string.Empty;
    private static StreamWriter? _writer;

    public static void Info(string message) => Write("INFO", message);
    public static void Debug(string message) => Write("DEBUG", message);
    public static void Warn(string message) => Write("WARN", message);
    public static void Error(string message, Exception? ex = null)
        => Write("ERROR", ex is null ? message : $"{message}{Environment.NewLine}{ex}");

    private static void Write(string level, string message)
    {
        try
        {
            lock (Gate)
            {
                var now = DateTime.Now;
                var day = now.ToString("yyyyMMdd");
                if (_writer is null || day != _currentDay)
                {
                    _writer?.Dispose();
                    _currentDay = day;
                    var path = Path.Combine(AppPaths.LogsDir, $"app-{day}.log");
                    _writer = new StreamWriter(path, append: true, Encoding.UTF8);
                    _writer.AutoFlush = true;
                }

                _writer.WriteLine($"[{now:yyyy-MM-dd HH:mm:ss.fff}] [{level}] {message}");
            }
        }
        catch
        {
            // Logging must never crash the app.
        }
    }
}


