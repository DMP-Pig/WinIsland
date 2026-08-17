using WinIsland.Services;

namespace WinIsland.UI;

public sealed record EnumOption<T>(T Value, string Display);

/// <summary>View model for the settings window. Edits a working copy, saves on demand.</summary>
public sealed class SettingsViewModel : ObservableObject
{
    private readonly SettingsService _service;

    public SettingsViewModel(SettingsService service)
    {
        _service = service;
        Working = service.Current.Clone();
        Language = Working.Language;

        ThemeOptions = new[]
        {
            new EnumOption<ThemeMode>(ThemeMode.Auto, Localization.Get("Appearance_ThemeAuto")),
            new EnumOption<ThemeMode>(ThemeMode.Light, Localization.Get("Appearance_ThemeLight")),
            new EnumOption<ThemeMode>(ThemeMode.Dark, Localization.Get("Appearance_ThemeDark")),
        };
        PositionOptions = new[]
        {
            new EnumOption<IslandPosition>(IslandPosition.Center, Localization.Get("Appearance_PositionCenter")),
            new EnumOption<IslandPosition>(IslandPosition.Right, Localization.Get("Appearance_PositionRight")),
        };
        MonitorOptions = new[]
        {
            new EnumOption<MonitorSelection>(MonitorSelection.Primary, Localization.Get("Appearance_MonitorPrimary")),
            new EnumOption<MonitorSelection>(MonitorSelection.All, Localization.Get("Appearance_MonitorAll")),
            new EnumOption<MonitorSelection>(MonitorSelection.Index, Localization.Get("Appearance_MonitorIndex")),
        };
        PresetColors = new[]
        {
            "#6C5CE7", "#5B8DEF", "#00B894", "#E17055", "#E84393", "#FDCB6E",
            "#00CEC9", "#A29BFE", "#FD79A8", "#55EFC4", "#74B9FF", "#DFE6E9",
        };
    }

    public AppSettings Working { get; }

    public string Language
    {
        get => Working.Language;
        set
        {
            Working.Language = value;
            OnPropertyChanged();
        }
    }

    public IReadOnlyList<EnumOption<ThemeMode>> ThemeOptions { get; }
    public IReadOnlyList<EnumOption<IslandPosition>> PositionOptions { get; }
    public IReadOnlyList<EnumOption<MonitorSelection>> MonitorOptions { get; }
    public IReadOnlyList<string> PresetColors { get; }

    public void Save()
    {
        _service.Apply(Working);
        Localization.CurrentLanguage = Working.Language;
    }
}
