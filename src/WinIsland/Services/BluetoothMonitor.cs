using System;
using System.Collections.Generic;
using System.Threading;
using Windows.Devices.Enumeration;

namespace WinIsland.Services;

/// <summary>
/// 监听已配对蓝牙设备的连接/断开（耳机/音箱/鼠标等）。
/// 轮询 AEP：连接 = System.Devices.Aep.IsConnected 或 IsPresent 为真（部分设备只报其一）。
/// 轮询间隔 5s，带诊断日志；事件在后台线程触发，调用方需封送到 UI 线程。
/// </summary>
public sealed class BluetoothMonitor : IDisposable
{
    private const string ConnectedKey = "System.Devices.Aep.IsConnected";
    private const string PresentKey = "System.Devices.Aep.IsPresent";
    private const string NameKey = "System.ItemNameDisplay";

    private System.Threading.Timer? _timer;
    private readonly Dictionary<string, string> _names = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, bool> _connected = new(StringComparer.OrdinalIgnoreCase);
    private readonly object _gate = new();
    private bool _started;
    private bool _firstPass = true;

    /// <summary>蓝牙设备连接（参数：设备名）。</summary>
    public event EventHandler<string>? DeviceConnected;
    /// <summary>蓝牙设备断开（参数：设备名）。</summary>
    public event EventHandler<string>? DeviceDisconnected;

    public void Start()
    {
        lock (_gate)
        {
            if (_started) return;
            _started = true;
            _firstPass = true;
            _names.Clear();
            _connected.Clear();
        }
        _timer?.Dispose();
        _timer = new System.Threading.Timer(_ => Poll(), null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5));
        AppLogger.Info("Bluetooth monitor started (poll 5s).");
    }

    private void Poll()
    {
        try
        {
            var devices = DeviceInformation.FindAllAsync(
                Windows.Devices.Bluetooth.BluetoothDevice.GetDeviceSelector(),
                new[] { ConnectedKey, PresentKey, NameKey },
                DeviceInformationKind.AssociationEndpoint)
                .AsTask().GetAwaiter().GetResult();

            var current = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
            foreach (var d in devices)
            {
                var conn = IsConnectedFlag(d);
                var name = (d.Properties.TryGetValue(NameKey, out var n) && n is string s && s.Length > 0) ? s : d.Name;
                if (!_names.ContainsKey(d.Id)) _names[d.Id] = name;
                current[d.Id] = conn;
                AppLogger.Debug($"BT: '{name}' connected={conn} IsConnected={Prop(d, ConnectedKey)} IsPresent={Prop(d, PresentKey)}");
            }

            lock (_gate)
            {
                if (_firstPass)
                {
                    _firstPass = false;
                    _connected.Clear();
                    foreach (var kv in current) _connected[kv.Key] = kv.Value;
                    AppLogger.Info($"Bluetooth baseline: {current.Count} devices.");
                    return;
                }

                foreach (var kv in current)
                {
                    var was = _connected.TryGetValue(kv.Key, out var w) && w;
                    if (kv.Value && !was) Raise(DeviceConnected, kv.Key);
                    else if (!kv.Value && was) Raise(DeviceDisconnected, kv.Key);
                    _connected[kv.Key] = kv.Value;
                }

                // 从枚举中消失的设备视为断开
                var gone = new List<string>();
                foreach (var id in _connected.Keys) if (!current.ContainsKey(id)) gone.Add(id);
                foreach (var id in gone)
                {
                    if (_connected.TryGetValue(id, out var w) && w) Raise(DeviceDisconnected, id);
                    _connected.Remove(id);
                }
            }
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Bluetooth poll failed: {ex.Message}");
        }
    }

    private static bool IsConnectedFlag(DeviceInformation d)
    {
        if (d.Properties.TryGetValue(ConnectedKey, out var c) && c is bool cb && cb) return true;
        if (d.Properties.TryGetValue(PresentKey, out var p) && p is bool pb && pb) return true;
        return false;
    }

    private static string Prop(DeviceInformation d, string key)
        => d.Properties.TryGetValue(key, out var v) ? v?.ToString() ?? "null" : "absent";

    private void Raise(EventHandler<string>? ev, string id)
    {
        var name = _names.TryGetValue(id, out var n) ? n : "蓝牙设备";
        AppLogger.Info($"Bluetooth event: {(ev == DeviceConnected ? "connected" : "disconnected")} '{name}'");
        ev?.Invoke(this, name);
    }

    public void Stop()
    {
        lock (_gate)
        {
            _started = false;
            _firstPass = true;
            _names.Clear();
            _connected.Clear();
        }
        try { _timer?.Dispose(); _timer = null; } catch { /* ignore */ }
        AppLogger.Info("Bluetooth monitor stopped.");
    }

    public void Dispose() => Stop();
}
