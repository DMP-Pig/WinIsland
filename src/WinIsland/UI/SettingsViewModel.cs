using WinIsland.Services;

namespace WinIsland.UI;

public sealed record EnumOption<T>(T Value, string Display);

/// <summary>组件设置的一行：名称 + 空闲/播放两列勾选。</summary>
/// <summary>媒体程序配置行：是否启用 + 顺序。</summary>
public sealed class MediaAppRow : ObservableObject
{
    private readonly Action<MediaAppRow> _onChanged;
    public string Key { get; }
    public string Name { get; }
    private bool _enabled;
    public bool Enabled
    {
        get => _enabled;
        set { if (Set(ref _enabled, value)) _onChanged(this); }
    }
    public MediaAppRow(string key, string name, bool enabled, Action<MediaAppRow> onChanged)
    {
        Key = key; Name = name; _enabled = enabled; _onChanged = onChanged;
    }
}
/// <summary>顺序条里的一个组件（含歌曲信息）。</summary>
public sealed class OrderItem : ObservableObject
{
    private readonly string _nameKey;
    public string Key { get; }
    public string Name => Localization.Get(_nameKey);
    public OrderItem(string key, string nameKey) { Key = key; _nameKey = nameKey; }
    public void RefreshName() => OnPropertyChanged(nameof(Name));
}
public sealed class ComponentRow : ObservableObject
{
    private readonly string _nameKey;
    private readonly ComponentFlags _c;
    private readonly Func<ComponentFlags, bool> _idleGet;
    private readonly Action<ComponentFlags, bool> _idleSet;
    private readonly Func<ComponentFlags, bool> _playGet;
    private readonly Action<ComponentFlags, bool> _playSet;
    private readonly Action<ComponentRow> _onChanged;

    public string Key { get; }

    public ComponentRow(string key, string nameKey, ComponentFlags c,
        Func<ComponentFlags, bool> idleGet, Action<ComponentFlags, bool> idleSet,
        Func<ComponentFlags, bool> playGet, Action<ComponentFlags, bool> playSet,
        Action<ComponentRow>? onChanged = null)
    {
        Key = key; _nameKey = nameKey; _c = c;
        _idleGet = idleGet; _idleSet = idleSet;
        _playGet = playGet; _playSet = playSet;
        _onChanged = onChanged ?? (_ => { });
    }

    public string Name => Localization.Get(_nameKey);

    public bool Idle { get => _idleGet(_c); set { _idleSet(_c, value); OnPropertyChanged(); _onChanged(this); } }
    public bool Playing { get => _playGet(_c); set { _playSet(_c, value); OnPropertyChanged(); _onChanged(this); } }

    public void RefreshName() => OnPropertyChanged(nameof(Name));
}

/// <summary>View model for the settings window. Edits a working copy, saves on demand.</summary>
public sealed class SettingsViewModel : ObservableObject
{
    private readonly SettingsService _service;

    public SettingsViewModel(SettingsService service, MediaAppRegistry? registry = null)
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
        _orderItems = new List<OrderItem>();
        RebuildOrderItems();
        _mediaAppRows = BuildMediaApps(Working.MediaApps, registry);
        Localization.LanguageChanged += (_, _) => { foreach (var r in Components) r.RefreshName(); foreach (var o in OrderItems) o.RefreshName(); };

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
    public IReadOnlyList<OrderItem> OrderItems => _orderItems;
    public IReadOnlyList<MediaAppRow> MediaAppRows => _mediaAppRows;

    private List<MediaAppRow> _mediaAppRows = new();

    private List<ComponentRow> _components = new();
    private List<OrderItem> _orderItems = new();

    private List<ComponentRow> BuildComponents(ComponentFlags c) => new()
    {
        new("Time", "Comp_Time", c, x => x.TimeWhenIdle, (x, v) => x.TimeWhenIdle = v, x => x.TimeWhenPlaying, (x, v) => x.TimeWhenPlaying = v, _ => RebuildOrderItems()),
        new("Weather", "Comp_Weather", c, x => x.WeatherWhenIdle, (x, v) => x.WeatherWhenIdle = v, x => x.WeatherWhenPlaying, (x, v) => x.WeatherWhenPlaying = v, _ => RebuildOrderItems()),
        new("Date", "Comp_Date", c, x => x.DateWhenIdle, (x, v) => x.DateWhenIdle = v, x => x.DateWhenPlaying, (x, v) => x.DateWhenPlaying = v, _ => RebuildOrderItems()),
        new("Cpu", "Comp_Cpu", c, x => x.CpuWhenIdle, (x, v) => x.CpuWhenIdle = v, x => x.CpuWhenPlaying, (x, v) => x.CpuWhenPlaying = v, _ => RebuildOrderItems()),
        new("Ram", "Comp_Ram", c, x => x.RamWhenIdle, (x, v) => x.RamWhenIdle = v, x => x.RamWhenPlaying, (x, v) => x.RamWhenPlaying = v, _ => RebuildOrderItems()),
        new("Net", "Comp_Net", c, x => x.NetWhenIdle, (x, v) => x.NetWhenIdle = v, x => x.NetWhenPlaying, (x, v) => x.NetWhenPlaying = v, _ => RebuildOrderItems()),
        new("Battery", "Comp_Battery", c, x => x.BatteryWhenIdle, (x, v) => x.BatteryWhenIdle = v, x => x.BatteryWhenPlaying, (x, v) => x.BatteryWhenPlaying = v, _ => RebuildOrderItems()),
    };

