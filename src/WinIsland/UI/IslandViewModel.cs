using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WinIsland.Services;

namespace WinIsland.UI;

/// <summary>
/// View model for the island window: maps coordinator snapshots to bindable state,
/// interpolates playback position for a smooth progress bar, and manages lyrics.
/// </summary>
public sealed class IslandViewModel : ObservableObject, IDisposable
{
    private readonly MediaCoordinator _coordinator;
    private readonly SettingsService _settings;
    private readonly LyricsService _lyricsService;
    private readonly DispatcherTimer _progressTimer;
    private readonly DispatcherTimer _widgetTimer;
    private readonly WeatherService _weather = new();
    private int _weatherTick;
    private DateTime _trackStartTime = DateTime.UtcNow;
    private bool _useFreeClock;   // 播放器不报 SMTC 进度（如 Cider）时用本地时钟推进卡拉OK

    private MediaSnapshot? _snapshot;
    private LyricsResult _lyrics = LyricsResult.Empty;
    private DateTime _lastPositionTime;
    private double _interpolatedPosition;
    private int _lyricIndex = -1;
    private DateTime? _positionStaleSinceUtc; // 上报位置明显回退的起始时刻（防瞬间 0/过期位置打回开头）
    private bool _expanded;
    private bool _visible;
    private bool _userHidden;
    private bool _suppressVolume;
    private int _suppressSeek;
    private string _lyricsKey = string.Empty;
    private string? _restoredTrackKey;       // 上次退出时保存的曲目（用于启动恢复位置）
    private double _restoredPosition;        // 上次退出时保存的位置（秒）
    private bool _karaokeFrozen;             // 暂停时高亮是否已冻结（避免位置校正导致跳动）
    private bool _restoredMode;                // 启动恢复后信任恢复位置：暂不采纳回退/过期位置
    private bool _toggleInFlight;               // 播放/暂停命令在途（防连点重复触发）
    private bool _statusOverrideActive;         // 乐观状态保护期：期间不被快照打回
    private PlaybackStatus _optimisticStatus;   // 乐观目标状态（等待快照确认）
    private DateTime _statusOverrideUntilUtc;   // 保护期截止时间
    private bool _pauseLock;                       // 暂停锁定：期间不随快照恢复播放、不推进歌词
    private PlaybackStatus _restoredStatus = PlaybackStatus.Closed; // 上次退出的状态（用于启动恢复）

    // ── 上岛推送（第三方软件推送到灵动岛）──
    private IslandPush? _activePush;


    public IslandViewModel(MediaCoordinator coordinator, SettingsService settings, LyricsService lyricsService)
    {
        _coordinator = coordinator;
        _settings = settings;
        _lyricsService = lyricsService;

        // 启动时恢复上次退出的播放位置（暂停后重启不跳回开头）
        var restored = PlaybackStateStore.Load();
        if (restored is not null)
        {
            _restoredTrackKey = restored.TrackKey;
            _restoredPosition = restored.PositionSeconds;
            _restoredStatus = string.Equals(restored.Status, "Paused", StringComparison.OrdinalIgnoreCase)
                ? PlaybackStatus.Paused : PlaybackStatus.Playing;
        }

        // 系统状态计数器（CPU/内存）—— 复用实例，避免每次采样都新建
        _cpuCounter = CreateCounter("Processor", "% Processor Time", "_Total");
        _ramCounter = CreateCounter("Memory", "% Committed Bytes In Use", null);

        PlayPauseCommand = new AsyncRelayCommand(_ => TogglePlayPauseLocalAsync());
        NextCommand = new AsyncRelayCommand(_ => _coordinator.NextAsync());
        PreviousCommand = new AsyncRelayCommand(_ => _coordinator.PreviousAsync());
        OpenSettingsCommand = new RelayCommand(_ => OpenSettingsRequested?.Invoke(this, EventArgs.Empty));
        ToggleLyricsWindowCommand = new RelayCommand(_ => ToggleLyricsWindowRequested?.Invoke(this, EventArgs.Empty));

        _coordinator.SnapshotChanged += OnSnapshotChanged;
        _coordinator.MediaEnded += OnMediaEnded;
        Localization.LanguageChanged += (_, _) => RaiseAllText();

        _progressTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
        _progressTimer.Tick += (_, _) => AdvanceProgress();
        _progressTimer.Start();

        _widgetTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _widgetTimer.Tick += async (_, _) =>
        {
            if (!IsVisible) return;
            ClockText = DateTime.Now.ToString("HH:mm");
            DateText = DateTime.Now.ToString("M月d日 ddd", System.Globalization.CultureInfo.GetCultureInfo("zh-CN"));
            CheckPushExpiry();
            UpdateSystemStats();
            if (ShowIdleWeather && ++_weatherTick % 60 == 1)
            {
                var w = await _weather.GetWeatherAsync(_settings.Current.WeatherCity);
                if (w is not null && WeatherText != w) WeatherText = w; // 失败保留旧值，避免天气忽有忽无
            }
        };
        _widgetTimer.Start();
    }

    public event EventHandler? OpenSettingsRequested;
    public event EventHandler? ToggleLyricsWindowRequested;

    // ── Commands ───────────────────────────────────────────────
    public AsyncRelayCommand PlayPauseCommand { get; }
    public AsyncRelayCommand NextCommand { get; }
    public AsyncRelayCommand PreviousCommand { get; }
    public RelayCommand OpenSettingsCommand { get; }
    public RelayCommand ToggleLyricsWindowCommand { get; }

    // ── Track ──────────────────────────────────────────────────
    private string _title = string.Empty;
    public string Title { get => _title; private set => Set(ref _title, value); }

    private string _artist = string.Empty;
    public string Artist { get => _artist; private set => Set(ref _artist, value); }

    private string _album = string.Empty;
    public string Album { get => _album; private set => Set(ref _album, value); }

    private string _sourceLabel = string.Empty;
    public string SourceLabel { get => _sourceLabel; private set => Set(ref _sourceLabel, value); }

    private string _sourceDetail = string.Empty;
    public string SourceDetail { get => _sourceDetail; private set => Set(ref _sourceDetail, value); }

    private ImageSource? _artwork;
    public ImageSource? Artwork { get => _artwork; private set => Set(ref _artwork, value); }

    // ── Playback ───────────────────────────────────────────────
    private PlaybackStatus _status;
    public PlaybackStatus Status { get => _status; private set => Set(ref _status, value); }

    public bool IsPlaying => Status == PlaybackStatus.Playing;
    public bool IsPaused => Status == PlaybackStatus.Paused;

