using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Brush = System.Windows.Media.Brush;
using Point = System.Windows.Point;
using WinIsland.Services;

namespace WinIsland.UI;

/// <summary>
/// iOS 风格灵动岛窗口。
/// 窗口尺寸固定（400x400、透明、点击穿透），仅内部卡片（Card）形变：
/// 紧凑 = 340x56 胶囊，展开 = 400x~384 卡片向下生长。
/// 动画只作用于单个元素，由 WPF 合成线程 60fps 驱动，避免窗口级 Resize 卡顿。
/// 点击穿透通过 WM_NCHITTEST 显式处理：卡片内可交互，卡片外穿透。
/// </summary>
public partial class IslandWindow : Window, INotifyPropertyChanged
{
    // 尺寸来自设置（可调），带安全钳制
    private double ManualCompactW => Math.Clamp(_settings.Current.CompactWidth, 240, 520);
    private double ManualCompactH => Math.Clamp(_settings.Current.CompactHeight, 48, 140);

    /// <summary>
    /// 实测紧凑内容宽度（岛可见时精确贴合组件）。
    /// 岛隐藏/未布局时返回「估算与手动值取较大者」，避免启动瞬间 Card 过窄导致组件挤压、显示不完整。
    /// </summary>
    private double MeasureCompactWidthNow()
    {
        var fallback = Math.Max(_vm.EstimatedCompactWidth, ManualCompactW);
        try
        {
            if (!IsLoaded || !_vm.IsVisible) return fallback;
            PillRow.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
            var w = PillRow.DesiredSize.Width;
            return w >= 20 ? Math.Clamp(w + 56, 240, 580) : fallback; // 总留白 56（左侧 22 + 右侧 24，右侧略多）
        }
        catch
        {
            return fallback;
        }
    }

    private double _noPushCompactW;
    private bool _noPushWValid;

    /// <summary>推送卡片在紧凑态的单行估算宽度：标题 + 图标30 + 间距8 + 内边距24 + 余量6，保证文字完整。</summary>
    private double PushCardCompactWidth()
    {
        var title = _vm.ActivePush?.Title ?? string.Empty;
        double tw = 0;
        foreach (var ch in title) tw += ch > 0x2E7F ? 13 : 7;
        return Math.Min(tw, 150) + 68;
    }

    /// <summary>
    /// 紧凑宽度：推送出现时 = 「无推送宽度 + 推送卡片宽度」确定性加长，
    /// 确保推送卡片与所有组件完整显示（不依赖 UI 布局时序）；手动模式恒定。
    /// </summary>
    private double CompactWidth
    {
        get
        {
            if (!_settings.Current.CompactWidthAuto) return ManualCompactW;
            var autoW = MeasureCompactWidthNow();
            if (_vm.HasActivePush)
            {
                var baseW = _noPushWValid ? _noPushCompactW : Math.Max(autoW, ManualCompactW);
                return Math.Clamp(baseW + PushCardCompactWidth(), 240, 620);
            }
            _noPushCompactW = autoW;
            _noPushWValid = true;
            return autoW;
        }
    }
    private double _noPushCompactH;   // 无上岛推送时的紧凑高度（缓存）
    private bool _noPushHValid;

    /// <summary>紧凑高度：上岛推送不改变高度（与没上岛前一致），推送卡片在紧凑高度内自适应显示。</summary>
    private double CompactHeight
    {
        get
        {
            if (!_settings.Current.CompactHeightAuto) return ManualCompactH; // 手动模式：高度恒定
            if (_vm.HasActivePush)
                return _noPushHValid ? Math.Clamp(_noPushCompactH, 44, 160) : Math.Clamp(_vm.EstimatedCompactHeight, 44, 160);
            _noPushCompactH = _vm.EstimatedCompactHeight;
            _noPushHValid = true;
            return _vm.EstimatedCompactHeight;
        }
    }
    private double ExpandedWidth => _settings.Current.ExpandedWidthAuto
        ? _vm.EstimatedExpandedWidth
        : Math.Clamp(_settings.Current.ExpandedWidth, CompactWidth, 620);
    private double MaxExpandedHeight => _settings.Current.MaxExpandedHeightAuto
        ? _vm.EstimatedExpandedHeight
        : Math.Clamp(_settings.Current.MaxExpandedHeight, 240, 620);