    private static readonly (string Key, string NameKey)[] OrderDefs =
    {
        ("Time", "Comp_Time"),
        ("Weather", "Comp_Weather"),
        ("Date", "Comp_Date"),
        ("Cpu", "Comp_Cpu"),
        ("Ram", "Comp_Ram"),
        ("Net", "Comp_Net"),
        ("Battery", "Comp_Battery"),
        ("Song", "Comp_Song"),
    };

    /// <summary>
    /// 重建「拖动顺序」条：只保留已勾选（空闲或播放任一勾选）的组件；
    /// 没有勾选行的组件（如 Song 歌曲信息）始终显示。顺序沿用 WidgetOrder。
    /// </summary>
    private void RebuildOrderItems()
    {
        var keys = (Working.WidgetOrder ?? string.Empty)
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        foreach (var d in OrderDefs) if (!keys.Contains(d.Key)) keys.Add(d.Key);

        var used = new HashSet<string>();
        var result = new List<OrderItem>();
        foreach (var k in keys)
        {
            var d = Array.Find(OrderDefs, x => x.Key == k);
            if (d.Key is null || !used.Add(d.Key)) continue;
            var row = _components.FirstOrDefault(c => c.Key == k);
            // 无勾选行的组件（Song）始终显示；其余只有被勾选才显示
            if (row is null || row.Idle || row.Playing)
                result.Add(new OrderItem(d.Key, d.NameKey));
        }
        _orderItems = result;
        OnPropertyChanged(nameof(OrderItems));
    }

    private List<MediaAppRow> BuildMediaApps(List<MediaAppEntry>? saved, MediaAppRegistry? registry)
    {
        var result = new List<MediaAppRow>();
        void Add(string key, string name, bool enabled)
        {
            if (result.Any(r => r.Key == key)) return;
            result.Add(new MediaAppRow(key, name, enabled, _ => SyncMediaApps()));
        }

        // 先按已保存的顺序/启用
        if (saved is not null)
            foreach (var e in saved) Add(e.Key, e.Key, e.Enabled);
        // 再补上运行中发现的程序（默认启用）
        if (registry is not null)
            foreach (var (key, name) in registry.Known) Add(key, name, true);
        return result;
    }

    private void SyncMediaApps()
    {
        Working.MediaApps = _mediaAppRows
            .Select(r => new MediaAppEntry { Key = r.Key, Enabled = r.Enabled })
            .ToList();
    }

    /// <summary>调整媒体程序顺序（优先级）。</summary>
    public void MoveMediaApp(MediaAppRow row, int delta)
    {
        var i = _mediaAppRows.IndexOf(row);
        var j = i + delta;
        if (i < 0 || j < 0 || j >= _mediaAppRows.Count) return;
        (_mediaAppRows[i], _mediaAppRows[j]) = (_mediaAppRows[j], _mediaAppRows[i]);
        OnPropertyChanged(nameof(MediaAppRows));
        SyncMediaApps();
    }

    public void MoveOrderItemTo(OrderItem item, int newIndex)
    {
        var i = _orderItems.IndexOf(item);
        if (i < 0) return;
        newIndex = Math.Clamp(newIndex, 0, _orderItems.Count - 1);
        if (i == newIndex) return;
        _orderItems.RemoveAt(i);
        _orderItems.Insert(newIndex, item);
        OnPropertyChanged(nameof(OrderItems));

        // 把新顺序写回 WidgetOrder：已显示项按新顺序在前，未勾选项按原顺序补在后面，
        // 这样取消勾选再勾选后仍能记住相对位置。
        var shown = _orderItems.Select(x => x.Key).ToList();
        var hidden = _components.Where(r => !r.Idle && !r.Playing).Select(r => r.Key)
            .Concat(OrderDefs.Select(d => d.Key)).Distinct()
            .Where(k => !shown.Contains(k)).ToList();
        Working.WidgetOrder = string.Join(",", shown.Concat(hidden));
    }

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
