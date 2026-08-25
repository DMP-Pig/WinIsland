using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Collections.ObjectModel;
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
    private readonly List<IslandPush> _pushes = new();
    private IslandPush? _activePush;

    // ── 效率工具 / 波纹 / 键盘指示灯 ──
    private readonly AudioWaveService _wave;
    private readonly KeyboardIndicatorMonitor _keyboard;
    private readonly ClipboardHistoryService _clipboard;
    private readonly TodoService _todo;
    private readonly ScheduleService _schedule;
    private readonly PomodoroService _pomodoro;
    private int _capsLockSecondsLeft;   // 键盘指示灯剩余显示秒数（由 _widgetTimer 每秒递减）
    private int _volumeTempSecondsLeft;          // 音量指示剩余显示秒数
    private double _lastVolumeTempValue = -1;    // 上次轮询到的系统音量（变化时触发上岛）
    private bool _lastVolumeTempMuted;           // 上次轮询到的静音状态
    // ── 多播放器选择器（迷你播放器 / 设置中切换媒体来源）──
    private readonly ObservableCollection<MediaSessionItem> _mediaSessions = new();
    private MediaSessionItem? _selectedMediaSession;
    private bool _suppressSessionSwitch;

    public IslandViewModel(MediaCoordinator coordinator, SettingsService settings, LyricsService lyricsService,
        AudioWaveService? wave = null, KeyboardIndicatorMonitor? keyboard = null,
        ClipboardHistoryService? clipboard = null, TodoService? todo = null,
        ScheduleService? schedule = null, PomodoroService? pomodoro = null)
    {
        _coordinator = coordinator;
        _settings = settings;
        _lyricsService = lyricsService;

        // 效率工具 / 波纹 / 键盘指示灯：默认自建实例（App 可注入共享实例）
        _wave = wave ?? new AudioWaveService();
        _keyboard = keyboard ?? new KeyboardIndicatorMonitor();
        _clipboard = clipboard ?? new ClipboardHistoryService();
        _todo = todo ?? new TodoService();
        _schedule = schedule ?? new ScheduleService();
        _pomodoro = pomodoro ?? new PomodoroService();
        _wave.Start();
        _wave.SetPlaying(false);
        _keyboard.StateChanged += OnKeyboardStateChanged;
        _clipboard.Changed += RefreshClipboardSummary;
        _todo.Changed += RefreshTodoSummary;
        _schedule.Changed += RefreshScheduleSummary;
        _pomodoro.Tick += RefreshTimerText;
        _pomodoro.Completed += OnPomodoroCompleted;

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
        _coordinator.SessionsChanged += (_, _) => RefreshMediaSessions();
        RefreshMediaSessions();
        Localization.LanguageChanged += (_, _) => RaiseAllText();

        _progressTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
        _progressTimer.Tick += (_, _) => AdvanceProgress();
        _progressTimer.Start();

        _widgetTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _widgetTimer.Tick += async (_, _) =>
        {
            // 始终轮询的轻量检测：即使灵动岛隐藏，音量/复制/下载/截图等事件也要能触发上岛
            PollVolumeTemp();
            UpdateVolumeTempCountdown();
            PollFileCopy();
            PollDownloadProgress();
            UpdateScreenshotCountdown();
            if (!IsVisible) return;
            ClockText = DateTime.Now.ToString("HH:mm");
            DateText = FormatDateText(DateTime.Now);
            CheckPushExpiry();
            if (_activePush is not null) OnPropertyChanged(nameof(ActivePushProgress)); // v3 动态进度按秒推进
            UpdateSystemStats();
            UpdateCapsLockCountdown();
            RefreshClipboardSummary();
            RefreshTodoSummary();
            RefreshScheduleSummary();
            RefreshTimerText();
            // 开会静音助手：检测前台窗口会议状态（仅勾选组件或开启会议勿扰时才有意义，但检测开销极小）
            if (ShowIdleMeeting || (_settings.Current.MeetingAssistantEnabled && _settings.Current.MeetingAutoDnd))
            {
                MeetingMonitor.SetCustomKeywords(_settings.Current.MeetingKeywords);
                var meetingChanged = MeetingMonitor.Check();
                var newMeetingText = MeetingMonitor.IsInMeeting
                    ? $"{Localization.Get("Comp_Meeting")} · {MeetingMonitor.AppName}"
                    : string.Empty;
                if (meetingChanged || MeetingText != newMeetingText)
                {
                    MeetingText = newMeetingText;
                    RebuildCompactItems();
                }
            }
            if (ShowIdleInputMethod) InputMethodText = InputMethodMonitor.GetStatusText();
            if (ShowIdleQuickToggles) RefreshQuickToggles();
            if (ShowIdleWeather && ++_weatherTick % 60 == 1)
            {
                var w = await _weather.GetWeatherAsync(_settings.Current.WeatherCity);
                if (w is not null) // 失败保留旧值，避免天气忽有忽无
                {
                    _weatherInfo = w;
                    WeatherText = FormatWeatherCompact(w);
                    WeatherDetailText = FormatWeatherDetail(w);
                }
            }
        };
        _widgetTimer.Start();
    }

    public event EventHandler? OpenSettingsRequested;
    public event EventHandler? ToggleLyricsWindowRequested;

    // ── 多播放器选择器 ────────────────────────────────────────
    /// <summary>当前可用媒体会话（SMTC 全部 + Cider 伪会话），供迷你播放器/设置切换来源。</summary>
    public ObservableCollection<MediaSessionItem> MediaSessions => _mediaSessions;

    public MediaSessionItem? SelectedMediaSession
    {
        get => _selectedMediaSession;
        set
        {
            if (!Set(ref _selectedMediaSession, value)) return;
            if (_suppressSessionSwitch || value is null) return;
            _ = SwitchSelectedSessionAsync(value.AppId);
        }
    }

    /// <summary>刷新媒体会话列表并保持当前选中（不会触发切换）。</summary>
    public void RefreshMediaSessions()
    {
        try
        {
            var sessions = _coordinator.GetAvailableSessions();
            var selectedId = _selectedMediaSession?.AppId;
            _suppressSessionSwitch = true;
            try
            {
                _mediaSessions.Clear();
                foreach (var s in sessions)
                    _mediaSessions.Add(new MediaSessionItem(s.AppId, s.AppName, s.IsCurrent));

                _selectedMediaSession = _mediaSessions.FirstOrDefault(x => x.AppId == selectedId)
                    ?? _mediaSessions.FirstOrDefault(x => x.IsCurrent)
                    ?? _mediaSessions.FirstOrDefault();
            }
            finally
            {
                _suppressSessionSwitch = false;
            }
            OnPropertyChanged(nameof(SelectedMediaSession));
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"刷新媒体会话列表失败: {ex.Message}");
        }
    }

    private async Task SwitchSelectedSessionAsync(string appId)
    {
        try
        {
            await _coordinator.SwitchSessionAsync(appId);
            RefreshMediaSessions();
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"切换媒体来源失败: {ex.Message}");
        }
    }

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
    public string SourceDetail
    {
        get => _sourceDetail;
        private set
        {
            if (Set(ref _sourceDetail, value))
                OnPropertyChanged(nameof(HasSourceDetail));
        }
    }
    /// <summary>是否有播放来源名（歌名旁的小徽标是否显示）。</summary>
    public bool HasSourceDetail => !string.IsNullOrEmpty(_sourceDetail);

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
            OnPropertyChanged(nameof(VolumeText));
            ShowVolumeTemp((int)Math.Round(_volume * 100), _volume < 0.001); // 拖动音量滑杆时临时上岛
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

    /// <summary>是否显示歌词翻译行（展开歌词快捷操作：翻译开关）。</summary>
    private bool _showLyricTranslation = true;
    public bool ShowLyricTranslation
    {
        get => _showLyricTranslation;
        private set
        {
            if (Set(ref _showLyricTranslation, value))
            {
                // 同步到每一行歌词（翻译行显隐）
                foreach (var l in LyricLines) l.ShowTranslation = value;
                OnPropertyChanged(nameof(LyricTranslateText));
            }
        }
    }
    /// <summary>翻译开关按钮文本（如「翻译：开」）。</summary>
    public string LyricTranslateText => Localization.Get("Lyric_Translation") + "：" + (ShowLyricTranslation ? Localization.Get("Quick_On") : Localization.Get("Quick_Off"));
    /// <summary>「复制当前句」按钮文本。</summary>
    public string LyricCopyText => Localization.Get("Lyric_CopyCurrent");
    /// <summary>翻译开关提示。</summary>
    public string LyricTranslateHint => Localization.Get("Lyric_TranslateHint");
    /// <summary>复制当前句提示。</summary>
    public string LyricCopyHint => Localization.Get("Lyric_CopyHint");

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

    private WeatherInfo? _weatherInfo;
    private string _weatherText = string.Empty;
    public string WeatherText { get => _weatherText; private set => Set(ref _weatherText, value); }
    private string _weatherDetailText = string.Empty;
    public string WeatherDetailText { get => _weatherDetailText; private set => Set(ref _weatherDetailText, value); }

    /// <summary>紧凑胶囊里的一个顺序组件。</summary>
    /// <summary>紧凑胶囊里的一个顺序组件（Kind=组件标识，Icon=显示图标字符，支持用户定制）。</summary>
    public sealed record IslandComponent(string Kind, string Icon); // "Time" | "Weather" | "Song"
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
    public bool ShowIdleGpu => HasMedia ? _settings.Current.Components.GpuWhenPlaying : _settings.Current.Components.GpuWhenIdle;
    public bool ShowIdleMic => HasMedia ? _settings.Current.Components.MicWhenPlaying : _settings.Current.Components.MicWhenIdle;
    public bool ShowIdleCam => HasMedia ? _settings.Current.Components.CamWhenPlaying : _settings.Current.Components.CamWhenIdle;
    public bool ShowIdleBattery => HasMedia ? _settings.Current.Components.BatteryWhenPlaying : _settings.Current.Components.BatteryWhenIdle;
    public bool ShowIdleVolume => HasMedia ? _settings.Current.Components.VolumeWhenPlaying : _settings.Current.Components.VolumeWhenIdle;
    public bool ShowIdleCapsLock => HasMedia ? _settings.Current.Components.CapsLockWhenPlaying : _settings.Current.Components.CapsLockWhenIdle;
    public bool ShowIdleClipboard => HasMedia ? _settings.Current.Components.ClipboardWhenPlaying : _settings.Current.Components.ClipboardWhenIdle;
    public bool ShowIdleTodo => HasMedia ? _settings.Current.Components.TodoWhenPlaying : _settings.Current.Components.TodoWhenIdle;
    public bool ShowIdleTimer => HasMedia ? _settings.Current.Components.TimerWhenPlaying : _settings.Current.Components.TimerWhenIdle;
    public bool ShowIdleSchedule => HasMedia ? _settings.Current.Components.ScheduleWhenPlaying : _settings.Current.Components.ScheduleWhenIdle;
    public bool ShowIdleHoliday => HasMedia ? _settings.Current.Components.HolidayWhenPlaying : _settings.Current.Components.HolidayWhenIdle;
    public bool ShowIdleMeeting => HasMedia ? _settings.Current.Components.MeetingWhenPlaying : _settings.Current.Components.MeetingWhenIdle;
    public bool ShowIdleDisk => HasMedia ? _settings.Current.Components.DiskWhenPlaying : _settings.Current.Components.DiskWhenIdle;
    public bool ShowIdleInputMethod => HasMedia ? _settings.Current.Components.InputMethodWhenPlaying : _settings.Current.Components.InputMethodWhenIdle;
    public bool ShowIdleQuickToggles => HasMedia ? _settings.Current.Components.QuickTogglesWhenPlaying : _settings.Current.Components.QuickTogglesWhenIdle;
    public bool ShowAnyWidget => ShowIdleTime || ShowIdleWeather || ShowIdleDate
        || ShowIdleCpu || ShowIdleRam || ShowIdleGpu || ShowIdleNet || ShowIdleBattery || ShowIdleMic || ShowIdleCam
        || ShowIdleVolume || ShowIdleCapsLock || ShowIdleClipboard || ShowIdleTodo
        || ShowIdleTimer || ShowIdleSchedule || ShowIdleHoliday || ShowIdleMeeting || ShowIdleDisk || ShowIdleInputMethod || ShowIdleQuickToggles;

    // ── 效率工具 / 波纹 文本 ──
    /// <summary>波纹强度（0..1），由 AudioWaveService 实时采集/模拟，UI 轮询。</summary>
    public double WaveLevel => _wave.Level;
    public string VolumeText => HasVolumeControl ? $"{(_volume * 100):0}%" : string.Empty;

    private string _capsLockText = string.Empty;
    /// <summary>按键指示灯文本（如「Caps 开」），出现 N 秒后自动清空。</summary>
    public string CapsLockText { get => _capsLockText; private set => Set(ref _capsLockText, value); }
    private string _screenshotStatusText = string.Empty;
    /// <summary>截图临时指示文本（按 PrintScreen 后出现，几秒后自动消失）。</summary>
    public string ScreenshotStatusText { get => _screenshotStatusText; private set => Set(ref _screenshotStatusText, value); }
    private int _screenshotSecondsLeft;   // 截图指示剩余显示秒数（由 _widgetTimer 每秒递减）
    private string _recordingText = string.Empty;
    private string _volumeTempText = string.Empty;
    /// <summary>音量/静音临时上岛文本（调节音量后出现，几秒后自动消失）。</summary>
    public string VolumeTempText { get => _volumeTempText; private set => Set(ref _volumeTempText, value); }
    private string _usageMergeText = string.Empty;
    /// <summary>「使用中」合并胶囊文本（麦克风/摄像头/会议/录屏合并为一个状态胶囊）。</summary>
    public string UsageMergeText { get => _usageMergeText; private set => Set(ref _usageMergeText, value); }
    private string _fileCopyText = string.Empty;
    /// <summary>文件复制/移动进行中文本。</summary>
    public string FileCopyText { get => _fileCopyText; private set => Set(ref _fileCopyText, value); }
    private string _downloadText = string.Empty;
    /// <summary>下载进行中文本。</summary>
    public string DownloadText { get => _downloadText; private set => Set(ref _downloadText, value); }
    /// <summary>录屏进行中指示（如「录制中 · OBS」；停止录制后清空）。</summary>
    public string RecordingText { get => _recordingText; private set => Set(ref _recordingText, value); }
    private string _clipboardSummary = string.Empty;
    public string ClipboardSummary { get => _clipboardSummary; private set => Set(ref _clipboardSummary, value); }
    private string _todoSummary = string.Empty;
    public string TodoSummary { get => _todoSummary; private set => Set(ref _todoSummary, value); }
    private string _timerText = string.Empty;
    public string TimerText { get => _timerText; private set => Set(ref _timerText, value); }
    private bool _timerPaused;
    /// <summary>番茄钟是否处于暂停态（组件显示 ⏸ 图标）。</summary>
    public bool TimerPaused { get => _timerPaused; private set => Set(ref _timerPaused, value); }
    private string _timerToolTip = string.Empty;
    /// <summary>番茄钟组件悬停提示（点击暂停/继续）。</summary>
    public string TimerToolTip { get => _timerToolTip; private set => Set(ref _timerToolTip, value); }
    private string _scheduleSummary = string.Empty;
    public string ScheduleSummary { get => _scheduleSummary; private set => Set(ref _scheduleSummary, value); }

    private IReadOnlyList<IslandComponent> _compactItems = Array.Empty<IslandComponent>();
    public IReadOnlyList<IslandComponent> CompactItems { get => _compactItems; private set => Set(ref _compactItems, value); }

    /// <summary>按 WidgetOrder 重建紧凑胶囊组件顺序（播放时含歌曲信息，闲置时去掉）。</summary>
    private void RebuildCompactItems()
    {
        // 局部工厂：按组件 Kind 解析显示图标（用户自定义优先，否则默认字形）
        IslandComponent I(string kind) => new(kind, ComponentIcons.Resolve(kind, _settings.Current.ComponentIcons));

        // 读取顺序，并补齐缺失的已知组件（兼容旧配置里只有 Time,Weather 的情况）
        var keys = (_settings.Current.WidgetOrder ?? "Time,Weather,Song")
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        foreach (var known in new[] { "Time", "Weather", "Date", "Cpu", "Ram", "Gpu", "Mic", "Cam", "Net", "Battery", "Song", "Volume", "CapsLock", "ScreenCap", "Recording", "VolumeTemp", "Usage", "FileCopy", "Download", "Clipboard", "Todo", "Timer", "Schedule", "Holiday", "Meeting" })
            if (!keys.Contains(known)) keys.Add(known);

        // 「使用中」合并胶囊：把勾选的 Mic/Cam/Meeting/Recording 合并为单个状态胶囊（默认关闭）
        var mergeItems = _settings.Current.UsageMergeItems ?? new List<string>();
        var mergeEnabled = _settings.Current.UsageMergeEnabled && mergeItems.Count > 0;
        var activeMerge = new List<string>();
        if (mergeEnabled)
        {
            if (mergeItems.Contains("Mic") && ShowIdleMic && !string.IsNullOrEmpty(MicText)) activeMerge.Add(Localization.Get("Comp_Mic"));
            if (mergeItems.Contains("Cam") && ShowIdleCam && !string.IsNullOrEmpty(CamText)) activeMerge.Add(Localization.Get("Comp_Cam"));
            if (mergeItems.Contains("Meeting") && ShowIdleMeeting && !string.IsNullOrEmpty(MeetingText)) activeMerge.Add(Localization.Get("Comp_Meeting"));
            if (mergeItems.Contains("Recording") && _settings.Current.ScreenCaptureNotifyEnabled && !string.IsNullOrEmpty(RecordingText)) activeMerge.Add(Localization.Get("ScreenCap_IslandRecording"));
        }
        var newUsageText = mergeEnabled && activeMerge.Count > 0
            ? Localization.Get("Comp_Usage") + " · " + string.Join(" · ", activeMerge)
            : string.Empty;
        if (UsageMergeText != newUsageText) UsageMergeText = newUsageText;

        var items = new List<IslandComponent>();
        var usageInserted = false;
        foreach (var key in keys)
        {
            // 合并模式：参与合并的项不再单独显示；「使用中」胶囊放在第一个激活合并项的位置
            var isMergedKey = mergeEnabled && mergeItems.Contains(key);
            if (isMergedKey)
            {
                if (!usageInserted && UsageMergeText.Length > 0)
                {
                    items.Add(I("Usage"));
                    usageInserted = true;
                }
                continue;
            }
            if (key == "Time" && ShowIdleTime) items.Add(I("Time"));
            else if (key == "Weather" && ShowIdleWeather) items.Add(I("Weather"));
            else if (key == "Date" && ShowIdleDate) items.Add(I("Date"));
            else if (key == "Cpu" && ShowIdleCpu) items.Add(I("Cpu"));
            else if (key == "Ram" && ShowIdleRam) items.Add(I("Ram"));
            else if (key == "Gpu" && ShowIdleGpu) items.Add(I("Gpu"));
            else if (key == "Mic" && ShowIdleMic && !string.IsNullOrEmpty(MicText)) items.Add(I("Mic"));
            else if (key == "Cam" && ShowIdleCam && !string.IsNullOrEmpty(CamText)) items.Add(I("Cam"));
            else if (key == "Net" && ShowIdleNet) items.Add(I("Net"));
            else if (key == "Battery" && ShowIdleBattery) items.Add(I("Battery"));
            else if (key == "Song" && HasMedia && _settings.Current.ShowMediaInfo) items.Add(I("Song"));
            else if (key == "CapsLock" && ShowIdleCapsLock && !string.IsNullOrEmpty(CapsLockText)) items.Add(I("CapsLock"));
            else if (key == "ScreenCap" && _settings.Current.ScreenCaptureNotifyEnabled && !string.IsNullOrEmpty(ScreenshotStatusText)) items.Add(I("ScreenCap"));
            else if (key == "Recording" && _settings.Current.ScreenCaptureNotifyEnabled && !string.IsNullOrEmpty(RecordingText)) items.Add(I("Recording"));
            else if (key == "VolumeTemp" && _settings.Current.VolumeTempIndicatorEnabled && !string.IsNullOrEmpty(VolumeTempText)) items.Add(I("VolumeTemp"));
            else if (key == "FileCopy" && _settings.Current.FileCopyNotifyEnabled && !string.IsNullOrEmpty(FileCopyText)) items.Add(I("FileCopy"));
            else if (key == "Download" && _settings.Current.DownloadProgressEnabled && !string.IsNullOrEmpty(DownloadText)) items.Add(I("Download"));
            else if (key == "Clipboard" && ShowIdleClipboard && !string.IsNullOrEmpty(ClipboardSummary)) items.Add(I("Clipboard"));
            else if (key == "Todo" && ShowIdleTodo && !string.IsNullOrEmpty(TodoSummary)) items.Add(I("Todo"));
            else if (key == "Timer" && ShowIdleTimer && !string.IsNullOrEmpty(TimerText)) items.Add(I("Timer"));
            else if (key == "Schedule" && ShowIdleSchedule && !string.IsNullOrEmpty(ScheduleSummary)) items.Add(I("Schedule"));
            else if (key == "Holiday" && ShowIdleHoliday && !string.IsNullOrEmpty(HolidayText)) items.Add(I("Holiday"));
            else if (key == "Meeting" && ShowIdleMeeting && !string.IsNullOrEmpty(MeetingText)) items.Add(I("Meeting"));
            else if (key == "Disk" && ShowIdleDisk) items.Add(I("Disk"));
            else if (key == "InputMethod" && ShowIdleInputMethod) items.Add(I("InputMethod"));
            else if (key == "QuickToggles" && ShowIdleQuickToggles) items.Add(I("QuickToggles"));
        }

        // 内容未变化时不重建，避免每个快照（每秒）都重创建组件导致闪烁
        if (_compactItems.Count == items.Count
            && _compactItems.Select(i => i.Kind).SequenceEqual(items.Select(i => i.Kind)))
            return;
        CompactItems = items;
    }


    private int _statsTick;
    private float? _cpuValue;
    private long _lastNetDownBytes;
    private long _lastNetUpBytes;
    private DateTime _lastNetTime = DateTime.UtcNow;
    // 网速迷你曲线：环形缓冲最近 32 秒下行速率（KB/s）
    private const int NetCurveSamples = 32;
    private readonly double[] _netCurveSamples = new double[NetCurveSamples];
    private int _netCurvePos;
    // 性能计数器复用实例：新建实例的第一次 NextValue() 会返回 0，导致 CPU 显示错乱
    private readonly System.Diagnostics.PerformanceCounter? _cpuCounter;
    private readonly System.Diagnostics.PerformanceCounter? _ramCounter;
    // GPU 占用：读取「GPU Engine」分类下所有 3D 引擎实例的最大利用率（实例随进程启停变化，需定期刷新）
    private readonly Dictionary<string, System.Diagnostics.PerformanceCounter> _gpuCounters = new();
    private bool _gpuProbed;
    private bool _gpuAvailable;

    private System.Diagnostics.PerformanceCounter? CreateCounter(string cat, string name, string? inst)
    {
        try { return inst is null ? new System.Diagnostics.PerformanceCounter(cat, name) : new System.Diagnostics.PerformanceCounter(cat, name, inst); }
        catch { return null; }
    }

    /// <summary>刷新 GPU 计数器实例并返回当前最大 3D 引擎利用率（%）；本机无 GPU Engine 计数器或新实例预热时返回 null。</summary>
    private double? SampleGpuUsage()
    {
        try
        {
            if (!_gpuProbed)
            {
                _gpuProbed = true;
                _gpuAvailable = System.Diagnostics.PerformanceCounterCategory.Exists("GPU Engine");
                if (!_gpuAvailable) return null;
            }
            if (!_gpuAvailable) return null;

            var names = new System.Diagnostics.PerformanceCounterCategory("GPU Engine").GetInstanceNames()
                .Where(n => n.IndexOf("engtype_3D", StringComparison.OrdinalIgnoreCase) >= 0).ToArray();

            // 清理已退出进程的引擎实例
            foreach (var k in _gpuCounters.Keys.ToList())
                if (!names.Contains(k, StringComparer.OrdinalIgnoreCase))
                {
                    try { _gpuCounters[k].Dispose(); } catch { }
                    _gpuCounters.Remove(k);
                }

            // 新建实例：首次 NextValue() 返回 0，先预热不显示
            bool created = false;
            foreach (var n in names)
            {
                if (_gpuCounters.ContainsKey(n)) continue;
                try
                {
                    var c = new System.Diagnostics.PerformanceCounter("GPU Engine", "Utilization Percentage", n);
                    _gpuCounters[n] = c;
                    created = true;
                }
                catch { }
            }
            if (created) return null;

            double max = 0;
            foreach (var c in _gpuCounters.Values)
            {
                try { max = Math.Max(max, c.NextValue()); } catch { }
            }
            return max;
        }
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
            if (hasBattery)
            {
                // 电池预估剩余时间（秒 → 时:分；充电中/未知时仅显示百分比）
                var text = $"{battery:0}%";
                var remainSecs = ps.BatteryLifeRemaining;
                if (remainSecs > 0 && battery > 0)
                {
                    var ts = TimeSpan.FromSeconds(remainSecs);
                    var t = ts.TotalHours >= 1 ? $"{(int)ts.TotalHours}:{ts.Minutes:00}" : $"{Math.Max(1, ts.Minutes)}:{ts.Seconds:00}";
                    text += $" \u00b7 {t}";
                }
                BatteryText = text;
            }
            else BatteryText = string.Empty;

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

            // 充电完成提醒（每个充电周期提醒一次；连接电源且电量达到阈值时）
            if (hasBattery && _settings.Current.ChargedNotifyEnabled && _settings.Current.ChargedThreshold > 0)
            {
                var acOnline = ps.PowerLineStatus == System.Windows.Forms.PowerLineStatus.Online;
                if (!_chargedNotified && acOnline && battery >= _settings.Current.ChargedThreshold)
                {
                    _chargedNotified = true;
                    ChargedRequested?.Invoke((int)Math.Round(battery));
                }
                if (!acOnline || battery < _settings.Current.ChargedThreshold - 5) _chargedNotified = false;
            }

            // 磁盘剩余空间（系统盘；仅组件开启或提醒开启时查询，查询开销小）
            if (ShowIdleDisk || _settings.Current.DiskAlertEnabled)
            {
                try
                {
                    var root = Path.GetPathRoot(Environment.SystemDirectory);
                    if (root is not null)
                    {
                        var di = new DriveInfo(root);
                        if (di.IsReady)
                        {
                            var freeGb = di.AvailableFreeSpace / (1024d * 1024d * 1024d);
                            var totalGb = di.TotalSize / (1024d * 1024d * 1024d);
                            DiskText = $"{root.TrimEnd('\\')} {freeGb:0}GB / {totalGb:0}GB";
                            // 剩余空间低于阈值提醒（恢复后复位，每个周期只提醒一次）
                            if (_settings.Current.DiskAlertEnabled && _settings.Current.DiskAlertThresholdGB > 0)
                            {
                                if (!_diskAlertNotified && freeGb < _settings.Current.DiskAlertThresholdGB)
                                {
                                    _diskAlertNotified = true;
                                    DiskLowRequested?.Invoke((int)Math.Floor(freeGb));
                                }
                                if (freeGb > _settings.Current.DiskAlertThresholdGB + 5) _diskAlertNotified = false;
                            }
                        }
                        else DiskText = string.Empty;
                    }
                    else DiskText = string.Empty;
                }
                catch { DiskText = string.Empty; }
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
                // GPU：与 CPU 同节奏采样（新实例先预热，避免首采显示 0%）
                if (ShowIdleGpu)
                {
                    var gpu = SampleGpuUsage();
                    GpuText = gpu.HasValue ? $"{gpu.Value:0}%" : "--";
                }
                // 麦克风/摄像头占用（隐私注册表，Start > Stop 表示占用中；仅勾选时轮询）
                if (ShowIdleMic || ShowIdleCam)
                {
                    var (mic, cam) = PrivacyDeviceMonitor.GetUsage();
                    if (ShowIdleMic) MicText = mic ? Localization.Get("Comp_Mic") : string.Empty;
                    if (ShowIdleCam) CamText = cam ? Localization.Get("Comp_Cam") : string.Empty;
                    RebuildCompactItems();
                }
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

            // 网络速度（每秒）：下行文字 + 上行文字 + 迷你曲线
            try
            {
                var iface = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
                    .FirstOrDefault(i => i.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up
                        && i.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback && i.Speed > 0);
                if (iface is not null)
                {
                    var stats = iface.GetIPv4Statistics();
                    var now = DateTime.UtcNow;
                    var secs = Math.Max(0.1, (now - _lastNetTime).TotalSeconds);
                    var downKbs = (stats.BytesReceived - _lastNetDownBytes) / 1024.0 / secs;
                    var upKbs = (stats.BytesSent - _lastNetUpBytes) / 1024.0 / secs;
                    _lastNetDownBytes = stats.BytesReceived;
                    _lastNetUpBytes = stats.BytesSent;
                    _lastNetTime = now;
                    NetText = FormatKbs(downKbs);
                    NetTextUp = FormatKbs(upKbs);
                    PushNetSample(downKbs);
                }
                else { NetText = string.Empty; NetTextUp = string.Empty; }
            }
            catch { NetText = string.Empty; NetTextUp = string.Empty; }

            // 节假日倒计时（仅勾选显示时计算，纯本地）
            if (ShowIdleHoliday)
            {
                var (hName, hDays) = NextHolidayInfo();
                HolidayText = hDays < 0 ? string.Empty : hDays == 0 ? $"今日 {hName}" : $"{hName} {hDays} 天后";
            }
            else if (HolidayText.Length > 0) HolidayText = string.Empty;
        }
        catch { /* 忽略性能计数器/电量异常 */ }
    }

    private static string FormatKbs(double kbs) =>
        kbs >= 1024 ? $"{kbs / 1024:0.0} MB/s" : $"{kbs:0} KB/s";

    /// <summary>把一次下行采样推入环形缓冲，并按需重建曲线点串（仅网络组件勾选时才有意义）。</summary>
    private void PushNetSample(double downKbs)
    {
        _netCurveSamples[_netCurvePos] = downKbs;
        _netCurvePos = (_netCurvePos + 1) % NetCurveSamples;
        if (_settings.Current.NetCurveEnabled && ShowIdleNet)
            NetCurvePoints = BuildNetCurvePoints();
    }

    /// <summary>生成 32 秒下行速率曲线点串（自动按峰值缩放）。</summary>
    private string BuildNetCurvePoints()
    {
        const double w = 64, h = 14;
        double max = 1.0;
        foreach (var v in _netCurveSamples) if (v > max) max = v;
        var sb = new System.Text.StringBuilder();
        for (int i = 0; i < NetCurveSamples; i++)
        {
            var idx = (_netCurvePos + i) % NetCurveSamples;
            var v = _netCurveSamples[idx];
            var x = i * (w / (NetCurveSamples - 1));
            var y = h - (Math.Min(v, max) / max) * (h - 1);
            sb.Append(x.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture)).Append(',')
              .Append(y.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture));
            if (i < NetCurveSamples - 1) sb.Append(' ');
        }
        return sb.ToString();
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
    private string _gpuText = string.Empty;
    public string GpuText { get => _gpuText; private set => Set(ref _gpuText, value); }
    private string _micText = string.Empty;
    /// <summary>麦克风占用指示文本（占用中显示「麦克风」，否则为空，组件随之出现/消失）。</summary>
    public string MicText { get => _micText; private set => Set(ref _micText, value); }
    private string _camText = string.Empty;
    /// <summary>摄像头占用指示文本（占用中显示「摄像头」，否则为空）。</summary>
    public string CamText { get => _camText; private set => Set(ref _camText, value); }
    private string _netText = string.Empty;
    public string NetText { get => _netText; private set => Set(ref _netText, value); }
    private string _netTextUp = string.Empty;
    public string NetTextUp { get => _netTextUp; private set => Set(ref _netTextUp, value); }
    private string _netCurvePoints = string.Empty;
    public string NetCurvePoints { get => _netCurvePoints; private set => Set(ref _netCurvePoints, value); }
    private string _batteryText = string.Empty;
    public string BatteryText { get => _batteryText; private set => Set(ref _batteryText, value); }
    private string _inputMethodText = string.Empty;
    /// <summary>输入法状态文本（如「中 · 微软拼音」）。</summary>
    public string InputMethodText { get => _inputMethodText; private set => Set(ref _inputMethodText, value); }
    /// <summary>输入法组件提示（点击切换中/英）。</summary>
    public string InputMethodHint => Localization.Get("Comp_InputMethodHint");
    private string _quickWifiText = string.Empty;
    /// <summary>快捷开关：WiFi 状态文本（如「WiFi 开」）。</summary>
    public string QuickWifiText { get => _quickWifiText; private set => Set(ref _quickWifiText, value); }
    private string _quickBtText = string.Empty;
    /// <summary>快捷开关：蓝牙状态文本。</summary>
    public string QuickBtText { get => _quickBtText; private set => Set(ref _quickBtText, value); }
    private string _quickNightText = string.Empty;
    /// <summary>快捷开关：夜间模式状态文本。</summary>
    public string QuickNightText { get => _quickNightText; private set => Set(ref _quickNightText, value); }
    private string _quickMuteText = string.Empty;
    /// <summary>快捷开关：静音状态文本。</summary>
    public string QuickMuteText { get => _quickMuteText; private set => Set(ref _quickMuteText, value); }
    /// <summary>快捷开关组件提示（点击各开关即时切换）。</summary>
    public string QuickTogglesHint => Localization.Get("Comp_QuickTogglesHint");
    private string _diskText = string.Empty;
    /// <summary>系统盘剩余空间文本（如「C: 385GB / 510GB」；无可用盘符时为空）。</summary>
    public string DiskText { get => _diskText; private set => Set(ref _diskText, value); }

    /// <summary>切歌时触发（参数：歌名、歌手），用于 Now Playing 横幅。</summary>
    public event Action<string, string>? NowPlayingRequested;
    /// <summary>低电量触发（参数：电量百分比）。</summary>
    public event Action<int>? LowBatteryRequested;
    /// <summary>充电完成触发（参数：电量百分比）。</summary>
    public event Action<int>? ChargedRequested;
    private bool _lowBatteryNotified;
    private bool _chargedNotified;
    /// <summary>磁盘剩余不足触发（参数：剩余 GB）。</summary>
    public event Action<int>? DiskLowRequested;
    private bool _diskAlertNotified;
    private string _holidayText = string.Empty;
    public string HolidayText { get => _holidayText; private set => Set(ref _holidayText, value); }
    private string _meetingText = string.Empty;
    /// <summary>会议中状态文本（如「会议中 · Microsoft Teams」；非会议时为空，组件随之消失）。</summary>
    public string MeetingText { get => _meetingText; private set => Set(ref _meetingText, value); }

    // ── 农历 / 节气（纯本地计算，ChineseLunisolarCalendar + 节气近似公式，不联网）──
    private static readonly string[] LunarMonths =
        { "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "冬月", "腊月" };
    private static readonly string[] LunarDays =
        { "初一", "初二", "初三", "初四", "初五", "初六", "初七", "初八", "初九", "初十",
          "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十",
          "廿一", "廿二", "廿三", "廿四", "廿五", "廿六", "廿七", "廿八", "廿九", "三十" };
    private static readonly string[] SolarTermNames =
        { "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至",
          "小暑", "大暑", "立秋", "处暑", "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至" };
    // 24 节气日期近似公式常数（平气法，1900-2100 误差 ≤1 天）
    private static readonly double[] SolarTermBase =
        { 0, 21208, 42467, 63836, 85337, 107014, 128867, 150921, 173149, 195551, 218072, 240693,
          263343, 285989, 308563, 331033, 353350, 375494, 397447, 419210, 440795, 462224, 483532, 504758 };

    /// <summary>日期组件文本：公历 + 农历月日（+ 当日节气）。</summary>
    private string FormatDateText(DateTime now)
    {
        var baseText = now.ToString("M月d日 ddd", System.Globalization.CultureInfo.GetCultureInfo("zh-CN"));
        if (!_settings.Current.ShowLunarOnDate) return baseText;
        try
        {
            var cal = new System.Globalization.ChineseLunisolarCalendar();
            var year = cal.GetYear(now);
            var monthVal = cal.GetMonth(now);
            var leap = cal.IsLeapMonth(year, monthVal);
            var monthNum = leap ? monthVal - 1 : monthVal;
            var lunar = (leap ? "闰" : "") + LunarMonths[Math.Clamp(monthNum, 1, 12) - 1]
                + LunarDays[Math.Clamp(cal.GetDayOfMonth(now), 1, 30) - 1];
            var suffix = " 农历" + lunar;
            var term = SolarTermOf(now);
            if (term.Length > 0) suffix += " · " + term;
            return baseText + suffix;
        }
        catch { return baseText; } // 计算失败优雅降级为纯公历
    }

    /// <summary>当日节气名（不是节气返回空字符串）。</summary>
    private static string SolarTermOf(DateTime now)
    {
        try
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var baseMs = new DateTime(1900, 1, 6, 2, 5, 0, DateTimeKind.Utc).Subtract(epoch).TotalMilliseconds;
            for (int i = 0; i < SolarTermBase.Length; i++)
            {
                var ms = 31556925974.7 * (now.Year - 1900) + SolarTermBase[i] * 60000 + baseMs;
                var d = DateTimeOffset.FromUnixTimeMilliseconds((long)ms).UtcDateTime;
                if (d.Month == now.Month && d.Day == now.Day) return SolarTermNames[i];
            }
            return string.Empty;
        }
        catch { return string.Empty; }
    }

    /// <summary>内置节假日表（年份+公历/农历日期整理，纯本地不联网；如需可自行补充条目）。</summary>
    private static readonly (string Name, int Year, int Month, int Day)[] HolidayTable =
    {
        ("元旦", 2026, 1, 1), ("春节", 2026, 2, 17), ("清明", 2026, 4, 5), ("劳动节", 2026, 5, 1),
        ("端午", 2026, 6, 19), ("中秋", 2026, 9, 25), ("国庆", 2026, 10, 1),
        ("元旦", 2027, 1, 1), ("春节", 2027, 2, 6), ("清明", 2027, 4, 5), ("劳动节", 2027, 5, 1),
        ("端午", 2027, 6, 9), ("中秋", 2027, 9, 15), ("国庆", 2027, 10, 1),
    };

    /// <summary>下一个节假日名称与剩余天数（今天为 0；找不到返回空）。</summary>
    private static (string Name, int Days) NextHolidayInfo()
    {
        var today = DateTime.Today;
        var best = ("", -1);
        foreach (var h in HolidayTable)
        {
            DateTime d;
            try { d = new DateTime(h.Year, h.Month, h.Day); } catch { continue; }
            if (d < today) continue;
            var days = (int)(d - today).TotalDays;
            if (best.Item2 < 0 || days < best.Item2) best = (h.Name, days);
        }
        return best;
    }

    // ── 上岛推送（第三方软件推送到灵动岛）──────────────────
    public IReadOnlyList<IslandPush> ActivePushes => _pushes;

    public IslandPush? ActivePush
    {
        get => _activePush;
        private set
        {
            if (!Set(ref _activePush, value)) return;
            OnPropertyChanged(nameof(HasActivePush));
            OnPropertyChanged(nameof(ActivePushIcon));
            OnPropertyChanged(nameof(ActivePushTitle));
            OnPropertyChanged(nameof(ActivePushSubtitle));
            OnPropertyChanged(nameof(ActivePushHasSubtitle));
            OnPropertyChanged(nameof(ActivePushBody));
            OnPropertyChanged(nameof(ActivePushHasBody));
            OnPropertyChanged(nameof(ActivePushHasProgress));
            OnPropertyChanged(nameof(ActivePushProgress));
            OnPropertyChanged(nameof(ActivePushHasImage));
            OnPropertyChanged(nameof(ActivePushImageSource));
            OnPropertyChanged(nameof(ActivePushHasButtons));
            OnPropertyChanged(nameof(ActivePushButtons));
            OnPropertyChanged(nameof(ActivePushHasClick));
            OnPropertyChanged(nameof(ActivePushAccent));
            UpdateVisibility(); // 上岛卡片显示/消失影响灵动岛可见性
        }
    }

    public bool HasActivePush => ActivePush is not null;
    public string ActivePushIcon => string.IsNullOrEmpty(ActivePush?.Icon) ? "\uE7F4" : ActivePush!.Icon;
    public string ActivePushTitle => ActivePush?.Title ?? string.Empty;
    public string ActivePushSubtitle => ActivePush?.Subtitle ?? string.Empty;
    public bool ActivePushHasSubtitle => !string.IsNullOrEmpty(ActivePush?.Subtitle);
    public string ActivePushBody => ActivePush?.Body ?? string.Empty;
    public bool ActivePushHasBody => !string.IsNullOrEmpty(ActivePush?.Body);
    public bool ActivePushHasProgress => ActivePush?.EffectiveProgress is not null;
    public double ActivePushProgress => Math.Clamp(ActivePush?.EffectiveProgress ?? 0, 0, 1);

    /// <summary>上岛推送图片（v3）：data URI 或 http(s) 链接。</summary>
    public bool ActivePushHasImage => !string.IsNullOrEmpty(ActivePush?.Image);

    /// <summary>上岛推送图片源：data URI 解码为本地位图；http(s) 直接加载。</summary>
    public ImageSource? ActivePushImageSource
    {
        get
        {
            var img = ActivePush?.Image;
            if (string.IsNullOrWhiteSpace(img)) return null;
            if (img.StartsWith("data:image", StringComparison.OrdinalIgnoreCase))
            {
                var idx = img.IndexOf("base64,", StringComparison.OrdinalIgnoreCase);
                if (idx < 0) return null;
                try
                {
                    var b64 = img.Substring(idx + 7).Trim();
                    var bytes = Convert.FromBase64String(b64);
                    using var ms = new MemoryStream(bytes);
                    var bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.StreamSource = ms;
                    bmp.EndInit();
                    bmp.Freeze();
                    return bmp;
                }
                catch { return null; }
            }
            if (img.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                img.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                try { return new BitmapImage(new Uri(img, UriKind.Absolute)); }
                catch { return null; }
            }
            return null;
        }
    }
    public bool ActivePushHasButtons => ActivePush?.Buttons is { Count: > 0 };
    public IReadOnlyList<IslandPushButton> ActivePushButtons
        => (IReadOnlyList<IslandPushButton>)(ActivePush?.Buttons ?? new List<IslandPushButton>());
    /// <summary>整卡点击回跳（click）是否已配置。</summary>
    public bool ActivePushHasClick => ActivePush?.Click is not null;
    /// <summary>强调色（#RRGGBB / #AARRGGBB），未配置时为空字符串，由 UI 用类型默认色。</summary>
    public string ActivePushAccent => ActivePush?.Accent ?? string.Empty;

    /// <summary>估算文本宽度：中文/全角按 cjkPx，ASCII 按 asciiPx。</summary>
    private static double MeasureText(string s, double cjkPx, double asciiPx)
    {
        double w = 0;
        foreach (var ch in s) w += ch > 0x2E7F ? cjkPx : asciiPx;
        return w;
    }

    /// <summary>天气图标（按 WMO weather_code 选 emoji）。</summary>
    private static string WeatherIcon(int code) => code switch
    {
        0 => "\u2600\uFE0F",          // ☀️
        1 => "\uD83C\uDF24\uFE0F",   // 🌤️
        2 => "\u26C5",                  // ⛅
        3 => "\u2601\uFE0F",          // ☁️
        45 or 48 => "\uD83C\uDF2B\uFE0F", // 🌫️
        >= 51 and <= 57 => "\uD83C\uDF26\uFE0F", // 🌦️
        >= 61 and <= 67 => "\uD83C\uDF27\uFE0F", // 🌧️
        >= 71 and <= 77 => "\uD83C\uDF28\uFE0F", // 🌨️
        >= 80 and <= 82 => "\uD83C\uDF27\uFE0F", // 🌧️
        85 or 86 => "\uD83C\uDF28\uFE0F",        // 🌨️
        >= 95 => "\u26C8\uFE0F",      // ⛈️
        _ => "\uD83C\uDF21\uFE0F",   // 🌡️
    };

    /// <summary>天气描述（本地化）。</summary>
    private static string WeatherDesc(int code) => code switch
    {
        0 => Localization.Get("Weather_Desc0"),
        1 or 2 => Localization.Get("Weather_Desc12"),
        3 => Localization.Get("Weather_Desc3"),
        45 or 48 => Localization.Get("Weather_Desc45"),
        >= 51 and <= 57 => Localization.Get("Weather_Desc51"),
        >= 61 and <= 67 => Localization.Get("Weather_Desc61"),
        >= 71 and <= 77 => Localization.Get("Weather_Desc71"),
        >= 80 and <= 82 => Localization.Get("Weather_Desc80"),
        85 or 86 => Localization.Get("Weather_Desc85"),
        >= 95 => Localization.Get("Weather_Desc95"),
        _ => Localization.Get("Weather_Desc12"),
    };

    /// <summary>紧凑模式天气文案：图标 + 温度 + 描述 + 今日高/低温。</summary>
    private static string FormatWeatherCompact(WeatherInfo w)
    {
        var range = w.Low < w.High ? $" {w.Low:0}/{w.High:0}°" : string.Empty;
        return $"{WeatherIcon(w.Code)} {w.Temperature:0}° {WeatherDesc(w.Code)}{range}";
    }

    /// <summary>悬浮提示的完整天气信息（体感 / 湿度 / 风速 / 降水 / 更新时间）。</summary>
    private string FormatWeatherDetail(WeatherInfo w)
    {
        var feels = $"{Localization.Get("Weather_Feels")} {w.FeelsLike:0}°";
        var hum = $"{Localization.Get("Weather_Humidity")} {w.Humidity:0}%";
        var wind = $"{Localization.Get("Weather_Wind")} {w.WindSpeed:0}km/h";
        var precip = $"{Localization.Get("Weather_Precip")} {w.Precipitation:0}mm";
        var updated = $"{Localization.Get("Weather_Updated")} {w.Updated}";
        return $"{WeatherDesc(w.Code)} · {string.Join(" · ", feels, hum, wind, precip, updated)}";
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
                    case "Weather": w += Math.Min(MeasureText(WeatherText, 12, 6.5) + 8, 140); break;
                    case "Date": w += Math.Min(MeasureText(DateText, 13, 7) + 8, 100); break;
                    case "Cpu": w += MeasureText(CpuText, 11, 6) + 16; break;
                    case "Ram": w += MeasureText(RamText, 11, 6) + 16; break;
                    case "Gpu": w += MeasureText(GpuText, 11, 6) + 16; break;
                    case "Mic": w += MeasureText(MicText, 11, 6) + 16; break;
                    case "Cam": w += MeasureText(CamText, 11, 6) + 16; break;
                    case "Net": w += MeasureText(NetText, 11, 6) + 16; break;
                    case "Battery": w += MeasureText(BatteryText, 11, 6) + 16; break;
                    case "Holiday": w += Math.Min(MeasureText(HolidayText, 12, 6.5) + 8, 120); break;
                    case "Meeting": w += Math.Min(MeasureText(MeetingText, 11, 6) + 16, 160); break;
                    case "ScreenCap": w += MeasureText(ScreenshotStatusText, 11, 6) + 16; break;
                    case "Recording": w += Math.Min(MeasureText(RecordingText, 11, 6) + 16, 180); break;
                    case "VolumeTemp": w += MeasureText(VolumeTempText, 11, 6) + 24; break;
                    case "Usage": w += Math.Min(MeasureText(UsageMergeText, 11, 6) + 16, 200); break;
                    case "FileCopy": w += Math.Min(MeasureText(FileCopyText, 11, 6) + 16, 220); break;
                    case "Download": w += Math.Min(MeasureText(DownloadText, 11, 6) + 16, 220); break;
                    case "Song":
                        w += 40 + 6
                            + Math.Min(MeasureText(Title, 13, 7), 140)
                            + 6 + Math.Min(MeasureText(Artist, 11, 6), 100);
                        if (HasLyrics) w += 8 + Math.Min(MeasureText(CurrentLyricText, 12, 6.5), 300);
                        break;
                }
                w += 8; // 组件间右边距（模板 Margin 0,0,8,0 左右），这里按单边即可
            }
            if (HasMedia) w += 68 + 34; // 播放/暂停 + 下一首 按钮
            return Math.Clamp(w + 4, 260, 720);
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
            if (!string.IsNullOrEmpty(ActivePush.Subtitle)) h += 15; // 副标题行
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

    /// <summary>上岛 API 收到推送：加入/更新推送队列（同 id 覆盖、保留原过期时间），并按优先级刷新显示。</summary>
    public void PushIsland(IslandPush push)
    {
        var idx = _pushes.FindIndex(p => string.Equals(p.Id, push.Id, StringComparison.Ordinal));
        if (idx >= 0)
        {
            // 更新内容，保持原过期时间
            if (_pushes[idx].ExpiresAt is DateTime e) push.ExpiresAt = e;
            _pushes[idx] = push;
        }
        else
        {
            _pushes.Add(push);
        }
        RecomputeActivePush();
        AppLogger.Info($"Island push: '{push.Title}' (id={push.Id}, priority={push.Priority ?? "normal"})");
    }

    /// <summary>上岛 API 移除/过期指定推送。</summary>
    public void RemoveIslandPush(string id)
    {
        var removed = _pushes.RemoveAll(p => string.Equals(p.Id, id, StringComparison.Ordinal)) > 0;
        if (removed) RecomputeActivePush();
    }

    /// <summary>用户点击/关闭当前上岛卡片：只关闭当前条，队列中还有推送则继续显示。</summary>
    public void DismissActivePush()
    {
        if (ActivePush is null) return;
        _pushes.RemoveAll(p => ReferenceEquals(p, ActivePush));
        RecomputeActivePush();
    }

    /// <summary>按 优先级高→低、入队 早→晚 重排队列并选出当前显示项（过期项同时清除）。</summary>
    private void RecomputeActivePush()
    {
        _pushes.RemoveAll(p => p.ExpiresAt is DateTime e && e <= DateTime.UtcNow);
        // 稳定排序：优先级高→低，同级保持入队顺序（早→晚）
        var sorted = _pushes
            .Select((p, i) => (p, i))
            .OrderByDescending(t => t.p.PriorityRank)
            .ThenBy(t => t.i)
            .Select(t => t.p)
            .ToList();
        _pushes.Clear();
        _pushes.AddRange(sorted);
        ActivePush = _pushes.FirstOrDefault();
    }

    // ── 效率工具组件刷新（服务事件 → 摘要文本 → 重建组件）─────────
    /// <summary>番茄钟阶段结束（由上层弹通知）。</summary>
    public event Action<PomodoroPhase>? PomodoroCompletedRequested;

    private void OnKeyboardStateChanged(string key)
    {
        var label = key switch
        {
            "CapsLock" => "Caps",
            "NumLock" => "Num",
            _ => "ScrLk",
        };
        var on = key switch
        {
            "CapsLock" => _keyboard.Current.Caps,
            "NumLock" => _keyboard.Current.Num,
            _ => _keyboard.Current.Scroll,
        };
        CapsLockText = $"{label} {(on ? Localization.Get("On") : Localization.Get("Off"))}";
        _capsLockSecondsLeft = Math.Max(1, _settings.Current.KeyIndicatorSeconds);
        RebuildCompactItems();
    }

    /// <summary>每秒递减键盘指示灯剩余秒数，到 0 后清空（组件消失）。</summary>
    private void UpdateCapsLockCountdown()
    {
        if (_capsLockSecondsLeft <= 0) return;
        if (--_capsLockSecondsLeft <= 0)
        {
            CapsLockText = string.Empty;
            RebuildCompactItems();
        }
    }
    /// <summary>截图事件：灵动岛显示「已截图」临时指示（由 ScreenCaptureMonitor 事件转发）。</summary>
    public void NotifyScreenshotTaken()
    {
        if (!_settings.Current.ScreenCaptureNotifyEnabled) return;
        ScreenshotStatusText = Localization.Get("ScreenCap_IslandScreenshot");
        _screenshotSecondsLeft = Math.Max(1, _settings.Current.KeyIndicatorSeconds);
        RebuildCompactItems();
        UpdateVisibility(); // 无媒体且隐藏时也要能临时显示
    }

    /// <summary>录制状态变化：进入/退出录制时更新灵动岛「录制中」指示。</summary>
    public void SetRecordingStatus(bool recording, string app)
    {
        if (!_settings.Current.ScreenCaptureNotifyEnabled) return;
        RecordingText = recording
            ? $"{Localization.Get("ScreenCap_IslandRecording")}{(string.IsNullOrEmpty(app) ? string.Empty : " · " + app)}"
            : string.Empty;
        RebuildCompactItems();
        UpdateVisibility(); // 录制状态变化时重新评估可见性
    }

    /// <summary>每秒递减截图指示剩余秒数，到 0 后清空（组件消失）。</summary>
    private void UpdateScreenshotCountdown()
    {
        if (_screenshotSecondsLeft <= 0) return;
        if (--_screenshotSecondsLeft <= 0)
        {
            ScreenshotStatusText = string.Empty;
            RebuildCompactItems();
            UpdateVisibility(); // 临时指示消失后若原本隐藏则恢复隐藏
        }
    }
    /// <summary>音量/静音变化：显示临时上岛指示（几秒后自动消失）。</summary>
    public void ShowVolumeTemp(int percent, bool muted)
    {
        if (!_settings.Current.VolumeTempIndicatorEnabled) return;
        VolumeTempText = muted ? Localization.Get("VolumeTemp_Muted") : $"{percent}%";
        _volumeTempSecondsLeft = Math.Max(1, _settings.Current.VolumeTempIndicatorSeconds);
        RebuildCompactItems();
        UpdateVisibility(); // 无媒体且隐藏时也要能临时显示
    }

    /// <summary>每秒轮询系统音量（仅开启音量指示时）；变化时上岛。无变化时近乎零开销。</summary>
    private void PollVolumeTemp()
    {
        if (!_settings.Current.VolumeTempIndicatorEnabled) return;
        try
        {
            var v = SystemVolume.GetVolume();
            var muted = SystemVolume.IsMuted();
            if (v.HasValue && (Math.Abs(v.Value - _lastVolumeTempValue) > 0.001 || muted != _lastVolumeTempMuted))
            {
                _lastVolumeTempValue = v.Value;
                _lastVolumeTempMuted = muted;
                ShowVolumeTemp((int)Math.Round(v.Value * 100), muted);
            }
        }
        catch { /* 音频服务不可用时忽略 */ }
    }

    /// <summary>每秒递减音量指示剩余秒数，到 0 后清空（组件消失）。</summary>
    private void UpdateVolumeTempCountdown()
    {
        if (_volumeTempSecondsLeft <= 0) return;
        if (--_volumeTempSecondsLeft <= 0)
        {
            VolumeTempText = string.Empty;
            RebuildCompactItems();
            UpdateVisibility();
        }
    }

    /// <summary>每秒检测前台窗口是否正在复制/移动文件；变化时更新上岛文本。</summary>
    private void PollFileCopy()
    {
        if (!_settings.Current.FileCopyNotifyEnabled) return;
        var newText = FileTransferMonitor.IsCopyingOrMoving() ? Localization.Get("FileCopy_IslandText") : string.Empty;
        if (FileCopyText != newText)
        {
            FileCopyText = newText;
            RebuildCompactItems();
            UpdateVisibility(); // 复制开始时若隐藏则临时显示；结束时若原本隐藏则恢复隐藏
        }
    }

    /// <summary>每秒扫描下载目录中的浏览器临时文件；变化时更新上岛文本。</summary>
    private void PollDownloadProgress()
    {
        if (!_settings.Current.DownloadProgressEnabled) return;
        var count = DownloadDetector.ActiveDownloadCount();
        var newText = count > 0 ? string.Format(Localization.Get("Download_IslandText"), count) : string.Empty;
        if (DownloadText != newText)
        {
            DownloadText = newText;
            RebuildCompactItems();
            UpdateVisibility(); // 下载开始时若隐藏则临时显示；结束时若原本隐藏则恢复隐藏
        }
    }

    private void RefreshClipboardSummary() { ClipboardSummary = _clipboard.Summary; RebuildCompactItems(); }
    private void RefreshTodoSummary() { TodoSummary = _todo.Summary; RebuildCompactItems(); }
    private void RefreshScheduleSummary() { ScheduleSummary = _schedule.Summary; RebuildCompactItems(); }
    private void RefreshTimerText()
    {
        TimerPaused = _pomodoro.IsPaused;
        TimerToolTip = Localization.Get("Timer_ToggleHint");
        TimerText = _pomodoro.Phase == PomodoroPhase.Stopped ? string.Empty : _pomodoro.ClockText;
        RebuildCompactItems();
    }
    private void OnPomodoroCompleted(PomodoroPhase phase)
    {
        RefreshTimerText();
        PomodoroCompletedRequested?.Invoke(phase);
    }

    /// <summary>点击灵动岛上的番茄钟组件：暂停/继续切换（不抛异常）。</summary>
    public void ToggleTimerPause()
    {
        try
        {
            _pomodoro.TogglePause();
            RefreshTimerText();
        }
        catch (Exception ex) { AppLogger.Warn($"Timer toggle failed: {ex.Message}"); }
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

    /// <summary>执行整卡点击回跳动作（推送配置了 click 时），执行后关闭该条推送。</summary>
    public void ExecutePushClick()
    {
        if (ActivePush?.Click is not IslandPushButton click || string.IsNullOrWhiteSpace(click.Value)) return;
        ExecutePushAction(click);
        DismissActivePush();
    }

    /// <summary>每秒检查上岛推送是否过期（由 _widgetTimer 调用）。</summary>
    private void CheckPushExpiry()
    {
        if (_pushes.Any(p => p.ExpiresAt is DateTime e && e <= DateTime.UtcNow))
            RecomputeActivePush();
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
        _wave.SetPlaying(IsPlaying);
        OnPropertyChanged(nameof(VolumeText));

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
        _wave.SetPlaying(false);
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
        if (_settings.Current.BilingualLyrics)
        {
            // 双语歌词：相邻时间戳的翻译行自动合并到主句下方显示
            var pairs = LrcParser.PairLines(result.Document.Lines, TimeSpan.FromMilliseconds(250), enable: true);
            LyricLines = pairs.Select(x => new LyricLineViewModel(x.Main, x.Translation)).ToList();
        }
        else
        {
            LyricLines = result.Document.Lines.Select(l => new LyricLineViewModel(l)).ToList();
        }
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
            || comp.TitleWhenIdle || comp.ArtistWhenIdle || comp.LyricsWhenIdle || comp.ProgressWhenIdle
            || comp.DiskWhenIdle;
        var showWidgets = !hasMedia && (_settings.Current.ShowWidgetsWhenNoMedia || alwaysVisible || anyIdleComp);
        ShowIdleWidgets = !hasMedia; // 空闲面板可见性（内部按组件勾选）

        // 有上岛推送时也要显示灵动岛（否则第三方推送看不到）
        // 截图/录屏/音量/复制/下载等临时指示激活时也强制显示（到期自动消失后恢复隐藏）
        var anyTempStatus = (ScreenshotStatusText.Length > 0 || RecordingText.Length > 0 || VolumeTempText.Length > 0
            || FileCopyText.Length > 0 || DownloadText.Length > 0);
        var show = !_userHidden && (hasMedia || showWidgets || HasActivePush || !_settings.Current.HideWhenNoMedia || anyTempStatus);
        // 常驻时不因暂停而隐藏
        if (!alwaysVisible && hasMedia && Status == PlaybackStatus.Paused && !_settings.Current.ShowWhenPaused)
            show = false;

        // 条件规则引擎：隐藏/强制显示/强制收起（多个规则叠加，隐藏优先）
        var ruleEval = RuleEngine.Evaluate(_settings.Current, hasMedia, _snapshot?.Track.SourceAppId);
        if (ruleEval.ForceHide) show = false;
        else if (ruleEval.ForceShow) show = true;
        if (ruleEval.ForceCollapse && IsExpanded) IsExpanded = false;

        if (!ShowIdleWeather) { WeatherText = string.Empty; WeatherDetailText = string.Empty; } // 仅天气组件不显示时才清空
        if (!ShowIdleMic) MicText = string.Empty;    // 麦克风/摄像头组件不勾选时清空
        if (!ShowIdleCam) CamText = string.Empty;

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
        OnPropertyChanged(nameof(ShowIdleMic));
        OnPropertyChanged(nameof(ShowIdleCam));
        OnPropertyChanged(nameof(ShowIdleRam));
        OnPropertyChanged(nameof(ShowIdleNet));
        OnPropertyChanged(nameof(ShowIdleBattery));
        OnPropertyChanged(nameof(ShowIdleVolume));
        OnPropertyChanged(nameof(ShowIdleCapsLock));
        OnPropertyChanged(nameof(ShowIdleClipboard));
        OnPropertyChanged(nameof(ShowIdleTodo));
        OnPropertyChanged(nameof(ShowIdleTimer));
        OnPropertyChanged(nameof(ShowIdleSchedule));
        OnPropertyChanged(nameof(ShowIdleHoliday));
        OnPropertyChanged(nameof(ShowIdleMeeting));
        OnPropertyChanged(nameof(ShowIdleDisk));
        OnPropertyChanged(nameof(ShowIdleInputMethod));
        OnPropertyChanged(nameof(ShowIdleQuickToggles));
        OnPropertyChanged(nameof(HolidayText));
        OnPropertyChanged(nameof(VolumeText));

        IsVisible = show;
    }

    /// <summary>点击输入法组件：切换中/英输入法后立即刷新状态文本。</summary>
    public void ToggleInputMethod()
    {
        InputMethodMonitor.ToggleChineseEnglish();
        InputMethodText = InputMethodMonitor.GetStatusText();
    }

    /// <summary>刷新快捷开关状态文本（Radio 2 秒缓存，其余本地即时读取；值不变不触发通知）。</summary>
    public async void RefreshQuickToggles()
    {
        try
        {
            await QuickSwitchService.RefreshRadiosAsync();
            QuickWifiText = FormatQuickSwitch("Quick_Wifi", QuickSwitchService.HasWifi, QuickSwitchService.IsWifiOn);
            QuickBtText = FormatQuickSwitch("Quick_Bluetooth", QuickSwitchService.HasBluetooth, QuickSwitchService.IsBluetoothOn);
            QuickNightText = Localization.Get("Quick_Night") + " " + (QuickSwitchService.IsNightMode ? Localization.Get("Quick_On") : Localization.Get("Quick_Off"));
            QuickMuteText = Localization.Get("Quick_Mute") + " " + (QuickSwitchService.IsMuted ? Localization.Get("Quick_On") : Localization.Get("Quick_Off"));
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"RefreshQuickToggles failed: {ex.Message}");
        }
    }

    private static string FormatQuickSwitch(string nameKey, bool available, bool on)
    {
        if (!available) return Localization.Get(nameKey) + " " + Localization.Get("Quick_NA");
        return Localization.Get(nameKey) + " " + (on ? Localization.Get("Quick_On") : Localization.Get("Quick_Off"));
    }

    /// <summary>切换歌词翻译显示开关。</summary>
    public void ToggleLyricTranslation()
    {
        ShowLyricTranslation = !ShowLyricTranslation;
    }

    /// <summary>复制当前歌词句到剪贴板（无歌词时无操作）。</summary>
    public void CopyCurrentLyric()
    {
        try
        {
            var text = CurrentLyricText;
            if (string.IsNullOrEmpty(text)) return;
            System.Windows.Clipboard.SetText(text);
            AppLogger.Info("Current lyric copied to clipboard.");
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"CopyCurrentLyric failed: {ex.Message}");
        }
    }

    /// <summary>点击快捷开关（which: wifi / bluetooth / night / mute）。</summary>
    public async void ToggleQuickSwitch(string which)
    {
        switch (which)
        {
            case "wifi":
                var ok = await QuickSwitchService.SetRadioAsync(false, !QuickSwitchService.IsWifiOn);
                if (!ok) TryOpenNetworkSettings(); // Radio 不可控（硬件/驱动限制）时兜底：打开系统网络设置
                break;
            case "bluetooth":
                await QuickSwitchService.SetRadioAsync(true, !QuickSwitchService.IsBluetoothOn);
                break;
            case "night":
                QuickSwitchService.ToggleNightMode();
                break;
            case "mute":
                QuickSwitchService.ToggleMute();
                break;
        }
        RefreshQuickToggles();
    }

    private static void TryOpenNetworkSettings()
    {
        try
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("ms-settings:network-wifi") { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Open network settings failed: {ex.Message}");
        }
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
        TimerToolTip = Localization.Get("Timer_ToggleHint");
        // 语言切换后刷新含本地化文案的临时状态（复制/下载/合并胶囊）
        PollFileCopy();
        PollDownloadProgress();
        RebuildCompactItems();
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
        _wave.SetPlaying(value == PlaybackStatus.Playing);
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
        // 释放效率工具服务（Stop/Dispose 幂等，App 退出时再次调用安全）
        _keyboard.Dispose();
        _clipboard.Dispose();
        _schedule.Dispose();
        _pomodoro.Dispose();
        _wave.Stop();
    }
}

/// <summary>多播放器选择器中的一行（AppId + 名称 + 是否当前跟随）。</summary>
public sealed class MediaSessionItem : ObservableObject
{
    private bool _isCurrent;

    public string AppId { get; }
    public string AppName { get; }

    public bool IsCurrent
    {
        get => _isCurrent;
        set => Set(ref _isCurrent, value);
    }

    public MediaSessionItem(string appId, string appName, bool isCurrent)
    {
        AppId = appId;
        AppName = appName;
        _isCurrent = isCurrent;
    }
}
