using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;

namespace WinIsland.UI;

/// <summary>
/// 逐字卡拉OK歌词控件：把歌词按字符拆分，前 N 个字符用高亮色、其余用基础色，
/// N 随播放进度推进（<see cref="HighlightCount"/>），实现「字一个个点亮」的卡拉OK效果。
/// </summary>
public class KaraokeTextBlock : TextBlock
{
    public static readonly DependencyProperty KaraokeTextProperty =
        DependencyProperty.Register(nameof(KaraokeText), typeof(string), typeof(KaraokeTextBlock),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure, OnRenderPropsChanged));

    public static readonly DependencyProperty HighlightCountProperty =
        DependencyProperty.Register(nameof(HighlightCount), typeof(int), typeof(KaraokeTextBlock),
            new FrameworkPropertyMetadata(0, OnRenderPropsChanged));

    public static readonly DependencyProperty HighlightBrushProperty =
        DependencyProperty.Register(nameof(HighlightBrush), typeof(Brush), typeof(KaraokeTextBlock),
            new FrameworkPropertyMetadata(Brushes.White, OnRenderPropsChanged));

    public static readonly DependencyProperty BaseBrushProperty =
        DependencyProperty.Register(nameof(BaseBrush), typeof(Brush), typeof(KaraokeTextBlock),
            new FrameworkPropertyMetadata(Brushes.Gray, OnRenderPropsChanged));

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
        => ((KaraokeTextBlock)d).UpdateRuns();

    private void UpdateRuns()
    {
        Inlines.Clear();
        var text = KaraokeText ?? string.Empty;
        var n = Math.Clamp(HighlightCount, 0, text.Length);
        var hl = HighlightBrush ?? Brushes.White;
        var bs = BaseBrush ?? Brushes.Gray;
        if (n > 0) Inlines.Add(new Run(text.Substring(0, n)) { Foreground = hl });
        if (n < text.Length) Inlines.Add(new Run(text.Substring(n)) { Foreground = bs });
    }
}
