using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Automation;

namespace WinIsland.Services;

/// <summary>一条被捕获的系统通知。</summary>
public sealed record SystemNotification(string AppName, string Title, string Body);

/// <summary>
/// 接管 Windows 通知（尽力而为）：Windows 没有公开的“拦截其它应用通知”API。
/// 通过 UI Automation 扫描：1) 通知中心元素；2) 顶层 CoreWindow 通知窗口。
/// 带诊断日志；捕获不到时静默，不影响主流程。
/// </summary>
public sealed class SystemNotificationMonitor : IDisposable
{
    private readonly object _gate = new();
    private readonly HashSet<string> _seen = new(StringComparer.OrdinalIgnoreCase);
    private System.Threading.Timer? _timer;
    private bool _started;
    private int _notFoundCount;

    public event EventHandler<SystemNotification>? NotificationCaptured;

    public void Start()
    {
        lock (_gate)
        {
            if (_started) return;
            _started = true;
            _seen.Clear();
        }
        _timer?.Dispose();
        _timer = new System.Threading.Timer(_ => Poll(), null, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(1.5));
        AppLogger.Info("System notification monitor started.");
    }

    private void Poll()
    {
        try
        {
            var root = AutomationElement.RootElement;
            var found = false;

            // 1) 通知中心（多语言/多版本名称）
            string[] names = { "通知中心", "Notification Center", "Windows 通知", "Action Center", "操作中心" };
            AutomationElement? center = null;
            foreach (var n in names)
            {
                try
                {
                    center = root.FindFirst(TreeScope.Children,
                        new PropertyCondition(AutomationElement.NameProperty, n, PropertyConditionFlags.IgnoreCase));
                    if (center is not null) { found = true; break; }
                }
                catch { /* per-name */ }
            }

            // 2) 兜底：扫描所有顶层窗口，找 CoreWindow / 通知类窗口
            if (center is null)
            {
                var all = root.FindAll(TreeScope.Children, Condition.TrueCondition);
                foreach (AutomationElement w in all)
                {
                    var cls = Safe(() => w.Current.ClassName) ?? string.Empty;
                    var nm = Safe(() => w.Current.Name) ?? string.Empty;
                    if (cls.IndexOf("CoreWindow", StringComparison.OrdinalIgnoreCase) >= 0
                        || cls.IndexOf("Notification", StringComparison.OrdinalIgnoreCase) >= 0
                        || nm.IndexOf("通知", StringComparison.OrdinalIgnoreCase) >= 0
                        || nm.IndexOf("Notification", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        center = w;
                        found = true;
                        break;
                    }
                }
            }

            if (center is null)
            {
                // 节流：每 10 次轮询（约 15s）记一次，避免刷屏
                if (++_notFoundCount % 10 == 1) AppLogger.Debug("SysNotify: no notification center/window found.");
                return;
            }
            _notFoundCount = 0;
            if (!found) return;

            var texts = center.FindAll(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text));
            var lines = new List<string>();
            foreach (AutomationElement t in texts)
            {
                var s = Safe(() => t.Current.Name);
                if (!string.IsNullOrWhiteSpace(s)) lines.Add(s.Trim());
            }
            if (lines.Count == 0)
            {
                AppLogger.Debug("SysNotify: center found but no text items.");
                return;
            }

            var key = string.Join("|", lines);
            lock (_gate)
            {
                if (!_seen.Add(key)) return;
                if (_seen.Count > 300) _seen.Clear();
            }

            var title = lines.FirstOrDefault() ?? string.Empty;
            var body = lines.Count > 1 ? string.Join(" ", lines.Skip(1)) : string.Empty;
            if (body == title) body = string.Empty;
            AppLogger.Info($"SysNotify captured: title='{title}' body='{body}'");
            NotificationCaptured?.Invoke(this, new SystemNotification("Windows", title, body));
        }
        catch (Exception ex)
        {
            AppLogger.Debug($"SysNotify poll error: {ex.Message}");
        }
    }

    private static string? Safe(Func<string> f) { try { return f(); } catch { return null; } }

    public void Stop()
    {
        lock (_gate) { _started = false; }
        try { _timer?.Dispose(); _timer = null; } catch { /* ignore */ }
        AppLogger.Info("System notification monitor stopped.");
    }

    public void Dispose() => Stop();
}
