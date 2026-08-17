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
/// 逐字卡拉OK歌词控件（带平滑过渡动画）。
/// 按字符着色：已点亮字符用高亮色，边界字符在 60fps 下从基础色平滑混色到高亮色，
/// 高亮按阅读顺序从左到右流动（换行也正确，不会出现多行同时点亮）；
/// 每句从 0 开始，第一个字保持未点亮再逐步点亮。
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

    private readonly DispatcherTimer _animTimer;
    private double _currentFraction;   // 当前已点亮比例（0..1，平滑推进）
    private double _targetFraction;    // 目标比例（0..1，来自 HighlightFraction）
    private string _lastText = string.Empty;

    public KaraokeTextBlock()
    {
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
            _currentFraction = 0;
            Render(); // 立即显示新句（全未点亮），再随进度平滑点亮
            _animTimer.Start();
            return;
        }

        if (Math.Abs(_currentFraction - _targetFraction) < 0.002)
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
        // 缓动逼近：差距大时走得快、接近时变慢，产生自然的加速/减速（iOS 式平滑填充）。
        _currentFraction += (_targetFraction - _currentFraction) * 0.22;
        if (Math.Abs(_currentFraction - _targetFraction) < 0.002)
        {
            _currentFraction = _targetFraction;
            _animTimer.Stop();
        }
        Render();
    }

    private void Render()
    {
        Inlines.Clear();
        var text = KaraokeText ?? string.Empty;
        if (text.Length == 0) return;

        var f = Math.Clamp(_currentFraction, 0, 1);
        var hl = ToColor(HighlightBrush) ?? System.Windows.Media.Colors.White;
        var bs = ToColor(BaseBrush) ?? System.Windows.Media.Colors.Gray;

        // 按字符着色（而非二维渐变）：文字换行时高亮也按阅读顺序从左到右逐行流动，
        // 不会出现“每一行开头都亮一段”的两条高亮线。
        var len = text.Length;
        var litChars = Math.Min((int)Math.Floor(f * len), len);      // 已完全点亮的字符数
        var blend = f * len - litChars;                              // 边界字符混色比例 0..1
        if (litChars >= len) blend = 1;                              // 整句点亮

        if (litChars > 0)
        {
            Inlines.Add(new Run(text.Substring(0, litChars)) { Foreground = Frozen(new System.Windows.Media.SolidColorBrush(hl)) });
        }

        if (litChars < len)
        {
            // 边界字符：从基础色平滑过渡到高亮色（这就是“流畅”的过渡动画）
            var bc = Lerp(bs, hl, Math.Clamp(blend, 0, 1));
            Inlines.Add(new Run(text[litChars].ToString()) { Foreground = Frozen(new System.Windows.Media.SolidColorBrush(bc)) });
        }

        if (litChars + 1 < len)
        {
            Inlines.Add(new Run(text.Substring(litChars + 1)) { Foreground = Frozen(new System.Windows.Media.SolidColorBrush(bs)) });
        }
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