    private double _durationSeconds;
    public double DurationSeconds { get => _durationSeconds; private set => Set(ref _durationSeconds, value); }

    private TimeSpan _position;
    public TimeSpan Position
    {
        get => _position;
        private set
        {
            if (!Set(ref _position, value)) return;
            OnPropertyChanged(nameof(PositionText));
        }
    }

    /// <summary>当前位置文本（精确到秒，如 1:23 / 1:02:03），与 DurationText 同格式。</summary>
    public string PositionText
    {
        get
        {
            var total = (int)Math.Max(0, _position.TotalSeconds);
            return total >= 3600
                ? TimeSpan.FromSeconds(total).ToString(@"h\:mm\:ss")
                : TimeSpan.FromSeconds(total).ToString(@"m\:ss");
        }
    }

    public string DurationText =>
        DurationSeconds >= 3600
            ? TimeSpan.FromSeconds(DurationSeconds).ToString(@"h\:mm\:ss")
            : TimeSpan.FromSeconds(DurationSeconds).ToString(@"m\:ss");

    private double _progress;
    public double Progress { get => _progress; private set => Set(ref _progress, value); }

    private bool _canPlayPause = true;
    public bool CanPlayPause { get => _canPlayPause; private set => Set(ref _canPlayPause, value); }

    private bool _canNext = true;
    public bool CanNext { get => _canNext; private set => Set(ref _canNext, value); }

    private bool _canPrevious = true;
    public bool CanPrevious { get => _canPrevious; private set => Set(ref _canPrevious, value); }

    private bool _canSeek;
    public bool CanSeek { get => _canSeek; private set => Set(ref _canSeek, value); }

    private bool _hasVolumeControl;
    public bool HasVolumeControl { get => _hasVolumeControl; private set => Set(ref _hasVolumeControl, value); }

    private double _volume;
    public double Volume
    {
        get => _volume;
        set
        {
            if (!Set(ref _volume, Math.Clamp(value, 0, 1))) return;
            if (_suppressVolume) return;
            _ = _coordinator.SetVolumeAsync(_volume);
        }
    }

    public string PlayPauseGlyph => IsPlaying ? "\uE769" : "\uE768"; // Pause / Play (Segoe MDL2)

    // ── Lyrics ─────────────────────────────────────────────────
    private IReadOnlyList<LyricLineViewModel> _lyricLines = Array.Empty<LyricLineViewModel>();
    public IReadOnlyList<LyricLineViewModel> LyricLines { get => _lyricLines; private set => Set(ref _lyricLines, value); }

    public bool HasLyrics => LyricLines.Count > 0;

    public int LyricIndex
    {
        get => _lyricIndex;
        private set
        {
            if (_lyricIndex == value) return;
            var old = _lyricIndex;
            _lyricIndex = value;
            if (old >= 0 && old < LyricLines.Count) LyricLines[old].IsCurrent = false;
            if (value >= 0 && value < LyricLines.Count) LyricLines[value].IsCurrent = true;
            // 换句时解除暂停冻结，让 UpdateKaraokeHighlight 按当前（正确）位置重算一次
            _karaokeFrozen = false;
            CurrentLyricText = LyricLines.Count > 0
                ? LyricLines[Math.Clamp(value, 0, LyricLines.Count - 1)].Text
                : string.Empty;
            OnPropertyChanged();
        }
    }

    private string _lyricsStatus = string.Empty;
    public string LyricsStatus { get => _lyricsStatus; private set => Set(ref _lyricsStatus, value); }

    /// <summary>当前歌词行文本（紧凑胶囊内未悬停时也显示）。</summary>
    private string _currentLyricText = string.Empty;
    public string CurrentLyricText { get => _currentLyricText; private set => Set(ref _currentLyricText, value); }

    /// <summary>紧凑态逐字卡拉OK已点亮字符数。</summary>
    private double _compactHighlightFraction;
    public double CompactHighlightFraction { get => _compactHighlightFraction; private set => Set(ref _compactHighlightFraction, value); }

    // ── Idle widgets（无媒体时组件）───────────────────────────
    private bool _hasMedia;
    public bool HasMedia { get => _hasMedia; private set => Set(ref _hasMedia, value); }

    private bool _showIdleWidgets;
    public bool ShowIdleWidgets { get => _showIdleWidgets; private set => Set(ref _showIdleWidgets, value); }

    private string _clockText = string.Empty;
    public string ClockText { get => _clockText; private set => Set(ref _clockText, value); }

    private string _weatherText = string.Empty;
    public string WeatherText { get => _weatherText; private set => Set(ref _weatherText, value); }

    /// <summary>紧凑胶囊里的一个顺序组件。</summary>
    public sealed record IslandComponent(string Kind); // "Time" | "Weather" | "Song"
    // 歌曲相关组件（封面/歌名/歌手/歌词/进度条）：只在播放时显示，固定开启
    public bool ShowCover => HasMedia;
    public bool ShowTitle => HasMedia;
    public bool ShowArtist => HasMedia;
    public bool ShowLyrics => HasMedia;
    public bool ShowCompactProgress => HasMedia;

    // 时间/天气：空闲与播放分别按勾选显示
    public bool ShowIdleTime => HasMedia ? _settings.Current.Components.TimeWhenPlaying : _settings.Current.Components.TimeWhenIdle;
    public bool ShowIdleWeather => HasMedia ? _settings.Current.Components.WeatherWhenPlaying : _settings.Current.Components.WeatherWhenIdle;
    public bool ShowIdleDate => HasMedia ? _settings.Current.Components.DateWhenPlaying : _settings.Current.Components.DateWhenIdle;
    public bool ShowIdleCpu => HasMedia ? _settings.Current.Components.CpuWhenPlaying : _settings.Current.Components.CpuWhenIdle;
    public bool ShowIdleRam => HasMedia ? _settings.Current.Components.RamWhenPlaying : _settings.Current.Components.RamWhenIdle;
    public bool ShowIdleNet => HasMedia ? _settings.Current.Components.NetWhenPlaying : _settings.Current.Components.NetWhenIdle;
    public bool ShowIdleBattery => HasMedia ? _settings.Current.Components.BatteryWhenPlaying : _settings.Current.Components.BatteryWhenIdle;
    public bool ShowAnyWidget => ShowIdleTime || ShowIdleWeather || ShowIdleDate
        || ShowIdleCpu || ShowIdleRam || ShowIdleNet || ShowIdleBattery;

    private IReadOnlyList<IslandComponent> _compactItems = Array.Empty<IslandComponent>();
    public IReadOnlyList<IslandComponent> CompactItems { get => _compactItems; private set => Set(ref _compactItems, value); }

