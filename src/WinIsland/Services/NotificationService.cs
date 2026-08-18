using System;
using System.Collections.Generic;
using System.Windows.Threading;
using WinIsland.UI;

namespace WinIsland.Services;

/// <summary>
/// 在屏幕右上角弹出玻璃通知横幅（蓝牙提示、接管到的系统通知等）。
/// 最多同时显示 3 条，自动消失。
/// </summary>
public sealed class NotificationService
{
    private readonly Dispatcher _dispatcher;
    private readonly SettingsService _settings;
    private readonly List<NotificationBannerWindow> _active = new();
    private const int MaxVisible = 3;

    public NotificationService(Dispatcher dispatcher, SettingsService settings)
    {
        _dispatcher = dispatcher;
        _settings = settings;
    }

    public void Show(string title, string body, string glyph = "\uE7F4")
    {
        if (_dispatcher.CheckAccess())
        {
            ShowCore(title, body, glyph);
        }
        else
        {
            _dispatcher.BeginInvoke(() => ShowCore(title, body, glyph));
        }
    }

    private void ShowCore(string title, string body, string glyph)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(title)) return;

            var screen = System.Windows.Forms.Screen.PrimaryScreen
                ?? System.Windows.Forms.Screen.AllScreens[0];
            var timeout = Math.Max(1, _settings.Current.NotificationTimeoutSeconds);

            var win = new NotificationBannerWindow(title, body, glyph, timeout, screen, _active.Count);
            win.Closed += (_, _) => _active.Remove(win);
            _active.Add(win);
            win.Show();
            AppLogger.Info(string.Format("Banner shown: '{0}'", title));

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
}
