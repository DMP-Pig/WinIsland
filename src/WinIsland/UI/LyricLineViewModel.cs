using WinIsland.Services;

namespace WinIsland.UI;

/// <summary>One lyrics line with an IsCurrent flag for highlight styling.</summary>
public sealed class LyricLineViewModel : ObservableObject
{
    private readonly LyricLine _line;
    private bool _isCurrent;
    private double _highlightFraction;

    public LyricLineViewModel(LyricLine line) => _line = line;

    public string Text => _line.Text;
    public TimeSpan Time => _line.Time;

    public bool IsCurrent
    {
        get => _isCurrent;
        set => Set(ref _isCurrent, value);
    }

    /// <summary>逐字卡拉OK：已点亮比例（0..1，连续值，用于平滑过渡动画）。</summary>
    public double HighlightFraction
    {
        get => _highlightFraction;
        set => Set(ref _highlightFraction, value);
    }
}
