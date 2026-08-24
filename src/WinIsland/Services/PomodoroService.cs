using System;
using System.Windows.Threading;

namespace WinIsland.Services;

public enum PomodoroPhase { Stopped, Work, Break }

/// <summary>
/// 番茄钟/倒计时：1 秒驱动，组件显示 mm:ss，到期触发事件（由上层弹通知）。
/// </summary>
public sealed class PomodoroService : IDisposable
{
    private readonly DispatcherTimer _timer;
    private DateTime _endTime;
    private TimeSpan _remaining;

    public PomodoroService()
    {
        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _timer.Tick += (_, _) => OnTick();
    }

    public PomodoroPhase Phase { get; private set; } = PomodoroPhase.Stopped;

    /// <summary>剩余时间字符串 mm:ss。</summary>
    public string ClockText
    {
        get
        {
            var r = Phase == PomodoroPhase.Stopped ? _remaining : _remaining;
            return r.TotalHours >= 1 ? $"{(int)r.TotalHours:00}:{r.Minutes:00}:{r.Seconds:00}" : $"{r.Minutes:00}:{r.Seconds:00}";
        }
    }

    public event Action? Tick;
    /// <summary>阶段结束（参数为刚结束的阶段）。</summary>
    public event Action<PomodoroPhase>? Completed;

    public void StartWork(int minutes)
    {
        Start(TimeSpan.FromMinutes(Math.Max(1, minutes)), PomodoroPhase.Work);
    }

    public void StartBreak(int minutes)
    {
        Start(TimeSpan.FromMinutes(Math.Max(1, minutes)), PomodoroPhase.Break);
    }

    public void Stop()
    {
        Phase = PomodoroPhase.Stopped;
        _timer.Stop();
        Tick?.Invoke();
    }

    private void Start(TimeSpan duration, PomodoroPhase phase)
    {
        Phase = phase;
        _remaining = duration;
        _endTime = DateTime.Now + duration;
        _timer.Start();
        Tick?.Invoke();
    }

    private void OnTick()
    {
        _remaining = _endTime - DateTime.Now;
        if (_remaining < TimeSpan.Zero) _remaining = TimeSpan.Zero;
        Tick?.Invoke();
        if (_remaining == TimeSpan.Zero)
        {
            _timer.Stop();
            var finished = Phase;
            Phase = PomodoroPhase.Stopped;
            Completed?.Invoke(finished);
        }
    }

    public void Dispose() => _timer.Stop();
}
