using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
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
    /// <summary>字号缩放系数：整张卡片 LayoutTransform 缩放，逻辑尺寸 = 视觉尺寸 / 缩放比。</summary>
    private double FontScale => Math.Clamp(_settings.Current.FontScale, 0.8, 1.4);
    private double ManualCompactW => Math.Clamp(_settings.Current.CompactWidth / FontScale, 240 / FontScale, 520 / FontScale);
    private double ManualCompactH => Math.Clamp(_settings.Current.CompactHeight / FontScale, 48 / FontScale, 140 / FontScale);

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
            // 字号缩放：逻辑宽度按比例缩小，卡片渲染时再放大，最终视觉宽度不变
            return w >= 20 ? Math.Clamp((w + 56) / FontScale, 240 / FontScale, 720 / FontScale) : fallback; // 总留白 56（左侧 22 + 右侧 24，右侧略多）
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
        return (Math.Min(tw, 150) + 68) / FontScale;
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
                return Math.Clamp(baseW + PushCardCompactWidth(), 240 / FontScale, 720 / FontScale);
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
                return _noPushHValid ? Math.Clamp(_noPushCompactH, 44 / FontScale, 160 / FontScale) : Math.Clamp(_vm.EstimatedCompactHeight / FontScale, 44 / FontScale, 160 / FontScale);
            _noPushCompactH = _vm.EstimatedCompactHeight / FontScale;
            _noPushHValid = true;
            return _noPushCompactH;
        }
    }
    private double ExpandedWidth => _settings.Current.ExpandedWidthAuto
        ? _vm.EstimatedExpandedWidth / FontScale
        : Math.Clamp(_settings.Current.ExpandedWidth / FontScale, CompactWidth, 720 / FontScale);
    private double MaxExpandedHeight => _settings.Current.MaxExpandedHeightAuto
        ? _vm.EstimatedExpandedHeight / FontScale
        : Math.Clamp(_settings.Current.MaxExpandedHeight / FontScale, 240 / FontScale, 620 / FontScale);

    private const int WM_NCHITTEST = 0x0084;
    private const int HTCLIENT = 1;
    private const int HTTRANSPARENT = -1;

    private readonly IslandViewModel _vm;
    private readonly ThemeService _theme;
    private readonly SettingsService _settings;
    private readonly System.Windows.Forms.Screen _screen;
    private readonly DispatcherTimer _collapseTimer;
    private readonly DispatcherTimer _compactRestoreTimer;
    private bool _waveRendering;                  // 波纹渲染中（已挂接合成帧事件）
    private DispatcherTimer? _waveTimer;                  // 低功耗模式：波纹降帧定时器（~30fps）
    private double _lastWaveTime;                 // 上一帧时间（秒），用于帧率无关平滑
    private readonly System.Diagnostics.Stopwatch _waveClock = System.Diagnostics.Stopwatch.StartNew();
    private readonly List<ScaleTransform> _waveBarsExpanded = new();
    private readonly List<ScaleTransform> _waveBarsCompact = new();
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

        // 声音波纹：挂接合成帧事件，按显示器刷新率（~60fps）驱动，空闲时摘除不占 CPU
        if (WaveBar1 is not null)
        {
            _waveBarsExpanded.AddRange(new[] { WaveBar1, WaveBar2, WaveBar3, WaveBar4, WaveBar5, WaveBar6, WaveBar7 });
            _waveBarsCompact.AddRange(new[] { WaveBarC1, WaveBarC2, WaveBarC3, WaveBarC4, WaveBarC5, WaveBarC6, WaveBarC7 });
        }

        // 悬停不展开；移出时若已展开则延迟收起
        Card.MouseLeave += (_, _) =>
        {
            if (_vm.IsExpanded) _collapseTimer.Start();
        };

        // 双击检测：单击延迟 280ms 后切换展开/收起；窗口内第二次单击则执行快捷动作
        _clickDebounce.Tick += (_, _) =>
        {
            _clickDebounce.Stop();
            if (!_pendingClick) return;
            _pendingClick = false;
            _collapseTimer.Stop();
            _vm.IsExpanded = !_vm.IsExpanded;
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
        _settings.Changed += (_, _) =>
        {
            ApplyExpandedSectionVisibility();
            ApplyAppearance();
            RefreshWave();
            ApplyCoverTint();
        };

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
    public bool ExpandedShowArtTitle => _vm.HasMedia && _settings.Current.ExpandedShowArtTitle
        && _settings.Current.ExpandedCardStyle != "Hero"; // Hero 大卡片模板下隐藏经典小封面区
    /// <summary>媒体大卡片模板（Hero）：大封面背景 + 歌名/歌手/专辑叠加。</summary>
    public bool ExpandedHeroCard => _vm.HasMedia && _settings.Current.ExpandedCardStyle == "Hero";

    public bool ExpandedShowProgress => _vm.HasMedia && _settings.Current.ExpandedShowProgress;
    public bool ExpandedShowControls => _vm.HasMedia && _settings.Current.ExpandedShowControls;
    public bool ExpandedShowLyrics => _vm.HasMedia && _settings.Current.ExpandedShowLyrics;

    // ── 单行模式：紧凑态所有组件一行显示 ──
    public bool SingleLineMode => _settings.Current.SingleLineMode;
    // 声音波纹：播放中 + 开启波纹设置 + 岛可见才显示（空闲时停止计时器）
    public bool HasWave => _vm.IsVisible && _vm.HasMedia && _vm.IsPlaying && _settings.Current.WaveVisualizerEnabled;

    // 上岛推送内容：单行模式下只显示图标+标题（隐藏正文/进度/按钮）
    public bool PushShowBody => _vm.ActivePushHasBody && !SingleLineMode;
    public bool PushShowProgress => _vm.ActivePushHasProgress && !SingleLineMode;
    public bool PushShowButtons => _vm.ActivePushHasButtons && !SingleLineMode;

    // ── 点击展开 / 解锁拖动 / 右键菜单 ─────────────────────────

    private Point _downPoint;
    private bool _mouseDownOnCard;
    private bool _draggedCard;
    private readonly DispatcherTimer _clickDebounce = new() { Interval = TimeSpan.FromMilliseconds(280) };
    private bool _pendingClick;
    private Point _lastClickUp;

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
            CancelPendingClick();
            _collapseTimer.Stop();
            try { DragMove(); } catch { /* ignore */ }
            e.Handled = true;
        }
    }

    private void OnCardMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (_draggedCard) { _draggedCard = false; CancelPendingClick(); return; }
        if (!_mouseDownOnCard) return;
        _mouseDownOnCard = false;

        // 点击按钮/滑块不触发展开切换（按钮自己处理点击）
        if (IsInteractiveElement(e.OriginalSource)) return;

        // 上岛推送整卡点击回跳：点在推送卡片上且配置了 click 时，执行回跳而不展开
        if (_vm.ActivePushHasClick && IsWithinPushCard(e.OriginalSource))
        {
            CancelPendingClick();
            _vm.ExecutePushClick();
            e.Handled = true;
            return;
        }

        var pos = e.GetPosition(this);
        // 双击：与上一次单击距离相近且在窗口期内 → 执行快捷动作
        if (_pendingClick && _clickDebounce.IsEnabled &&
            Math.Abs(pos.X - _lastClickUp.X) < 24 && Math.Abs(pos.Y - _lastClickUp.Y) < 24)
        {
            CancelPendingClick();
            ExecuteDoubleClickAction();
            e.Handled = true;
            return;
        }

        // 单击：挂起，等待双击窗口超时后再切换展开/收起
        _pendingClick = true;
        _lastClickUp = pos;
        _clickDebounce.Stop();
        _clickDebounce.Start();
        e.Handled = true;
    }

    private void CancelPendingClick()
    {
        _pendingClick = false;
        _clickDebounce.Stop();
    }

    /// <summary>双击快捷动作：播放/暂停、打开设置或无动作（在设置-通用中配置）。</summary>
    private void ExecuteDoubleClickAction()
    {
        switch (_settings.Current.DoubleClickAction)
        {
            case "OpenSettings":
                _vm.OpenSettingsCommand.Execute(null);
                break;
            case "None":
                break;
            default: // PlayPause
                if (_vm.CanPlayPause)
                    _vm.PlayPauseCommand.Execute(null);
                break;
        }
    }

    /// <summary>判断点击源是否位于上岛推送卡片内部。</summary>
    private bool IsWithinPushCard(object source)
    {
        var d = source as DependencyObject;
        while (d is not null)
        {
            if (ReferenceEquals(d, CompactPushCard) || ReferenceEquals(d, ExpandedPushCard))
                return true;
            d = d is System.Windows.Media.Visual or System.Windows.Media.Media3D.Visual3D
                ? VisualTreeHelper.GetParent(d)
                : LogicalTreeHelper.GetParent(d);
        }
        return false;
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

    /// <summary>点击番茄钟组件：暂停/继续。</summary>
    private void TimerItem_Click(object sender, RoutedEventArgs e)
    {
        _vm.ToggleTimerPause();
        e.Handled = true;
    }

    /// <summary>点击输入法组件：切换中/英输入法。</summary>
    private void InputMethodItem_Click(object sender, RoutedEventArgs e)
    {
        _vm.ToggleInputMethod();
        e.Handled = true;
    }

    /// <summary>点击快捷开关（Button.Tag: wifi / bluetooth / night / mute）。</summary>
    private void QuickToggle_Click(object sender, RoutedEventArgs e)
    {
        if ((sender as FrameworkElement)?.Tag is string which)
            _vm.ToggleQuickSwitch(which);
        e.Handled = true;
    }

    /// <summary>歌词翻译开关：显示 / 隐藏翻译行。</summary>
    private void LyricTranslate_Click(object sender, RoutedEventArgs e)
    {
        _vm.ToggleLyricTranslation();
        e.Handled = true;
    }

    /// <summary>复制当前歌词句到剪贴板。</summary>
    private void CopyCurrentLyric_Click(object sender, RoutedEventArgs e)
    {
        _vm.CopyCurrentLyric();
        e.Handled = true;
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
        ApplyAppearance();
        RefreshWave();
        ApplyCoverTint();
    }

    /// <summary>展开卡片分区块的可见性随设置即时刷新。</summary>
    private void ApplyExpandedSectionVisibility()
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ExpandedShowArtTitle)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ExpandedHeroCard)));
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
    /// <summary>
    /// 确保透明窗口尺寸足够容纳当前卡片（含紧凑态自动宽度），
    /// 否则卡片超出窗口边界会被裁剪。紧凑卡片视觉宽度 = CompactWidth × FontScale。
    /// </summary>
    private void EnsureWindowSizeFits()
    {
        if (!IsLoaded) return;
        var settingExpanded = Math.Clamp(_settings.Current.ExpandedWidth, 300, 620);
        var settingMaxH = Math.Clamp(_settings.Current.MaxExpandedHeight, 240, 620);
        var w = Math.Max(settingExpanded, Math.Max(CompactWidth * FontScale + 8, 640)) + 24;
        var h = Math.Max(settingMaxH, 220) + 24;
        if (Math.Abs(Width - w) > 0.5 || Math.Abs(Height - h) > 0.5)
        {
            Width = w;
            Height = h;
            Dispatcher.BeginInvoke(Reposition, System.Windows.Threading.DispatcherPriority.Loaded);
        }
    }

    public void ApplySize()
    {
        // 窗口固定为能容纳最大推送卡片/展开内容的大小：推送时只需动画 Card 形变，避免窗口级 Resize 卡顿
        EnsureWindowSizeFits();
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

    /// <summary>应用外观参数：圆角 / 字体 / 字号缩放。字号缩放作用于整张卡片（LayoutTransform），
    /// 逻辑尺寸同步除以缩放比，最终视觉尺寸与设置一致、不溢出不裁剪。</summary>
    private void ApplyAppearance()
    {
        try { System.Windows.Documents.TextElement.SetFontFamily(Card, new System.Windows.Media.FontFamily(_settings.Current.FontFamily)); } catch { /* 非法字体名忽略 */ }
        Card.CornerRadius = new CornerRadius(Math.Clamp(_settings.Current.CornerRadius, 16, 40));
        // 字体缩放 = 1 时清空 LayoutTransform（走普通布局路径，动画期间布局更轻、更快）；
        // 只有用户设置缩放时才使用 ScaleTransform，避免无谓的变换开销。
        Card.LayoutTransform = Math.Abs(FontScale - 1.0) < 0.001 ? null : new ScaleTransform(FontScale, FontScale);
        ApplySize();
    }

    /// <summary>推送到达/更新/过期时：Card 尺寸用弹簧动画平滑过渡到新大小（丝滑不生硬）。</summary>
    private void AnimateCompactSize()
    {
        if (!IsLoaded) return;
        if (_vm.IsExpanded) { ApplySize(); return; }
        EnsureWindowSizeFits(); // 先扩宽窗口，避免卡片动画期间超出窗口被裁剪
        var (styleEase, styleMs) = GetSizeAnimationStyle(expand: false);
        var lm = _settings.Current.LowPowerMode ? 0.6 : 1.0;
        var dur = (int)Math.Clamp(460 * (styleMs / 760.0), 300, 640) * lm;
        var sb = new Storyboard();
        AddAnim(sb, Card, FrameworkElement.WidthProperty, CompactWidth, (int)dur, styleEase);
        AddAnim(sb, Card, FrameworkElement.HeightProperty, CompactHeight, (int)dur, styleEase);
        Timeline.SetDesiredFrameRate(sb, 60); // 稳定 60fps（120Hz 显示器上也按 60fps 渲染，减少开销不掉帧）
        sb.Begin();
    }

    /// <summary>第三方应用上岛：推送卡片淡入 + 轻微缩放的丝滑动画。</summary>
    private void PlayPushCardAnimation()
    {
        if (!IsLoaded || CompactPushCard is null || !_vm.HasActivePush) return;
        CompactPushCard.Opacity = 0;
        CompactPushScale.ScaleX = CompactPushScale.ScaleY = 0.94;
        var sb = new Storyboard();
        var (styleEase, styleMs) = GetSizeAnimationStyle(expand: true);
        var smooth = new CubicEase { EasingMode = EasingMode.EaseOut };
        var lm = _settings.Current.LowPowerMode ? 0.6 : 1.0;
        var scaleDur = (int)(Math.Min(440, styleMs) * lm);
        AddAnim(sb, CompactPushCard, UIElement.OpacityProperty, 1, (int)(320 * lm), smooth);
        AddAnim(sb, CompactPushScale, ScaleTransform.ScaleXProperty, 1, scaleDur, styleEase);
        AddAnim(sb, CompactPushScale, ScaleTransform.ScaleYProperty, 1, scaleDur, styleEase);
        Timeline.SetDesiredFrameRate(sb, 60); // 稳定 60fps（120Hz 显示器上也按 60fps 渲染，减少开销不掉帧）
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
                RefreshWave(); // 若启动时已有媒体在播放，确保波纹定时器在岛显示后启动
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
            case nameof(IslandViewModel.CurrentLyricText):
                // 当前歌词行变化时，若处于紧凑态则平滑调整宽度，避免长歌词被裁切/遮挡
                if (!_vm.IsExpanded && _vm.IsVisible) AnimateCompactSize();
                break;
            case nameof(IslandViewModel.HasActivePush):
                ApplySize();           // 确保窗口足够大（首次）
                AnimateCompactSize();  // 尺寸变化：弹簧动画，丝滑
                PlayPushCardAnimation(); // 上岛卡片：淡入 + 缩放动画
                ApplyExpandedSectionVisibility();
                break;
            case nameof(IslandViewModel.HasMedia):
                ApplyExpandedSectionVisibility();
                RefreshWave();
                break;
            case nameof(IslandViewModel.IsPlaying):
                RefreshWave();
                break;
            case nameof(IslandViewModel.Artwork):
                ApplyCoverTint();
                break;
        }
    }

    // ── 声音波纹 / 封面取色 ─────────────────────────────────────

    private void RefreshWave()
    {
        var on = HasWave;
        var lowPower = _settings.Current.LowPowerMode;
        // 三态：关闭 / 普通（CompositionTarget 60fps）/ 低功耗定时器（~30fps）
        var wantTimer = on && lowPower;
        var wantComposition = on && !lowPower;
        var isTimer = _waveTimer is not null;
        var isComposition = _waveRendering && !isTimer;
        if (wantTimer != isTimer || wantComposition != isComposition)
        {
            StopWaveRender();
            if (wantTimer)
            {
                _waveRendering = true;
                _waveTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(33) };
                _waveTimer.Tick += (_, _) => OnWaveFrame(null, EventArgs.Empty);
                _waveTimer.Start();
            }
            else if (wantComposition)
            {
                _waveRendering = true;
                CompositionTarget.Rendering += OnWaveFrame;
            }
        }
        if (!on)
        {
            foreach (var sc in _waveBarsExpanded) sc.ScaleY = 0.16;
            foreach (var sc in _waveBarsCompact) sc.ScaleY = 0.16;
        }
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasWave)));
    }
    /// <summary>停止波纹渲染（摘除合成帧事件 + 停止降帧定时器）。</summary>
    private void StopWaveRender()
    {
        _waveRendering = false;
        if (_waveTimer is not null)
        {
            _waveTimer.Stop();
            _waveTimer = null;
        }
        CompositionTarget.Rendering -= OnWaveFrame;
    }
    /// <summary>合成帧回调：按帧间隔指数平滑，随真实音频电平起伏，动画连贯不卡顿。</summary>
    private void OnWaveFrame(object? sender, EventArgs e)
    {
        try
        {
            if (!IsLoaded || !HasWave)
            {
                RefreshWave();
                return;
            }
            var now = _waveClock.Elapsed.TotalSeconds;
            var dt = Math.Min(0.05, Math.Max(0.001, now - _lastWaveTime));
            _lastWaveTime = now;

            var level = Math.Clamp(_vm.WaveLevel, 0, 1);
            var height = Math.Clamp(_settings.Current.WaveHeight, 0.25, 2.0);
            var alpha = 1.0 - Math.Exp(-dt * 22.0); // 帧率无关的指数平滑
            UpdateWaveSet(_waveBarsCompact, level, now, alpha, height);
            UpdateWaveSet(_waveBarsExpanded, level, now, alpha, height);
        }
        catch
        {
            // 渲染异常绝不影响主流程
        }
    }

    private void UpdateWaveSet(IReadOnlyList<ScaleTransform> bars, double level, double t, double alpha, double height)
    {
        var n = bars.Count;
        for (var i = 0; i < n; i++)
        {
            var sc = bars[i];
            double target;
            if (_vm.IsPlaying)
            {
                var phase = t * 6.0 - i * 0.9;
                var wave = 0.5 + 0.5 * Math.Sin(phase);
                target = Math.Clamp((0.10 + (0.12 + 0.72 * level) * wave) * height, 0.08, 1.0);
            }
            else
            {
                target = 0.08;
            }
            sc.ScaleY += (target - sc.ScaleY) * alpha;
        }
    }
    /// <summary>展开背景随专辑封面取色：1x1 采样主色 + 主题底色线性渐变，失败则回退主题背景。</summary>
    private void ApplyCoverTint()
    {
        try
        {
            var src = _vm.Artwork;
            if (src is null || !_settings.Current.CoverTintBackground)
            {
                ClearCoverTint();
                return;
            }
            var color = SampleCoverColor(src);
            if (color is null) { ClearCoverTint(); return; }
            var c = color.Value;
            var baseColor = (_theme.CardBackground as SolidColorBrush)?.Color
                ?? System.Windows.Media.Color.FromArgb(0xF0, 0x14, 0x14, 0x1E);
            var g = new LinearGradientBrush
            {
                StartPoint = new System.Windows.Point(0, 0),
                EndPoint = new System.Windows.Point(1, 1),
            };
            g.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(0xE6, c.R, c.G, c.B), 0));
            g.GradientStops.Add(new GradientStop(baseColor, 1));
            g.Freeze();
            Card.Background = g;
        }
        catch
        {
            ClearCoverTint();
        }
    }

    /// <summary>恢复 Card 背景为绑定的主题色（移除封面取色）。</summary>
    private void ClearCoverTint()
    {
        try
        {
            Card.SetBinding(Border.BackgroundProperty, new System.Windows.Data.Binding(nameof(CardBackground))
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(Window), 1),
            });
        }
        catch
        {
            Card.Background = _theme.CardBackground;
        }
    }

    /// <summary>把封面渲染到 1x1 位图采样主色（RGBA）。</summary>
    private static System.Windows.Media.Color? SampleCoverColor(ImageSource src)
    {
        try
        {
            var rtb = new RenderTargetBitmap(1, 1, 96, 96, PixelFormats.Pbgra32);
            var img = new System.Windows.Controls.Image { Source = src, Stretch = Stretch.UniformToFill };
            rtb.Render(img);
            var px = new byte[4];
            rtb.CopyPixels(px, 4, 0);
            if (px[3] < 40) return null; // 透明/未加载完成，放弃取色
            return System.Windows.Media.Color.FromArgb(255, px[2], px[1], px[0]);
        }
        catch
        {
            return null;
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
        Timeline.SetDesiredFrameRate(sb, 60); // 稳定 60fps（120Hz 显示器上也按 60fps 渲染，减少开销不掉帧）
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
        Timeline.SetDesiredFrameRate(sb, 60); // 稳定 60fps（120Hz 显示器上也按 60fps 渲染，减少开销不掉帧）
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
        // 动效皮肤（33）：Spring= iOS 弹簧（默认）/ Soft=柔和 / Elastic=弹性 / Fade=简洁渐隐
        var (styleEase, styleSizeMs) = GetSizeAnimationStyle(expand);
        var smooth = new CubicEase { EasingMode = EasingMode.EaseOut };
        var lm = _settings.Current.LowPowerMode ? 0.6 : 1.0; // 低功耗模式（37）：动画时间缩短，更快进入空闲

        // 卡片尺寸：动效皮肤曲线（展开/收起时长由皮肤决定）
        AddAnim(sb, Card, FrameworkElement.WidthProperty, width, (int)(styleSizeMs * lm), styleEase);
        AddAnim(sb, Card, FrameworkElement.HeightProperty, height, (int)(styleSizeMs * lm), styleEase);

        // 展开内容：错峰淡入 + 轻微缩放/位移（展开延迟 95ms，让尺寸先动、内容跟上）
        var contentDelay = TimeSpan.FromMilliseconds((expand ? 95 : 0) * lm);
        AddAnim(sb, ExpandedContent, UIElement.OpacityProperty, expand ? 1 : 0, (int)((expand ? 480 : 300) * lm), smooth, contentDelay);
        AddAnim(sb, ExpandedScale, ScaleTransform.ScaleXProperty, expand ? 1 : 0.98, (int)((expand ? 720 : 640) * lm), styleEase, contentDelay);
        AddAnim(sb, ExpandedScale, ScaleTransform.ScaleYProperty, expand ? 1 : 0.98, (int)((expand ? 720 : 640) * lm), styleEase, contentDelay);
        AddAnim(sb, ExpandedTranslate, TranslateTransform.YProperty, expand ? 0 : 10, (int)((expand ? 720 : 640) * lm), smooth, contentDelay);

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
        Timeline.SetDesiredFrameRate(sb, 60); // 稳定 60fps（120Hz 显示器上也按 60fps 渲染，减少开销不掉帧）
        sb.Begin();
    }

    /// <summary>
    /// 动效皮肤（33）：返回 (尺寸缓动, 基准时长毫秒) 元组。
    /// Spring = iOS 阻尼弹簧（默认，轻微过冲回弹）；Soft = 柔和弹簧（回弹更少更软）；
    /// Elastic = 弹性回弹（明显弹跳）；Fade = 简洁渐隐（无回弹，最克制）。
    /// </summary>
    private (IEasingFunction Easing, int SizeMs) GetSizeAnimationStyle(bool expand)
    {
        switch (_settings.Current.AnimationStyle)
        {
            case "Soft":
                return (new SoftSpringEase(), expand ? 1050 : 920);
            case "Elastic":
                return (new ElasticEase
                {
                    Oscillations = 1,
                    Springiness = 6,
                    EasingMode = EasingMode.EaseOut,
                }, expand ? 960 : 830);
            case "Fade":
                return (new CubicEase { EasingMode = EasingMode.EaseOut }, expand ? 620 : 520);
            default: // Spring
                return (new SpringEase { Damping = 10, Stiffness = 200, Mass = 1 }, expand ? 880 : 760);
        }
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
        var s = _settings.Current;
        var pos = ScreenHelper.ComputePosition(_screen, s.Position,
            ActualWidth, ActualHeight, s.OffsetX, s.OffsetY);
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
