using WinIsland.Services;

namespace WinIsland.UI;

/// <summary>One lyrics line with an IsCurrent flag for highlight styling.</summary>
public sealed class LyricLineViewModel : ObservableObject
{
    private readonly LyricLine _line;
    private bool _isCurrent;

    public LyricLineViewModel(LyricLine line) => _line = line;

    public string Text => _line.Text;
    public TimeSpan Time => _line.Time;

    public bool IsCurrent
    {
        get => _isCurrent;
        set => Set(ref _isCurrent, value);
    }
}
