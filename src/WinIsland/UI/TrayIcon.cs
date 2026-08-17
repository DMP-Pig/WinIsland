using System.Windows;
using WinIsland.Services;

namespace WinIsland.UI;

/// <summary>
/// System tray icon with a context menu (show/hide, lyrics window, auto-start, settings, exit).
/// Uses WinForms NotifyIcon (part of the WindowsDesktop runtime, no extra dependency).
/// </summary>
public sealed class TrayIcon : IDisposable
{
    private readonly System.Windows.Forms.NotifyIcon _icon;
    private readonly System.Windows.Forms.ToolStripMenuItem _showHideItem;
    private readonly System.Windows.Forms.ToolStripMenuItem _lyricsItem;
    private readonly System.Windows.Forms.ToolStripMenuItem _autoStartItem;
    private readonly SettingsService _settings;

    public TrayIcon(SettingsService settings)
    {
        _settings = settings;
        _icon = new System.Windows.Forms.NotifyIcon
        {
            Text = "WinIsland",
            Icon = LoadIcon(),
            Visible = true,
        };
        _icon.DoubleClick += (_, _) => ShowHideRequested?.Invoke(this, EventArgs.Empty);

        var menu = new System.Windows.Forms.ContextMenuStrip();
        _showHideItem = new System.Windows.Forms.ToolStripMenuItem(Localization.Get("ShowHide"));
        _showHideItem.Click += (_, _) => ShowHideRequested?.Invoke(this, EventArgs.Empty);

        _lyricsItem = new System.Windows.Forms.ToolStripMenuItem(Localization.Get("LyricsWindow"));
        _lyricsItem.Click += (_, _) => ToggleLyricsRequested?.Invoke(this, EventArgs.Empty);

        _autoStartItem = new System.Windows.Forms.ToolStripMenuItem(Localization.Get("AutoStart"));
        _autoStartItem.Checked = AutoStart.IsEnabled();
        _autoStartItem.Click += (_, _) => AutoStartRequested?.Invoke(this, EventArgs.Empty);

        var settingsItem = new System.Windows.Forms.ToolStripMenuItem(Localization.Get("Settings"));
        settingsItem.Click += (_, _) => SettingsRequested?.Invoke(this, EventArgs.Empty);

        var exitItem = new System.Windows.Forms.ToolStripMenuItem(Localization.Get("Exit"));
        exitItem.Click += (_, _) => ExitRequested?.Invoke(this, EventArgs.Empty);

        menu.Items.Add(_showHideItem);
        menu.Items.Add(_lyricsItem);
        menu.Items.Add(_autoStartItem);
        menu.Items.Add(new System.Windows.Forms.ToolStripSeparator());
        menu.Items.Add(settingsItem);
        menu.Items.Add(new System.Windows.Forms.ToolStripSeparator());
        menu.Items.Add(exitItem);

        _icon.ContextMenuStrip = menu;
        _menu = menu;

        Localization.LanguageChanged += (_, _) => RefreshText();
        RefreshText();
    }

    private System.Windows.Forms.ContextMenuStrip _menu;

    public event EventHandler? ShowHideRequested;
    public event EventHandler? SettingsRequested;
    public event EventHandler? ToggleLyricsRequested;
    public event EventHandler? AutoStartRequested;
    public event EventHandler? ExitRequested;

    public void SetLyricsChecked(bool on) => _lyricsItem.Checked = on;
    public void SetAutoStartChecked(bool on) => _autoStartItem.Checked = on;

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
        _showHideItem.Text = Localization.Get("ShowHide");
        _lyricsItem.Text = Localization.Get("LyricsWindow");
        _autoStartItem.Text = Localization.Get("AutoStart");
        _menu.Items[4].Text = Localization.Get("Settings");
        _menu.Items[6].Text = Localization.Get("Exit");
    }

    public void Dispose()
    {
        _icon.Visible = false;
        _icon.Dispose();
        _menu.Dispose();
    }
}
