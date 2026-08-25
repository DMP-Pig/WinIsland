using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;

namespace WinIsland.UI;

/// <summary>
/// 逐字卡拉OK歌词控件（带平滑过渡动画 + 逐字点亮回弹反馈）。
/// 按字符着色：已点亮字符用高亮色，边界字符在 60fps 下从基础色平滑混色到高亮色，
/// 高亮按阅读顺序从左到右流动（换行也正确，不会出现多行同时点亮）；
/// 每句从 0 开始，第一个字保持未点亮再逐步点亮；
/// 每点亮一个新字时，整行做一次极轻微的放大-回弹（弹簧感），让逐字点亮更有「节奏感」。
/// </summary>
public class KaraokeTextBlock : TextBlock
{
    public static readonly DependencyProperty KaraokeTextProperty =
        DependencyProperty.Register(nameof(KaraokeText), typeof(string), typeof(KaraokeTextBlock),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure, OnRenderPropsChanged));

    /// <summary>目标高亮比例 0..1（连续值，由 ViewModel 每 100ms 平滑驱动）。</summary>
    public static readonly DependencyProperty HighlightFractionProperty =
        DependencyProperty.Register(nameof(HighlightFraction), typeof(double), typeof(KaraokeTextBlock),
            new FrameworkPropertyMetadata(0.0, OnRenderPropsChanged));

    public static readonly DependencyProperty HighlightBrushProperty =
        DependencyProperty.Register(nameof(HighlightBrush), typeof(Brush), typeof(KaraokeTextBlock),
            new FrameworkPropertyMetadata(Brushes.White, OnRenderPropsChanged));

    public static readonly DependencyProperty BaseBrushProperty =
        DependencyProperty.Register(nameof(BaseBrush), typeof(Brush), typeof(KaraokeTextBlock),
            new FrameworkPropertyMetadata(Brushes.Gray, OnRenderPropsChanged));

    /// <summary>是否正在播放：播放中换行从 0 点亮；暂停/启动恢复换行直接显示目标高亮。</summary>
    public static readonly DependencyProperty IsPlayingProperty =
        DependencyProperty.Register(nameof(IsPlaying), typeof(bool), typeof(KaraokeTextBlock),
            new FrameworkPropertyMetadata(false, OnRenderPropsChanged));

    private readonly DispatcherTimer _animTimer;
    private double _currentFraction;   // 当前已点亮比例（0..1，平滑推进）
    private double _targetFraction;    // 目标比例（0..1，来自 HighlightFraction）
    private string _lastText = string.Empty;
    private int _lastLitChars;         // 上次已完全点亮的字符数（用于检测「新字点亮」触发回弹）
    private bool _popActive;           // 回弹动画进行中
    private double _popScale = 1.0;    // 当前回弹缩放（1.0 = 静止）
    private readonly ScaleTransform _pop = new(1.0, 1.0);
    // 缓存 3 个 Run：同一句歌词内只更新 Foreground（仅重绘、不触发布局），换句时才重建文本
    private Run? _litRun;      // 已完全点亮部分（高亮色）
    private Run? _blendRun;    // 边界字符（基础色→高亮色混色）
    private Run? _restRun;     // 未点亮部分（基础色）

    public KaraokeTextBlock()
    {
        RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
        RenderTransform = _pop;
        _animTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(16) }; // ≈60fps
        _animTimer.Tick += (_, _) => TickAnimation();
        IsVisibleChanged += (_, _) =>
        {
            if (!IsVisible) _animTimer.Stop();
            else RefreshTarget();
        };
    }

    public string KaraokeText
    {
        get => (string)GetValue(KaraokeTextProperty);
        set => SetValue(KaraokeTextProperty, value);
    }

    public double HighlightFraction
    {
        get => (double)GetValue(HighlightFractionProperty);
        set => SetValue(HighlightFractionProperty, value);
    }

    public Brush HighlightBrush
    {
        get => (Brush)GetValue(HighlightBrushProperty);
        set => SetValue(HighlightBrushProperty, value);
    }

    public Brush BaseBrush
    {
        get => (Brush)GetValue(BaseBrushProperty);
        set => SetValue(BaseBrushProperty, value);
    }

    public bool IsPlaying
    {
        get => (bool)GetValue(IsPlayingProperty);
        set => SetValue(IsPlayingProperty, value);
    }

    private static void OnRenderPropsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((KaraokeTextBlock)d).RefreshTarget();

    private void RefreshTarget()
    {
        var text = KaraokeText ?? string.Empty;
        _targetFraction = Math.Clamp(HighlightFraction, 0, 1);

        // 换行时从 0 开始：新句第一个字保持未点亮，随进度从左到右平滑点亮。
        if (!string.Equals(text, _lastText, StringComparison.Ordinal))
        {
            _lastText = text;
            _lastLitChars = 0; // 新句：还没有任何字点亮
            if (IsPlaying)
            {
                // 播放中换句：从 0 开始，第一个字先不亮，随进度平滑点亮
                _currentFraction = 0;
                _animTimer.Start();
            }
            else
            {
                // 暂停/启动恢复换行：直接显示目标高亮（即暂停时刻的样子），不跳 0
                _currentFraction = _targetFraction;
                _animTimer.Stop();
            }
            Render();
            return;
        }

        if (Math.Abs(_currentFraction - _targetFraction) < 0.002 && !_popActive)
        {
            _currentFraction = _targetFraction;
            _animTimer.Stop();
            Render();
            return;
        }

        if (!_animTimer.IsEnabled) _animTimer.Start();
    }

    private void TickAnimation()
    {
        // 缓动逼近：差距大时走得快、接近时变慢（0.5 让高亮更跟拍，同时保持平滑）。
        _currentFraction += (_targetFraction - _currentFraction) * 0.5;
        if (Math.Abs(_currentFraction - _targetFraction) < 0.002)
        {
            _currentFraction = _targetFraction;
            // 颜色已到位，但回弹还没结束的话继续驱动动画
            if (!_popActive) _animTimer.Stop();
        }
        // 回弹衰减：1.08 → 1.0 弹簧回落（指数衰减，平滑不生硬）
        if (_popActive)
        {
            _popScale += (1.0 - _popScale) * 0.30;
            if (1.0 - _popScale < 0.002)
            {
                _popScale = 1.0;
                _popActive = false;
                if (Math.Abs(_currentFraction - _targetFraction) < 0.002) _animTimer.Stop();
            }
            _pop.ScaleX = _pop.ScaleY = _popScale;
        }
        Render();
    }

    private void Render()
    {
        var text = KaraokeText ?? string.Empty;

        // 空文本：清空 Inlines（同时释放缓存的 Run）
        if (text.Length == 0)
        {
            if (Inlines.Count > 0)
            {
                Inlines.Clear();
                _litRun = _blendRun = _restRun = null;
            }
            return;
        }

        var f = Math.Clamp(_currentFraction, 0, 1);
        var hl = ToColor(HighlightBrush) ?? System.Windows.Media.Colors.White;
        var bs = ToColor(BaseBrush) ?? System.Windows.Media.Colors.Gray;

        // 按字符着色（而非二维渐变）：文字换行时高亮也按阅读顺序从左到右逐行流动，
        // 不会出现“每一行开头都亮一段”的两条高亮线。
        var len = text.Length;
        var litChars = Math.Min((int)Math.Floor(f * len), len);      // 已完全点亮的字符数
        var blend = f * len - litChars;                              // 边界字符混色比例 0..1
        if (litChars >= len) blend = 1;                              // 整句点亮

        // 逐字点亮回弹：播放中每「点亮一个新字」触发一次极轻微放大-回弹（1.08 → 1.0）
        if (IsPlaying && !_popActive && litChars > _lastLitChars && litChars > 0 && litChars <= len)
        {
            _popScale = 1.08;
            _popActive = true;
            _pop.ScaleX = _pop.ScaleY = _popScale;
            if (!_animTimer.IsEnabled) _animTimer.Start();
        }
        _lastLitChars = litChars;

        // 换句时才重建 Inlines（文本变化必然触发布局）；同一句只更新颜色（仅重绘，60fps 下不卡布局）
        if (_litRun is null || !string.Equals(_lastText, text, StringComparison.Ordinal))
        {
            _lastText = text;
            _litRun = new Run();
            _blendRun = new Run();
            _restRun = new Run();
            Inlines.Clear();
            Inlines.Add(_litRun);
            Inlines.Add(_blendRun);
            Inlines.Add(_restRun);
        }

        _litRun.Text = litChars > 0 ? text.Substring(0, litChars) : string.Empty;
        _litRun.Foreground = Frozen(new System.Windows.Media.SolidColorBrush(hl));

        _blendRun!.Text = litChars < len ? text[litChars].ToString() : string.Empty;
        _blendRun.Foreground = litChars < len
            ? Frozen(new System.Windows.Media.SolidColorBrush(Lerp(bs, hl, Math.Clamp(blend, 0, 1))))
            : Frozen(new System.Windows.Media.SolidColorBrush(bs));

        _restRun!.Text = litChars + 1 < len ? text.Substring(litChars + 1) : string.Empty;
        _restRun.Foreground = Frozen(new System.Windows.Media.SolidColorBrush(bs));
    }

    private static System.Windows.Media.SolidColorBrush Frozen(System.Windows.Media.SolidColorBrush b) { b.Freeze(); return b; }

    private static System.Windows.Media.Color Lerp(System.Windows.Media.Color a, System.Windows.Media.Color b, double t)
        => System.Windows.Media.Color.FromArgb(
            (byte)(a.A + (b.A - a.A) * t),
            (byte)(a.R + (b.R - a.R) * t),
            (byte)(a.G + (b.G - a.G) * t),
            (byte)(a.B + (b.B - a.B) * t));

    private static System.Windows.Media.Color? ToColor(Brush? brush)
        => (brush as SolidColorBrush)?.Color;
}