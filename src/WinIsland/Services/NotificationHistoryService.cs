using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace WinIsland.Services;

/// <summary>一条通知历史记录。Source = 来源应用（如 QQ.exe / Bluetooth），供白名单与一键打开使用。</summary>
public sealed record NotificationHistoryEntry(DateTime Time, string Title, string Body, string Glyph, string? Source)
{
    /// <summary>是否已读（横幅展示过 / 用户点过）。未读用于灵动岛角标计数。</summary>
    public bool Read { get; set; } = false;

    public string TimeText => Time.ToString("HH:mm:ss");
    public string SourceText => string.IsNullOrWhiteSpace(Source) ? string.Empty : Source;
}

/// <summary>
/// 通知历史 / 通知中心数据源：最近 N 条通知的内存 + JSON 持久化。
/// 存储于 %APPDATA%\WinIsland\notification-history.json，上限 50 条。
/// 9 通知中心一页化 / 12 通知一键处理 均基于此服务。
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

    public void Add(string title, string body, string glyph, string? source = null)
    {
        lock (_gate)
        {
            _entries.Add(new NotificationHistoryEntry(DateTime.Now,
                title ?? string.Empty, body ?? string.Empty, glyph ?? string.Empty, source));
            while (_entries.Count > MaxEntries) _entries.RemoveAt(0);
            SaveCore();
        }
        Changed?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>移除单条（通知中心 ✕ 一键删除）。</summary>
    public void Remove(NotificationHistoryEntry entry)
    {
        lock (_gate)
        {
            if (_entries.Remove(entry)) SaveCore();
        }
        Changed?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>把与标题/正文匹配的未读条目标记为已读（横幅展示过 / 用户点击处理后调用）。</summary>
    public void MarkReadMatching(string title, string body)
    {
        lock (_gate)
        {
            var hit = false;
            foreach (var e in _entries)
            {
                if (!e.Read && string.Equals(e.Title, title, StringComparison.Ordinal) &&
                    string.Equals(e.Body, body, StringComparison.Ordinal))
                {
                    e.Read = true;
                    hit = true;
                }
            }
            if (hit) SaveCore();
        }
        Changed?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>全部标记为已读（灵动岛展开 / 通知中心"全部已读"）。</summary>
    public void MarkAllRead()
    {
        lock (_gate)
        {
            var hit = false;
            foreach (var e in _entries)
            {
                if (!e.Read) { e.Read = true; hit = true; }
            }
            if (hit) SaveCore();
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