    /// <summary>按 WidgetOrder 重建紧凑胶囊组件顺序（播放时含歌曲信息，空闲时去掉）。</summary>
    private void RebuildCompactItems()
    {
        // 读取顺序，并补齐缺失的已知组件（兼容旧配置里只有 Time,Weather 的情况）
        var keys = (_settings.Current.WidgetOrder ?? "Time,Weather,Song")
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        foreach (var known in new[] { "Time", "Weather", "Date", "Cpu", "Ram", "Net", "Battery", "Song" })
            if (!keys.Contains(known)) keys.Add(known);

        var items = new List<IslandComponent>();
        foreach (var key in keys)
        {
            if (key == "Time" && ShowIdleTime) items.Add(new IslandComponent("Time"));
            else if (key == "Weather" && ShowIdleWeather) items.Add(new IslandComponent("Weather"));
            else if (key == "Date" && ShowIdleDate) items.Add(new IslandComponent("Date"));
            else if (key == "Cpu" && ShowIdleCpu) items.Add(new IslandComponent("Cpu"));
            else if (key == "Ram" && ShowIdleRam) items.Add(new IslandComponent("Ram"));
            else if (key == "Net" && ShowIdleNet) items.Add(new IslandComponent("Net"));
            else if (key == "Battery" && ShowIdleBattery) items.Add(new IslandComponent("Battery"));
            else if (key == "Song" && HasMedia && _settings.Current.ShowMediaInfo) items.Add(new IslandComponent("Song"));
        }

        // 内容未变化时不重建，避免每个快照（每秒）都重创建组件导致闪烁
        if (_compactItems.Count == items.Count
            && _compactItems.Select(i => i.Kind).SequenceEqual(items.Select(i => i.Kind)))
            return;
        CompactItems = items;
    }

    private int _statsTick;
    private float? _cpuValue;
    private long _lastNetBytes;
    private DateTime _lastNetTime = DateTime.UtcNow;
    // 性能计数器复用实例：新建实例的第一次 NextValue() 会返回 0，导致 CPU 显示错乱
    private readonly System.Diagnostics.PerformanceCounter? _cpuCounter;
    private readonly System.Diagnostics.PerformanceCounter? _ramCounter;

    private System.Diagnostics.PerformanceCounter? CreateCounter(string cat, string name, string? inst)
    {
        try { return inst is null ? new System.Diagnostics.PerformanceCounter(cat, name) : new System.Diagnostics.PerformanceCounter(cat, name, inst); }
        catch { return null; }
    }

    /// <summary>更新 CPU/内存/网络/电池/日期 等系统状态文本（每秒一次，UI 线程）。</summary>
    private void UpdateSystemStats()
    {
        try
        {
            var ps = System.Windows.Forms.SystemInformation.PowerStatus;
            var hasBattery = ps.BatteryChargeStatus != System.Windows.Forms.BatteryChargeStatus.NoSystemBattery;
            var battery = ps.BatteryLifePercent * 100f;
            BatteryText = hasBattery ? $"{battery:0}%" : string.Empty;

            // 低电量提醒（每个充电周期提醒一次）
            if (hasBattery && _settings.Current.LowBatteryThreshold > 0)
            {
                if (!_lowBatteryNotified && battery <= _settings.Current.LowBatteryThreshold)
                {
                    _lowBatteryNotified = true;
                    LowBatteryRequested?.Invoke((int)battery);
                }
                if (battery > _settings.Current.LowBatteryThreshold + 5) _lowBatteryNotified = false;
            }

            // CPU / 内存（每 2 秒；计数器实例复用，避免首次采样为 0）
            if (++_statsTick % 2 == 0)
            {
                try
                {
                    if (_cpuCounter is not null)
                    {
                        _cpuValue = (float?)_cpuCounter.NextValue();
                        CpuText = _cpuValue.HasValue ? $"{_cpuValue.Value:0}%" : "--";
                    }
                }
                catch { CpuText = "--"; }
                try
                {
                    if (_ramCounter is not null)
                    {
                        var ram = _ramCounter.NextValue();
                        RamText = $"{ram:0}%";
                    }
                }
                catch { RamText = "--"; }
            }

            // 网络速度（每秒）
            try
            {
                var iface = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
                    .FirstOrDefault(i => i.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up
                        && i.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback && i.Speed > 0);
                if (iface is not null)
                {
                    var stats = iface.GetIPv4Statistics();
                    var bytes = stats.BytesReceived + stats.BytesSent;
                    var now = DateTime.UtcNow;
                    var secs = Math.Max(0.1, (now - _lastNetTime).TotalSeconds);
                    var kbs = (bytes - _lastNetBytes) / 1024.0 / secs;
                    _lastNetBytes = bytes;
                    _lastNetTime = now;
                    NetText = kbs >= 1024 ? $"{kbs / 1024:0.0} MB/s" : $"{kbs:0} KB/s";
                }
                else NetText = string.Empty;
            }
            catch { NetText = string.Empty; }
        }
        catch { /* 忽略性能计数器/电量异常 */ }
    }


    // 组件摆放顺序（WidgetOrder 中的下标决定左右列）
    public double WidgetTimeFontSize => HasMedia ? 14 : 16; // 空闲时钟字号适中，启动不突兀

    // 系统状态/日期组件文本
    private string _dateText = string.Empty;
    public string DateText { get => _dateText; private set => Set(ref _dateText, value); }
    private string _cpuText = string.Empty;
    public string CpuText { get => _cpuText; private set => Set(ref _cpuText, value); }
    private string _ramText = string.Empty;
    public string RamText { get => _ramText; private set => Set(ref _ramText, value); }
    private string _netText = string.Empty;
    public string NetText { get => _netText; private set => Set(ref _netText, value); }
    private string _batteryText = string.Empty;
    public string BatteryText { get => _batteryText; private set => Set(ref _batteryText, value); }

    /// <summary>切歌时触发（参数：歌名、歌手），用于 Now Playing 横幅。</summary>
    public event Action<string, string>? NowPlayingRequested;
    /// <summary>低电量触发（参数：电量百分比）。</summary>
    public event Action<int>? LowBatteryRequested;
    private bool _lowBatteryNotified;
    // ── 上岛推送（第三方软件推送到灵动岛）──────────────────
    public IslandPush? ActivePush
    {
        get => _activePush;
        private set
        {
            if (!Set(ref _activePush, value)) return;
            OnPropertyChanged(nameof(HasActivePush));
            OnPropertyChanged(nameof(ActivePushIcon));
            OnPropertyChanged(nameof(ActivePushTitle));
            OnPropertyChanged(nameof(ActivePushBody));
            OnPropertyChanged(nameof(ActivePushHasBody));
            OnPropertyChanged(nameof(ActivePushHasProgress));
            OnPropertyChanged(nameof(ActivePushProgress));
            OnPropertyChanged(nameof(ActivePushHasButtons));
            OnPropertyChanged(nameof(ActivePushButtons));
            UpdateVisibility(); // 上岛卡片显示/消失影响灵动岛可见性
        }
    }

