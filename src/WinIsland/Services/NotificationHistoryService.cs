using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace WinIsland.Services;

/// <summary>一条通知历史记录。</summary>
public sealed record NotificationHistoryEntry(DateTime Time, string Title, string Body, string Glyph)
{
    public string TimeText => Time.ToString("HH:mm:ss");
}

/// <summary>
/// 通知历史：最近 N 条通知的内存 + JSON 持久化。
/// 存储于 %APPDATA%\WinIsland\notification-history.json，上限 50 条。
/// </summary>
public sealed class NotificationHistoryService : IDisposable
{
    private const int MaxEntries = 50;
    private readonly string _file;
    private readonly object _gate = new();
    private readonly List<NotificationHistoryEntry> _entries = new();

    public NotificationHistoryService(string? file = null)
    {
        _file = file ?? Path.Combine(AppPaths.AppDataDir, "notification-history.json");
        Load();
    }

    /// <summary>按时间倒序（最新在前）的通知列表。</summary>
    public IReadOnlyList<NotificationHistoryEntry> Entries
    {
        get { lock (_gate) return _entries.OrderByDescending(e => e.Time).ToList(); }
    }

    public event EventHandler? Changed;

    public void Add(string title, string body, string glyph)
    {
        lock (_gate)
        {
            _entries.Add(new NotificationHistoryEntry(DateTime.Now, title ?? string.Empty, body ?? string.Empty, glyph ?? string.Empty));
            while (_entries.Count > MaxEntries) _entries.RemoveAt(0);
            SaveCore();
        }
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public void Clear()
    {
        lock (_gate)
        {
            _entries.Clear();
            SaveCore();
        }
        Changed?.Invoke(this, EventArgs.Empty);
    }

    private void Load()
    {
        try
        {
            if (File.Exists(_file))
            {
                var json = File.ReadAllText(_file);
                var items = JsonSerializer.Deserialize<List<NotificationHistoryEntry>>(json);
                if (items is not null)
                {
                    _entries.Clear();
                    _entries.AddRange(items.Where(e => e.Time != default));
                }
            }
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Notification history load failed: {ex.Message}");
        }
    }

    private void SaveCore()
    {
        try
        {
            AppPaths.EnsureDirectories();
            var json = JsonSerializer.Serialize(_entries);
            var tmp = _file + ".tmp";
            File.WriteAllText(tmp, json);
            File.Move(tmp, _file, overwrite: true);
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Notification history save failed: {ex.Message}");
        }
    }

    public void Dispose() { /* nothing to dispose */ }
}
