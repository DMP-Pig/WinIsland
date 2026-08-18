using System;
using System.Net.Http;
using System.Text.Json;

namespace WinIsland.Services;

/// <summary>
/// 天气组件数据源：Open-Meteo（免费、无需 API Key、无账号）。
/// 先地理编码城市名，再取当前天气；结果缓存 10 分钟。
/// 仅在用户开启“显示天气”并填写城市时才会联网。
/// </summary>
public sealed class WeatherService
{
    private readonly HttpClient _http = new() { Timeout = TimeSpan.FromSeconds(8) };
    private string? _city;
    private string? _last;
    private DateTime _lastUtc;
    private const int CacheMinutes = 10;

    public async Task<string?> GetWeatherAsync(string city)
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

            // 2) 当前天气
            var wUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current=temperature_2m,weather_code&timezone=auto";
            var w = await _http.GetStringAsync(wUrl);
            using var wDoc = JsonDocument.Parse(w);
            var current = wDoc.RootElement.GetProperty("current");
            var temp = current.GetProperty("temperature_2m").GetDouble();
            var code = current.GetProperty("weather_code").GetInt32();

            _last = $"{Math.Round(temp)}°C {CodeToDesc(code)}";
            _city = city;
            _lastUtc = DateTime.UtcNow;
            return _last;
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Weather fetch failed: {ex.Message}");
            return null;
        }
    }

    private static string CodeToDesc(int code) => code switch
    {
        0 => "晴",
        1 or 2 => "多云",
        3 => "阴",
        45 or 48 => "雾",
        >= 51 and <= 57 => "毛毛雨",
        >= 61 and <= 67 => "雨",
        >= 71 and <= 77 => "雪",
        >= 80 and <= 82 => "阵雨",
        85 or 86 => "阵雪",
        >= 95 => "雷暴",
        _ => "多云",
    };
}
