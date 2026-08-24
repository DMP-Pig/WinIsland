using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using Application = System.Windows.Application;
using Localization = WinIsland.UI.Localization;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Threading;
using WinIsland.Diagnostics;
using WinIsland.Services;
using WinIsland.UI;

namespace WinIsland;

public partial class App : Application
{
    private SettingsService? _settings;
    private ThemeService? _theme;
    private CiderMediaProvider? _cider;
    private MediaCoordinator? _coordinator;
    private LyricsService? _lyrics;
    private IslandViewModel? _vm;
    private readonly List<IslandWindow> _windows = new();
    private LyricsWindow? _lyricsWindow;
    private TrayIcon? _tray;
    private SingleInstance? _singleInstance;
    private AppSettings? _lastPositionSettings;
    private BluetoothMonitor? _bluetooth;
    private SystemNotificationMonitor? _systemNotifications;
    private NotificationService? _notifications;
    private NotificationHistoryService? _notificationHistory;
    private GlobalHotkeyService? _hotkeys;
    private IslandApiServer? _islandApi;
    private MediaAppRegistry? _mediaApps;

    public App()
    {
        ShutdownMode = ShutdownMode.OnExplicitShutdown;
    }

    [DllImport("user32.dll")]
    private static extern bool SetProcessDpiAwarenessContext(IntPtr value);
    private static readonly IntPtr DpiAwarenessPerMonitorV2 = new(-4);

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // 窗口创建前强制 PerMonitorV2，避免启动瞬间按系统缩放渲染导致文字先大后恢复正常
        try { SetProcessDpiAwarenessContext(DpiAwarenessPerMonitorV2); }
        catch { /* manifest 已声明，忽略 */ }

        // ── Crash safety: log everything, never show a raw crash dialog. ──
        DispatcherUnhandledException += (_, args) =>
        {
            AppLogger.Error("Unhandled dispatcher exception", args.Exception);
            args.Handled = true;
        };
        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
            AppLogger.Error("Unhandled AppDomain exception", args.ExceptionObject as Exception);
        TaskScheduler.UnobservedTaskException += (_, args) =>
        {
            AppLogger.Error("Unobserved task exception", args.Exception);
            args.SetObserved();
        };

        AppPaths.EnsureDirectories();
        AppLogger.Info($"WinIsland starting. PID={Environment.ProcessId}");

        // ── Single instance ──
        _singleInstance = new SingleInstance();
        if (!_singleInstance.TryAcquire())
        {
            AppLogger.Info("Another instance is running; signaling show and exiting.");
            Shutdown();
            return;
        }

        _singleInstance.ShowRequested += (_, _) => Dispatcher.BeginInvoke(() => _vm?.ForceShow());

        // ── Services ──
        _settings = new SettingsService();
        _lastPositionSettings = _settings.Current.Clone();
        _theme = new ThemeService();
        _theme.Apply(_settings.Current);
        Localization.CurrentLanguage = _settings.Current.Language;

        _cider = new CiderMediaProvider(_settings);
        _mediaApps = new MediaAppRegistry();
        var smtc = new SmtcMediaProvider(_settings, _mediaApps, preferredAppId: "Cider"); // Cider session priority, avoid other active sessions
        var title = new WindowTitleMediaProvider();
        _coordinator = new MediaCoordinator(_settings, smtc, _cider, title, Dispatcher);
        _lyrics = new LyricsService(_settings, _cider);

        // ── 通知服务（右上角横幅 + 蓝牙/系统通知监控）──
        _notificationHistory = new NotificationHistoryService();
        _notifications = new NotificationService(Dispatcher, _settings, _notificationHistory);
        _bluetooth = new BluetoothMonitor();
        _bluetooth.DeviceConnected += (_, name) => _notifications.Show("蓝牙设备已连接", name, "\uE702");
        _bluetooth.DeviceDisconnected += (_, name) => _notifications.Show("蓝牙设备已断开", name, "\uE702");
        _systemNotifications = new SystemNotificationMonitor();
        _systemNotifications.NotificationCaptured += (_, n) => _notifications.Show(n.Title, n.Body, "\uE945");

