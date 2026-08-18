using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.Media.Control;

using System.IO;

namespace WinIsland.Services;

/// <summary>
/// Media provider backed by the Windows global media session
/// (<c>Windows.Media.Control.GlobalSystemMediaTransportControlsSession</c>).
/// Covers Spotify, 网易云音乐, QQ音乐, Apple Music (official), Groove, 电影和电视, ...
/// </summary>
public sealed class SmtcMediaProvider : IDisposable
{
    private readonly object _gate = new();
    private GlobalSystemMediaTransportControlsSessionManager? _manager;
    private GlobalSystemMediaTransportControlsSession? _session;
    private MediaSnapshot? _lastSnapshot;
    private CancellationTokenSource _cts = new();
    private bool _disposed;
    private readonly string? _preferredAppId;

    /// <param name="preferredAppId">优先跟随的媒体应用标识（如 "Cider"），防止被其它活跃会话抢走。</param>
    private readonly SettingsService _settings;
    private readonly MediaAppRegistry _registry;

    public SmtcMediaProvider(SettingsService settings, MediaAppRegistry registry, string? preferredAppId = null)
    {
        _settings = settings;
        _registry = registry;
        _preferredAppId = preferredAppId;
    }

    public bool IsAvailable { get; private set; }

    /// <summary>Most recently published snapshot (null when nothing is active).</summary>
    public MediaSnapshot? LastSnapshot { get; private set; }

    /// <summary>Raised on the calling thread whenever a new snapshot is available.</summary>
    public event EventHandler<MediaSnapshot>? SnapshotReady;

    /// <summary>Raised (UI thread) when the availability changes.</summary>
    public event EventHandler<bool>? AvailabilityChanged;

    public async Task StartAsync(CancellationToken ct = default)
    {
        // Bind the cancellation token used by background work.
        _cts = CancellationTokenSource.CreateLinkedTokenSource(ct);

        // SMTC manager requests must run on an STA/COM-initialized thread; the caller
        // is the WPF UI thread. Retry with backoff: the API can transiently fail at
        // logon or when the shell hasn't finished starting.
        for (var attempt = 1; attempt <= 5 && !_cts.IsCancellationRequested; attempt++)
        {
            try
            {
                _manager = await GlobalSystemMediaTransportControlsSessionManager.RequestAsync().AsTask(_cts.Token);
                if (_manager is null) throw new InvalidOperationException("Session manager is null");
                _manager.CurrentSessionChanged += OnCurrentSessionChanged;
                _manager.SessionsChanged += OnSessionsChanged;
                IsAvailable = true;
                AppLogger.Info("SMTC manager acquired.");
                AvailabilityChanged?.Invoke(this, true);
                RefreshSession();
                return;
            }
            catch (Exception ex)
            {
                AppLogger.Warn($"SMTC manager request failed (attempt {attempt}): {ex.Message}");
                await Task.Delay(TimeSpan.FromSeconds(3 * attempt), CancellationToken.None).ConfigureAwait(false);
            }
        }

        IsAvailable = false;
        AvailabilityChanged?.Invoke(this, false);
        AppLogger.Warn("SMTC unavailable after retries; falling back to window-title mode.");
    }

    private void OnCurrentSessionChanged(GlobalSystemMediaTransportControlsSessionManager sender, CurrentSessionChangedEventArgs args)
        => RefreshSession();

    private void OnSessionsChanged(GlobalSystemMediaTransportControlsSessionManager sender, SessionsChangedEventArgs args)
        => RefreshSession();

