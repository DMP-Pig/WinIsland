using WinIsland.Services;

namespace WinIsland.UI;

/// <summary>一行歌词视图模型：主句（逐字卡拉OK高亮）+ 可选的翻译行。</summary>
public sealed class LyricLineViewModel : ObservableObject
{
    private readonly LyricLine _line;
    private bool _isCurrent;
    private double _highlightFraction;

    public LyricLineViewModel(LyricLine line, string? translation = null)
    {
        _line = line;
        Translation = string.IsNullOrWhiteSpace(translation) ? null : translation;
    }

    public string Text => _line.Text;
    public TimeSpan Time => _line.Time;

    /// <summary>翻译行文本（可能为 null）。</summary>
    public string? Translation { get; }

    public bool HasTranslation => Translation is not null;

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
