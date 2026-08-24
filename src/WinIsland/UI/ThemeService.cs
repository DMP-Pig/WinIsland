using System.Windows;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using Brushes = System.Windows.Media.Brushes;
using WinIsland.Services;

namespace WinIsland.UI;

/// <summary>
/// Computes the brush palette for the island from settings (theme mode, accent color,
/// opacity) and re-applies it when settings or the OS theme change.
/// </summary>
public sealed class ThemeService
{
    private AppSettings _settings = new();

    public bool IsDark { get; private set; } = true;

    public Brush CardBackground { get; private set; } = Brushes.Transparent;
    public Brush CardBorder { get; private set; } = Brushes.Transparent;
    public Brush TextPrimary { get; private set; } = Brushes.White;
    public Brush TextSecondary { get; private set; } = Brushes.Gray;
    public Brush AccentBrush { get; private set; } = Brushes.Transparent;
    public Brush AccentBorderBrush { get; private set; } = Brushes.Transparent;
    public Brush ButtonHoverBrush { get; private set; } = Brushes.Transparent;
    public Brush SliderTrackBrush { get; private set; } = Brushes.Gray;
    public Brush SliderThumbBrush { get; private set; } = Brushes.White;
    public Color AccentColor { get; private set; } = Colors.Indigo;
    public Color TintColor { get; private set; } = Color.FromRgb(0x1E, 0x1E, 0x2E);
    public double Opacity { get; private set; } = 0.92;

    public event EventHandler? ThemeChanged;

    public void Apply(AppSettings settings)
    {
        _settings = settings;
        var dark = settings.Theme switch
        {
            ThemeMode.Light => false,
            ThemeMode.Dark => true,
            _ => ThemeHelper.IsSystemDark(),
        };
        IsDark = dark;
        Opacity = Math.Clamp(settings.Opacity, 0.3, 1.0);
        AccentColor = ThemeHelper.ParseColor(settings.AccentColor, Color.FromRgb(0x6C, 0x5C, 0xE7));

        // 主题预设：覆盖强调色（Default 使用用户自定义强调色）
        var preset = settings.ThemePreset;
        if (!string.IsNullOrEmpty(preset) && preset != "Default")
        {
            AccentColor = preset switch
            {
                "Ocean" => Color.FromRgb(0x5B, 0x8D, 0xEF),
                "Forest" => Color.FromRgb(0x00, 0xB8, 0x94),
                "Sunset" => Color.FromRgb(0xE1, 0x70, 0x55),
                "Neon" => Color.FromRgb(0xE8, 0x43, 0x93),
                "Mono" => Color.FromRgb(0x8A, 0x8A, 0x93),
                "Grape" => Color.FromRgb(0xA2, 0x9B, 0xFE),
                _ => AccentColor,
            };
        }

        if (dark)
        {
            TintColor = Color.FromRgb(0x16, 0x16, 0x22);
            TextPrimary = new SolidColorBrush(Color.FromArgb(235, 245, 245, 250));
            TextSecondary = new SolidColorBrush(Color.FromArgb(170, 245, 245, 250));
            CardBorder = new SolidColorBrush(Color.FromArgb(60, 255, 255, 255));
            ButtonHoverBrush = new SolidColorBrush(Color.FromArgb(36, 255, 255, 255));
            SliderTrackBrush = new SolidColorBrush(Color.FromArgb(120, 255, 255, 255));
            SliderThumbBrush = new SolidColorBrush(Color.FromArgb(235, 255, 255, 255));
        }
        else
        {
            TintColor = Color.FromRgb(0xF2, 0xF2, 0xF6);
            TextPrimary = new SolidColorBrush(Color.FromArgb(230, 25, 25, 32));
            TextSecondary = new SolidColorBrush(Color.FromArgb(150, 25, 25, 32));
            CardBorder = new SolidColorBrush(Color.FromArgb(70, 255, 255, 255));
            ButtonHoverBrush = new SolidColorBrush(Color.FromArgb(30, 0, 0, 0));
            SliderTrackBrush = new SolidColorBrush(Color.FromArgb(110, 0, 0, 0));
            SliderThumbBrush = new SolidColorBrush(Color.FromArgb(230, 25, 25, 32));
        }

        var accent = Color.FromArgb((byte)(dark ? 255 : 235), AccentColor.R, AccentColor.G, AccentColor.B);
        AccentBrush = new SolidColorBrush(accent);
        AccentBorderBrush = new SolidColorBrush(Color.FromArgb(160, AccentColor.R, AccentColor.G, AccentColor.B));

        // iOS 风格实心深色胶囊（不透明，避免透出桌面内容，视觉更干净）
        CardBackground = new SolidColorBrush(Color.FromArgb(255, TintColor.R, TintColor.G, TintColor.B));

        FreezeAll();
        ThemeChanged?.Invoke(this, EventArgs.Empty);
    }

    private void FreezeAll()
    {
        foreach (var b in new[] { CardBackground, CardBorder, TextPrimary, TextSecondary, AccentBrush, AccentBorderBrush, ButtonHoverBrush, SliderTrackBrush, SliderThumbBrush })
            b.Freeze();
    }
}

