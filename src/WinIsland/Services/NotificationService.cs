using System;
using System.Collections.Generic;
using System.Windows.Threading;
using WinIsland.UI;

namespace WinIsland.Services;

/// <summary>
/// 在屏幕右上角弹出玻璃通知横幅（蓝牙提示、接管到的系统通知等）。
/// 最多同时显示 3 条，自动消失；支持同来源折叠（11 通知折叠）。
/// </summary>
public sealed class NotificationService
{
    private readonly Dispatcher _dispatcher;
    private readonly SettingsService _settings;
    private readonly NotificationHistoryService? _history;
    private readonly List<NotificationBannerWindow> _active = new();
    private const int MaxVisible = 3;

    public NotificationService(Dispatcher dispatcher, SettingsService settings, NotificationHistoryService? history = null)
    {
        _dispatcher = dispatcher;
        _settings = settings;
        _history = history;
    }

    /// <summary>弹出通知横幅。source = 来源应用（exe 名 / 显示名），用于勿扰白名单与通知中心打开来源。</summary>
    /// <summary>弹出通知横幅。source = 来源应用（exe 名 / 显示名），用于勿扰白名单与通知中心打开来源。
    /// actions = 可选操作按钮（#9：如蓝牙「断开」「设置」），点击后执行回调并收起横幅。</summary>
    public void Show(string title, string body, string glyph = "\uE7F4", string? source = null,
        IReadOnlyList<(string Label, Action Callback)>? actions = null)
    {
        if (_dispatcher.CheckAccess())
        {
            ShowCore(title, body, glyph, source, actions);
        }
        else
        {
            _dispatcher.BeginInvoke(() => ShowCore(title, body, glyph, source, actions));
        }
    }

    private void ShowCore(string title, string body, string glyph, string? source,
        IReadOnlyList<(string Label, Action Callback)>? actions = null)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(title)) return;

            // 无论是否勿扰都先写入历史，避免勿扰期间丢失提醒记录
            _history?.Add(title, body, glyph, source);

            // 勿扰模式：白名单来源不受影响；其它来源不弹横幅，仅进历史（留在未读）
            if (DoNotDisturb.IsActive(_settings.Current, source))
            {
                AppLogger.Info($"Banner suppressed by DnD: '{title}'");
                return;
            }

            var screen = System.Windows.Forms.Screen.PrimaryScreen
                ?? System.Windows.Forms.Screen.AllScreens[0];
            var timeout = Math.Max(1, _settings.Current.NotificationTimeoutSeconds);

            // 11 通知折叠：同来源同标题的活动横幅只更新计数，不新增窗口
            if (_settings.Current.NotifyFoldEnabled)
            {
                var foldKey = (source ?? string.Empty) + "\u0001" + title;
                foreach (var w in _active)
                {
                    if (w.FoldKey == foldKey)
                    {
                        w.Refresh(title, body, glyph, timeout);
                        _history?.MarkReadMatching(title, body);
                        return;
                    }
                }
            }

            var win = new NotificationBannerWindow(title, body, glyph, timeout, screen, _active.Count,
                (source ?? string.Empty) + "\u0001" + title, actions: actions);
            win.Closed += (_, _) => _active.Remove(win);
            _active.Add(win);
            win.Show();
            // 横幅已展示，这条通知视为已读
            _history?.MarkReadMatching(title, body);
            AppLogger.Info(string.Format("Banner shown: '{0}' from {1}", title, source ?? "-"));

            // 超过上限时收起最早的一条
            while (_active.Count > MaxVisible)
            {
                var oldest = _active[0];
                if (oldest is not null && oldest.IsVisible) oldest.Close();
                else _active.RemoveAt(0);
            }
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Notification banner failed: {ex.Message}");
        }
    }

    /// <summary>
    /// 复制进度横幅（27 复制进度）：Windows 不暴露真实剪贴板读取进度，
    /// 按文本长度估算耗时并以 ~30fps 平滑推进；完成后切换为「已复制」并自动关闭。
    /// </summary>
    public void ShowCopyProgress(string title, string body, int estimatedMs,
        string doneTitle, string doneBody, string glyph = "", string? source = null)
    {
        if (_dispatcher.CheckAccess())
            ShowProgressCore(title, body, estimatedMs, doneTitle, doneBody, glyph, source);
        else
            _dispatcher.BeginInvoke(() => ShowProgressCore(title, body, estimatedMs, doneTitle, doneBody, glyph, source));
    }

    private void ShowProgressCore(string title, string body, int estimatedMs,
        string doneTitle, string doneBody, string glyph, string? source)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(title)) return;

            // 勿扰模式：与其它通知一致，来源可进白名单
            if (DoNotDisturb.IsActive(_settings.Current, source))
            {
                AppLogger.Info($"Copy progress suppressed by DnD: '{title}'");
                return;
            }

            var screen = System.Windows.Forms.Screen.PrimaryScreen
                ?? System.Windows.Forms.Screen.AllScreens[0];
            var timeout = Math.Max(1, _settings.Current.NotificationTimeoutSeconds);

            var win = new NotificationBannerWindow(title, body, glyph, timeout, screen, _active.Count,
                "CopyProgress" + title, progressMode: true);
            win.Closed += (_, _) => _active.Remove(win);
            _active.Add(win);
            win.Show();

            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(33) };
            var sw = System.Diagnostics.Stopwatch.StartNew();
            timer.Tick += (_, _) =>
            {
                if (!win.IsVisible)
                {
                    timer.Stop();
                    return;
                }
                var p = sw.ElapsedMilliseconds / (double)Math.Max(1, estimatedMs);
                if (p >= 1)
                {
                    timer.Stop();
                    win.Complete(doneTitle, doneBody, glyph, timeout);
                    _history?.Add(doneTitle, doneBody, glyph, source);
                    _history?.MarkReadMatching(doneTitle, doneBody);
                }
                else
                {
                    win.SetProgress(p);
                }
            };
            win.Closed += (_, _) => timer.Stop();
            timer.Start();
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Copy progress banner failed: {ex.Message}");
        }
    }
}
