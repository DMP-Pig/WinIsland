using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinIsland.Services;

/// <summary>一次天气快照（Open-Meteo 数据，纯数值不含单位，展示文案由 UI 层本地化）。</summary>
public sealed record WeatherInfo(
    double Temperature, int Code, double FeelsLike, double Humidity,
    double WindSpeed, double Precipitation, double High, double Low, string Updated);

/// <summary>
/// 天气组件数据源：Open-Meteo（免费、无需 API Key、无账号）。
/// 先地理编码城市名，再一次请求取当前天气 + 体感/湿度/风速/降水 + 今日最高/最低温；
/// 结果缓存 10 分钟。仅在用户开启“显示天气”并填写城市时才会联网。
/// </summary>
public sealed class WeatherService
{
    private readonly HttpClient _http = new() { Timeout = TimeSpan.FromSeconds(8) };
    private string? _city;
    private WeatherInfo? _last;
    private DateTime _lastUtc;
    private const int CacheMinutes = 10;

    public async Task<WeatherInfo?> GetWeatherAsync(string city)
    {
        if (string.IsNullOrWhiteSpace(city)) return null;
        if (string.Equals(_city, city, StringComparison.OrdinalIgnoreCase)
            && _last is not null && DateTime.UtcNow - _lastUtc < TimeSpan.FromMinutes(CacheMinutes))
            return _last;

        try
        {
            // 1) 地理编码（Open-Meteo geocoding，免费）
            var geoUrl = "https://geocoding-api.open-meteo.com/v1/search?name="
                + Uri.EscapeDataString(city) + "&count=1&language=zh";
            var geo = await _http.GetStringAsync(geoUrl);
            using var geoDoc = JsonDocument.Parse(geo);
            if (!geoDoc.RootElement.TryGetProperty("results", out var results) || results.GetArrayLength() == 0)
                return null;
            var first = results[0];
            var lat = first.GetProperty("latitude").GetDouble();
            var lon = first.GetProperty("longitude").GetDouble();

            // 2) 当前天气 + 今日高低温（一次请求）
            var wUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}"
                + "&current=temperature_2m,relative_humidity_2m,apparent_temperature,weather_code,wind_speed_10m,precipitation"
                + "&daily=temperature_2m_max,temperature_2m_min"
                + "&timezone=auto&forecast_days=1";
            var w = await _http.GetStringAsync(wUrl);
            using var wDoc = JsonDocument.Parse(w);
            var current = wDoc.RootElement.GetProperty("current");
            var temp = current.GetProperty("temperature_2m").GetDouble();
            var code = TryInt(current, "weather_code", 0);
            var feels = TryDouble(current, "apparent_temperature", temp);
            var hum = TryDouble(current, "relative_humidity_2m", 0);
            var wind = TryDouble(current, "wind_speed_10m", 0);
            var precip = TryDouble(current, "precipitation", 0);
            double high = temp, low = temp;
            if (wDoc.RootElement.TryGetProperty("daily", out var daily)
                && daily.TryGetProperty("temperature_2m_max", out var highs) && highs.GetArrayLength() > 0)
                high = highs[0].GetDouble();
            if (wDoc.RootElement.TryGetProperty("daily", out daily)
                && daily.TryGetProperty("temperature_2m_min", out var lows) && lows.GetArrayLength() > 0)
                low = lows[0].GetDouble();

            var info = new WeatherInfo(temp, code, feels, hum, wind, precip, high, low, DateTime.Now.ToString("HH:mm"));
            _city = city;
            _last = info;
            _lastUtc = DateTime.UtcNow;
            return info;
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Weather fetch failed: {ex.Message}");
            return null;
        }
    }

    private static int TryInt(JsonElement el, string name, int fallback)
        => el.TryGetProperty(name, out var v) && v.ValueKind == JsonValueKind.Number ? v.GetInt32() : fallback;

    private static double TryDouble(JsonElement el, string name, double fallback)
        => el.TryGetProperty(name, out var v) && v.ValueKind == JsonValueKind.Number ? v.GetDouble() : fallback;
}
