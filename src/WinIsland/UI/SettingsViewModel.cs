using WinIsland.Services;

namespace WinIsland.UI;

public sealed record EnumOption<T>(T Value, string Display);

/// <summary>组件设置的一行：名称 + 空闲/播放两列勾选。</summary>
public sealed class ComponentRow : ObservableObject
{
    private readonly string _nameKey;
    private readonly ComponentFlags _c;
    private readonly Func<ComponentFlags, bool> _idleGet;
    private readonly Action<ComponentFlags, bool> _idleSet;
    private readonly Func<ComponentFlags, bool> _playGet;
    private readonly Action<ComponentFlags, bool> _playSet;

    public ComponentRow(string nameKey, ComponentFlags c,
        Func<ComponentFlags, bool> idleGet, Action<ComponentFlags, bool> idleSet,
        Func<ComponentFlags, bool> playGet, Action<ComponentFlags, bool> playSet)
    {
        _nameKey = nameKey; _c = c;
        _idleGet = idleGet; _idleSet = idleSet;
        _playGet = playGet; _playSet = playSet;
    }

    public string Name => Localization.Get(_nameKey);

    public bool Idle { get => _idleGet(_c); set { _idleSet(_c, value); OnPropertyChanged(); } }
    public bool Playing { get => _playGet(_c); set { _playSet(_c, value); OnPropertyChanged(); } }

    public void RefreshName() => OnPropertyChanged(nameof(Name));
}

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
        Components = BuildComponents(Working.Components);
        Localization.LanguageChanged += (_, _) => { foreach (var r in Components) r.RefreshName(); };

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
    public IReadOnlyList<ComponentRow> Components { get; }

    private static IReadOnlyList<ComponentRow> BuildComponents(ComponentFlags c) => new ComponentRow[]
    {
        new("Comp_Time", c, x => x.TimeWhenIdle, (x, v) => x.TimeWhenIdle = v, x => x.TimeWhenPlaying, (x, v) => x.TimeWhenPlaying = v),
        new("Comp_Weather", c, x => x.WeatherWhenIdle, (x, v) => x.WeatherWhenIdle = v, x => x.WeatherWhenPlaying, (x, v) => x.WeatherWhenPlaying = v),
        new("Comp_Cover", c, x => x.CoverWhenIdle, (x, v) => x.CoverWhenIdle = v, x => x.CoverWhenPlaying, (x, v) => x.CoverWhenPlaying = v),
        new("Comp_Title", c, x => x.TitleWhenIdle, (x, v) => x.TitleWhenIdle = v, x => x.TitleWhenPlaying, (x, v) => x.TitleWhenPlaying = v),
        new("Comp_Artist", c, x => x.ArtistWhenIdle, (x, v) => x.ArtistWhenIdle = v, x => x.ArtistWhenPlaying, (x, v) => x.ArtistWhenPlaying = v),
        new("Comp_Lyrics", c, x => x.LyricsWhenIdle, (x, v) => x.LyricsWhenIdle = v, x => x.LyricsWhenPlaying, (x, v) => x.LyricsWhenPlaying = v),
        new("Comp_Progress", c, x => x.ProgressWhenIdle, (x, v) => x.ProgressWhenIdle = v, x => x.ProgressWhenPlaying, (x, v) => x.ProgressWhenPlaying = v),
    };

    public void Save()
    {
        _service.Apply(Working);
        Localization.CurrentLanguage = Working.Language;
    }
}
