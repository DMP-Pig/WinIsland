using System.Globalization;
using System.Text.RegularExpressions;

namespace WinIsland.Services;

public sealed record LyricLine(TimeSpan Time, string Text)
{
    public override string ToString() => $"[{Time:hh\\:mm\\:ss\\.ff}] {Text}";
}

public sealed class LrcDocument
{
    public string? Title { get; init; }
    public string? Artist { get; init; }
    public string? Album { get; init; }
    public long OffsetMs { get; init; }          // positive shifts timestamps later
    public IReadOnlyList<LyricLine> Lines { get; init; } = Array.Empty<LyricLine>();
    public bool IsEmpty => Lines.Count == 0;

    /// <summary>Index of the line active at <paramref name="position"/>, or -1.</summary>
    public int IndexAt(TimeSpan position)
    {
        var pos = position + TimeSpan.FromMilliseconds(OffsetMs);
        int idx = -1;
        for (var i = 0; i < Lines.Count; i++)
        {
            if (Lines[i].Time <= pos) idx = i;
            else break;
        }

        return idx;
    }
}

/// <summary>
/// Parser for the LRC (lyrics) format: <c>[mm:ss.xx]line text</c> plus metadata
/// tags such as <c>[ar:]</c>, <c>[ti:]</c>, <c>[offset:]</c>. Handles multiple
/// timestamps per line and hour-length timestamps.
/// </summary>
public static class LrcParser
{
    private static readonly Regex TimestampRegex = new(
        @"\[(?:(?<h>\d{1,2}):)?(?<m>\d{1,2}):(?<s>\d{1,2})(?:[.:](?<f>\d{1,3}))?\]",
        RegexOptions.Compiled);

    private static readonly Regex MetaRegex = new(
        @"^\[(?<tag>ar|ti|al|by|offset|length|re|ve):(?<value>.*)\]$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static LrcDocument Parse(string lrcText)
    {
        if (string.IsNullOrWhiteSpace(lrcText)) return new LrcDocument();

        string? title = null, artist = null, album = null;
        long offsetMs = 0;
        var lines = new List<LyricLine>();

        foreach (var raw in lrcText.Split('\n'))
        {
            var line = raw.TrimEnd('\r');
            if (string.IsNullOrWhiteSpace(line)) continue;

            // Metadata tag?
            var meta = MetaRegex.Match(line);
            if (meta.Success)
            {
                var tag = meta.Groups["tag"].Value.ToLowerInvariant();
                var value = meta.Groups["value"].Value.Trim();
                switch (tag)
                {
                    case "ti": title = value; break;
                    case "ar": artist = value; break;
                    case "al": album = value; break;
                    case "offset":
                        if (long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var o))
                            offsetMs = o;
                        break;
                }

                continue;
            }

            // Collect every [mm:ss.xx] timestamp on the line.
            var matches = TimestampRegex.Matches(line);
            if (matches.Count == 0) continue;

            var text = TimestampRegex.Replace(line, string.Empty).Trim();
            foreach (Match m in matches)
            {
                var ts = ParseTime(m);
                lines.Add(new LyricLine(ts, text));
            }
        }

        // Timestamps can be slightly out of order; sort for reliable binary search.
        lines.Sort((a, b) => a.Time.CompareTo(b.Time));

        return new LrcDocument
        {
            Title = title,
            Artist = artist,
            Album = album,
            OffsetMs = offsetMs,
            Lines = lines,
        };
    }

    private static TimeSpan ParseTime(Match m)
    {
        var hours = m.Groups["h"].Success ? int.Parse(m.Groups["h"].Value, CultureInfo.InvariantCulture) : 0;
        var minutes = int.Parse(m.Groups["m"].Value, CultureInfo.InvariantCulture);
        var seconds = int.Parse(m.Groups["s"].Value, CultureInfo.InvariantCulture);
        var frac = m.Groups["f"].Success ? m.Groups["f"].Value : string.Empty;
        var ms = frac.Length == 0 ? 0 : (int)(double.Parse("0." + frac, CultureInfo.InvariantCulture) * 1000);
        return new TimeSpan(0, hours, minutes, seconds, ms);
    }
}
