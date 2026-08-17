using System.Globalization;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Text.Json;

namespace WinIsland.Services;

/// <summary>
/// Optional online-lyrics provider (Netease Cloud Music unofficial API).
/// OFF by default — the user must enable it in settings; see README for the
/// copyright caveat. All calls are short-timeout and never block the UI.
/// </summary>
public sealed class OnlineLyricsService
{
    private readonly HttpClient _http;

    public OnlineLyricsService()
    {
        var handler = new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.All };
        _http = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(5) };
        _http.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) WinIsland/0.1");
        _http.DefaultRequestHeaders.Referrer = new Uri("https://music.163.com/");
    }

    public async Task<string?> FetchLrcAsync(string title, string artist, CancellationToken ct = default)
    {
        try
        {
            var query = string.IsNullOrEmpty(artist) ? title : $"{title} {artist}";
            var songId = await SearchSongIdAsync(query, ct);
            if (songId is null) return null;

            var url = $"https://music.163.com/api/song/lyric?id={songId}&lv=1&kv=1&tv=-1";
            var json = await _http.GetStringAsync(url, ct);
            using var doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("lrc", out var lrc) ||
                !lrc.TryGetProperty("lyric", out var lyric) ||
                lyric.ValueKind != JsonValueKind.String)
                return null;

            var text = lyric.GetString();
            return string.IsNullOrWhiteSpace(text) ? null : text;
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Online lyrics failed: {ex.Message}");
            return null;
        }
    }

    private async Task<string?> SearchSongIdAsync(string query, CancellationToken ct)
    {
        var url = $"https://music.163.com/api/search/get/web?s={Uri.EscapeDataString(query)}&type=1&limit=5";
        var json = await _http.GetStringAsync(url, ct);
        using var doc = JsonDocument.Parse(json);
        if (!doc.RootElement.TryGetProperty("result", out var result) ||
            !result.TryGetProperty("songs", out var songs) ||
            songs.ValueKind != JsonValueKind.Array)
            return null;

        foreach (var song in songs.EnumerateArray())
        {
            if (song.TryGetProperty("id", out var id) && id.ValueKind == JsonValueKind.Number)
                return id.GetInt64().ToString(CultureInfo.InvariantCulture);
        }

        return null;
    }
}


