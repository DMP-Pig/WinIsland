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
/// 不再按整数个字符硬切 Run，而是用「线性渐变扫过」的方式渲染高亮：
/// 高亮边界在 60fps 下以缓动逼近目标进度，像 iOS 卡拉OK一样平滑“流”过歌词，
/// 避免一个字一个字“跳”的卡顿感。
/// </summary>
public class KaraokeTextBlock : TextBlock
{
    public static readonly DependencyProperty KaraokeTextProperty =
        DependencyProperty.Register(nameof(KaraokeText), typeof(string), typeof(KaraokeTextBlock),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure, OnRenderPropsChanged));

    /// <summary>目标高亮字符数（由 ViewModel 按播放进度驱动）。</summary>
    public static readonly DependencyProperty HighlightCountProperty =
        DependencyProperty.Register(nameof(HighlightCount), typeof(int), typeof(KaraokeTextBlock),
            new FrameworkPropertyMetadata(0, OnRenderPropsChanged));

    public static readonly DependencyProperty HighlightBrushProperty =
        DependencyProperty.Register(nameof(HighlightBrush), typeof(Brush), typeof(KaraokeTextBlock),
            new FrameworkPropertyMetadata(Brushes.White, OnRenderPropsChanged));

    public static readonly DependencyProperty BaseBrushProperty =
        DependencyProperty.Register(nameof(BaseBrush), typeof(Brush), typeof(KaraokeTextBlock),
            new FrameworkPropertyMetadata(Brushes.Gray, OnRenderPropsChanged));

    private readonly DispatcherTimer _animTimer;
    private double _currentFraction;   // 当前已点亮比例（0..1，平滑推进）
    private double _targetFraction;    // 目标比例 = HighlightCount / Text.Length
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

    public int HighlightCount
    {
        get => (int)GetValue(HighlightCountProperty);
        set => SetValue(HighlightCountProperty, value);
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
        var len = text.Length;
        _targetFraction = len == 0 ? 0 : Math.Clamp(HighlightCount, 0, len) / (double)len;

        // 换行时直接对齐，避免上一行残留的高亮“滑”到新行开头。
        if (!string.Equals(text, _lastText, StringComparison.Ordinal))
        {
            _lastText = text;
            _currentFraction = _targetFraction;
            _animTimer.Stop();
            Render();
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

        var f = _currentFraction;
        var hl = ToColor(HighlightBrush) ?? System.Windows.Media.Colors.White;
        var bs = ToColor(BaseBrush) ?? System.Windows.Media.Colors.Gray;

        Brush fg;
        if (f <= 0.001)
        {
            fg = new System.Windows.Media.SolidColorBrush(bs);
        }
        else if (f >= 0.999)
        {
            fg = new System.Windows.Media.SolidColorBrush(hl);
        }
        else
        {
            // 高亮→基础色渐变扫过：f 之前是高亮，f~f+edge 之间柔和过渡，之后为基础色。
            var edge = 0.10; // 过渡带宽度（占文本比例，约 1~2 个字）
            var g = new System.Windows.Media.LinearGradientBrush
            {
                StartPoint = new System.Windows.Point(0, 0.5),
                EndPoint = new System.Windows.Point(1, 0.5),
            };
            g.GradientStops.Add(new System.Windows.Media.GradientStop(hl, 0));
            g.GradientStops.Add(new System.Windows.Media.GradientStop(hl, f));
            g.GradientStops.Add(new System.Windows.Media.GradientStop(bs, Math.Min(1, f + edge)));
            g.GradientStops.Add(new System.Windows.Media.GradientStop(bs, 1));
            fg = g;
        }

        fg.Freeze();
        Inlines.Add(new Run(text) { Foreground = fg });
    }

    private static System.Windows.Media.Color? ToColor(Brush? brush)
        => (brush as SolidColorBrush)?.Color;
}