        _vm = new IslandViewModel(_coordinator, _settings, _lyrics);
        _vm.OpenSettingsRequested += (_, _) => OpenSettings();
        _vm.ToggleLyricsWindowRequested += (_, _) => ToggleLyricsWindow();
        // 播放媒体时不弹「正在播放」通知（用户要求；蓝牙/低电量/系统通知等仍保留）
        // _vm.NowPlayingRequested += (title, artist) =>
        //     _notifications?.Show(Localization.Get("NowPlaying_Title"), string.IsNullOrEmpty(artist) ? title : $"{title} - {artist}", "\uE8D6");
        _vm.LowBatteryRequested += percent =>
            _notifications?.Show(Localization.Get("LowBattery_Title"), $"{percent}%", "\uEBA0");

        // ── 上岛 API：第三方软件推送信息到灵动岛 ──
        _islandApi = new IslandApiServer(_settings);
        _islandApi.PushReceived += push => Dispatcher.BeginInvoke(() => _vm?.PushIsland(push));
        _islandApi.PushRemoved += id => Dispatcher.BeginInvoke(() => _vm?.RemoveIslandPush(id));
        if (_settings.Current.IslandApiEnabled) _islandApi.Start();

        // ── Island windows (one per selected monitor) ──
        RecreateWindows();

        // ── Tray ──
        _tray = new TrayIcon(_settings);
        _tray.ShowHideRequested += (_, _) => _vm.ToggleUserVisible();
        _tray.SettingsRequested += (_, _) => OpenSettings();
        _tray.ToggleLyricsRequested += (_, _) => ToggleLyricsWindow();
        _tray.AutoStartRequested += (_, _) =>
        {
            var enable = !AutoStart.IsEnabled();
            AutoStart.SetEnabled(enable);
            _settings.Update(s => s.StartWithWindows = enable);
            _tray.SetAutoStartChecked(enable);
        };
        _tray.ExitRequested += (_, _) => Shutdown();

        // ── 全局快捷键（Ctrl+Alt+P/←/→/I）──
        _hotkeys = new GlobalHotkeyService();
        _hotkeys.PlayPausePressed += () => _vm?.PlayPauseCommand.Execute(null);
        _hotkeys.NextPressed += () => _ = _coordinator?.NextAsync();
        _hotkeys.PreviousPressed += () => _ = _coordinator?.PreviousAsync();
        _hotkeys.ToggleVisibilityPressed += () => _vm?.ToggleUserVisible();
        _hotkeys.SetEnabled(_settings.Current.GlobalHotkeysEnabled);

        // ── Settings changed → re-apply live ──
        _settings.Changed += (_, s) =>
        {
            _theme?.Apply(s);
            Localization.CurrentLanguage = s.Language;

            if (AutoStart.IsEnabled() != s.StartWithWindows)
                AutoStart.SetEnabled(s.StartWithWindows);

            if (s.StandaloneLyricsWindow && _lyricsWindow is null)
                ShowLyricsWindow();
            if (!s.StandaloneLyricsWindow && _lyricsWindow is { IsVisible: true })
                _lyricsWindow.Hide();

            // 仅位置/显示器类设置变更时重定位；锁定等其它变更保留拖动后的位置
            var posChanged = _lastPositionSettings is null
                || s.Position != _lastPositionSettings.Position
                || s.Monitor != _lastPositionSettings.Monitor
                || s.MonitorIndex != _lastPositionSettings.MonitorIndex
                || s.OffsetX != _lastPositionSettings.OffsetX
                || s.OffsetY != _lastPositionSettings.OffsetY;
            _lastPositionSettings = s.Clone();
            if (posChanged) RecreateWindows();

            // 通知监控开关
            if (s.BluetoothNotifyEnabled) _bluetooth?.Start(); else _bluetooth?.Stop();
            if (s.NotificationTakeoverEnabled) _systemNotifications?.Start(); else _systemNotifications?.Stop();

            // 全局快捷键开关
            _hotkeys?.SetEnabled(s.GlobalHotkeysEnabled);

            // 上岛 API：启用/端口变化时重启
            if (_islandApi is not null)
            {
                var running = _islandApi.IsRunning;
                if (s.IslandApiEnabled && !running) _islandApi.Start();
                else if (!s.IslandApiEnabled && running) _islandApi.Stop();
            }

            // 尺寸设置变更 → 应用到灵动岛
            foreach (var w in _windows) w.ApplySize();

            _vm?.UpdateVisibility();
        };