    public bool HasActivePush => ActivePush is not null;
    public string ActivePushIcon => string.IsNullOrEmpty(ActivePush?.Icon) ? "\uE7F4" : ActivePush!.Icon;
    public string ActivePushTitle => ActivePush?.Title ?? string.Empty;
    public string ActivePushBody => ActivePush?.Body ?? string.Empty;
    public bool ActivePushHasBody => !string.IsNullOrEmpty(ActivePush?.Body);
    public bool ActivePushHasProgress => ActivePush?.Progress is not null;
    public double ActivePushProgress => Math.Clamp(ActivePush?.Progress ?? 0, 0, 1);
    public bool ActivePushHasButtons => ActivePush?.Buttons is { Count: > 0 };
    public IReadOnlyList<IslandPushButton> ActivePushButtons
        => (IReadOnlyList<IslandPushButton>)(ActivePush?.Buttons ?? new List<IslandPushButton>());

    /// <summary>估算文本宽度：中文/全角按 cjkPx，ASCII 按 asciiPx。</summary>
    private static double MeasureText(string s, double cjkPx, double asciiPx)
    {
        double w = 0;
        foreach (var ch in s) w += ch > 0x2E7F ? cjkPx : asciiPx;
        return w;
    }

    /// <summary>估算紧凑宽度：按激活组件内容（时间/日期/天气/系统状态/歌曲）逐项累加，稳定可靠（不依赖 UI 布局时机）。</summary>
    public double EstimatedCompactWidth
    {
        get
        {
            double w = 4; // 左内边距
            foreach (var item in CompactItems)
            {
                switch (item.Kind)
                {
                    case "Time": w += 48; break;
                    case "Weather": w += Math.Min(MeasureText(WeatherText, 12, 6.5) + 8, 90); break;
                    case "Date": w += Math.Min(MeasureText(DateText, 13, 7) + 8, 100); break;
                    case "Cpu": w += MeasureText(CpuText, 11, 6) + 16; break;
                    case "Ram": w += MeasureText(RamText, 11, 6) + 16; break;
                    case "Net": w += MeasureText(NetText, 11, 6) + 16; break;
                    case "Battery": w += MeasureText(BatteryText, 11, 6) + 16; break;
                    case "Song": w += 40 + 6 + Math.Min(MeasureText(Title, 13, 7), 90); break;
                }
                w += 8; // 组件间右边距（模板 Margin 0,0,8,0 左右），这里按单边即可
            }
            if (HasMedia) w += 68; // 播放/暂停 + 下一首 按钮
            return Math.Clamp(w + 4, 260, 460);
        }
    }

    /// <summary>估算紧凑高度：按内容实际高度计算（不再取手动设置值，避免上下留白过大）。</summary>
    public double EstimatedCompactHeight
    {
        get
        {
            double contentH = 40; // 单行内容高（时间/日期/上岛单行/歌曲封面）
            if (HasActivePush) contentH = Math.Max(contentH, _settings.Current.SingleLineMode ? 40 : Math.Min(PushCompactHeight, 160));
            if (HasMedia && _settings.Current.ShowMediaInfo && !_settings.Current.SingleLineMode) contentH = Math.Max(contentH, 68);
            return Math.Clamp(contentH + 12, 48, 160); // 内容高 + 上下内边距(6+6)
        }
    }

    /// <summary>估算展开宽度：至少 420，有上岛推送时取推送宽度。</summary>
    public double EstimatedExpandedWidth
    {
        get
        {
            var w = Math.Max(420, _settings.Current.ExpandedWidth);
            if (HasActivePush) w = Math.Max(w, PushCompactWidth);
            return Math.Clamp(w, 360, 640);
        }
    }

    /// <summary>估算展开高度：按上岛卡片 + 歌曲各区块累加。</summary>
    public double EstimatedExpandedHeight
    {
        get
        {
            double h = 24;
            if (HasActivePush)
            {
                h += 96;
                if (!string.IsNullOrEmpty(ActivePushBody)) h += 34;
                if (ActivePushHasButtons) h += 40;
                if (ActivePushHasProgress) h += 12;
            }
            if (HasMedia)
            {
                if (_settings.Current.ExpandedShowArtTitle) h += 90;
                if (_settings.Current.ExpandedShowProgress) h += 40;
                if (_settings.Current.ExpandedShowControls) h += 42;
                if (_settings.Current.ExpandedShowLyrics) h += 190;
            }
            return Math.Clamp(h, 200, 620);
        }
    }

    /// <summary>上岛推送时的紧凑宽度：按标题/正文/按钮中最宽者自适应（上限 640），推送消失后恢复原设置。</summary>
    public double PushCompactWidth
    {
        get
        {
            var baseW = Math.Max(_settings.Current.CompactWidth, 300);
            if (ActivePush is null) return baseW;
            double need = 0;
            need = Math.Max(need, MeasureText(ActivePush.Title ?? string.Empty, 15, 8));
            if (!string.IsNullOrEmpty(ActivePush.Body))
                need = Math.Max(need, Math.Min(MeasureText(ActivePush.Body, 13.5, 7), 460)); // 正文单行最宽，超出换行
            if (ActivePush.Buttons is { Count: > 0 })
            {
                double btnW = 0;
                foreach (var b in ActivePush.Buttons) btnW += MeasureText(b.Label ?? string.Empty, 12, 6.5) + 26;
                btnW += (ActivePush.Buttons.Count - 1) * 8;
                need = Math.Max(need, btnW);
            }
            // 基础宽 + 超出 180px 的部分，限制在 280~640
            return Math.Clamp(baseW + Math.Max(0, need - 180), 280, 640);
        }
    }

