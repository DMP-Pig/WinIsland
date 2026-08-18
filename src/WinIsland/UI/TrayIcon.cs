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
        ApplyModernMenu();

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

    // ── 现代化托盘右键菜单（圆角 + 明暗主题）──
    private void ApplyModernMenu()
    {
        var dark = _settings.Current.Theme switch
        {
            ThemeMode.Light => false,
            ThemeMode.Dark => true,
            _ => ThemeHelper.IsSystemDark(),
        };
        var bg = dark ? System.Drawing.Color.FromArgb(0xFF, 0x1B, 0x1B, 0x26) : System.Drawing.Color.FromArgb(0xFF, 0xF5, 0xF5, 0xFA);
        var fg = dark ? System.Drawing.Color.FromArgb(0xFF, 0xF2, 0xF2, 0xF7) : System.Drawing.Color.FromArgb(0xFF, 0x1D, 0x1D, 0x24);
        _menu.BackColor = bg;
        _menu.ForeColor = fg;
        _menu.Renderer = new GlassMenuRenderer(dark);
        foreach (System.Windows.Forms.ToolStripItem it in _menu.Items)
        {
            it.ForeColor = fg;
            if (it is System.Windows.Forms.ToolStripMenuItem mi && mi.Checked)
            {
                // 选中项用强调色文字（如开机自启）
                mi.ForeColor = dark ? System.Drawing.Color.FromArgb(0xFF, 0xA8, 0x9C, 0xFF) : System.Drawing.Color.FromArgb(0xFF, 0x6C, 0x5C, 0xE7);
            }
        }
        // 弹窗圆角（每次打开重新设置，窗口句柄每次重建）
        _menu.Opened += (_, _) =>
        {
            try
            {
                var h = _menu.Handle;
                if (h != IntPtr.Zero && _menu.Width > 0)
                {
                    var hrgn = CreateRoundRectRgn(0, 0, _menu.Width + 1, _menu.Height + 1, 12, 12);
                    if (hrgn != IntPtr.Zero)
                        _menu.Region = System.Drawing.Region.FromHrgn(hrgn);
                }
            }
            catch { /* ignore */ }
        };
    }

    [System.Runtime.InteropServices.DllImport("gdi32.dll")]
    private static extern IntPtr CreateRoundRectRgn(int left, int top, int right, int bottom, int w, int h);

    private sealed class GlassColorTable : System.Windows.Forms.ProfessionalColorTable
    {
        private readonly bool _dark;
        public GlassColorTable(bool dark) => _dark = dark;
        public override System.Drawing.Color MenuItemSelected
            => _dark ? System.Drawing.Color.FromArgb(60, 255, 255, 255) : System.Drawing.Color.FromArgb(24, 0, 0, 0);
        public override System.Drawing.Color MenuItemBorder => System.Drawing.Color.Transparent;
        public override System.Drawing.Color CheckBackground
            => _dark ? System.Drawing.Color.FromArgb(0xFF, 0x6C, 0x5C, 0xE7) : System.Drawing.Color.FromArgb(0xFF, 0x6C, 0x5C, 0xE7);
        public override System.Drawing.Color CheckSelectedBackground
            => _dark ? System.Drawing.Color.FromArgb(0xFF, 0x6C, 0x5C, 0xE7) : System.Drawing.Color.FromArgb(0xFF, 0x6C, 0x5C, 0xE7);
        public override System.Drawing.Color SeparatorDark
            => _dark ? System.Drawing.Color.FromArgb(80, 255, 255, 255) : System.Drawing.Color.FromArgb(40, 0, 0, 0);
        public override System.Drawing.Color SeparatorLight => System.Drawing.Color.Transparent;
        public override System.Drawing.Color ToolStripDropDownBackground => System.Drawing.Color.Transparent;
    }

    private sealed class GlassMenuRenderer : System.Windows.Forms.ToolStripProfessionalRenderer
    {
        private readonly bool _dark;
        public GlassMenuRenderer(bool dark) : base(new GlassColorTable(dark))
        {
            _dark = dark;
            RoundedEdges = true;
        }

        protected override void OnRenderToolStripBackground(System.Windows.Forms.ToolStripRenderEventArgs e)
        {
            var c = _dark ? System.Drawing.Color.FromArgb(0xFF, 0x1B, 0x1B, 0x26) : System.Drawing.Color.FromArgb(0xFF, 0xF5, 0xF5, 0xFA);
            using var b = new System.Drawing.SolidBrush(c);
            e.Graphics.FillRectangle(b, e.AffectedBounds);
        }

        protected override void OnRenderMenuItemBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected || e.Item.Pressed)
            {
                var rc = new System.Drawing.Rectangle(3, 1, e.Item.Width - 6, e.Item.Height - 2);
                using var b = new System.Drawing.SolidBrush(_dark
                    ? System.Drawing.Color.FromArgb(60, 255, 255, 255)
                    : System.Drawing.Color.FromArgb(24, 0, 0, 0));
                using var path = Rounded(rc, 7);
                e.Graphics.FillPath(b, path);
            }
            else if (e.Item is System.Windows.Forms.ToolStripMenuItem { Checked: true })
            {
                var rc = new System.Drawing.Rectangle(3, 1, e.Item.Width - 6, e.Item.Height - 2);
                using var b = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(36, 0x6C, 0x5C, 0xE7));
                using var path = Rounded(rc, 7);
                e.Graphics.FillPath(b, path);
            }
        }

        protected override void OnRenderSeparator(System.Windows.Forms.ToolStripSeparatorRenderEventArgs e)
        {
            var y = e.Item.Height / 2;
            using var pen = new System.Drawing.Pen(_dark
                ? System.Drawing.Color.FromArgb(80, 255, 255, 255)
                : System.Drawing.Color.FromArgb(40, 0, 0, 0));
            e.Graphics.DrawLine(pen, 16, y, e.Item.Width - 16, y);
        }

        private static System.Drawing.Drawing2D.GraphicsPath Rounded(System.Drawing.Rectangle r, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(r.X, r.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(r.Right - radius * 2, r.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(r.Right - radius * 2, r.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(r.X, r.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
    public void Dispose()
    {
        _icon.Visible = false;
        _icon.Dispose();
        _menu.Dispose();
    }
}
