using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Automation;

namespace WinIsland.Services;

/// <summary>一条被捕获的系统通知。</summary>
public sealed record SystemNotification(string AppName, string Title, string Body);

/// <summary>
/// 接管 Windows 通知（尽力而为）：Windows 没有公开的“拦截其它应用通知”API，
/// 这里通过 UI Automation 轮询“通知中心”，把新增的通知项镜像出来。
/// 可靠性受系统版本/权限影响，捕获不到时不会崩溃，静默跳过。
/// </summary>
public sealed class SystemNotificationMonitor : IDisposable
{
    private readonly object _gate = new();
    private readonly HashSet<string> _seen = new(StringComparer.OrdinalIgnoreCase);
    private System.Threading.Timer? _timer;
    private bool _started;

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
    }

    private void Poll()
    {
        try
        {
            // 尝试找到“通知中心”根元素（多语言/多版本名称）
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
                catch { /* per-name try */ }
            }

            // 兜底：按类名找 Toast 宿主窗口（Win10/11 弹通知时会创建 CoreWindow）
            if (center is null)
            {
                center = root.FindFirst(TreeScope.Children,
                    new PropertyCondition(AutomationElement.ClassNameProperty, "Windows.UI.Core.CoreWindow"));
            }

            if (center is null) return;

            var texts = center.FindAll(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text));
            var lines = new List<string>();
            foreach (AutomationElement t in texts)
            {
                var s = t.Current.Name;
                if (!string.IsNullOrWhiteSpace(s)) lines.Add(s.Trim());
            }
            if (lines.Count == 0) return;

            var key = string.Join("|", lines);
            lock (_gate)
            {
                if (!_seen.Add(key)) return; // 已捕获过
                if (_seen.Count > 200) _seen.Clear();
            }

            var app = "Windows";
            var title = lines.FirstOrDefault() ?? string.Empty;
            var body = lines.Count > 1 ? string.Join(" ", lines.Skip(1)) : string.Empty;
            // 去掉纯标题重复
            if (body == title) body = string.Empty;
            NotificationCaptured?.Invoke(this, new SystemNotification(app, title, body));
        }
        catch
        {
            // 尽力而为：任何异常都静默，不影响主流程
        }
    }

    public void Stop()
    {
        lock (_gate) { _started = false; }
        try { _timer?.Dispose(); _timer = null; } catch { /* ignore */ }
    }

    public void Dispose() => Stop();
}
