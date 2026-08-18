using System;
using System.Collections.Generic;
using Windows.Devices.Enumeration;

namespace WinIsland.Services;

/// <summary>
/// 监听已配对蓝牙设备（耳机/音箱/鼠标等）的连接与断开。
/// 用 DeviceWatcher 跟踪 AEP 的 System.Devices.Aep.IsConnected 状态变化，
/// 事件在后台线程触发，调用方需自行封送到 UI 线程。
/// 说明：部分经典蓝牙设备不通过 AEP 上报连接状态，属系统限制（尽力而为）。
/// </summary>
public sealed class BluetoothMonitor : IDisposable
{
    private const string ConnectedKey = "System.Devices.Aep.IsConnected";
    private const string NameKey = "System.ItemNameDisplay";

    private DeviceWatcher? _watcher;
    private readonly HashSet<string> _connectedIds = new(StringComparer.OrdinalIgnoreCase);
    private readonly object _gate = new();
    private bool _started;
    private bool _enumerationDone;

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
            _enumerationDone = false;
            _connectedIds.Clear();
        }

        try
        {
            var selector = Windows.Devices.Bluetooth.BluetoothDevice.GetDeviceSelector();
            _watcher = DeviceInformation.CreateWatcher(selector, null, DeviceInformationKind.AssociationEndpoint);
            _watcher.Added += OnInfo;
            _watcher.Updated += OnUpdate;
            _watcher.Removed += OnRemoved;
            _watcher.EnumerationCompleted += (_, _) =>
            {
                lock (_gate) _enumerationDone = true;
                AppLogger.Info("Bluetooth monitor: enumeration completed.");
            };
            _watcher.Start();
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Bluetooth monitor start failed: {ex.Message}");
            lock (_gate) _started = false;
        }
    }

    private void OnInfo(DeviceWatcher sender, DeviceInformation info)
        => Process(info.Id, info.Properties, isRemoved: false);

    private void OnUpdate(DeviceWatcher sender, DeviceInformationUpdate update)
        => Process(update.Id, update.Properties, isRemoved: false);

    private void OnRemoved(DeviceWatcher sender, DeviceInformationUpdate update)
        => Process(update.Id, update.Properties, isRemoved: true);

    private void Process(string id, IReadOnlyDictionary<string, object> props, bool isRemoved)
    {
        string name = string.Empty;
        if (props.TryGetValue(NameKey, out var n) && n is string s && !string.IsNullOrWhiteSpace(s))
            name = s;

        var connected = false;
        if (!isRemoved && props.TryGetValue(ConnectedKey, out var c) && c is bool b)
            connected = b;

        EventHandler<string>? ev = null;
        lock (_gate)
        {
            if (!_enumerationDone)
            {
                // 初次枚举：只建立基线，不触发提示，避免启动时刷屏
                if (connected) _connectedIds.Add(id); else _connectedIds.Remove(id);
                return;
            }

            if (connected && _connectedIds.Add(id))
                ev = DeviceConnected;
            else if (!connected && _connectedIds.Remove(id))
                ev = DeviceDisconnected;
        }

        if (ev is not null && !string.IsNullOrEmpty(name))
            ev(this, name);
    }

    public void Stop()
    {
        lock (_gate)
        {
            _started = false;
            _enumerationDone = false;
            _connectedIds.Clear();
        }
        try
        {
            if (_watcher is not null)
            {
                _watcher.Added -= OnInfo;
                _watcher.Updated -= OnUpdate;
                _watcher.Removed -= OnRemoved;
                _watcher.Stop();
                _watcher = null;
            }
        }
        catch { /* ignore */ }
    }

    public void Dispose() => Stop();
}
