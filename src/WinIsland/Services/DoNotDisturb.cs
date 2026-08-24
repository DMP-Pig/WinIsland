using System;

namespace WinIsland.Services;

/// <summary>勿扰模式判定：手动开关或时间段自动生效。</summary>
public static class DoNotDisturb
{
    /// <summary>是否处于勿扰：手动开关或时间段自动生效；白名单来源（10 勿扰白名单）不受勿扰影响。</summary>
    public static bool IsActive(AppSettings s) => IsActive(s, null);

    public static bool IsActive(AppSettings s, string? source)
    {
        if (s.DoNotDisturbManual && !IsAllowlisted(s, source)) return true;
        if (!s.DoNotDisturbEnabled) return false;
        var start = Math.Clamp(s.DoNotDisturbStartHour, 0, 23);
        var end = Math.Clamp(s.DoNotDisturbEndHour, 0, 23);
        if (start == end) return false;
        if (IsAllowlisted(s, source)) return false;
        var now = DateTime.Now.Hour;
        return start < end ? now >= start && now < end : now >= start || now < end;
    }

    /// <summary>来源（exe 名 / 应用显示名，大小写不敏感）是否在勿扰白名单内。</summary>
    private static bool IsAllowlisted(AppSettings s, string? source)
    {
        if (string.IsNullOrWhiteSpace(source)) return false;
        foreach (var item in s.DnDAllowlist)
        {
            var t = item?.Trim();
            if (string.IsNullOrEmpty(t)) continue;
            if (string.Equals(t, source, StringComparison.OrdinalIgnoreCase)) return true;
        }
        return false;
    }
}
