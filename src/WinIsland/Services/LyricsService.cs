using System.IO;
using System.Text;

namespace WinIsland.Services;

public enum LyricsSourceKind { None, LocalFile, Cider, Online }

public sealed record LyricsResult(LrcDocument Document, LyricsSourceKind Source, string SourceDetail)
{
    public bool IsEmpty => Document.IsEmpty;
    public static LyricsResult Empty { get; } = new(new LrcDocument(), LyricsSourceKind.None, string.Empty);
}

/// <summary>
/// Resolves lyrics for the currently playing track.
/// Priority: local .lrc file > player/client lyrics (Cider API) > online API (opt-in).
/// Missing lyrics degrade gracefully to an empty result — callers just hide the panel.
/// </summary>
public sealed class LyricsService
{
    private readonly SettingsService _settings;
    private readonly CiderMediaProvider? _cider;
    private readonly OnlineLyricsService _online = new();
    private readonly Dictionary<string, LyricsResult> _cache = new();
    private readonly object _cacheLock = new();

    public LyricsService(SettingsService settings, CiderMediaProvider? cider)
    {
        _settings = settings;
        _cider = cider;
    }

    /// <summary>Get lyrics for a track, using cached results when the same song repeats.</summary>
    public async Task<LyricsResult> GetLyricsAsync(MediaSnapshot snapshot, CancellationToken ct = default)
    {
        var key = TrackKey(snapshot.Track);
        lock (_cacheLock)
        {
            if (_cache.TryGetValue(key, out var cached)) return cached;
        }

        var result = await LoadAsync(snapshot, ct);
        lock (_cacheLock)
        {
            if (_cache.Count > 8) _cache.Clear(); // tiny LRU-ish cache
            _cache[key] = result;
        }

        return result;
    }

    private async Task<LyricsResult> LoadAsync(MediaSnapshot snapshot, CancellationToken ct)
    {
        var track = snapshot.Track;

        // 1) Local .lrc files.
        var localPath = FindLocalLrc(track);
        if (localPath is not null)
        {
            try
            {
                var text = await File.ReadAllTextAsync(localPath, ct);
                var doc = LrcParser.Parse(text);
                if (!doc.IsEmpty) return new LyricsResult(doc, LyricsSourceKind.LocalFile, localPath);
            }
            catch (Exception ex)
            {
                AppLogger.Warn($"Failed to read local LRC {localPath}: {ex.Message}");
            }
        }

        // 2) Cider API lyrics (only meaningful when the active source is Cider).
        if (snapshot.Source == MediaSourceKind.Cider && _cider is not null)
        {
            var lrc = await _cider.GetLyricsAsync();
            if (!string.IsNullOrWhiteSpace(lrc))
            {
                var doc = LrcParser.Parse(lrc);
                if (!doc.IsEmpty) return new LyricsResult(doc, LyricsSourceKind.Cider, "Cider API");
            }
        }

        // 3) Online API — strictly opt-in.
        if (_settings.Current.OnlineLyricsEnabled)
        {
            var lrc = await _online.FetchLrcAsync(track.Title, track.Artist, ct);
            if (!string.IsNullOrWhiteSpace(lrc))
            {
                var doc = LrcParser.Parse(lrc);
                if (!doc.IsEmpty) return new LyricsResult(doc, LyricsSourceKind.Online, "Netease");
            }
        }

        return LyricsResult.Empty;
    }

    /// <summary>
    /// Find a matching .lrc file in configured + conventional folders.
    /// File names are matched after normalisation (lowercase, spaces/punctuation removed),
    /// so "Demo Artist - Demo Song.lrc" matches artist "Demo Artist" + title "Demo Song".
    /// Supported layouts: "Title.lrc", "Artist - Title.lrc", "Artist-Title.lrc", "Artist-Title-Album.lrc".
    /// </summary>
    public string? FindLocalLrc(TrackInfo track)
    {
        try
        {
            var dirs = new List<string>();
            var configured = _settings.Current.LyricsFolder;
            if (!string.IsNullOrWhiteSpace(configured)) dirs.Add(configured.Trim());
            dirs.Add(AppPaths.LyricsDir);
            dirs.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Lyrics"));
            dirs.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));

            var title = Sanitize(track.Title);
            var artist = Sanitize(track.Artist);
            var album = Sanitize(track.Album);
            var keys = new HashSet<string>(StringComparer.Ordinal);
            if (title.Length > 0) keys.Add(title);
            if (artist.Length > 0 && title.Length > 0)
            {
                keys.Add(artist + title);                       // "Demo Artist - Demo Song" -> demoartistdemosong
                if (album.Length > 0) keys.Add(artist + title + album);
            }

            foreach (var dir in dirs)
            {
                if (!Directory.Exists(dir)) continue;
                try
                {
                    foreach (var file in Directory.EnumerateFiles(dir, "*.lrc", SearchOption.TopDirectoryOnly))
                    {
                        // 文件名同样去掉空格/连字符/大小写后与 key 比对
                        var name = Sanitize(Path.GetFileNameWithoutExtension(file));
                        if (name.Length == 0) continue;
                        foreach (var key in keys)
                        {
                            if (name == key || name.EndsWith(key, StringComparison.Ordinal)) return file;
                        }
                    }
                }
                catch { /* unreadable folder */ }
            }
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"FindLocalLrc failed: {ex.Message}");
        }

        return null;
    }

    /// <summary>清空歌词缓存（例如在线歌词开关变化后强制重新获取）。</summary>
    public void ClearCache()
    {
        lock (_cacheLock) _cache.Clear();
    }

    internal static string TrackKey(TrackInfo track) =>
        $"{Sanitize(track.Artist)}\u0001{Sanitize(track.Title)}\u0001{Sanitize(track.Album)}";

    internal static string Sanitize(string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;
        var sb = new StringBuilder(s.Length);
        foreach (var ch in s.ToLowerInvariant())
        {
            if (char.IsLetterOrDigit(ch)) sb.Append(ch);
        }

        return sb.ToString();
    }
}

