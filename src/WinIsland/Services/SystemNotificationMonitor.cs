using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;

namespace WinIsland.Services;

/// <summary>一条被捕获的系统通知。</summary>
public sealed record SystemNotification(string AppName, string Title, string Body);

/// <summary>
/// 接管 Windows 通知（尽力而为）：
/// 1) UIA 扫描“通知中心”；
/// 2) EnumWindows 监听新出现的右上/右下小弹窗（QQ 等自绘通知）并读取文本。
/// Windows 无公开拦截 API，此为尽力而为；带诊断日志，失败静默。
/// </summary>
public sealed class SystemNotificationMonitor : IDisposable
{
    private readonly object _gate = new();
    private readonly HashSet<string> _seenText = new(StringComparer.OrdinalIgnoreCase);
    private readonly HashSet<IntPtr> _visibleWindows = new();
    private System.Threading.Timer? _timer;
    private bool _started;
    private bool _baselineDone;
    private int _notFoundCount;

    public event EventHandler<SystemNotification>? NotificationCaptured;

    // ── Win32 ──
    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    [DllImport("user32.dll")] private static extern bool EnumWindows(EnumWindowsProc cb, IntPtr lParam);
    [DllImport("user32.dll")] private static extern bool IsWindowVisible(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    [DllImport("user32.dll")] private static extern int GetClassName(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);
    [DllImport("user32.dll")] private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint pid);
    [StructLayout(LayoutKind.Sequential)] private struct RECT { public int Left, Top, Right, Bottom; }

    private static readonly HashSet<string> ShellClasses = new(StringComparer.OrdinalIgnoreCase)
    {
        "Shell_TrayWnd", "Shell_SecondaryTrayWnd", "Progman", "WorkerW",
        "Windows.UI.Core.CoreWindow", // 通知中心宿主也扫，但文本为空时跳过
        "DV2ControlHost", "BaseBar", "SystemSettings",
    };

    public void Start()
    {
        lock (_gate)
        {
            if (_started) return;
            _started = true;
            _seenText.Clear();
            _visibleWindows.Clear();
            _baselineDone = false;
        }
        _timer?.Dispose();
        _timer = new System.Threading.Timer(_ => Poll(), null, TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(800));
        AppLogger.Info("System notification monitor started (poll 800ms).");
    }

    private void Poll()
    {
        try
        {
            ScanNotificationCenter();
            ScanPopups();
        }
        catch (Exception ex)
        {
            AppLogger.Debug($"SysNotify poll error: {ex.Message}");
        }
    }

