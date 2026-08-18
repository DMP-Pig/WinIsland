using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
using WinIsland.Services;

namespace WinIsland.UI;

/// <summary>
/// System tray icon with a WPF glass context menu (same style as the island menu).
/// Uses a hidden off-screen host window to place the menu at the cursor.
/// </summary>
public sealed class TrayIcon : IDisposable
{
    private readonly System.Windows.Forms.NotifyIcon _icon;
    private readonly MenuItem _showHideItem;
    private readonly MenuItem _lyricsItem;
    private readonly MenuItem _autoStartItem;
    private readonly SettingsService _settings;
    private readonly ContextMenu _menu;
    private readonly Window _menuHost;

    public TrayIcon(SettingsService settings)
    {
        _settings = settings;

        // 隐藏宿主窗口：用于承载 WPF ContextMenu 并在鼠标处弹出
        _menuHost = new Window
        {
            Width = 1, Height = 1,
            WindowStyle = WindowStyle.None,
            AllowsTransparency = true,
            ShowInTaskbar = false,
            Opacity = 0,
            Left = -20000, Top = -20000,
        };
        _menuHost.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/UI/MenuGlass.xaml"),
        });

        _menu = new ContextMenu
        {
            PlacementTarget = _menuHost,
            Placement = PlacementMode.MousePoint,
        };

        ApplyMenuTheme(_menuHost);
        _menuHost.Show(); // 创建句柄并保持（屏外透明）

        _showHideItem = new MenuItem { Header = Localization.Get("ShowHide") };
        _showHideItem.Click += (_, _) => ShowHideRequested?.Invoke(this, EventArgs.Empty);

        _lyricsItem = new MenuItem { Header = Localization.Get("LyricsWindow") };
        _lyricsItem.Click += (_, _) => ToggleLyricsRequested?.Invoke(this, EventArgs.Empty);

        _autoStartItem = new MenuItem
        {
            Header = Localization.Get("AutoStart"),
            IsCheckable = true,
            IsChecked = AutoStart.IsEnabled(),
        };
        _autoStartItem.Click += (_, _) => AutoStartRequested?.Invoke(this, EventArgs.Empty);

        var settingsItem = new MenuItem { Header = Localization.Get("Settings") };
        settingsItem.Click += (_, _) => SettingsRequested?.Invoke(this, EventArgs.Empty);

        var exitItem = new MenuItem { Header = Localization.Get("Exit") };
        exitItem.Click += (_, _) => ExitRequested?.Invoke(this, EventArgs.Empty);

        _menu.Items.Add(_showHideItem);
        _menu.Items.Add(_lyricsItem);
        _menu.Items.Add(_autoStartItem);
        _menu.Items.Add(new Separator());
        _menu.Items.Add(settingsItem);
        _menu.Items.Add(new Separator());
        _menu.Items.Add(exitItem);

        _icon = new System.Windows.Forms.NotifyIcon
        {
            Text = "WinIsland",
            Icon = LoadIcon(),
            Visible = true,
        };
        _icon.DoubleClick += (_, _) => ShowHideRequested?.Invoke(this, EventArgs.Empty);
        _icon.MouseUp += (s, e) =>
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                _menu.Placement = PlacementMode.MousePoint;
                _menu.IsOpen = true;
            }
        };

        Localization.LanguageChanged += (_, _) => RefreshText();
        RefreshText();
    }

    public event EventHandler? ShowHideRequested;
    public event EventHandler? SettingsRequested;
    public event EventHandler? ToggleLyricsRequested;
    public event EventHandler? AutoStartRequested;
    public event EventHandler? ExitRequested;

    public void SetLyricsChecked(bool on) => _lyricsItem.IsChecked = on;
    public void SetAutoStartChecked(bool on) => _autoStartItem.IsChecked = on;

    /// <summary>Show a transient balloon notification (used sparingly).</summary>
    public void Notify(string title, string text)
    {
        try
        {
            _icon.BalloonTipTitle = title;
            _icon.BalloonTipText = text;
            _icon.ShowBalloonTip(2000);
        }
        catch { /* ignore */ }
    }

    private static System.Drawing.Icon LoadIcon()
    {
        try
        {
            var stream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/Assets/winisland.ico"))?.Stream;
            if (stream is not null) return new System.Drawing.Icon(stream);
        }
        catch { /* fall through */ }

        return System.Drawing.SystemIcons.Application;
    }

    private void RefreshText()
    {
        _showHideItem.Header = Localization.Get("ShowHide");
        _lyricsItem.Header = Localization.Get("LyricsWindow");
        _autoStartItem.Header = Localization.Get("AutoStart");
        (_menu.Items[4] as MenuItem)!.Header = Localization.Get("Settings");
        (_menu.Items[6] as MenuItem)!.Header = Localization.Get("Exit");
    }

    /// <summary>按当前主题给托盘菜单设置液态玻璃配色（与灵动岛菜单一致）。</summary>
    private void ApplyMenuTheme(Window host)
    {
        var dark = _settings.Current.Theme switch
        {
            ThemeMode.Light => false,
            ThemeMode.Dark => true,
            _ => ThemeHelper.IsSystemDark(),
        };
        SolidColorBrush bg, border, text, hover;
        if (dark)
        {
            bg = new SolidColorBrush(Color.FromArgb(0xEE, 0x1B, 0x1B, 0x26));
            border = new SolidColorBrush(Color.FromArgb(0x59, 0xFF, 0xFF, 0xFF));
            text = new SolidColorBrush(Color.FromRgb(0xF2, 0xF2, 0xF7));
            hover = new SolidColorBrush(Color.FromArgb(0x30, 0xFF, 0xFF, 0xFF));
        }
        else
        {
            bg = new SolidColorBrush(Color.FromArgb(0xEE, 0xF5, 0xF5, 0xFA));
            border = new SolidColorBrush(Color.FromArgb(0x66, 0xFF, 0xFF, 0xFF));
            text = new SolidColorBrush(Color.FromRgb(0x1D, 0x1D, 0x24));
            hover = new SolidColorBrush(Color.FromArgb(0x18, 0x00, 0x00, 0x00));
        }
        bg.Freeze(); border.Freeze(); text.Freeze(); hover.Freeze();
        host.Resources["MenuBgBrush"] = bg;
        host.Resources["MenuBorderBrush"] = border;
        host.Resources["MenuTextBrush"] = text;
        host.Resources["MenuHoverBrush"] = hover;

        _menu.Background = bg;
        _menu.Foreground = text;
        _menu.BorderBrush = border;
    }

    public void Dispose()
    {
        _icon.Visible = false;
        _icon.Dispose();
        try { _menuHost.Close(); } catch { /* ignore */ }
    }
}
