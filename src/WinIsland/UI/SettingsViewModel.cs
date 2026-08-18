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

    public string Key { get; }

    public ComponentRow(string key, string nameKey, ComponentFlags c,
        Func<ComponentFlags, bool> idleGet, Action<ComponentFlags, bool> idleSet,
        Func<ComponentFlags, bool> playGet, Action<ComponentFlags, bool> playSet)
    {
        Key = key; _nameKey = nameKey; _c = c;
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
        _components = BuildComponents(Working.Components);
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
    public IReadOnlyList<ComponentRow> Components => _components;

    private List<ComponentRow> _components = new();

    private List<ComponentRow> BuildComponents(ComponentFlags c) => new()
    {
        new("Time", "Comp_Time", c, x => x.TimeWhenIdle, (x, v) => x.TimeWhenIdle = v, x => x.TimeWhenPlaying, (x, v) => x.TimeWhenPlaying = v),
        new("Weather", "Comp_Weather", c, x => x.WeatherWhenIdle, (x, v) => x.WeatherWhenIdle = v, x => x.WeatherWhenPlaying, (x, v) => x.WeatherWhenPlaying = v),
    };

    /// <summary>把组件移动到指定索引（拖拽排序用）。</summary>
    public void MoveComponentTo(ComponentRow row, int newIndex)
    {
        var i = _components.IndexOf(row);
        if (i < 0) return;
        newIndex = Math.Clamp(newIndex, 0, _components.Count - 1);
        if (i == newIndex) return;
        _components.RemoveAt(i);
        _components.Insert(newIndex, row);
        OnPropertyChanged(nameof(Components));
        Working.WidgetOrder = string.Join(",", _components.Select(x => x.Key));
    }

    public void Save()
    {
        _service.Apply(Working);
        Localization.CurrentLanguage = Working.Language;
    }
}