    // ── 1) UIA 通知中心 ──
    private void ScanNotificationCenter()
    {
        var root = AutomationElement.RootElement;
        string[] names = { "通知中心", "Notification Center", "Windows 通知", "Action Center", "操作中心" };
        AutomationElement? center = null;
        foreach (var n in names)
        {
            try
            {
                center = root.FindFirst(TreeScope.Children,
                    new PropertyCondition(AutomationElement.NameProperty, n, PropertyConditionFlags.IgnoreCase));
                if (center is not null) break;
            }
            catch { /* per-name */ }
        }

        if (center is null)
        {
            if (++_notFoundCount % 12 == 1) AppLogger.Debug("SysNotify: no notification center found.");
            return;
        }
        _notFoundCount = 0;

        var texts = center.FindAll(TreeScope.Descendants,
            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text));
        var lines = new List<string>();
        foreach (AutomationElement t in texts)
        {
            try { var s = t.Current.Name; if (!string.IsNullOrWhiteSpace(s)) lines.Add(s.Trim()); } catch { }
        }
        if (lines.Count > 0) RaiseIfNew(lines);
    }

    // ── 2) 通用弹窗捕获（QQ 等自绘通知）──
    private void ScanPopups()
    {
        var current = new List<IntPtr>();
        EnumWindows((h, l) => { current.Add(h); return true; }, IntPtr.Zero);

        try
        {
            var area = System.Windows.Forms.Screen.PrimaryScreen?.WorkingArea
                ?? System.Windows.Forms.Screen.AllScreens[0].WorkingArea;

            var seenVisible = new HashSet<IntPtr>();
            var newOnes = new List<IntPtr>();

            foreach (var h in current)
            {
                if (!IsWindowVisible(h)) continue;
                if (OwnsProcess(h)) continue; // 排除本应用自身窗口（横幅/灵动岛）
                if (!GetWindowRect(h, out var r)) continue;
                var w = r.Right - r.Left;
                var hh = r.Bottom - r.Top;
                var cls = GetWindowClass(h);
                if (ShellClasses.Contains(cls)) continue;
                if (cls.IndexOf("DV2ControlHost", StringComparison.OrdinalIgnoreCase) >= 0) continue; // 右键菜单宿主

                // 严格限定：只收 Windows 右下角通知区（贴右缘 + 贴底部 + 通知尺寸），
                // 避免把右键菜单、按键提示等细碎弹窗也弹出来。
                if (w < 240 || w > 560 || hh < 60 || hh > 300) continue;          // 通知尺寸
                var rightNear = r.Right >= area.Right - 80 && r.Right <= area.Right + 30;  // 右缘贴近屏幕右
                var bottomNear = r.Bottom >= area.Bottom - 120 && r.Bottom <= area.Bottom + 30; // 底缘贴近工作区底部
                if (!rightNear || !bottomNear) continue;

                seenVisible.Add(h);
                lock (_gate)
                {
                    // “隐藏→显示”或“新出现”都视为新的弹窗（QQ 等会复用同一个窗口句柄）
                    if (_baselineDone && !_visibleWindows.Contains(h)) newOnes.Add(h);
                }
            }

            lock (_gate)
            {
                _visibleWindows.Clear();
                foreach (var h in seenVisible) _visibleWindows.Add(h);
                if (!_baselineDone) { _baselineDone = true; return; }
            }

            foreach (var h in newOnes)
            {
                var text = ReadWindowText(h);
                if (string.IsNullOrWhiteSpace(text)) continue;
                AppLogger.Info($"SysNotify popup: '{text}'");
                RaiseIfNew(new[] { text });
            }
        }
        catch (Exception ex)
        {
            AppLogger.Debug($"SysNotify popup scan error: {ex.Message}");
        }
    }

    private bool OwnsProcess(IntPtr h)
    {
        try { GetWindowThreadProcessId(h, out var pid); return pid == (uint)Environment.ProcessId; }
        catch { return false; }
    }

    private static string GetWindowClass(IntPtr h)
    {
        try { var sb = new System.Text.StringBuilder(256); GetClassName(h, sb, sb.Capacity); return sb.ToString(); }
        catch { return string.Empty; }
    }

    private string ReadWindowText(IntPtr h)
    {
        try
        {
            var el = AutomationElement.FromHandle(h);
            if (el is null) return string.Empty;
            var sb = new List<string>();
            var name = Safe(() => el.Current.Name);
            if (!string.IsNullOrWhiteSpace(name)) sb.Add(name.Trim());
            var texts = el.FindAll(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text));
            foreach (AutomationElement t in texts)
            {
                try { var s = t.Current.Name; if (!string.IsNullOrWhiteSpace(s)) sb.Add(s.Trim()); } catch { }
            }
            return string.Join(" ", sb.Distinct());
        }
        catch { return string.Empty; }
    }

    private void RaiseIfNew(IEnumerable<string> linesArr)
    {
        var lines = linesArr.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
        if (lines.Count == 0) return;
        var key = string.Join("|", lines);
        lock (_gate)
        {
            if (!_seenText.Add(key)) return;
            if (_seenText.Count > 400) _seenText.Clear();
        }

        var title = lines[0];
        var body = lines.Count > 1 ? string.Join(" ", lines.Skip(1)) : string.Empty;
        if (body == title) body = string.Empty;
        AppLogger.Info($"SysNotify captured: title='{title}' body='{body}'");
        NotificationCaptured?.Invoke(this, new SystemNotification("Windows", title, body));
    }

    private static string? Safe(Func<string> f) { try { return f(); } catch { return null; } }

    public void Stop()
    {
        lock (_gate) { _started = false; _visibleWindows.Clear(); _seenText.Clear(); }
        try { _timer?.Dispose(); _timer = null; } catch { /* ignore */ }
        AppLogger.Info("System notification monitor stopped.");
    }

    public void Dispose() => Stop();
}
