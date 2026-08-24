using System;

namespace WinIsland.Services;

/// <summary>勿扰模式判定：手动开关或时间段自动生效。</summary>
public static class DoNotDisturb
{
    public static bool IsActive(AppSettings s)
    {
        if (s.DoNotDisturbManual) return true;
        if (!s.DoNotDisturbEnabled) return false;
        var start = Math.Clamp(s.DoNotDisturbStartHour, 0, 23);
        var end = Math.Clamp(s.DoNotDisturbEndHour, 0, 23);
        if (start == end) return false;
        var now = DateTime.Now.Hour;
        return start < end ? now >= start && now < end : now >= start || now < end;
    }
}
