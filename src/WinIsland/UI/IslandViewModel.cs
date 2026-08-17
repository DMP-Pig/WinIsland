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


    public IslandViewModel(MediaCoordinator coordinator, SettingsService settings, LyricsService lyricsService)
    {
        _coordinator = coordinator;
        _settings = settings;
        _lyricsService = lyricsService;

        PlayPauseCommand = new AsyncRelayCommand(_ => _coordinator.TogglePlayPauseAsync());
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
    public TimeSpan Position { get => _position; private set => Set(ref _position, value); }

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
    private int _compactHighlightCount;
    public int CompactHighlightCount { get => _compactHighlightCount; private set => Set(ref _compactHighlightCount, value); }

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
        _snapshot = snapshot;

        Title = snapshot.Track.Title;
        Artist = snapshot.Track.Artist;
        Album = snapshot.Track.Album;
        SourceLabel = snapshot.SourceLabel;
        SourceDetail = snapshot.Track.SourceAppName;
        Status = snapshot.Status;
        DurationSeconds = snapshot.DurationSeconds;
        OnPropertyChanged(nameof(DurationText));
        var hasRealPosition = snapshot.DurationSeconds > 0 || snapshot.PositionSeconds > 0;
        var reported = Math.Max(0, snapshot.PositionSeconds);
        if (snapshot.DurationSeconds > 0) reported = Math.Min(reported, snapshot.DurationSeconds); // 防御：上报值不越界
        if (trackChanged)
        {
            _useFreeClock = !hasRealPosition;
            _trackStartTime = DateTime.UtcNow - TimeSpan.FromSeconds(reported);
            _interpolatedPosition = reported;
            _positionStaleSinceUtc = null;
        }
        else if (hasRealPosition)
        {
            var current = _interpolatedPosition;
            var seeking = _suppressSeek > 0; // 用户正在拖拽进度条
            if (ShouldAdoptReportedPosition(reported, current, seeking))
            {
                _useFreeClock = false;
                _trackStartTime = DateTime.UtcNow - TimeSpan.FromSeconds(reported);
                _interpolatedPosition = reported;
                _positionStaleSinceUtc = null;
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
        LyricIndex = -1;
        CurrentLyricText = LyricLines.Count > 0 ? LyricLines[0].Text : string.Empty;


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
        var count = (int)(frac * cur.Text.Length);

        if (LyricLines.Count > LyricIndex)
        {
            var lvm = LyricLines[LyricIndex];
            if (lvm.HighlightCount != count) lvm.HighlightCount = count;
        }

        if (CompactHighlightCount != count) CompactHighlightCount = count;

    }

    // ── Visibility ─────────────────────────────────────────────
    public void UpdateVisibility()
    {
        var hasMedia = _snapshot is not null && Status is PlaybackStatus.Playing or PlaybackStatus.Paused;
        var show = !_userHidden && (hasMedia || !_settings.Current.HideWhenNoMedia);
        if (hasMedia && Status == PlaybackStatus.Paused && !_settings.Current.ShowWhenPaused)
            show = false;

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
        _interpolatedPosition = target;
        _lastPositionTime = DateTime.UtcNow;
        await _coordinator.SeekAsync(target);
    }

    public void Dispose()
    {
        _progressTimer.Stop();
        _coordinator.SnapshotChanged -= OnSnapshotChanged;
        _coordinator.MediaEnded -= OnMediaEnded;
    }
}