        // Sync registry state once at startup (in case settings were edited externally).
        if (AutoStart.IsEnabled() != _settings.Current.StartWithWindows)
            AutoStart.SetEnabled(_settings.Current.StartWithWindows);

        // ── Start media pipeline ──
        _coordinator.Start();

        if (_settings.Current.BluetoothNotifyEnabled) _bluetooth?.Start();
        if (_settings.Current.NotificationTakeoverEnabled) _systemNotifications?.Start();
        _vm.UpdateVisibility();

        if (_settings.Current.StandaloneLyricsWindow)
            ShowLyricsWindow();

        // ── Diagnostics mode: report and exit. ──
        if (e.Args.Contains("--diagnose", StringComparer.OrdinalIgnoreCase))
            _ = RunDiagnosticsAsync();

        // ── Demo mode: show a fake track for preview / testing. ──
        if (e.Args.Contains("--demo", StringComparer.OrdinalIgnoreCase))
            _vm.InjectDemoMedia();

        // ── Open the settings window at startup (also handy from the CLI). ──
        if (e.Args.Contains("--settings", StringComparer.OrdinalIgnoreCase))
            Dispatcher.BeginInvoke(OpenSettings);
    }

    private async Task RunDiagnosticsAsync()
    {
        try
        {
            var text = await DiagnosticsCommand.RunAsync(_settings!, _cider);
            var path = Path.Combine(AppPaths.AppDataDir, "diagnostics.txt");
            await File.WriteAllTextAsync(path, text);
            MessageBox.Show($"诊断信息已写入：\n{path}\n\n---\n{text}",
                "WinIsland 诊断", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            AppLogger.Error("Diagnostics failed", ex);
        }
    }

    private void RecreateWindows()
    {
        if (_settings is null || _vm is null || _theme is null) return;

        var screens = ScreenHelper.ResolveScreens(_settings.Current);

        // Recreate only when the monitor selection actually changed.
        if (_windows.Count == screens.Count &&
            _windows.Select(w => w.Screen.DeviceName).SequenceEqual(screens.Select(s => s.DeviceName)))
        {
            foreach (var w in _windows) w.Reposition();
            return;
        }

        foreach (var w in _windows)
        {
            w.Close();
        }

        _windows.Clear();
        foreach (var screen in screens)
        {
            var win = new IslandWindow(_vm, _theme, _settings, screen);
            _windows.Add(win);
            // Force the window to load now so IsVisible changes (which can happen before
            // the first Show) can drive it; OnLoaded re-applies the correct visibility.
            win.Show();
            if (!_vm.IsVisible) win.Hide();
        }
    }

    private void OpenSettings()
    {
        if (_settings is null) return;
        var vm = new SettingsViewModel(_settings, _mediaApps);
        var win = new SettingsWindow(vm, _settings, _cider, _notificationHistory);
        win.ShowDialog();
    }

    private void ToggleLyricsWindow()
    {
        if (_lyricsWindow is { IsVisible: true })
        {
            _lyricsWindow.Hide();
            _settings!.Update(s => s.StandaloneLyricsWindow = false);
            _tray?.SetLyricsChecked(false);
        }
        else
        {
            ShowLyricsWindow();
        }
    }

    private void ShowLyricsWindow()
    {
        if (_vm is null) return;
        if (_lyricsWindow is null)
        {
            _lyricsWindow = new LyricsWindow(_vm);
        }

        _lyricsWindow.PositionNearBottom();
        _lyricsWindow.Show();
        _tray?.SetLyricsChecked(true);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        AppLogger.Info("WinIsland exiting.");
        try
        {
            _settings?.Save();
            _vm?.SavePlaybackState(); // 退出前保存播放位置，重启后恢复（暂停时不再跳回开头）
            _vm?.Dispose();
            _coordinator?.Dispose();
            _bluetooth?.Dispose();
            _systemNotifications?.Dispose();
            _hotkeys?.Dispose();
            _islandApi?.Dispose();
            _tray?.Dispose();
            _singleInstance?.Dispose();
            foreach (var w in _windows) w.Close();
        }
        catch (Exception ex)
        {
            AppLogger.Error("Error during shutdown", ex);
        }

        base.OnExit(e);
    }
}


