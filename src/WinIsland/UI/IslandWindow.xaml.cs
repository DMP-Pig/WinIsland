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
    private double CompactWidth => Math.Clamp(_settings.Current.CompactWidth, 240, 520);
    private double CompactHeight => Math.Clamp(_settings.Current.CompactHeight, 48, 140);
    private double ExpandedWidth => Math.Clamp(_settings.Current.ExpandedWidth, CompactWidth, 620);
    private double MaxExpandedHeight => Math.Clamp(_settings.Current.MaxExpandedHeight, 240, 620);

    private const int WM_NCHITTEST = 0x0084;
    private const int HTCLIENT = 1;
    private const int HTTRANSPARENT = -1;

    private readonly IslandViewModel _vm;
    private readonly ThemeService _theme;
    private readonly SettingsService _settings;
    private readonly System.Windows.Forms.Screen _screen;
    private readonly DispatcherTimer _collapseTimer;
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

    /// <summary>按设置调整窗口与卡片尺寸（紧凑/展开），并重新定位。</summary>
    public void ApplySize()
    {
        var w = Math.Max(ExpandedWidth, CompactWidth) + 24;
        var h = Math.Max(MaxExpandedHeight, CompactHeight) + 24;
        if (Math.Abs(Width - w) > 0.5 || Math.Abs(Height - h) > 0.5)
        {
            Width = w;
            Height = h;
        }
        if (!_vm.IsExpanded)
        {
            Card.Width = CompactWidth;
            Card.Height = CompactHeight;
        }
        Dispatcher.BeginInvoke(Reposition, System.Windows.Threading.DispatcherPriority.Loaded);
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
                break;
            case nameof(IslandViewModel.IsExpanded):
                AnimateSize();
                if (_vm.IsExpanded && _vm.LyricIndex >= 0)
                    Dispatcher.BeginInvoke(() => ScrollLyricsTo(_vm.LyricIndex), DispatcherPriority.Loaded);
                break;
            case nameof(IslandViewModel.LyricIndex):
                if (_vm.LyricIndex >= 0) QueueLyricsScroll(_vm.LyricIndex);
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
        var fade = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(180));
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
        // 展开时收起胶囊行（紧凑封面/按钮不叠加在大封面上方），行高归零，再测量高度
        ContentGrid.RowDefinitions[0].Height = GridLength.Auto;
        PillRow.Visibility = Visibility.Collapsed;
        ExpandedContent.Visibility = Visibility.Visible;
        ContentGrid.Measure(new System.Windows.Size(ExpandedWidth - 24, double.PositiveInfinity));
        var targetHeight = Math.Clamp(ContentGrid.DesiredSize.Height + 16, 200, MaxExpandedHeight);
        AnimateCard(ExpandedWidth, targetHeight, expand: true);
    }

    private void Collapse()
    {
        // 先恢复胶囊行（紧凑行占满并垂直居中），再缩回紧凑尺寸
        ContentGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Star);
        PillRow.Visibility = Visibility.Visible;
        AnimateCard(CompactWidth, CompactHeight, expand: false,
            onCompleted: () => ExpandedContent.Visibility = Visibility.Collapsed);
    }

    /// <summary>
    /// 动画：卡片尺寸用 iOS 阻尼弹簧（先快后慢、轻微过冲回弹），
    /// 展开内容错峰淡入缩放（延迟 50ms、更短时长），整体节奏非线性、不生硬。
    /// </summary>
    private void AnimateCard(double width, double height, bool expand, Action? onCompleted = null)
    {
        _currentStoryboard?.Stop();
        _currentStoryboard = null;

        var sb = new Storyboard();
        var spring = new SpringEase { Damping = 11, Stiffness = 220, Mass = 1 };
        var smooth = new CubicEase { EasingMode = EasingMode.EaseOut };

        // 卡片尺寸：弹簧曲线（展开 380ms / 收起 300ms，收起更快更干脆）
        AddAnim(sb, Card, FrameworkElement.WidthProperty, width, expand ? 380 : 300, spring);
        AddAnim(sb, Card, FrameworkElement.HeightProperty, height, expand ? 380 : 300, spring);

        // 展开内容：错峰淡入 + 轻微缩放/位移（延迟 50ms，让尺寸先动、内容跟上）
        var contentDelay = TimeSpan.FromMilliseconds(expand ? 55 : 0);
        AddAnim(sb, ExpandedContent, UIElement.OpacityProperty, expand ? 1 : 0, expand ? 200 : 110, smooth, contentDelay);
        AddAnim(sb, ExpandedScale, ScaleTransform.ScaleXProperty, expand ? 1 : 0.98, 320, spring, contentDelay);
        AddAnim(sb, ExpandedScale, ScaleTransform.ScaleYProperty, expand ? 1 : 0.98, 320, spring, contentDelay);
        AddAnim(sb, ExpandedTranslate, TranslateTransform.YProperty, expand ? 0 : 10, 320, smooth, contentDelay);

        // 胶囊标题：展开后淡出（由大图区接管）
        AddAnim(sb, PillText, UIElement.OpacityProperty, expand ? 0 : 1, 140, smooth, TimeSpan.FromMilliseconds(expand ? 30 : 0));

        sb.Completed += (_, _) =>
        {
            _currentStoryboard = null;
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