    private const int WM_NCHITTEST = 0x0084;
    private const int HTCLIENT = 1;
    private const int HTTRANSPARENT = -1;

    private readonly IslandViewModel _vm;
    private readonly ThemeService _theme;
    private readonly SettingsService _settings;
    private readonly System.Windows.Forms.Screen _screen;
    private readonly DispatcherTimer _collapseTimer;
    private readonly DispatcherTimer _compactRestoreTimer;
    private Storyboard? _currentStoryboard;
    private HwndSource? _hwndSource;

    public System.Windows.Forms.Screen Screen { get; }

    public IslandWindow(IslandViewModel vm, ThemeService theme, SettingsService settings, System.Windows.Forms.Screen screen)
    {
        _vm = vm;
        _theme = theme;
        _settings = settings;
        _screen = screen;
        Screen = screen;

        DataContext = vm;
        InitializeComponent();

        // 收起延迟（鼠标移出展开态 700ms 后收起）
        _collapseTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(700) };
        _collapseTimer.Tick += (_, _) =>
        {
            _collapseTimer.Stop();
            _vm.IsExpanded = false;
        };

        // 收起动画可能被快速切换打断导致 Card 尺寸残留：动画结束后兜底恢复精确紧凑尺寸
        _compactRestoreTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(900) };
        _compactRestoreTimer.Tick += (_, _) =>
        {
            _compactRestoreTimer.Stop();
            if (IsLoaded && !_vm.IsExpanded)
            {
                Card.BeginAnimation(FrameworkElement.WidthProperty, null);
                Card.BeginAnimation(FrameworkElement.HeightProperty, null);
                Card.Width = CompactWidth;
                Card.Height = CompactHeight;
                ContentGrid.RowDefinitions[1].Height = GridLength.Auto;
            }
        };

        _lyricsScrollTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(16) };
        _lyricsScrollTimer.Tick += (_, _) => SmoothScrollStep();

        // 悬停不展开；移出时若已展开则延迟收起
        Card.MouseLeave += (_, _) =>
        {
            if (_vm.IsExpanded) _collapseTimer.Start();
        };

        // 点击展开/收起；解锁状态下支持鼠标拖动
        Card.PreviewMouseLeftButtonDown += OnCardMouseLeftButtonDown;
        Card.PreviewMouseMove += OnCardMouseMove;
        Card.PreviewMouseLeftButtonUp += OnCardMouseLeftButtonUp;

        // 进度条拖拽 seek
        ProgressSlider.AddHandler(Thumb.DragStartedEvent, new DragStartedEventHandler((_, _) => _vm.BeginSeek()));
        ProgressSlider.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(async (_, _) => await _vm.EndSeekAsync(ProgressSlider.Value)));

        _vm.PropertyChanged += OnVmPropertyChanged;
        _theme.ThemeChanged += (_, _) => ApplyTheme();
        _settings.Changed += (_, _) => ApplyExpandedSectionVisibility();

        Loaded += OnLoaded;
        DpiChanged += (_, _) => Reposition();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    // Theme passthrough for XAML bindings.
    public Brush TextPrimary => _theme.TextPrimary;
    public Brush TextSecondary => _theme.TextSecondary;
    public Brush AccentBrush => _theme.AccentBrush;
    public Brush AccentBorderBrush => _theme.AccentBorderBrush;
    public Brush CardBackground => _theme.CardBackground;
    public Brush CardBorder => _theme.CardBorder;
    public Brush ButtonHoverBrush => _theme.ButtonHoverBrush;
    public Brush SliderTrackBrush => _theme.SliderTrackBrush;
    public Brush SliderThumbBrush => _theme.SliderThumbBrush;

    // ── 展开卡片分区块开关（来自设置，绑定到展开内容）──
    // 歌曲相关区域仅在“有媒体播放”时显示；只有上岛推送时展开态以上岛内容为主，避免空歌曲区
    public bool ExpandedShowArtTitle => _vm.HasMedia && _settings.Current.ExpandedShowArtTitle;
    public bool ExpandedShowProgress => _vm.HasMedia && _settings.Current.ExpandedShowProgress;
    public bool ExpandedShowControls => _vm.HasMedia && _settings.Current.ExpandedShowControls;
    public bool ExpandedShowLyrics => _vm.HasMedia && _settings.Current.ExpandedShowLyrics;

    // ── 单行模式：紧凑态所有组件一行显示 ──
    public bool SingleLineMode => _settings.Current.SingleLineMode;
    // 上岛推送内容：单行模式下只显示图标+标题（隐藏正文/进度/按钮）
    public bool PushShowBody => _vm.ActivePushHasBody && !SingleLineMode;
    public bool PushShowProgress => _vm.ActivePushHasProgress && !SingleLineMode;
    public bool PushShowButtons => _vm.ActivePushHasButtons && !SingleLineMode;

    // ── 点击展开 / 解锁拖动 / 右键菜单 ─────────────────────────

    private Point _downPoint;
    private bool _mouseDownOnCard;
    private bool _draggedCard;

    private void OnCardMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _mouseDownOnCard = true;
        _draggedCard = false;
        _downPoint = e.GetPosition(this);
    }

    private void OnCardMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
    {
        if (!_mouseDownOnCard || e.LeftButton != MouseButtonState.Pressed) return;
        if (_settings.Current.IsLocked) return; // 上锁不可拖动

        var pos = e.GetPosition(this);
        if (Math.Abs(pos.X - _downPoint.X) > 4 || Math.Abs(pos.Y - _downPoint.Y) > 4)
        {
            _mouseDownOnCard = false;
            _draggedCard = true;
            _collapseTimer.Stop();
            try { DragMove(); } catch { /* ignore */ }
            e.Handled = true;
        }
    }

    private void OnCardMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (_draggedCard) { _draggedCard = false; return; }
        if (!_mouseDownOnCard) return;
        _mouseDownOnCard = false;

        // 点击按钮/滑块不触发展开切换
        if (IsInteractiveElement(e.OriginalSource)) return;

        _collapseTimer.Stop();
        _vm.IsExpanded = !_vm.IsExpanded;
        e.Handled = true;
    }

    private static bool IsInteractiveElement(object source)
    {
        var d = source as DependencyObject;
        while (d is not null)
        {
            if (d is System.Windows.Controls.Primitives.ButtonBase or Slider
                or System.Windows.Controls.Primitives.Thumb or System.Windows.Controls.Primitives.RepeatButton)
                return true;
            // Run/Inline 等 ContentElement 不是 Visual，VisualTreeHelper.GetParent 会抛异常，
            // 需沿逻辑树向上（歌词 Run → TextBlock），到达 UIElement 后继续沿视觉树。
            d = d is System.Windows.Media.Visual or System.Windows.Media.Media3D.Visual3D
                ? VisualTreeHelper.GetParent(d)
                : LogicalTreeHelper.GetParent(d);
        }

        return false;
    }

    private void Card_ContextMenuOpening(object sender, ContextMenuEventArgs e)
    {
        MenuLock.Header = _settings.Current.IsLocked
            ? Localization.Get("Island_Unlock")
            : Localization.Get("Island_Lock");
        MenuOnlineLyrics.Header = Localization.Get("Island_OnlineLyrics");
        MenuOnlineLyrics.IsChecked = _settings.Current.OnlineLyricsEnabled;
    }

    /// <summary>上岛推送按钮点击：执行动作（打开 URL / 启动程序）后关闭当前推送。</summary>
    private void PushButton_Click(object sender, RoutedEventArgs e)
    {
        if ((sender as FrameworkElement)?.DataContext is IslandPushButton button)
        {
            _vm.ExecutePushAction(button);
            _vm.DismissActivePush();
        }
    }

    private void MenuOnlineLyrics_Click(object sender, RoutedEventArgs e)
    {
        _settings.Update(s => s.OnlineLyricsEnabled = !s.OnlineLyricsEnabled);
        _ = _vm.RefreshLyricsAsync();
        Card_ContextMenuOpening(sender, null!);
    }

    private void MenuCenterAlign_Click(object sender, RoutedEventArgs e)
    {
        // 上下不变，左右居中
        var work = ScreenHelper.DpiWorkArea(_screen);
        var cardPos = Card.TransformToAncestor(this).Transform(new Point(0, 0));
        var cardCenterInWindow = cardPos.X + Card.ActualWidth / 2;
        Left = work.Left + work.Width / 2 - cardCenterInWindow;
    }

    private void MenuLock_Click(object sender, RoutedEventArgs e)
    {
        _settings.Update(s => s.IsLocked = !s.IsLocked);
        Card_ContextMenuOpening(sender, null!);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // 点击穿透：卡片外返回 HTTRANSPARENT
        var hwnd = new WindowInteropHelper(this).Handle;
        _hwndSource = HwndSource.FromHwnd(hwnd);
        _hwndSource?.AddHook(WndProc);

        ApplyTheme();
        ApplySize();
        ApplyCardAlignment();
        Reposition();
        if (_vm.IsVisible) ShowIsland(instant: true);
        else Hide();
    }

    private void ApplyTheme()
    {
        ApplyMenuTheme();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextPrimary)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextSecondary)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AccentBrush)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AccentBorderBrush)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CardBackground)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CardBorder)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonHoverBrush)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SliderTrackBrush)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SliderThumbBrush)));
    }

    /// <summary>展开卡片分区块的可见性随设置即时刷新。</summary>
    private void ApplyExpandedSectionVisibility()
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ExpandedShowArtTitle)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ExpandedShowProgress)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ExpandedShowControls)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ExpandedShowLyrics)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SingleLineMode)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PushShowBody)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PushShowProgress)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PushShowButtons)));
    }

    /// <summary>按设置调整窗口与卡片尺寸（紧凑/展开）。仅当窗口尺寸真正变化时才重定位，
    /// 避免上锁/其它设置变更把用户拖动后的位置弹回默认。</summary>
    public void ApplySize()
    {
        // 窗口固定为能容纳最大推送卡片/展开内容的大小：推送时只需动画 Card 形变，避免窗口级 Resize 卡顿
        var settingExpanded = Math.Clamp(_settings.Current.ExpandedWidth, 300, 620);
        var settingMaxH = Math.Clamp(_settings.Current.MaxExpandedHeight, 240, 620);
        var w = Math.Max(settingExpanded, 640) + 24;
        var h = Math.Max(settingMaxH, 220) + 24;
        var sizeChanged = Math.Abs(Width - w) > 0.5 || Math.Abs(Height - h) > 0.5;
        if (sizeChanged)
        {
            Width = w;
            Height = h;
            Dispatcher.BeginInvoke(Reposition, System.Windows.Threading.DispatcherPriority.Loaded);
        }
        if (!_vm.IsExpanded)
        {
            Card.Width = CompactWidth;
            Card.Height = CompactHeight;
        }
        // 自动调节尺寸时：胶囊行左侧额外留白（左侧横向距离更大），手动模式保持对称
        PillRow.Margin = _settings.Current.CompactWidthAuto
            ? new Thickness(8, 0, 0, 0)
            : new Thickness(0);
    }

    /// <summary>推送到达/更新/过期时：Card 尺寸用弹簧动画平滑过渡到新大小（丝滑不生硬）。</summary>
    private void AnimateCompactSize()
    {
        if (!IsLoaded) return;
        if (_vm.IsExpanded) { ApplySize(); return; }
        var spring = new SpringEase { Damping = 10, Stiffness = 200, Mass = 1 };
        var sb = new Storyboard();
        AddAnim(sb, Card, FrameworkElement.WidthProperty, CompactWidth, 460, spring);
        AddAnim(sb, Card, FrameworkElement.HeightProperty, CompactHeight, 460, spring);
        sb.Begin();
    }

    /// <summary>第三方应用上岛：推送卡片淡入 + 轻微缩放的丝滑动画。</summary>
    private void PlayPushCardAnimation()
    {
        if (!IsLoaded || CompactPushCard is null || !_vm.HasActivePush) return;
        CompactPushCard.Opacity = 0;
        CompactPushScale.ScaleX = CompactPushScale.ScaleY = 0.94;
        var sb = new Storyboard();
        var spring = new SpringEase { Damping = 10, Stiffness = 200, Mass = 1 };
        var smooth = new CubicEase { EasingMode = EasingMode.EaseOut };
        AddAnim(sb, CompactPushCard, UIElement.OpacityProperty, 1, 320, smooth);
        AddAnim(sb, CompactPushScale, ScaleTransform.ScaleXProperty, 1, 440, spring);
        AddAnim(sb, CompactPushScale, ScaleTransform.ScaleYProperty, 1, 440, spring);
        sb.Begin();
    }
    /// <summary>右键菜单主题色（圆角液态玻璃）。</summary>
    private void ApplyMenuTheme()
    {
        void Add(string key, Brush b) { b.Freeze(); Resources[key] = b; }
        SolidColorBrush bg, border, text, hover;
        if (_theme.IsDark)
        {
            bg = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xEE, 0x1B, 0x1B, 0x26));
            border = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0x59, 0xFF, 0xFF, 0xFF));
            text = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0xF2, 0xF2, 0xF7));
            hover = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0x30, 0xFF, 0xFF, 0xFF));
        }
        else
        {
            bg = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xEE, 0xF5, 0xF5, 0xFA));
            border = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0x66, 0xFF, 0xFF, 0xFF));
            text = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0x1D, 0x1D, 0x24));
            hover = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0x18, 0x00, 0x00, 0x00));
        }
        Add("MenuBgBrush", bg);
        Add("MenuBorderBrush", border);
        Add("MenuTextBrush", text);
        Add("MenuHoverBrush", hover);

        // 直接设置菜单背景/前景，保证即使资源查找失败也不会出现白底
        if (IslandMenu is not null)
        {
            IslandMenu.Background = bg;
            IslandMenu.Foreground = text;
            IslandMenu.BorderBrush = border;
        }
    }
    private void ApplyCardAlignment()
    {
        Card.HorizontalAlignment = _settings.Current.Position == IslandPosition.Right
            ? System.Windows.HorizontalAlignment.Right
            : System.Windows.HorizontalAlignment.Center;
        Card.Margin = _settings.Current.Position == IslandPosition.Right
            ? new Thickness(0, 0, 4, 0)
            : new Thickness(0);
    }

    private void OnVmPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(IslandViewModel.IsVisible):
                if (_vm.IsVisible) ShowIsland(instant: false);
                else HideIsland();
                ApplySize(); // 岛显示/隐藏时重新测量自动尺寸
                break;
            case nameof(IslandViewModel.IsExpanded):
                AnimateSize();
                if (_vm.IsExpanded && _vm.LyricIndex >= 0)
                    Dispatcher.BeginInvoke(() => ScrollLyricsTo(_vm.LyricIndex), DispatcherPriority.Loaded);
                if (!_vm.IsExpanded) _compactRestoreTimer.Start(); // 收起后兜底恢复精确尺寸，避免多次切换后上下间距异常
                break;
            case nameof(IslandViewModel.LyricIndex):
                if (_vm.LyricIndex >= 0) QueueLyricsScroll(_vm.LyricIndex);
                break;
            case nameof(IslandViewModel.HasActivePush):
                ApplySize();           // 确保窗口足够大（首次）
                AnimateCompactSize();  // 尺寸变化：弹簧动画，丝滑
                PlayPushCardAnimation(); // 上岛卡片：淡入 + 缩放动画
                ApplyExpandedSectionVisibility();
                break;
            case nameof(IslandViewModel.HasMedia):
                ApplyExpandedSectionVisibility();
                break;
        }
    }

    // ── 点击穿透 ──────────────────────────────────────────────

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg == WM_NCHITTEST)
        {
            var x = (short)(lParam.ToInt64() & 0xFFFF);
            var y = (short)((lParam.ToInt64() >> 16) & 0xFFFF);
            var local = PointFromScreen(new Point(x, y));
            handled = true;
            return IsPointOverCard(local) ? (IntPtr)HTCLIENT : (IntPtr)HTTRANSPARENT;
        }

        return IntPtr.Zero;
    }

    private bool IsPointOverCard(Point local)
    {
        var pos = Card.TransformToAncestor(this).Transform(new Point(0, 0));
        return local.X >= pos.X && local.Y >= pos.Y &&
               local.X <= pos.X + Card.ActualWidth &&
               local.Y <= pos.Y + Card.ActualHeight;
    }

    // ── 显示 / 隐藏 ────────────────────────────────────────────

    private void ShowIsland(bool instant)
    {
        if (!IsLoaded) return;
        if (!IsVisible)
        {
            Reposition();
            Show();
        }

        if (instant)
        {
            Opacity = 1;
            return;
        }

        Opacity = 0;
        BeginOpacity(1, 220);
    }

    private void HideIsland()
    {
        if (!IsVisible) return;
        var sb = new Storyboard();
        // 非线性淡出：先快后慢（EaseIn），消失过程不匀速、不生硬
        var fade = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(200))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseIn },
        };
        Storyboard.SetTarget(fade, this);
        Storyboard.SetTargetProperty(fade, new PropertyPath(OpacityProperty));
        sb.Children.Add(fade);
        sb.Completed += (_, _) => { if (!_vm.IsVisible) Hide(); };
        sb.Begin();
    }

    private void BeginOpacity(double to, int ms)
    {
        var sb = new Storyboard();
        var fade = new DoubleAnimation(Opacity, to, TimeSpan.FromMilliseconds(ms))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
        };
        Storyboard.SetTarget(fade, this);
        Storyboard.SetTargetProperty(fade, new PropertyPath(OpacityProperty));
        sb.Children.Add(fade);
        sb.Begin();
    }

    // ── iOS 风格形变动画 ──────────────────────────────────────

    private void AnimateSize()
    {
        if (!IsLoaded) return;
        if (_vm.IsExpanded) Expand();
        else Collapse();
    }

    private void Expand()
    {
        // 胶囊行保持可见（动画淡出），展开内容覆盖全卡片（动画淡入），两者重叠交叉过渡，
        // 避免 Card 深色背景透过内容间隙产生"黑掉"现象
        ContentGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
        ContentGrid.RowDefinitions[1].Height = GridLength.Auto;
        ContentGrid.VerticalAlignment = VerticalAlignment.Center;
        PillRow.BeginAnimation(UIElement.OpacityProperty, null);
        ExpandedContent.BeginAnimation(UIElement.OpacityProperty, null);

        // 先测量展开内容自然高度（ScrollViewer 内容总高），得到卡片目标高度
        ExpandedContent.Opacity = 0;
        ExpandedContent.Visibility = Visibility.Visible;
        ExpandedContent.Measure(new System.Windows.Size(ExpandedWidth - 24, double.PositiveInfinity));
        var contentH = ExpandedContent.DesiredSize.Height;
        var targetHeight = Math.Clamp(contentH + 24, 200, MaxExpandedHeight);

        // 重新显示胶囊行：动画期间淡出，与展开内容交叉过渡
        PillRow.BeginAnimation(UIElement.OpacityProperty, null);
        PillRow.Visibility = Visibility.Visible;
        PillRow.Opacity = 1;

        AnimateCard(ExpandedWidth, targetHeight, expand: true);
    }

    private void Collapse()
    {
        // 先恢复胶囊行（紧凑行占满并垂直居中），再缩回紧凑尺寸
        ContentGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
        ContentGrid.RowDefinitions[1].Height = GridLength.Auto; // 展开行恢复自适应
        // 保持垂直居中：收回后组件上下对称（此前设为 Top 会导致贴顶、下方留白，展开收回后距离不同）
        ContentGrid.VerticalAlignment = VerticalAlignment.Center;

        // 清除展开动画的残留（HoldEnd 会把 PillRow.Opacity 锁在 0，直接设本地值无效）
        PillRow.BeginAnimation(UIElement.OpacityProperty, null);
        ExpandedContent.BeginAnimation(UIElement.OpacityProperty, null);
        PillRow.Visibility = Visibility.Visible;
        PillRow.Opacity = 1;
        ExpandedContent.Visibility = Visibility.Collapsed; // 立即隐藏展开内容，避免残留覆盖

        AnimateCard(CompactWidth, CompactHeight, expand: false,
            onCompleted: () => { Card.Width = CompactWidth; Card.Height = CompactHeight; });
    }

    /// <summary>
    /// 动画：卡片尺寸用 iOS 阻尼弹簧（先快后慢、轻微过冲回弹），
    /// 展开内容错峰淡入缩放（延迟 50ms、更短时长），整体节奏非线性、不生硬。
    /// </summary>
    private void AnimateCard(double width, double height, bool expand, Action? onCompleted = null)
    {
        _currentStoryboard?.Stop();
        _currentStoryboard = null;

        // 减少动态效果：关闭弹簧/错峰动画，直接瞬时切换（无障碍 / 省电）
        if (_settings.Current.ReduceMotion)
        {
            Card.Width = width;
            Card.Height = height;
            PillRow.Visibility = expand ? Visibility.Collapsed : Visibility.Visible;
            PillRow.Opacity = expand ? 0 : 1;
            ExpandedContent.Visibility = expand ? Visibility.Visible : Visibility.Collapsed;
            ExpandedContent.Opacity = expand ? 1 : 0;
            ExpandedScale.ScaleX = ExpandedScale.ScaleY = expand ? 1 : 0.98;
            ExpandedTranslate.Y = expand ? 0 : 10;
            onCompleted?.Invoke();
            return;
        }

        var sb = new Storyboard();
        // iOS 风格弹簧：快启动 + 轻微弹性回弹 + 慢收尾（低阻尼、高刚度）
        var spring = new SpringEase { Damping = 10, Stiffness = 200, Mass = 1 };
        var smooth = new CubicEase { EasingMode = EasingMode.EaseOut };

        // 卡片尺寸：弹簧曲线（展开 880ms / 收起 760ms）
        AddAnim(sb, Card, FrameworkElement.WidthProperty, width, expand ? 880 : 760, spring);
        AddAnim(sb, Card, FrameworkElement.HeightProperty, height, expand ? 880 : 760, spring);

        // 展开内容：错峰淡入 + 轻微缩放/位移（展开延迟 95ms，让尺寸先动、内容跟上）
        var contentDelay = TimeSpan.FromMilliseconds(expand ? 95 : 0);
        AddAnim(sb, ExpandedContent, UIElement.OpacityProperty, expand ? 1 : 0, expand ? 480 : 300, smooth, contentDelay);
        AddAnim(sb, ExpandedScale, ScaleTransform.ScaleXProperty, expand ? 1 : 0.98, expand ? 720 : 640, spring, contentDelay);
        AddAnim(sb, ExpandedScale, ScaleTransform.ScaleYProperty, expand ? 1 : 0.98, expand ? 720 : 640, spring, contentDelay);
        AddAnim(sb, ExpandedTranslate, TranslateTransform.YProperty, expand ? 0 : 10, expand ? 720 : 640, smooth, contentDelay);

        // 胶囊行：展开后淡出（由大图区接管）；收起时立即恢复完全不透明，
        // 避免缩回瞬间胶囊内容还在淡入而出现"空内容"
        if (expand)
            AddAnim(sb, PillRow, UIElement.OpacityProperty, 0, 320, smooth, TimeSpan.FromMilliseconds(70));
        else
            PillRow.Opacity = 1;

        sb.Completed += (_, _) =>
        {
            _currentStoryboard = null;
            // 关键：清除动画对 Card 尺寸的 HoldEnd 锁定，否则之后设置本地尺寸（含自动重算）不生效，
            // 多次展开/收起后组件上下间距会残留异常
            Card.BeginAnimation(FrameworkElement.WidthProperty, null);
            Card.BeginAnimation(FrameworkElement.HeightProperty, null);
            // 必须写回最终尺寸：清除动画后若只依赖本地值，Card 会回退到紧凑时设置的
            // 本地尺寸（Width/Height），展开态瞬间缩回紧凑大小导致内容被裁剪而黑屏
            Card.Width = width;
            Card.Height = height;
            // 动画结束后整理可见性：展开态折叠胶囊行并固定展开内容不透明，收起态恢复胶囊行
            if (_vm.IsExpanded)
            {
                PillRow.Visibility = Visibility.Collapsed;
                PillRow.Opacity = 0;
                ExpandedContent.Opacity = 1;
            }
            else
            {
                PillRow.Visibility = Visibility.Visible;
                PillRow.Opacity = 1;
            }
            onCompleted?.Invoke();
        };
        _currentStoryboard = sb;
        sb.Begin();
    }

    private void AddAnim(Storyboard sb, DependencyObject target, DependencyProperty prop, double to, int ms, IEasingFunction easing, TimeSpan? beginTime = null)
    {
        var anim = new DoubleAnimation(to, TimeSpan.FromMilliseconds(ms))
        {
            EasingFunction = easing,
            BeginTime = beginTime ?? TimeSpan.Zero,
        };
        Storyboard.SetTarget(anim, target);
        Storyboard.SetTargetProperty(anim, new PropertyPath(prop));
        sb.Children.Add(anim);
    }

    // ── 定位 ──────────────────────────────────────────────────

    public void Reposition()
    {
        if (!IsLoaded) return;
        var pos = ScreenHelper.ComputePosition(_screen, _settings.Current.Position,
            ActualWidth, ActualHeight, _settings.Current.OffsetX, _settings.Current.OffsetY);
        Left = pos.X;
        Top = pos.Y;
        ApplyCardAlignment();
    }

    // ── 歌词自动滚动 ──────────────────────────────────────────

    private bool _lyricsScrollQueued;
    private readonly DispatcherTimer _lyricsScrollTimer;
    private double _lyricsScrollTarget;

    private void QueueLyricsScroll(int index)
    {
        if (!IsLoaded || !IsVisible || !_vm.IsExpanded) return;
        if (_lyricsScrollQueued) return;
        _lyricsScrollQueued = true;
        Dispatcher.BeginInvoke(() =>
        {
            _lyricsScrollQueued = false;
            // 执行时取最新索引：快速切句时排队中的旧索引会被最新句覆盖，滚动始终跟随当前句
            var current = _vm.LyricIndex >= 0 ? _vm.LyricIndex : index;
            ScrollLyricsTo(current);
        }, DispatcherPriority.Loaded);
    }

    private void ScrollLyricsTo(int index)
    {
        if (LyricsList.Items.Count == 0) return;
        index = Math.Clamp(index, 0, LyricsList.Items.Count - 1);
        var container = LyricsList.ItemContainerGenerator.ContainerFromIndex(index) as FrameworkElement;
        if (container is null) return;

        var viewer = LyricsScroll;
        var relY = container.TransformToAncestor(viewer).Transform(new Point(0, 0)).Y;
        // 视口相对坐标 + 当前偏移 = 内容坐标；再减去半个视口/加上半个行高使当前句居中
        var target = viewer.VerticalOffset + relY - viewer.ViewportHeight / 2 + container.ActualHeight / 2;
        target = Math.Max(0, target);
        _lyricsScrollTarget = target;
        _lyricsScrollTimer.Start();
    }

    /// <summary>平滑滚动：每帧按比例逼近目标偏移（当前句居中）。</summary>
    private void SmoothScrollStep()
    {
        var current = LyricsScroll.VerticalOffset;
        var delta = _lyricsScrollTarget - current;
        if (Math.Abs(delta) < 0.5)
        {
            LyricsScroll.ScrollToVerticalOffset(_lyricsScrollTarget);
            _lyricsScrollTimer.Stop();
            return;
        }

        LyricsScroll.ScrollToVerticalOffset(current + delta * 0.22);
    }
}