    /// <summary>上岛推送时的紧凑高度：按内容行数（标题/正文/进度/按钮）自适应；单行模式只显示图标+标题。</summary>
    public double PushCompactHeight
    {
        get
        {
            if (ActivePush is null) return _settings.Current.CompactHeight;
            if (_settings.Current.SingleLineMode) return Math.Max(46, _settings.Current.CompactHeight);
            double h = 46; // 图标/标题行 + 内边距
            if (!string.IsNullOrEmpty(ActivePush.Body))
            {
                var bodyW = Math.Min(MeasureText(ActivePush.Body, 13.5, 7), 460);
                var lineW = Math.Max(90, PushCompactWidth - 40);
                var lines = Math.Max(1, (int)Math.Ceiling(bodyW / lineW));
                h += lines * 17 + 4;
            }
            if (ActivePush.Progress is not null) h += 12;
            if (ActivePush.Buttons is { Count: > 0 }) h += 34;
            return Math.Clamp(h, 54, 210);
        }
    }

    /// <summary>上岛 API 收到推送：更新当前上岛卡片（同 id 覆盖），并按条显示时长安排过期。</summary>
    public void PushIsland(IslandPush push)
    {
        // 若当前已有同 id 推送则保留原过期时间基础；否则用新计算的 ExpiresAt
        var existing = ActivePush;
        if (existing is not null && string.Equals(existing.Id, push.Id, StringComparison.Ordinal) && existing.ExpiresAt is DateTime e)
        {
            // 更新内容，保持原过期时间
            push.ExpiresAt = e;
        }
        ActivePush = push;
        AppLogger.Info($"Island push: '{push.Title}' (id={push.Id})");
    }

    /// <summary>上岛 API 移除/过期指定推送。</summary>
    public void RemoveIslandPush(string id)
    {
        if (ActivePush is not null && string.Equals(ActivePush.Id, id, StringComparison.Ordinal))
            ActivePush = null;
    }

    /// <summary>用户点击/关闭当前上岛卡片。</summary>
    public void DismissActivePush()
    {
        if (ActivePush is not null) ActivePush = null;
    }

