using System;
using System.Collections.Generic;
using System.Threading;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;

namespace WinIsland.Services;

/// <summary>
/// 监听已配对蓝牙设备的连接/断开。
/// 轮询 BluetoothDevice.ConnectionStatus（真实连接状态，AEP 的 IsConnected/IsPresent 不可靠）。
/// 事件在后台线程触发，调用方需封送到 UI 线程。
/// </summary>
public sealed class BluetoothMonitor : IDisposable
{
    private const string NameKey = "System.ItemNameDisplay";

    private System.Threading.Timer? _timer;
    private readonly Dictionary<string, string> _names = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, bool> _connected = new(StringComparer.OrdinalIgnoreCase);
    private readonly object _gate = new();
    private bool _started;
    private bool _firstPass = true;
    private bool _polling;

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
        _timer = new System.Threading.Timer(_ => Poll(), null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(8));
        AppLogger.Info("Bluetooth monitor started (ConnectionStatus poll 8s).");
    }

    private void Poll()
    {
        if (_polling) return;
        _polling = true;
        try
        {
            var devices = DeviceInformation.FindAllAsync(
                BluetoothDevice.GetDeviceSelector(),
                new[] { NameKey },
                DeviceInformationKind.AssociationEndpoint)
                .AsTask().GetAwaiter().GetResult();

            var current = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
            foreach (var d in devices)
            {
                var name = (d.Properties.TryGetValue(NameKey, out var n) && n is string s && s.Length > 0) ? s : d.Name;
                if (!_names.ContainsKey(d.Id)) _names[d.Id] = name;

                var conn = false;
                try
                {
                    var bt = BluetoothDevice.FromIdAsync(d.Id).AsTask().GetAwaiter().GetResult();
                    conn = bt is not null && bt.ConnectionStatus == BluetoothConnectionStatus.Connected;
                }
                catch (Exception ex)
                {
                    AppLogger.Debug($"BT FromIdAsync failed for '{name}': {ex.Message}");
                }
                current[d.Id] = conn;
                AppLogger.Debug($"BT: '{name}' ConnectionStatus={(conn ? "Connected" : "Disconnected")}");
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
        finally
        {
            _polling = false;
        }
    }

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
