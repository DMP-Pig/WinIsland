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
    private MiniPlayerWindow? _miniPlayer;
    private TrayIcon? _tray;
    private SingleInstance? _singleInstance;
    private AppSettings? _lastPositionSettings;
    private BluetoothMonitor? _bluetooth;
    private SystemNotificationMonitor? _systemNotifications;
    private NetworkStatusMonitor? _network;
    private NotificationService? _notifications;
    private NotificationHistoryService? _notificationHistory;
    private GlobalHotkeyService? _hotkeys;
    private QuickLauncherWindow? _launcher;
    private ClipboardPanelWindow? _clipboardPanel;
    private IslandApiServer? _islandApi;
    private MediaAppRegistry? _mediaApps;
    private ScreenCaptureMonitor? _screenCapture;
    private CalendarService? _calendar;
    private RssMailService? _rssMail;

    // ── 效率工具 / 波纹 / 更新服务（共享实例：设置页与灵动岛组件共用同一份数据）──
    private AudioWaveService? _wave;
    private KeyboardIndicatorMonitor? _keyboard;
    private ClipboardHistoryService? _clipboard;
    private TodoService? _todo;
    private ScheduleService? _schedule;
    private PomodoroService? _pomodoro;
    private UpdaterService? _updater;

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
        _bluetooth.DeviceConnected += (_, name) => _notifications.Show("蓝牙设备已连接", name, "\uE702", "Bluetooth");
        _bluetooth.DeviceDisconnected += (_, name) => _notifications.Show("蓝牙设备已断开", name, "\uE702", "Bluetooth");
        _systemNotifications = new SystemNotificationMonitor();
        _systemNotifications.NotificationCaptured += (_, n) => _notifications.Show(n.Title, n.Body, "\uE945", n.AppName);
        // 断网 / 网络恢复提醒（每次状态变化只提示一次；去抖在服务内部）
        _network = new NetworkStatusMonitor();
        _network.NetworkLost += (_, _) =>
        {
            if (_settings!.Current.NetworkNotifyEnabled)
                _notifications?.Show(Localization.Get("Network_LostTitle"), Localization.Get("Network_LostBody"), "\uE945", "WinIsland");
        };
        _network.NetworkRestored += (_, _) =>
        {
            if (_settings!.Current.NetworkNotifyEnabled)
                _notifications?.Show(Localization.Get("Network_BackTitle"), Localization.Get("Network_BackBody"), "\uE945", "WinIsland");
        };

        // ── 效率工具 / 波纹 / 更新服务 ──
        _wave = new AudioWaveService();
        _keyboard = new KeyboardIndicatorMonitor();
        _clipboard = new ClipboardHistoryService();
        _todo = new TodoService();
        _schedule = new ScheduleService();
        _pomodoro = new PomodoroService();
        _updater = new UpdaterService();
        if (_settings.Current.WaveVisualizerEnabled) _wave.Start();
        _wave.SetSyncEnabled(_settings.Current.WaveSyncEnabled);
        _wave.SetSensitivity(_settings.Current.WaveSensitivity);
        _clipboard.SetEnabled(_settings.Current.ClipboardHistoryEnabled);
        _clipboard.MaxEntries = _settings.Current.ClipboardHistoryMax;
        // 复制提示（14 已复制 / 15 验证码 / 27 复制进度）：独立于剪贴板历史，任何一项开启即轮询
        _clipboard.EntryAdded += OnClipboardEntryAdded;
        UpdateClipboardPolling();
        _schedule.Reminder += item => _notifications?.Show("日程提醒", item.Title, "\uE8B7", "WinIsland");

        _vm = new IslandViewModel(_coordinator, _settings, _lyrics,
            _wave, _keyboard, _clipboard, _todo, _schedule, _pomodoro);
        // 番茄钟到点提醒
        _vm.PomodoroCompletedRequested += phase =>
            _notifications?.Show("番茄钟结束",
                phase == PomodoroPhase.Work ? "工作阶段结束，休息一下吧" : "休息结束，开始新的专注吧",
                "\uE823", "WinIsland");
        _vm.OpenSettingsRequested += (_, _) => OpenSettings();
        _vm.ToggleLyricsWindowRequested += (_, _) => ToggleLyricsWindow();
        // 播放媒体时不弹「正在播放」通知（用户要求；蓝牙/低电量/系统通知等仍保留）
        // _vm.NowPlayingRequested += (title, artist) =>
        //     _notifications?.Show(Localization.Get("NowPlaying_Title"), string.IsNullOrEmpty(artist) ? title : $"{title} - {artist}", "\uE8D6");
        _vm.LowBatteryRequested += percent =>
            _notifications?.Show(Localization.Get("LowBattery_Title"), $"{percent}%", "\uEBA0", "WinIsland");
        _vm.ChargedRequested += percent =>
            _notifications?.Show(Localization.Get("Charged_Title"),
                string.Format(Localization.Get("Charged_Body"), percent), "\uEBA0", "WinIsland");
        _vm.DiskLowRequested += gb =>
            _notifications?.Show(Localization.Get("Disk_Title"),
                string.Format(Localization.Get("Disk_Body"), gb), "\uEDA2", "WinIsland");

        // ── 上岛 API：第三方软件推送信息到灵动岛 ──
        _islandApi = new IslandApiServer(_settings);
        _islandApi.PushReceived += push => Dispatcher.BeginInvoke(() => _vm?.PushIsland(push));
        _islandApi.PushRemoved += id => Dispatcher.BeginInvoke(() => _vm?.RemoveIslandPush(id));
        if (_settings.Current.IslandApiEnabled) _islandApi.Start();

        // ── 屏幕录制 / 截图提示（PrintScreen 钩子 + 录制进程轮询；默认关）──
        _screenCapture = new ScreenCaptureMonitor();
        _screenCapture.ScreenshotTaken += () =>
        {
            _vm?.NotifyScreenshotTaken(); // 灵动岛「已截图」临时指示
            if (_settings!.Current.ScreenCaptureNotifyEnabled && _settings.Current.ScreenshotNotifyEnabled)
                _notifications?.Show(Localization.Get("ScreenCap_ScreenshotTitle"),
                    Localization.Get("ScreenCap_ScreenshotBody"), "\uE7B3", "WinIsland");
        };
        _screenCapture.RecordingChanged += (recording, app) =>
        {
            _vm?.SetRecordingStatus(recording, app); // 灵动岛「录制中」指示
            if (!_settings!.Current.ScreenCaptureNotifyEnabled || !_settings.Current.RecordingNotifyEnabled) return;
            if (recording)
                _notifications?.Show(Localization.Get("ScreenCap_RecordingTitle"),
                    string.Format(Localization.Get("ScreenCap_RecordingBody"), app), "\uE786", "WinIsland");
        };
        // 录屏智能勿扰（录屏时自动勿扰）也需要轮询录制状态，与提示开关共用监控实例
        if (_settings.Current.ScreenCaptureNotifyEnabled || _settings.Current.RecordingDndEnabled)
        {
            _screenCapture.ScreenshotEnabled = _settings.Current.ScreenshotNotifyEnabled;
            _screenCapture.RecordingEnabled = _settings.Current.RecordingNotifyEnabled || _settings.Current.RecordingDndEnabled;
            _screenCapture.Start();
        }

        // ── 日历事件提醒（.ics 本地解析；事件到点弹右上角横幅，默认关）──
        _calendar = new CalendarService();
        _calendar.Reminder += ev => Dispatcher.BeginInvoke(() =>
        {
            if (!_settings!.Current.CalendarEnabled) return;
            var start = ev.Start.LocalDateTime;
            var isAllDay = start.Date == start && ev.End - ev.Start >= TimeSpan.FromDays(1);
            var body = isAllDay
                ? $"{start:yyyy-MM-dd}  {ev.Title}"
                : $"{start:HH:mm} ~ {ev.End.LocalDateTime:HH:mm}  {ev.Title}";
            _notifications?.Show(Localization.Get("Calendar_ReminderTitle"), body, "\uE787", "Calendar");
        });
        _calendar.Refresh(_settings.Current.CalendarIcsPath);

        // ── RSS 订阅 / 邮件提醒（后台轮询；默认关，仅开启后联网）──
        _rssMail = new RssMailService();
        _rssMail.RssItemReceived += (title, summary, link) => Dispatcher.BeginInvoke(() =>
        {
            if (!_settings!.Current.RssNotifyEnabled) return;
            _notifications?.Show(Localization.Get("Rss_NotifyTitle"),
                string.IsNullOrEmpty(summary) ? title : summary, "\uE8A5", "RSS");
        });
        _rssMail.MailReceived += (subject, from, date) => Dispatcher.BeginInvoke(() =>
        {
            if (!_settings!.Current.MailNotifyEnabled) return;
            var body = string.IsNullOrEmpty(from)
                ? date
                : (string.IsNullOrEmpty(date) ? from : $"{from} · {date}");
            _notifications?.Show(Localization.Get("Mail_NotifyTitle"),
                string.IsNullOrEmpty(subject) ? body : $"{subject}\n{body}", "\uE715", "Mail");
        });
        ApplyRssMail(_settings.Current);

        // ── Island windows (one per selected monitor) ──
        RecreateWindows();

        // ── 迷你播放器（独立悬浮小窗，跟随媒体状态自动显隐）──
        _miniPlayer = new MiniPlayerWindow(_vm, _theme, _settings);
        _vm.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(IslandViewModel.HasMedia)) UpdateMiniPlayerVisibility();
        };
        UpdateMiniPlayerVisibility();

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
        _tray.DoNotDisturbRequested += (_, _) =>
        {
            var on = !DoNotDisturb.IsActive(_settings!.Current);
            _settings.Update(s => s.DoNotDisturbManual = on);
            _tray.SetDoNotDisturbChecked(on);
        };
        _tray.UpdateRequested += async (_, _) =>
        {
            try
            {
                if (_updater is null) return;
                var found = await _updater.CheckAsync();
                MessageBox.Show(found ? Localization.Get("Update_Found") : Localization.Get("Update_None"),
                    Localization.Get("Update_Title"), MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { AppLogger.Error("Check update failed", ex); }
        };
        _tray.LogsRequested += (_, _) =>
        {
            try { new LogViewerWindow().Show(); }
            catch (Exception ex) { AppLogger.Error("Open log viewer failed", ex); }
        };
        _tray.ExitRequested += (_, _) => Shutdown();

        // ── 全局快捷键（35 全局快捷键大全：组合键可在设置中自定义，见 GlobalHotkeyService）──
        _hotkeys = new GlobalHotkeyService(_settings.Current);
        _hotkeys.PlayPausePressed += () => _vm?.PlayPauseCommand.Execute(null);
        _hotkeys.NextPressed += () => _ = _coordinator?.NextAsync();
        _hotkeys.PreviousPressed += () => _ = _coordinator?.PreviousAsync();
        _hotkeys.ToggleVisibilityPressed += () => _vm?.ToggleUserVisible();
        _hotkeys.ExpandPressed += () =>
        {
            if (_vm is not null) _vm.IsExpanded = !_vm.IsExpanded; // 展开/收起
        };
        // 快速启动器：Ctrl+Space 弹出/收起（快捷键本身由 GlobalHotkeyService 注册）
        _launcher = new QuickLauncherWindow(_theme);
        _hotkeys.LauncherPressed += () => Dispatcher.BeginInvoke(() => _launcher?.Toggle());
        // 剪贴板历史面板（Ctrl+Alt+V）
        _clipboardPanel = new ClipboardPanelWindow(_theme, _clipboard);
        _hotkeys.ClipboardPanelPressed += () => Dispatcher.BeginInvoke(() => _clipboardPanel?.Toggle());
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

            // 屏幕录制/截图提示：开关或细分项变化时实时生效
            if (_screenCapture is not null)
            {
                _screenCapture.ScreenshotEnabled = s.ScreenshotNotifyEnabled;
                _screenCapture.RecordingEnabled = s.RecordingNotifyEnabled;
                var capRunning = _screenCapture.IsRunning;
                var capWanted = s.ScreenCaptureNotifyEnabled || s.RecordingDndEnabled;
                _screenCapture.RecordingEnabled = s.RecordingNotifyEnabled || s.RecordingDndEnabled;
                if (capWanted && !capRunning) _screenCapture.Start();
                else if (!capWanted && capRunning) _screenCapture.Stop();
            }

            // 日历提醒：路径/开关变化立即重新解析（服务内部有文件未变短路）
            _calendar?.Refresh(s.CalendarIcsPath);

            // RSS / 邮件提醒：开关、地址、间隔变化立即生效
            ApplyRssMail(s);

            // 全局快捷键：组合键 / 开关变化实时重新注册
            _hotkeys?.Apply(s);

            // 上岛 API：启用/端口变化时重启
            if (_islandApi is not null)
            {
                var running = _islandApi.IsRunning;
                if (s.IslandApiEnabled && !running) _islandApi.Start();
                else if (!s.IslandApiEnabled && running) _islandApi.Stop();
            }

            // 波纹可视化开关
            if (s.WaveVisualizerEnabled) _wave?.Start(); else _wave?.Stop();
            _wave?.SetSyncEnabled(s.WaveSyncEnabled);
            _wave?.SetSensitivity(s.WaveSensitivity);

            // 剪贴板历史开关与保留条数；复制提示（已复制/验证码/进度）无需历史也可轮询
            if (_clipboard is not null)
            {
                _clipboard.SetEnabled(s.ClipboardHistoryEnabled);
                _clipboard.MaxEntries = s.ClipboardHistoryMax;
            }
            UpdateClipboardPolling();

            // 尺寸设置变更 → 应用到灵动岛
            foreach (var w in _windows) w.ApplySize();

            _vm?.UpdateVisibility();
            UpdateMiniPlayerVisibility();
        };

        // Sync registry state once at startup (in case settings were edited externally).
        if (AutoStart.IsEnabled() != _settings.Current.StartWithWindows)
            AutoStart.SetEnabled(_settings.Current.StartWithWindows);

        // ── Start media pipeline ──
        _coordinator.Start();

        if (_settings.Current.BluetoothNotifyEnabled) _bluetooth?.Start();
        if (_settings.Current.NotificationTakeoverEnabled) _systemNotifications?.Start();
        _network?.Start(); // 网络监控很轻量，始终启动；是否弹横幅由 NetworkNotifyEnabled 开关控制
        _vm.UpdateVisibility();

        if (_settings.Current.StandaloneLyricsWindow)
            ShowLyricsWindow();

        // 启动时自动检查新版本（可选；需联网，默认关闭）
        if (_settings.Current.AutoUpdateCheck)
            _ = CheckForUpdatesAsync(showWhenUpToDate: false);

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

    /// <summary>检查更新；有新版时弹通知（可带下载链接）。</summary>
    private async Task CheckForUpdatesAsync(bool showWhenUpToDate)
    {
        try
        {
            if (_updater is null) return;
            var hasNew = await _updater.CheckAsync();
            if (!hasNew && showWhenUpToDate)
                _notifications?.Show(Localization.Get("Update_Title"), Localization.Get("Update_None"), "\uE72E");
        }
        catch (Exception ex)
        {
            AppLogger.Error("Update check failed", ex);
        }
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
        var win = new SettingsWindow(vm, _settings, _cider, _notificationHistory,
            _todo, _schedule, _clipboard, _pomodoro, _updater);
        win.ShowDialog();
    }

    /// <summary>把 RSS/邮件设置应用到后台轮询服务（开关/地址/间隔等变化时即时生效）。</summary>
    private void ApplyRssMail(AppSettings s)
    {
        _rssMail?.Configure(
            s.RssNotifyEnabled, s.RssUrls, s.RssIntervalMinutes,
            s.MailNotifyEnabled, s.MailPop3Server, s.MailPop3Port, s.MailUseSsl,
            s.MailUser, s.MailPassword, s.MailCheckMinutes);
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

    /// <summary>迷你播放器显隐策略：开关开启且正在播放媒体时显示，否则隐藏并保存位置。</summary>
    private void UpdateMiniPlayerVisibility()
    {
        if (_miniPlayer is null || _settings is null || _vm is null) return;
        var s = _settings.Current;
        if (s.MiniPlayerEnabled && _vm.HasMedia)
        {
            if (!_miniPlayer.IsVisible)
            {
                _miniPlayer.PositionFromSettings();
                _miniPlayer.Show();
            }
        }
        else if (_miniPlayer.IsVisible)
        {
            SaveMiniPlayerPosition();
            _miniPlayer.Hide();
        }
    }

    /// <summary>持久化迷你播放器位置（直接写 Current 并落盘，不触发 Changed 避免递归）。</summary>
    private void SaveMiniPlayerPosition()
    {
        if (_miniPlayer is null || _settings is null) return;
        try
        {
            _settings.Current.MiniPlayerLeft = _miniPlayer.Left;
            _settings.Current.MiniPlayerTop = _miniPlayer.Top;
            _settings.Save();
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"保存迷你播放器位置失败: {ex.Message}");
        }
    }

    /// <summary>根据设置决定剪贴板轮询：历史记录或任一复制提示开启时轮询。</summary>
    private void UpdateClipboardPolling()
    {
        if (_clipboard is null || _settings is null) return;
        var s = _settings.Current;
        var copyUi = s.CopyToastEnabled || s.CodeToastEnabled || s.CopyProgressEnabled;
        _clipboard.SetPolling(s.ClipboardHistoryEnabled || copyUi);
    }

    /// <summary>剪贴板新增内容 → 已复制 / 验证码 / 大文本进度提示（14/15/27）。</summary>
    private void OnClipboardEntryAdded(ClipboardEntry entry)
    {
        try
        {
            if (_notifications is null || _settings is null) return;
            var s = _settings.Current;
            var text = entry.Text;
            if (string.IsNullOrWhiteSpace(text)) return;

            // 验证码高亮提示优先（短信场景，通常很短）
            if (s.CodeToastEnabled && VerificationCodeDetector.TryExtract(text, out var code))
            {
                _notifications.Show(Localization.Get("Clipboard_CodeTitle"),
                    Localization.Get("Clipboard_CodeBody").Replace("{code}", code),
                    "", "Clipboard");
                return;
            }

            // 大文本：复制进度（Windows 不暴露真实进度，按长度估算动画）
            if (s.CopyProgressEnabled && text.Length >= Math.Max(500, s.CopyProgressThreshold))
            {
                var estimatedMs = Math.Clamp(400 + text.Length / 60, 400, 1800);
                _notifications.ShowCopyProgress(
                    Localization.Get("Clipboard_CopyingTitle"),
                    Localization.Get("Clipboard_CopyingBody"),
                    estimatedMs,
                    Localization.Get("Clipboard_CopiedTitle"),
                    ClipboardPreview(text),
                    "", "Clipboard");
                return;
            }

            // 普通复制：已复制提示
            if (s.CopyToastEnabled)
                _notifications.Show(Localization.Get("Clipboard_CopiedTitle"), ClipboardPreview(text), "", "Clipboard");
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"剪贴板提示失败: {ex.Message}");
        }
    }

    /// <summary>剪贴板预览：取首行、截断 45 字符。</summary>
    private static string ClipboardPreview(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;
        var t = text.Trim();
        var nl = t.IndexOfAny(new[] { '\r', '\n' });
        if (nl >= 0) t = t.Substring(0, nl);
        return t.Length <= 45 ? t : t.Substring(0, 42) + "…";
    }

    protected override void OnExit(ExitEventArgs e)
    {
        AppLogger.Info("WinIsland exiting.");
        try
        {
            SaveMiniPlayerPosition();
            _settings?.Save();
            _vm?.SavePlaybackState(); // 退出前保存播放位置，重启后恢复（暂停时不再跳回开头）
            _vm?.Dispose();
            _wave?.Dispose();
            _keyboard?.Dispose();
            _clipboard?.Dispose();
            _todo?.Dispose();
            _schedule?.Dispose();
            _pomodoro?.Dispose();
            _coordinator?.Dispose();
            _bluetooth?.Dispose();
            _systemNotifications?.Dispose();
            _network?.Dispose();
            _hotkeys?.Dispose();
            _islandApi?.Dispose();
            _screenCapture?.Dispose();
            _calendar?.Dispose();
            _rssMail?.Dispose();
            _tray?.Dispose();
            _singleInstance?.Dispose();
            _miniPlayer?.Close();
            foreach (var w in _windows) w.Close();
        }
        catch (Exception ex)
        {
            AppLogger.Error("Error during shutdown", ex);
        }

        base.OnExit(e);
    }
}


