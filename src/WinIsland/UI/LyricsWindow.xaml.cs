using System.Windows;
using System.Windows.Input;

namespace WinIsland.UI;

/// <summary>Optional standalone always-on-top lyrics window.</summary>
public partial class LyricsWindow : Window
{
    private readonly IslandViewModel _vm;

    public LyricsWindow(IslandViewModel vm)
    {
        _vm = vm;
        DataContext = vm;
        InitializeComponent();
        // Draggable.
        MouseLeftButtonDown += (_, e) =>
        {
            if (e.ButtonState == MouseButtonState.Pressed) DragMove();
        };
    }

    /// <summary>Position near the bottom-center of the primary screen.</summary>
    public void PositionNearBottom()
    {
        var screen = System.Windows.Forms.Screen.PrimaryScreen!;
        var work = screen.WorkingArea;
        var scale = ScreenHelper.GetDpiScale(screen);
        Left = (work.X + work.Width / 2) / scale - ActualWidth / 2;
        Top = (work.Y + work.Height - (int)(64 * scale)) / scale - ActualHeight;
    }
}
