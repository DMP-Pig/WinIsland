using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Threading;

namespace WinIsland.Services;

/// <summary>一条剪贴板记录。</summary>
public sealed record ClipboardEntry(DateTime Time, string Text)
{
    public string TextPreview => Text.Length > 40 ? Text.Substring(0, 40) + "…" : Text;
}

/// <summary>
/// 剪贴板历史：轮询系统剪贴板文本（间隔 900ms，仅启用时运行），
/// 去重并保留最近 N 条，持久化到本机 JSON。内容绝不上传。
/// </summary>
public sealed class ClipboardHistoryService : IDisposable
{
    private const string DefaultFile = "clipboard-history.json";
    private readonly string _file;
    private readonly DispatcherTimer _timer;
    private readonly List<ClipboardEntry> _entries = new();
    private string _last = string.Empty;
    private bool _enabled;

    public ClipboardHistoryService()
    {
        _file = Path.Combine(AppPaths.AppDataDir, DefaultFile);
        Load();
        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(900) };
        _timer.Tick += (_, _) => Poll();
    }

    /// <summary>最新在前。</summary>
    public IReadOnlyList<ClipboardEntry> Entries
    {
        get { lock (_entries) return _entries.ToList(); }
    }

    /// <summary>组件显示用的简短摘要（如：剪贴板 · 3 条）。</summary>
    public string Summary
    {
        get { lock (_entries) return _entries.Count == 0 ? string.Empty : _entries.Count.ToString(); }
    }

    public event Action? Changed;

    /// <summary>保留条数上限（由设置同步，默认 15）。</summary>
    public int MaxEntries { get; set; } = 15;

    public void SetEnabled(bool enabled)
    {
        _enabled = enabled;
        if (enabled && !_timer.IsEnabled) _timer.Start();
        else if (!enabled && _timer.IsEnabled) _timer.Stop();
        if (enabled) Poll();
    }

    public void CopyToClipboard(string text)
    {
        try
        {
            System.Windows.Clipboard.SetText(text ?? string.Empty);
            _last = text ?? string.Empty;
        }
        catch (Exception ex) { AppLogger.Warn($"Clipboard set failed: {ex.Message}"); }
    }

    public void Clear()
    {
        lock (_entries) { _entries.Clear(); SaveCore(); }
        Changed?.Invoke();
    }

    private void Poll()
    {
        if (!_enabled) return;
        try
        {
            if (!System.Windows.Clipboard.ContainsText()) return;
            var text = System.Windows.Clipboard.GetText();
            if (string.IsNullOrWhiteSpace(text) || text.Length > 20000) return;
            if (text == _last) return;
            _last = text;
            lock (_entries)
            {
                _entries.RemoveAll(e => e.Text == text);
                _entries.Insert(0, new ClipboardEntry(DateTime.Now, text));
                var max = Math.Clamp(MaxEntries, 3, 200);
                while (_entries.Count > max) _entries.RemoveAt(_entries.Count - 1);
                SaveCore();
            }
            Changed?.Invoke();
        }
        catch (Exception ex)
        {
            AppLogger.Debug($"Clipboard poll: {ex.Message}");
        }
    }

    private void Load()
    {
        try
        {
            if (!File.Exists(_file)) return;
            var items = JsonSerializer.Deserialize<List<ClipboardEntry>>(File.ReadAllText(_file));
            if (items is null) return;
            lock (_entries) { _entries.Clear(); _entries.AddRange(items.Where(e => !string.IsNullOrWhiteSpace(e.Text))); }
        }
        catch (Exception ex) { AppLogger.Warn($"Clipboard history load: {ex.Message}"); }
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
        catch (Exception ex) { AppLogger.Warn($"Clipboard history save: {ex.Message}"); }
    }

    public void Dispose() => _timer.Stop();
}