    private void RefreshSession()
    {
        if (_manager is null) return;

        try
        {
            var current = _manager.GetCurrentSession();
            var sessions = _manager.GetSessions().ToList();

            // 记录见过的媒体程序（供设置界面展示）
            foreach (var s in sessions) _registry.Register(s.SourceAppUserModelId, s.SourceAppUserModelId);

            // 优先跟随指定应用（如 Cider）的会话，防止被其它活跃会话抢走
            if (_preferredAppId is not null)
            {
                var pref = sessions.FirstOrDefault(s =>
                    s.SourceAppUserModelId.IndexOf(_preferredAppId, StringComparison.OrdinalIgnoreCase) >= 0
                    && IsEnabled(s.SourceAppUserModelId) && IsActive(s));
                if (pref is not null) { Attach(pref); return; }
            }

            // 用户配置的媒体程序顺序/禁用：先按优先级、再按状态（playing > paused）
            var ordered = sessions
                .Where(s => IsEnabled(s.SourceAppUserModelId))
                .OrderBy(s => Priority(s.SourceAppUserModelId))
                .ThenByDescending(s => StatusRank(PlaybackStatusOf(s)));
            var best = ordered.FirstOrDefault(s => StatusRank(PlaybackStatusOf(s)) > 0);
            if (best is not null) { Attach(best); return; }

            // Prefer the system's "current" session when it is actually active.
            if (current is not null && IsEnabled(current.SourceAppUserModelId) && IsActive(current))
            {
                Attach(current);
                return;
            }

            Attach(null);
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"SMTC RefreshSession failed: {ex.Message}");
        }
    }

    /// <summary>该媒体程序是否启用：在 MediaApps 中则按它的 Enabled，否则默认启用。</summary>
    private bool IsEnabled(string appId)
    {
        var list = _settings.Current.MediaApps;
        if (list is null || list.Count == 0) return true;
        foreach (var e in list)
            if (string.Equals(e.Key, appId, StringComparison.OrdinalIgnoreCase)) return e.Enabled;
        return true; // 未在列表中的程序默认启用（优先级低于已列出的）
    }

    /// <summary>媒体程序优先级：在 MediaApps 中的位置越靠前优先级越高；未列出则最后。</summary>
    private int Priority(string appId)
    {
        var list = _settings.Current.MediaApps;
        if (list is not null)
        {
            for (var i = 0; i < list.Count; i++)
                if (string.Equals(list[i].Key, appId, StringComparison.OrdinalIgnoreCase)) return i;
        }
        return int.MaxValue;
    }
    private static bool IsActive(GlobalSystemMediaTransportControlsSession s)
    {
        try
        {
            return PlaybackStatusOf(s) is PlaybackStatus.Playing or PlaybackStatus.Paused;
        }
        catch { return false; }
    }

    private static PlaybackStatus PlaybackStatusOf(GlobalSystemMediaTransportControlsSession s)
    {
        try
        {
            return (PlaybackStatus)s.GetPlaybackInfo().PlaybackStatus;
        }
        catch { return PlaybackStatus.Closed; }
    }

    private static int StatusRank(PlaybackStatus status) => status switch
    {
        PlaybackStatus.Playing => 3,
        PlaybackStatus.Paused => 2,
        PlaybackStatus.Opened or PlaybackStatus.Changing => 1,
        _ => 0,
    };

    private void Attach(GlobalSystemMediaTransportControlsSession? next)
    {
        if (ReferenceEquals(_session, next)) return;
        lock (_gate)
        {
            if (_session is not null)
            {
                _session.MediaPropertiesChanged -= OnMediaPropertiesChanged;
                _session.PlaybackInfoChanged -= OnPlaybackInfoChanged;
                _session.TimelinePropertiesChanged -= OnTimelinePropertiesChanged;
            }

            _session = next;
            if (_session is not null)
            {
                _session.MediaPropertiesChanged += OnMediaPropertiesChanged;
                _session.PlaybackInfoChanged += OnPlaybackInfoChanged;
                _session.TimelinePropertiesChanged += OnTimelinePropertiesChanged;
            }
        }
        _ = PushAsync();
    }

    private void OnMediaPropertiesChanged(GlobalSystemMediaTransportControlsSession sender, MediaPropertiesChangedEventArgs args)
    {
        _ = PushAsync();
    }

    private void OnPlaybackInfoChanged(GlobalSystemMediaTransportControlsSession sender, PlaybackInfoChangedEventArgs args)
        => _ = PushAsync(useCachedTrack: true);

    private void OnTimelinePropertiesChanged(GlobalSystemMediaTransportControlsSession sender, TimelinePropertiesChangedEventArgs args)
        => _ = PushAsync(useCachedTrack: true);

    /// <summary>
    /// Read the current session and publish a snapshot. Position-only updates reuse the
    /// cached track metadata so we never hammer TryGetMediaPropertiesAsync (expensive).
    /// </summary>
    public async Task PushAsync(bool useCachedTrack = false)
    {
        var session = _session;
        if (session is null) return;

        try
        {
            var snapshot = useCachedTrack && LastSnapshot is not null
                ? BuildLightSnapshot(session, LastSnapshot)
                : await BuildSnapshotAsync(session).ConfigureAwait(true);
            if (snapshot is null)
            {
                return;
            }
            var changed = snapshot.Track != _lastSnapshot?.Track
                          || snapshot.Status != _lastSnapshot?.Status
                          || snapshot.Source != _lastSnapshot?.Source;
            if (changed)
            {
                _lastSnapshot = snapshot;
                LastSnapshot = snapshot;
                SnapshotReady?.Invoke(this, snapshot);
            }
            else if (snapshot.Status == PlaybackStatus.Playing ||
                     Math.Abs(snapshot.PositionSeconds - (_lastSnapshot?.PositionSeconds ?? -1)) > 0.5)
            {
                // Progress ticks: publish so the slider stays smooth.
                _lastSnapshot = snapshot;
                LastSnapshot = snapshot;
                SnapshotReady?.Invoke(this, snapshot);
            }
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"SMTC PushAsync failed: {ex.Message}");
        }
    }

    /// <summary>Fast path: update only status/position/timeline, keep cached metadata.</summary>
    private MediaSnapshot? BuildLightSnapshot(GlobalSystemMediaTransportControlsSession session, MediaSnapshot cached)
    {
        try
        {
            var info = session.GetPlaybackInfo();
            var status = (PlaybackStatus)info.PlaybackStatus;
            if (status is PlaybackStatus.Closed) return null;

            var tl = session.GetTimelineProperties();
            double position = 0, duration = cached.DurationSeconds;
            var canSeek = cached.CanSeek;
            if (tl is not null)
            {
                var start = tl.StartTime.TotalSeconds;
                position = Math.Max(0, tl.Position.TotalSeconds - start);
                var end = tl.EndTime.TotalSeconds;
                if (end > start) duration = end - start;
                canSeek = duration > 0;
            }

            return cached with
            {
                Status = status,
                PositionSeconds = position,
                DurationSeconds = duration,
                CanSeek = canSeek,
                UpdatedAt = DateTimeOffset.Now,
            };
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"SMTC light snapshot failed: {ex.Message}");
            return null;
        }
    }

    private async Task<MediaSnapshot?> BuildSnapshotAsync(GlobalSystemMediaTransportControlsSession session)
    {
        PlaybackStatus status = PlaybackStatus.Closed;
        try
        {
            var info = session.GetPlaybackInfo();
            status = (PlaybackStatus)info.PlaybackStatus;
        }
        catch { /* some apps expose no playback info */ }

        if (status is PlaybackStatus.Closed) return null;

        // Metadata (may be slow / throw for apps without media properties).
        var title = string.Empty;
        var artist = string.Empty;
        var album = string.Empty;
        var albumArtist = string.Empty;
        var artPath = string.Empty;
        var hasLyrics = false;
        try
        {
            var props = await session.TryGetMediaPropertiesAsync().AsTask().ConfigureAwait(true);
            if (props is not null)
            {
                title = props.Title ?? string.Empty;
                artist = props.Artist ?? string.Empty;
                album = props.AlbumTitle ?? string.Empty;
                albumArtist = props.AlbumArtist ?? string.Empty;
                hasLyrics = false;

                if (props.Thumbnail is not null)
                {
                    artPath = await SaveThumbnailAsync(props.Thumbnail, session.SourceAppUserModelId, title, artist);
                }
            }
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"SMTC media properties failed: {ex.Message}");
        }

        if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(artist)) return null;

        var sourceAppId = session.SourceAppUserModelId ?? string.Empty;
        var sourceAppName = SourceAppName(sourceAppId);

        // Timeline / duration.
        double position = 0, duration = 0;
        var canSeek = false;
        try
        {
            var tl = session.GetTimelineProperties();
            if (tl is not null)
            {
                var end = tl.EndTime.TotalSeconds;
                var start = tl.StartTime.TotalSeconds;
                position = Math.Max(0, tl.Position.TotalSeconds - start);
                duration = Math.Max(0, end - start);
                canSeek = duration > 0;
            }
        }
        catch { /* timeline may be missing */ }

        var controls = new { Can = true };
        bool canPlayPause = true, canNext = true, canPrevious = true;
        try
        {
            var c = session.GetPlaybackInfo()?.Controls;
            if (c is not null)
            {
                canPlayPause = c.IsPlayEnabled || c.IsPauseEnabled;
                canNext = c.IsNextEnabled;
                canPrevious = c.IsPreviousEnabled;
            }
        }
        catch { /* assume enabled */ }

        var track = new TrackInfo(title, artist, album, albumArtist, sourceAppName, sourceAppId, artPath, string.Empty,
            duration > 0 ? TimeSpan.FromSeconds(duration) : TimeSpan.Zero);

        return new MediaSnapshot
        {
            Track = track,
            Source = MediaSourceKind.Smtc,
            Status = status,
            PositionSeconds = position,
            DurationSeconds = duration,
            CanPlayPause = canPlayPause,
            CanNext = canNext,
            CanPrevious = canPrevious,
            CanSeek = canSeek,
            HasVolumeControl = false,
            HasLyrics = hasLyrics,
        };
    }

    private static async Task<string> SaveThumbnailAsync(Windows.Storage.Streams.IRandomAccessStreamReference thumb,
        string appId, string title, string artist)
    {
        try
        {
            var key = ArtworkCache.CacheKey("smtc", appId, title, artist);
            var existing = Directory.EnumerateFiles(AppPaths.ThumbCacheDir, $"{key}.*").FirstOrDefault();
            if (existing is not null) return existing;

            return await ArtworkCache.SaveStreamAsync(async (ms) =>
            {
                using var stream = await thumb.OpenReadAsync().AsTask();
                var reader = new Windows.Storage.Streams.DataReader(stream.GetInputStreamAt(0));
                var size = stream.Size;
                if (size == 0 || size > 10 * 1024 * 1024) return;
                await reader.LoadAsync((uint)size).AsTask();
                var bytes = new byte[size];
                reader.ReadBytes(bytes);
                await ms.WriteAsync(bytes);
            }, key);
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"SMTC thumbnail save failed: {ex.Message}");
            return string.Empty;
        }
    }

    private static string SourceAppName(string appId)
    {
        if (string.IsNullOrEmpty(appId)) return "Unknown";
        var name = appId.Split('.').FirstOrDefault(p => !string.IsNullOrEmpty(p) && p.Length > 2) ?? appId;
        // Strip common suffixes like "Spotify.exe.Spotify" -> "Spotify"
        var segments = appId.Split('.');
        foreach (var seg in segments)
        {
            if (seg.Equals("exe", StringComparison.OrdinalIgnoreCase)) continue;
            if (seg.Length > 2 && seg != "MusicUI" && !seg.Contains("SysApp")) return seg;
        }
        return name;
    }

    // ── Control ─────────────────────────────────────────────────

    public async Task<bool> TogglePlayPauseAsync()
    {
        var s = _session;
        if (s is null) return false;
        try
        {
            var ok = await s.TryTogglePlayPauseAsync().AsTask();
            if (!ok) await s.TryPlayAsync().AsTask();
            return ok;
        }
        catch (Exception ex) { AppLogger.Warn($"SMTC toggle failed: {ex.Message}"); return false; }
    }

    public async Task<bool> PlayAsync()
    {
        var s = _session;
        if (s is null) return false;
        try { return await s.TryPlayAsync().AsTask(); }
        catch (Exception ex) { AppLogger.Warn($"SMTC play failed: {ex.Message}"); return false; }
    }

    public async Task<bool> PauseAsync()
    {
        var s = _session;
        if (s is null) return false;
        try { return await s.TryPauseAsync().AsTask(); }
        catch (Exception ex) { AppLogger.Warn($"SMTC pause failed: {ex.Message}"); return false; }
    }
    public async Task<bool> NextAsync()
    {
        var s = _session;
        if (s is null) return false;
        try { return await s.TrySkipNextAsync().AsTask(); }
        catch (Exception ex) { AppLogger.Warn($"SMTC next failed: {ex.Message}"); return false; }
    }

    public async Task<bool> PreviousAsync()
    {
        var s = _session;
        if (s is null) return false;
        try { return await s.TrySkipPreviousAsync().AsTask(); }
        catch (Exception ex) { AppLogger.Warn($"SMTC prev failed: {ex.Message}"); return false; }
    }

    public async Task<bool> SeekAsync(double positionSeconds)
    {
        var s = _session;
        if (s is null) return false;
        try { return await s.TryChangePlaybackPositionAsync((long)(positionSeconds * 1000)).AsTask(); }
        catch (Exception ex) { AppLogger.Warn($"SMTC seek failed: {ex.Message}"); return false; }
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        try
        {
            _cts.Cancel();
            lock (_gate)
            {
                if (_manager is not null)
                {
                    _manager.CurrentSessionChanged -= OnCurrentSessionChanged;
                    _manager.SessionsChanged -= OnSessionsChanged;
                }

                if (_session is not null)
                {
                    _session.MediaPropertiesChanged -= OnMediaPropertiesChanged;
                    _session.PlaybackInfoChanged -= OnPlaybackInfoChanged;
                    _session.TimelinePropertiesChanged -= OnTimelinePropertiesChanged;
                }
            }
        }
        catch { /* ignore */ }
        _cts.Dispose();
    }
}