    /// <summary>执行上岛卡片按钮动作（打开 URL / 启动程序）。</summary>
    public void ExecutePushAction(IslandPushButton button)
    {
        if (button is null || string.IsNullOrWhiteSpace(button.Value)) return;
        try
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(button.Value)
            {
                UseShellExecute = true,
            });
            AppLogger.Info($"Island push action: {button.Action} -> {button.Value}");
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Island push action failed: {ex.Message}");
        }
    }

    /// <summary>每秒检查上岛推送是否过期（由 _widgetTimer 调用）。</summary>
    private void CheckPushExpiry()
    {
        if (ActivePush?.ExpiresAt is DateTime expiry && DateTime.UtcNow >= expiry)
            ActivePush = null;
    }

    // ── Visibility / expansion ─────────────────────────────────
    public bool IsExpanded
    {
        get => _expanded;
        set
        {
            if (!Set(ref _expanded, value)) return;
            OnPropertyChanged(nameof(ExpandedContentVisibility));
        }
    }

    public bool IsVisible
    {
        get => _visible;
        set => Set(ref _visible, value);
    }

    // Used by XAML to collapse expanded-only sections without a converter.
    public System.Windows.Visibility ExpandedContentVisibility
        => IsExpanded ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;

    // ── Coordinator events ─────────────────────────────────────
    private void OnSnapshotChanged(object? sender, MediaSnapshot snapshot)
    {
        // 用「歌手+歌名+专辑」判断换曲，而不是整条 TrackInfo 结构相等：
        // 封面 URL 等字段抖动不应触发换曲（否则会重置进度并把歌词打回开头）。
        var trackChanged = _snapshot is null ||
            LyricsService.TrackKey(_snapshot.Track) != LyricsService.TrackKey(snapshot.Track);
        var firstTrack = _snapshot is null;
        _snapshot = snapshot;
        if (trackChanged && !firstTrack && !string.IsNullOrEmpty(snapshot.Track.Title))
            NowPlayingRequested?.Invoke(snapshot.Track.Title, snapshot.Track.Artist);

        Title = snapshot.Track.Title;
        Artist = snapshot.Track.Artist;
        Album = snapshot.Track.Album;
        SourceLabel = snapshot.SourceLabel;
        SourceDetail = snapshot.Track.SourceAppName;
        var prevStatus = Status;
        if (trackChanged) { _statusOverrideActive = false; _pauseLock = false; } // 换曲后上一状态锁定作废
        if (_statusOverrideActive)
        {
            // 乐观状态保护期：直到快照确认目标状态或超时，否则保持按钮状态，不被快照打回
            if (snapshot.Status == _optimisticStatus || DateTime.UtcNow > _statusOverrideUntilUtc)
            {
                _statusOverrideActive = false;
                Status = snapshot.Status;
            }
        }
        else if (_pauseLock)
        {
            // 暂停锁定：用户点击暂停 / 重启恢复的是暂停 —— 忽略快照误报的 Playing
            // （Cider SMTC 在暂停时常仍报 Playing），保持暂停、不推进歌词；
            // 直到快照确认暂停/停止（解除锁定）或用户点击播放。
            if (snapshot.Status is PlaybackStatus.Paused or PlaybackStatus.Closed or PlaybackStatus.Stopped)
            {
                _pauseLock = false;
                Status = snapshot.Status;
            }
        }
        else
        {
            Status = snapshot.Status;
        }
        DurationSeconds = snapshot.DurationSeconds;
        // 暂停时保存一次位置（崩溃/退出后可恢复）
        if (Status == PlaybackStatus.Paused && prevStatus != PlaybackStatus.Paused) SavePlaybackState();
        OnPropertyChanged(nameof(DurationText));
        var hasRealPosition = snapshot.DurationSeconds > 0 || snapshot.PositionSeconds > 0;
        var reported = Math.Max(0, snapshot.PositionSeconds);
        if (snapshot.DurationSeconds > 0) reported = Math.Min(reported, snapshot.DurationSeconds); // 防御：上报值不越界
        if (trackChanged)
        {
            _restoredMode = false;
            // 启动恢复：Cider/SMTC 尚未返回真实位置时，用上次保存的位置作为初始值，
            // 避免暂停后重启先显示第 0 行、等真实位置到了再“跳”到暂停句。
            // 只要曲目匹配且有上次位置就恢复（即使第一帧带了时长但位置为 0，也以恢复位置为准，
            // 之后位置守卫会按真实上报值平滑校正，避免先显示第 0 行再跳）
            var restored = _restoredTrackKey is not null
                && LyricsService.TrackKey(snapshot.Track) == _restoredTrackKey
                && _restoredPosition > 0;
            if (restored)
            {
                _restoredMode = true; // 信任恢复位置，直到收到真实前进位置/换曲/seek
                _interpolatedPosition = _restoredPosition;
                if (_restoredStatus == PlaybackStatus.Paused)
                {
                    // 上次是暂停：锁定为暂停，重启后歌词保持不动（Cider SMTC 常误报 Playing）
                    _pauseLock = true;
                    SetStatusLocal(PlaybackStatus.Paused);
                }
                if (snapshot.DurationSeconds > 0) _interpolatedPosition = Math.Min(_interpolatedPosition, snapshot.DurationSeconds);
                _trackStartTime = DateTime.UtcNow - TimeSpan.FromSeconds(_interpolatedPosition);
            }
            else
            {
                _useFreeClock = !hasRealPosition;
                _trackStartTime = DateTime.UtcNow - TimeSpan.FromSeconds(reported);
                _interpolatedPosition = reported;
            }
            _restoredTrackKey = null; // 只恢复一次
            _positionStaleSinceUtc = null;
            _karaokeFrozen = false;
            SavePlaybackState();
        }
        else if (_pauseLock)
        {
            // 暂停锁定期间：位置保持冻结，不采纳快照位置（避免歌词/进度在暂停时继续走）
        }
        else if (hasRealPosition)
        {
            var current = _interpolatedPosition;
            var seeking = _suppressSeek > 0; // 用户正在拖拽进度条
            if (ShouldAdoptReportedPosition(reported, current, seeking))
            {
                _restoredMode = false; // 已收到真实前进位置，恢复正常守卫
                _useFreeClock = false;
                _trackStartTime = DateTime.UtcNow - TimeSpan.FromSeconds(reported);
                _interpolatedPosition = reported;
                _positionStaleSinceUtc = null;
            }
            else if (_restoredMode)
            {
                // 启动恢复信任期：持续报 0/过期位置（如 Cider SMTC）也保持恢复的位置，不触发回跳
            }
            else
            {
                // 忽略瞬间回退，保持当前插值进度继续推进，避免歌词/进度条突然跳回开头；
                // 若明显回退持续超过 ~4 秒（真正的重播或播放器端 seek），再采纳并回跳。
                if (_positionStaleSinceUtc is null) _positionStaleSinceUtc = DateTime.UtcNow;
                else if ((DateTime.UtcNow - _positionStaleSinceUtc.Value).TotalSeconds >= 4.0)
                {
                    _useFreeClock = false;
                    _trackStartTime = DateTime.UtcNow - TimeSpan.FromSeconds(reported);
                    _interpolatedPosition = reported;
                    _positionStaleSinceUtc = null;
                }
            }
        }
        // 无真实进度且非新曲目：保持自由时钟继续推进（不重置）
        _lastPositionTime = DateTime.UtcNow;
        CanPlayPause = snapshot.CanPlayPause;
        CanNext = snapshot.CanNext;
        CanPrevious = snapshot.CanPrevious;
        CanSeek = snapshot.CanSeek && snapshot.DurationSeconds > 0;
        HasVolumeControl = snapshot.HasVolumeControl;
        _suppressVolume = true;
        Volume = snapshot.Volume ?? 0;
        _suppressVolume = false;

        if (snapshot.Track.ArtworkPath.Length > 0)
            Artwork = LoadImage(snapshot.Track.ArtworkPath);
        else
            Artwork = null;

        if (trackChanged)
            _ = LoadLyricsAsync(snapshot);

        OnPropertyChanged(nameof(IsPlaying));
        OnPropertyChanged(nameof(IsPaused));
        OnPropertyChanged(nameof(PlayPauseGlyph));
        OnPropertyChanged(nameof(Position));
        OnPropertyChanged(nameof(HasLyrics));
        UpdateVisibility();
    }

    /// <summary>
    /// 判断是否应立即采用播放器上报的位置（秒）。
    /// 仅当位置前进 / 轻微回退（≤2s）/ 用户正在拖拽进度条时才立即采用；
    /// 播放到中途时瞬间上报 ~0 视为过期读数（如 Cider/SMTC 偶发返回 0），
    /// 返回 false，由调用方保持当前插值进度继续推进，避免歌词/进度条突然跳回开头。
    /// </summary>
    internal static bool ShouldAdoptReportedPosition(double reported, double current, bool seeking)
    {
        var sane = seeking || reported >= current - 2.0;
        var staleZero = !seeking && reported < 1.0 && current > 10.0;
        return sane && !staleZero;
    }
    private void OnMediaEnded(object? sender, EventArgs e)
    {
        _snapshot = null;
        _restoredMode = false;
        _statusOverrideActive = false;
        _pauseLock = false;
        Title = string.Empty;
        Artist = string.Empty;
        Album = string.Empty;
        Artwork = null;
        Status = PlaybackStatus.Closed;
        DurationSeconds = 0;
        Position = TimeSpan.Zero;
        Progress = 0;
        _interpolatedPosition = 0;
        _positionStaleSinceUtc = null;
        LyricLines = Array.Empty<LyricLineViewModel>();
        _lyrics = LyricsResult.Empty;
        LyricIndex = -1;
        CurrentLyricText = string.Empty;
        OnPropertyChanged(nameof(IsPlaying));
        OnPropertyChanged(nameof(IsPaused));
        OnPropertyChanged(nameof(PlayPauseGlyph));
        OnPropertyChanged(nameof(HasLyrics));
        UpdateVisibility();
    }

    private async Task LoadLyricsAsync(MediaSnapshot snapshot)
    {
        var key = LyricsService.TrackKey(snapshot.Track);
        if (key == _lyricsKey) return;
        _lyricsKey = key;

        var result = await _lyricsService.GetLyricsAsync(snapshot);
        if (_lyricsKey != key) return; // track changed while loading
        _lyrics = result;
        LyricLines = result.Document.Lines.Select(l => new LyricLineViewModel(l)).ToList();
        if (LyricLines.Count > 0)
        {
            // 直接按当前（已恢复的）位置定位当前句，避免启动瞬间先显示第 0 行再跳
            var idx = result.Document.IndexAt(TimeSpan.FromSeconds(Math.Max(0, _interpolatedPosition)));
            LyricIndex = idx < 0 ? -1 : idx;
            CurrentLyricText = LyricLines[Math.Clamp(idx, 0, LyricLines.Count - 1)].Text;
        }
        else
        {
            LyricIndex = -1;
            CurrentLyricText = string.Empty;
        }


        LyricsStatus = result.Source switch
        {
            LyricsSourceKind.LocalFile => Localization.Get("Lyrics_Local"),
            LyricsSourceKind.Cider => Localization.Get("Lyrics_FromCider"),
            LyricsSourceKind.Online => Localization.Get("Lyrics_Online"),
            _ => Localization.Get("LyricsUnavailable"),
        };
        OnPropertyChanged(nameof(HasLyrics));
    }

    // ── Progress interpolation ─────────────────────────────────
    private void AdvanceProgress()
    {
        if (_snapshot is null || _suppressSeek > 0) return;

        if (Status == PlaybackStatus.Playing)
        {
            var now = DateTime.UtcNow;
            if (_useFreeClock)
            {
                // 播放器不报 SMTC 进度（如 Cider）：用本地时钟从曲目开始推进卡拉OK
                _interpolatedPosition = (now - _trackStartTime).TotalSeconds;
            }
            else
            {
                _interpolatedPosition += (now - _lastPositionTime).TotalSeconds;
            }
            _lastPositionTime = now;
        }

        // 时长不可用（Cider）时用兜底时长，保证进度/卡拉OK仍推进
        var duration = DurationSeconds > 0 ? DurationSeconds : 300.0;
        Position = TimeSpan.FromSeconds(Math.Max(0, _interpolatedPosition));
        Progress = Math.Clamp(_interpolatedPosition / duration, 0, 1);

        if (HasLyrics)
        {
            var idx = _lyrics.Document.IndexAt(Position);
            if (idx != LyricIndex) LyricIndex = idx;
            UpdateKaraokeHighlight();
        }
    }

    /// <summary>逐字卡拉OK：把当前句的时长按字符均分，推进已点亮字符数。</summary>
    private void UpdateKaraokeHighlight()
    {
        if (LyricIndex < 0 || LyricIndex >= _lyrics.Document.Lines.Count) return;
        var lines = _lyrics.Document.Lines;
        var cur = lines[LyricIndex];
        var nextStart = (LyricIndex + 1 < lines.Count) ? lines[LyricIndex + 1].Time.TotalSeconds : cur.Time.TotalSeconds + 5.0;
        var duration = Math.Max(0.1, nextStart - cur.Time.TotalSeconds);
        var frac = Math.Clamp((Position.TotalSeconds - cur.Time.TotalSeconds) / duration, 0, 1);

        if (Status == PlaybackStatus.Playing)
        {
            // 播放中：实时推进（连续比例 0..1，供控件 60fps 缓动）
            _karaokeFrozen = false;
            SetHighlightFraction(frac);
        }
        else if (!_karaokeFrozen)
        {
            // 暂停：用当前（已正确恢复的）位置设置一次高亮，然后冻结，
            // 之后任何位置校正都不再改动高亮 → 稳定在暂停时刻的样子。
            SetHighlightFraction(frac);
            _karaokeFrozen = true;
        }
        // 已冻结：保持不动

    }

    private void SetHighlightFraction(double frac)
    {
        if (LyricLines.Count > LyricIndex)
        {
            var lvm = LyricLines[LyricIndex];
            if (Math.Abs(lvm.HighlightFraction - frac) > 0.0005) lvm.HighlightFraction = frac;
        }
        if (Math.Abs(CompactHighlightFraction - frac) > 0.0005) CompactHighlightFraction = frac;
    }

    /// <summary>保存当前播放位置（退出/暂停/切歌时调用，供下次启动恢复）。</summary>
    public void SavePlaybackState()
    {
        try
        {
            if (_snapshot is null) return;
            new PlaybackStateStore
            {
                TrackKey = LyricsService.TrackKey(_snapshot.Track),
                PositionSeconds = Math.Max(0, _interpolatedPosition),
                Status = Status.ToString(),
            }.Save();
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"SavePlaybackState failed: {ex.Message}");
        }
    }
    // ── Visibility ─────────────────────────────────────────────
    public void UpdateVisibility()
    {
        var hasMedia = _snapshot is not null && Status is PlaybackStatus.Playing or PlaybackStatus.Paused;
        HasMedia = hasMedia;
        var alwaysVisible = _settings.Current.IslandAlwaysVisible;
        var comp = _settings.Current.Components;
        // 空闲时是否有任意组件（或常驻/旧开关）需要显示
        var anyIdleComp = comp.TimeWhenIdle || comp.WeatherWhenIdle || comp.CoverWhenIdle
            || comp.TitleWhenIdle || comp.ArtistWhenIdle || comp.LyricsWhenIdle || comp.ProgressWhenIdle;
        var showWidgets = !hasMedia && (_settings.Current.ShowWidgetsWhenNoMedia || alwaysVisible || anyIdleComp);
        ShowIdleWidgets = !hasMedia; // 空闲面板可见性（内部按组件勾选）

        // 有上岛推送时也要显示灵动岛（否则第三方推送看不到）
        var show = !_userHidden && (hasMedia || showWidgets || HasActivePush || !_settings.Current.HideWhenNoMedia);
        // 常驻时不因暂停而隐藏
        if (!alwaysVisible && hasMedia && Status == PlaybackStatus.Paused && !_settings.Current.ShowWhenPaused)
            show = false;

        if (!ShowIdleWeather) WeatherText = string.Empty; // 仅天气组件不显示时才清空

        // 通知界面组件可见性变化
        OnPropertyChanged(nameof(ShowCover));
        OnPropertyChanged(nameof(ShowTitle));
        OnPropertyChanged(nameof(ShowArtist));
        OnPropertyChanged(nameof(ShowLyrics));
        OnPropertyChanged(nameof(ShowCompactProgress));
        OnPropertyChanged(nameof(ShowIdleTime));
        OnPropertyChanged(nameof(ShowIdleWeather));
        OnPropertyChanged(nameof(ShowAnyWidget));
        RebuildCompactItems();
        OnPropertyChanged(nameof(WidgetTimeFontSize));
        OnPropertyChanged(nameof(ShowIdleDate));
        OnPropertyChanged(nameof(ShowIdleCpu));
        OnPropertyChanged(nameof(ShowIdleRam));
        OnPropertyChanged(nameof(ShowIdleNet));
        OnPropertyChanged(nameof(ShowIdleBattery));

        IsVisible = show;
    }

    public void ToggleUserVisible()
    {
        _userHidden = !_userHidden;
        UpdateVisibility();
    }

    public void ForceShow() { _userHidden = false; UpdateVisibility(); }
    public void ForceHide() { _userHidden = true; UpdateVisibility(); }

    /// <summary>强制重新获取当前曲目的歌词（在线歌词开关变化后调用）。</summary>
    public async Task RefreshLyricsAsync()
    {
        _lyricsKey = string.Empty;
        _lyricsService.ClearCache();
        if (_snapshot is not null) await LoadLyricsAsync(_snapshot);
    }

    /// <summary>
    /// Demo mode (--demo): injects a fake track so the island can be previewed
    /// without any media playing. Also writes a sample .lrc for lyrics.
    /// </summary>
    public void InjectDemoMedia()
    {
        var artPath = CreateDemoArtwork();
        var lyricsDir = AppPaths.LyricsDir;
        Directory.CreateDirectory(lyricsDir);
        var lrcPath = Path.Combine(lyricsDir, "Demo Artist - Demo Song.lrc");
        if (!File.Exists(lrcPath))
        {
            File.WriteAllText(lrcPath,
                "[ti:Demo Song]\n[ar:Demo Artist]\n[al:Demo Album]\n" +
                "[00:00.00]Welcome to WinIsland\n[00:04.00]This is a demo track\n[00:08.00]Hover to expand\n[00:12.00]Drag the progress bar to seek\n[00:16.00]Lyrics scroll automatically\n[00:20.00]Enjoy your Dynamic Island\n[00:24.00]Thanks for trying WinIsland\n");
        }

        var track = new TrackInfo("Demo Song", "Demo Artist", "Demo Album", "Demo Artist",
            "Demo", "demo-source", artPath, string.Empty, TimeSpan.FromSeconds(210));
        var snap = new MediaSnapshot
        {
            Track = track,
            Source = MediaSourceKind.Smtc,
            Status = PlaybackStatus.Playing,
            PositionSeconds = 5,
            DurationSeconds = 210,
            CanPlayPause = true,
            CanNext = true,
            CanPrevious = true,
            CanSeek = true,
            HasVolumeControl = true,
            Volume = 0.6,
            HasLyrics = true,
        };
        OnSnapshotChanged(this, snap);
    }

    private static string CreateDemoArtwork()
    {
        try
        {
            var path = Path.Combine(AppPaths.ThumbCacheDir, "demo-art.jpg");
            if (File.Exists(path)) return path;
            using var bmp = new System.Drawing.Bitmap(320, 320);
            using (var g = System.Drawing.Graphics.FromImage(bmp))
            {
                var rect = new System.Drawing.Rectangle(0, 0, 320, 320);
                var brush = new System.Drawing.Drawing2D.LinearGradientBrush(rect,
                    System.Drawing.Color.FromArgb(255, 99, 102, 241),
                    System.Drawing.Color.FromArgb(255, 34, 211, 238), 45f);
                g.FillRectangle(brush, rect);
                g.DrawString("WinIsland", new System.Drawing.Font("Segoe UI", 28, System.Drawing.FontStyle.Bold),
                    System.Drawing.Brushes.White, 70, 130);
            }

            bmp.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
            return path;
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Demo artwork failed: {ex.Message}");
            return string.Empty;
        }
    }

    // ── Helpers ────────────────────────────────────────────────
    private static ImageSource? LoadImage(string path)
    {
        try
        {
            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.UriSource = new Uri(path, UriKind.Absolute);
            bmp.EndInit();
            bmp.Freeze();
            return bmp;
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"LoadImage failed for {path}: {ex.Message}");
            return null;
        }
    }

    private void RaiseAllText()
    {
        LyricsStatus = _lyrics.Source switch
        {
            LyricsSourceKind.LocalFile => Localization.Get("Lyrics_Local"),
            LyricsSourceKind.Cider => Localization.Get("Lyrics_FromCider"),
            LyricsSourceKind.Online => Localization.Get("Lyrics_Online"),
            _ => Localization.Get("LyricsUnavailable"),
        };
        OnPropertyChanged(nameof(LyricsStatus));
    }

    /// <summary>Begin a user drag on the progress slider.</summary>
    public void BeginSeek() => _suppressSeek++;

    /// <summary>Seek to the given fraction (0..1) after a drag.</summary>
    public async Task EndSeekAsync(double fraction)
    {
        _suppressSeek--;
        if (_snapshot is null || _snapshot.DurationSeconds <= 0) return;
        var target = Math.Clamp(fraction, 0, 1) * _snapshot.DurationSeconds;
        _restoredMode = false; // 用户 seek 后以新位置为准
        _interpolatedPosition = target;
        _lastPositionTime = DateTime.UtcNow;
        await _coordinator.SeekAsync(target);
    }

    /// <summary>
    /// 乐观播放/暂停：点击按钮后立即切换本地状态，避免快照状态延迟期间
    /// 本地进度继续推进，导致暂停后歌词高亮/进度条“跳回”暂停点。
    /// </summary>
    private async Task TogglePlayPauseLocalAsync()
    {
        if (_toggleInFlight) return; // 防连点：命令在途时忽略再次点击
        if (Status != PlaybackStatus.Playing && Status != PlaybackStatus.Paused) return;
        _toggleInFlight = true;
        var target = Status == PlaybackStatus.Playing ? PlaybackStatus.Paused : PlaybackStatus.Playing;
        try
        {
            _pauseLock = target == PlaybackStatus.Paused; // 暂停则锁定，播放则解除
            SetStatusLocal(target); // 立即切换按钮状态，避免延迟感
            _optimisticStatus = target;
            _statusOverrideActive = true;
            _statusOverrideUntilUtc = DateTime.UtcNow + TimeSpan.FromSeconds(8);
            var ok = await _coordinator.TogglePlayPauseAsync();
            if (!ok)
            {
                // 部分播放器/Cider 版本不支持 playpause 端点：回退到明确的 play/pause
                ok = target == PlaybackStatus.Paused
                    ? await _coordinator.PauseAsync()
                    : await _coordinator.PlayAsync();
            }
            if (!ok) AppLogger.Warn("Play/pause command returned failure; waiting for player state to settle.");
        }
        finally
        {
            _toggleInFlight = false;
        }
        // 保护期不在此结束：等快照确认目标状态或超时后再恢复快照驱动，防止按钮被打回
    }

    private void SetStatusLocal(PlaybackStatus value)
    {
        if (Status == value) return;
        Status = value;
        OnPropertyChanged(nameof(IsPlaying));
        OnPropertyChanged(nameof(IsPaused));
        OnPropertyChanged(nameof(PlayPauseGlyph));
        if (value == PlaybackStatus.Paused) SavePlaybackState(); // 暂停即保存，退出/崩溃后可恢复
    }
    public void Dispose()
    {
        _progressTimer.Stop();
        _widgetTimer.Stop();
        _coordinator.SnapshotChanged -= OnSnapshotChanged;
        _coordinator.MediaEnded -= OnMediaEnded;
    }
}




