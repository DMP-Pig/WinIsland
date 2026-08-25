using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Point = System.Windows.Point;
using DragEventArgs = System.Windows.DragEventArgs;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using DragDropEffects = System.Windows.DragDropEffects;
using System.Windows.Interop;
using System.Windows.Media;
using Microsoft.Win32;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using MessageBox = System.Windows.MessageBox;
using WinIsland.Diagnostics;
using WinIsland.Services;
using Color = System.Windows.Media.Color;
using Brush = System.Windows.Media.Brush;
using SolidColorBrush = System.Windows.Media.SolidColorBrush;

namespace WinIsland.UI;

public partial class SettingsWindow : Window
{
    private readonly SettingsViewModel _vm;
    private readonly SettingsService _service;
    private readonly CiderMediaProvider? _cider;
    private readonly DispatcherTimer _autoApply;
    private readonly NotificationHistoryService? _history;
    private readonly TodoService? _todo;
    private readonly ScheduleService? _schedule;
    private readonly ClipboardHistoryService? _clipboard;
    private readonly PomodoroService? _pomodoro;
    private readonly UpdaterService? _updater;
    private string _lastAppliedJson;
    private bool _sizeSlidersInitialized; // 初始化期间不触发"手动调整关闭自动"
    private bool _audioOutputLoading;     // 音频输出下拉初始化期间不触发切换

    public SettingsWindow(SettingsViewModel vm, SettingsService service, CiderMediaProvider? cider,
        NotificationHistoryService? history = null,
        TodoService? todo = null,
        ScheduleService? schedule = null,
        ClipboardHistoryService? clipboard = null,
        PomodoroService? pomodoro = null,
        UpdaterService? updater = null)
    {
        _vm = vm;
        _service = service;
        _cider = cider;
        _history = history;
        _todo = todo;
        _schedule = schedule;
        _clipboard = clipboard;
        _pomodoro = pomodoro;
        _updater = updater;
        DataContext = vm;
        InitializeComponent();

        // 初始化完成后显式设置初始选中页（不在 XAML 里用 IsSelected，避免解析期提前触发事件）
        NavList.SelectedIndex = 0;
        MainTabs.SelectedIndex = 0;

        ApplyLocalization();
        TxtMailPass.Password = _vm.Working.MailPassword; // PasswordBox 不支持绑定，回填初值
        RefreshHistory();
        InitAudioOutput();
        InitCardStyle();
        if (_history is not null) _history.Changed += (_, _) => RefreshHistory();

        // 效率工具：初值 + 变动刷新
        if (_todo is not null)
        {
            TodoList.ItemsSource = _todo.Items;
            _todo.Changed += () => TodoList.ItemsSource = _todo.Items;
        }
        if (_schedule is not null)
        {
            ScheduleList.ItemsSource = _schedule.Items;
            ScheduleTimeInput.ToolTip = Localization.Get("Schedule_TimeHint");
            _schedule.Changed += () => ScheduleList.ItemsSource = _schedule.Items;
        }
        if (_pomodoro is not null)
        {
            TxtPomodoroClock.Text = _pomodoro.ClockText;
            _pomodoro.Tick += () => TxtPomodoroClock.Text = _pomodoro.ClockText;
        }

        // 即时生效：轮询检测 Working 变化并立即应用（无保存按钮）
        _lastAppliedJson = JsonSerializer.Serialize(_vm.Working);
        _autoApply = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(300) };
        _autoApply.Tick += (_, _) => AutoApply();
        _autoApply.Start();

        // 语言切换后立即刷新界面文案
        Localization.LanguageChanged += (_, _) => ApplyLocalization();

        // 关闭时兜底保存最后一次改动
        Closed += (_, _) => { try { _vm.Save(); } catch { } };

        // 初始化完成后才允许"手动调整关闭自动"
        Loaded += (_, _) => _sizeSlidersInitialized = true;
    }

    /// <summary>填充音频输出设备下拉框（默认选中系统当前默认设备）。</summary>
    private void InitAudioOutput()
    {
        try
        {
            if (CboAudioOutput is null) return;
            var devices = SystemVolume.GetDevices();
            CboAudioOutput.ItemsSource = devices;
            _audioOutputLoading = true;
            CboAudioOutput.SelectedItem = devices.FirstOrDefault(d => d.IsDefault) ?? devices.FirstOrDefault();
            _audioOutputLoading = false;
        }
        catch (Exception ex)
        {
            _audioOutputLoading = false;
            AppLogger.Warn($"初始化音频输出列表失败: {ex.Message}");
        }
    }

    /// <summary>切换系统默认输出设备（IPolicyConfig 为未公开接口，已打开的播放器需重启后生效）。</summary>
    private void AudioOutput_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_audioOutputLoading || CboAudioOutput.SelectedItem is not SystemVolume.AudioDeviceInfo dev) return;
        try
        {
            if (SystemVolume.SetDefaultDevice(dev.Id) && TxtAudioOutputNote is not null)
                TxtAudioOutputNote.Text = Localization.Get("Media_AudioOutputApplied");
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"切换音频输出失败: {ex.Message}");
        }
    }

    /// <summary>滚轮滚动当前页签的 ScrollViewer（避免被 ComboBox/Slider 拦截）。</summary>
    private void Root_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
    {
        if (e.OriginalSource is not DependencyObject d) return;
        var sv = FindAncestor<System.Windows.Controls.ScrollViewer>(d);
        if (sv is null) return;

        // 纵向优先；横向 ScrollViewer（如组件顺序条）转横向滚动
        if (sv.ComputedVerticalScrollBarVisibility == System.Windows.Visibility.Visible)
        {
            sv.ScrollToVerticalOffset(sv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        else if (sv.ComputedHorizontalScrollBarVisibility == System.Windows.Visibility.Visible)
        {
            sv.ScrollToHorizontalOffset(sv.HorizontalOffset - e.Delta);
            e.Handled = true;
        }
    }

    /// <summary>组件顺序条：滚轮转横向滚动（内容未超宽时无效果，不影响外层纵向滚动）。</summary>
    private void OrderScroll_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
    {
        if (sender is System.Windows.Controls.ScrollViewer sv
            && sv.ComputedHorizontalScrollBarVisibility == System.Windows.Visibility.Visible)
        {
            sv.ScrollToHorizontalOffset(sv.HorizontalOffset - e.Delta);
            e.Handled = true;
        }
    }

    private static T? FindAncestor<T>(DependencyObject? d) where T : class
    {
        while (d is not null)
        {
            if (d is T t) return t;
            d = System.Windows.Media.VisualTreeHelper.GetParent(d)
                ?? System.Windows.LogicalTreeHelper.GetParent(d);
        }
        return null;
    }
    private void AutoApply()
    {
        try
        {
            var j = JsonSerializer.Serialize(_vm.Working);
            if (j != _lastAppliedJson)
            {
                _lastAppliedJson = j;
                _vm.Save();
            }
        }
        catch { /* ignore */ }
    }

    /// <summary>应用液态玻璃：亚克力模糊 + 圆角 + 明暗主题调色板。</summary>
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        var hwnd = new WindowInteropHelper(this).Handle;
        var dark = _service.Current.Theme switch
        {
            ThemeMode.Light => false,
            ThemeMode.Dark => true,
            _ => ThemeHelper.IsSystemDark(),
        };
        // 注意：不用 ApplyAcrylic —— 在 AllowsTransparency 窗口上会渲染出黑色大块
        WindowEffects.ApplyDarkMode(hwnd, dark);
        ApplyGlassPalette(dark);
    }

    private void ApplyGlassPalette(bool dark)
    {
        var accent = ThemeHelper.ParseColor(_service.Current.AccentColor, Color.FromRgb(0x6C, 0x5C, 0xE7));
        void Add(string key, Brush b) { b.Freeze(); Resources[key] = b; }

        if (dark)
        {
            Add("GlassBgBrush", new SolidColorBrush(Color.FromArgb(0xC8, 0x1B, 0x1B, 0x26)));
            Add("CardBgBrush", new SolidColorBrush(Color.FromArgb(0x22, 0xFF, 0xFF, 0xFF)));
            Add("GlassBorderBrush", new SolidColorBrush(Color.FromArgb(0x48, 0xFF, 0xFF, 0xFF)));
            Add("TextPrimaryBrush", new SolidColorBrush(Color.FromRgb(0xF2, 0xF2, 0xF7)));
            Add("TextSecondaryBrush", new SolidColorBrush(Color.FromArgb(0xB8, 0xE0, 0xE0, 0xEA)));
            Add("HoverBrush", new SolidColorBrush(Color.FromArgb(0x4D, 0xFF, 0xFF, 0xFF)));
            Add("ControlBgBrush", new SolidColorBrush(Color.FromArgb(0x24, 0xFF, 0xFF, 0xFF)));
            Add("ControlBorderBrush", new SolidColorBrush(Color.FromArgb(0x3C, 0xFF, 0xFF, 0xFF)));
            Add("TrackBrush", new SolidColorBrush(Color.FromArgb(0x5E, 0xFF, 0xFF, 0xFF)));
            Add("ScrollTrackBrush", new SolidColorBrush(Color.FromArgb(0x2A, 0xFF, 0xFF, 0xFF)));
            Add("ScrollThumbBrush", new SolidColorBrush(Color.FromArgb(0x9A, 0xFF, 0xFF, 0xFF)));
            Add("SidebarBrush", new SolidColorBrush(Color.FromArgb(0x1F, 0xFF, 0xFF, 0xFF)));
            Add("NavSelectedBrush", new SolidColorBrush(Color.FromArgb(0x3C, 0xFF, 0xFF, 0xFF)));
        }
        else
        {
            Add("GlassBgBrush", new SolidColorBrush(Color.FromArgb(0xE6, 0xF7, 0xF7, 0xFB)));
            Add("CardBgBrush", new SolidColorBrush(Color.FromArgb(0x8C, 0xFF, 0xFF, 0xFF)));
            Add("GlassBorderBrush", new SolidColorBrush(Color.FromArgb(0x6E, 0xFF, 0xFF, 0xFF)));
            Add("TextPrimaryBrush", new SolidColorBrush(Color.FromRgb(0x1D, 0x1D, 0x24)));
            Add("TextSecondaryBrush", new SolidColorBrush(Color.FromArgb(0xB0, 0x48, 0x48, 0x52)));
            Add("HoverBrush", new SolidColorBrush(Color.FromArgb(0x2E, 0x00, 0x00, 0x00)));
            Add("ControlBgBrush", new SolidColorBrush(Color.FromArgb(0xD9, 0xFF, 0xFF, 0xFF)));
            Add("ControlBorderBrush", new SolidColorBrush(Color.FromArgb(0x30, 0x00, 0x00, 0x00)));
            Add("TrackBrush", new SolidColorBrush(Color.FromArgb(0x50, 0x00, 0x00, 0x00)));
            Add("ScrollTrackBrush", new SolidColorBrush(Color.FromArgb(0x18, 0x00, 0x00, 0x00)));
            Add("ScrollThumbBrush", new SolidColorBrush(Color.FromArgb(0x66, 0x00, 0x00, 0x00)));
            Add("SidebarBrush", new SolidColorBrush(Color.FromArgb(0x66, 0xF2, 0xF2, 0xF7)));
            Add("NavSelectedBrush", new SolidColorBrush(Color.FromArgb(0x59, 0x1D, 0x1D, 0x24)));
        }

        Add("AccentBrush", new SolidColorBrush(accent));
        Add("AccentSoftBrush", new SolidColorBrush(Color.FromArgb(0x3D, accent.R, accent.G, accent.B)));

        // 根治文字颜色：Window.Foreground 的 DynamicResource 在属性继承链上可能不刷新，
        // 导致深色模式下继承了默认黑色前景的文字看不清。资源构建完成后直接写入确定的前景色
        // （深色主题=白 #F2F2F7，浅色主题=深 #1D1D24），所有依赖继承的文本立即生效。
        Foreground = (Brush)Resources["TextPrimaryBrush"];
    }

    private void TitleBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.ButtonState == System.Windows.Input.MouseButtonState.Pressed)
        {
            try { DragMove(); } catch { /* ignore */ }
        }
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();

    /// <summary>左侧导航对应的本地化键（与 TabItem 顺序一致，用于页标题）。</summary>
    private static readonly string[] NavKeys =
    {
        "Settings_General", "Settings_Appearance", "Settings_Components", "Settings_Media",
        "Settings_MediaInfo", "Settings_Lyrics", "Settings_Cider", "Settings_Island",
        "Settings_Productivity", "Settings_Update", "Settings_About", "Settings_Notifications", "Settings_Rules",
    };

    /// <summary>左侧导航选中 → 同步切换右侧页面。</summary>
    private void NavList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        // 防御：XAML 解析期间 MainTabs 可能尚未初始化完成
        if (MainTabs is null || MainTabs.Items.Count == 0) return;
        if (NavList.SelectedIndex >= 0 && NavList.SelectedIndex < MainTabs.Items.Count)
            MainTabs.SelectedIndex = NavList.SelectedIndex;
    }

    /// <summary>右侧页面切换 → 同步左侧导航高亮，并更新页标题。</summary>
    private void MainTabs_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        // 防御：XAML 解析期间 NavList 可能尚未初始化完成
        if (NavList is null) return;
        var idx = MainTabs.SelectedIndex;
        if (NavList.SelectedIndex != idx && idx >= 0)
            NavList.SelectedIndex = idx;
        UpdatePageTitle();
    }

    /// <summary>把页标题更新为当前分类名。</summary>
    private void UpdatePageTitle()
    {
        var idx = MainTabs.SelectedIndex;
        if (idx < 0 || idx >= NavKeys.Length) return;
        PageTitle.Text = Localization.Get(NavKeys[idx]);
        PageSubtitle.Text = string.Empty;
    }

    /// <summary>手动拖动尺寸滑杆时，若该尺寸处于"自动调整"状态则自动关闭自动调整（自动/手动二选一）。</summary>
    private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (!_sizeSlidersInitialized || sender is not Slider s || s.Tag is not string key) return;
        switch (key)
        {
            case "CompactWidth": if (_vm.Working.CompactWidthAuto) _vm.Working.CompactWidthAuto = false; break;
            case "CompactHeight": if (_vm.Working.CompactHeightAuto) _vm.Working.CompactHeightAuto = false; break;
            case "ExpandedWidth": if (_vm.Working.ExpandedWidthAuto) _vm.Working.ExpandedWidthAuto = false; break;
            case "ExpandedHeight": if (_vm.Working.MaxExpandedHeightAuto) _vm.Working.MaxExpandedHeightAuto = false; break;
        }
    }
    /// <summary>Refresh all hardcoded labels from the localization tables.</summary>
    private void ApplyLocalization()
    {
        Title = Localization.Get("Settings_Title");
        TabGeneral.Header = Localization.Get("Settings_General");
        TabAppearance.Header = Localization.Get("Settings_Appearance");
        TabMedia.Header = Localization.Get("Settings_Media");
        TabMediaInfo.Header = Localization.Get("Settings_MediaInfo");
        ChkShowMediaInfo.Content = Localization.Get("MediaInfo_Show");
        LblExpandedSections.Text = Localization.Get("MediaInfo_ExpandedSections");
        ChkExpandedArtTitle.Content = Localization.Get("MediaInfo_ArtTitle");
        ChkExpandedProgress.Content = Localization.Get("MediaInfo_Progress");
        ChkExpandedControls.Content = Localization.Get("MediaInfo_Controls");
        ChkExpandedLyrics.Content = Localization.Get("MediaInfo_Lyrics");
        TxtMediaInfoNote.Text = Localization.Get("MediaInfo_Note");
        LblCardStyle.Text = Localization.Get("MediaInfo_CardStyle");
        if (CboCardStyle is not null)
        {
            var prev = CboCardStyle.SelectedValue as string;
            InitCardStyle();
            CboCardStyle.SelectedValue = prev ?? _vm.Working.ExpandedCardStyle;
        }

        TabComponents.Header = Localization.Get("Settings_Components");
        LblCompName.Text = Localization.Get("Comp_Header_Name");
        LblCompIdle.Text = Localization.Get("Comp_Header_Idle");
        LblCompPlaying.Text = Localization.Get("Comp_Header_Playing");
        TxtCompNote.Text = Localization.Get("Comp_Note");
        LblCompOrder.Text = Localization.Get("Comp_OrderHint");
        TabLyrics.Header = Localization.Get("Settings_Lyrics");
        TabCider.Header = Localization.Get("Settings_Cider");
        TabAbout.Header = Localization.Get("Settings_About");
        TabNotify.Header = Localization.Get("Settings_Notifications");
        TabIsland.Header = Localization.Get("Settings_Island");
        TabRules.Header = Localization.Get("Settings_Rules");
        LblRulesIntro.Text = Localization.Get("Rules_Intro");
        BtnAddRule.Content = Localization.Get("Rules_Add");
        NavGeneral.Text = Localization.Get("Settings_General");
        NavAppearance.Text = Localization.Get("Settings_Appearance");
        NavComponents.Text = Localization.Get("Settings_Components");
        NavMedia.Text = Localization.Get("Settings_Media");
        NavMediaInfo.Text = Localization.Get("Settings_MediaInfo");
        NavLyrics.Text = Localization.Get("Settings_Lyrics");
        NavCider.Text = Localization.Get("Settings_Cider");
        NavIsland.Text = Localization.Get("Settings_Island");
        NavProductivity.Text = Localization.Get("Settings_Productivity");
        NavUpdate.Text = Localization.Get("Settings_Update");
        NavAbout.Text = Localization.Get("Settings_About");
        NavNotify.Text = Localization.Get("Settings_Notifications");
        NavRules.Text = Localization.Get("Settings_Rules");
        UpdatePageTitle();
        ChkIslandApi.Content = Localization.Get("Island_Enabled");
        LblIslandPort.Text = Localization.Get("Island_Port");
        LblIslandToken.Text = Localization.Get("Island_Token");
        LblIslandDuration.Text = Localization.Get("Island_DefaultDuration");
        TxtIslandNote.Text = Localization.Get("Island_Note");

        LblLanguage.Text = Localization.Get("General_Language");
        LblTheme.Text = Localization.Get("Appearance_Theme");
        LblAccent.Text = Localization.Get("Appearance_Accent");
        LblPosition.Text = Localization.Get("Appearance_Position");
        LblMonitor.Text = Localization.Get("Appearance_Monitor");
        LblMonitorIndex.Text = Localization.Get("Appearance_MonitorIndexLabel");
        LblOffsetX.Text = Localization.Get("Appearance_OffsetX");
        LblOffsetY.Text = Localization.Get("Appearance_OffsetY");
        LblOpacity.Text = Localization.Get("Appearance_Opacity");
        LblIslandSize.Text = Localization.Get("Appearance_IslandSize");
        LblCompactWidth.Text = Localization.Get("Appearance_CompactWidth");
        LblCompactHeight.Text = Localization.Get("Appearance_CompactHeight");
        LblExpandedWidth.Text = Localization.Get("Appearance_ExpandedWidth");
        LblExpandedHeight.Text = Localization.Get("Appearance_ExpandedHeight");
        ChkCompactWidthAuto.Content = Localization.Get("Appearance_Auto");
        ChkCompactHeightAuto.Content = Localization.Get("Appearance_Auto");
        ChkExpandedWidthAuto.Content = Localization.Get("Appearance_Auto");
        ChkExpandedHeightAuto.Content = Localization.Get("Appearance_Auto");
        LblWidgets.Text = Localization.Get("Appearance_Widgets");
        ChkShowWidgets.Content = Localization.Get("Appearance_ShowWidgets");
        ChkWidgetTime.Content = Localization.Get("Appearance_WidgetTime");
        ChkWidgetWeather.Content = Localization.Get("Appearance_WidgetWeather");
        LblWeatherCity.Text = Localization.Get("Appearance_WeatherCity");
        TxtWidgetNote.Text = Localization.Get("Appearance_WidgetNote");
        LblCompact.Text = Localization.Get("Appearance_Compact");
        LblLyricsFolder.Text = Localization.Get("Lyrics_Folder");
        LblCiderPort.Text = Localization.Get("Cider_Port");
        LblCiderToken.Text = Localization.Get("Cider_Token");

        ChkStartWithWindows.Content = Localization.Get("General_StartWithWindows");
        ChkStartHidden.Content = Localization.Get("General_StartHidden");
        ChkHideWhenNoMedia.Content = Localization.Get("General_HideWhenNoMedia");
        ChkShowWhenPaused.Content = Localization.Get("General_ShowWhenPaused");
        ChkAlwaysVisible.Content = Localization.Get("General_AlwaysVisible");
        LblDoubleClick.Text = Localization.Get("General_DoubleClick");
        CbiDcPlayPause.Content = Localization.Get("DoubleClick_PlayPause");
        CbiDcOpenSettings.Content = Localization.Get("DoubleClick_OpenSettings");
        CbiDcNone.Content = Localization.Get("DoubleClick_None");
        ChkReduceMotion.Content = Localization.Get("General_ReduceMotion");
        ChkLowPower.Content = Localization.Get("General_LowPower");
        ChkGlobalHotkeys.Content = Localization.Get("General_GlobalHotkeys");
        TxtHotkeysHint.Text = Localization.Get("General_HotkeysHint");
        LblHotkeyToggle.Text = Localization.Get("General_HotkeyToggle");
        LblHotkeyPlayPause.Text = Localization.Get("General_HotkeyPlayPause");
        LblHotkeyNext.Text = Localization.Get("General_HotkeyNext");
        LblHotkeyPrev.Text = Localization.Get("General_HotkeyPrev");
        LblHotkeyExpand.Text = Localization.Get("General_HotkeyExpand");
        ChkQuickLauncher.Content = Localization.Get("General_QuickLauncher");
        LblHotkeyLauncher.Text = Localization.Get("General_HotkeyLauncher");
        ChkClipboardPanel.Content = Localization.Get("General_ClipboardPanel");
        LblHotkeyClipboard.Text = Localization.Get("General_HotkeyClipboard");
        LblLowBattery.Text = Localization.Get("General_LowBattery");
        TxtLowBatteryHint.Text = Localization.Get("General_LowBatteryHint");
        LblHistory.Text = Localization.Get("Notifications_History");
        TxtHistoryEmpty.Text = Localization.Get("Notifications_HistoryEmpty");
        BtnClearHistory.Content = Localization.Get("Notifications_HistoryClear");
        BtnMarkAllRead.Content = Localization.Get("Notifications_MarkAllRead");
        ChkNotifyFold.Content = Localization.Get("Notifications_Fold");
        ChkUseSystemVolume.Content = Localization.Get("Media_UseSystemVolume");
        LblAudioOutput.Text = Localization.Get("Media_AudioOutput");
        TxtAudioOutputNote.Text = Localization.Get("Media_AudioOutputNote");
        ChkOnlineLyrics.Content = Localization.Get("Lyrics_Online");
        ChkStandaloneLyrics.Content = Localization.Get("Lyrics_StandaloneWindow");
        ChkBilingual.Content = Localization.Get("MediaInfo_Bilingual");
        ChkCiderEnabled.Content = Localization.Get("Cider_Enabled");
        ChkBluetoothNotify.Content = Localization.Get("Notifications_Bluetooth");
        ChkNotifyTakeover.Content = Localization.Get("Notifications_Takeover");
        LblNotifyTimeout.Text = Localization.Get("Notifications_Timeout");
        TxtNotifyNote.Text = Localization.Get("Notifications_Note");
        ChkCompactArt.Content = Localization.Get("Appearance_CompactArt");
        ChkCompactTitle.Content = Localization.Get("Appearance_CompactTitle");
        ChkCompactProgress.Content = Localization.Get("Appearance_CompactProgress");
        ChkSingleLine.Content = Localization.Get("Appearance_SingleLine");
        ChkMiniPlayer.Content = Localization.Get("Appearance_MiniPlayer");

        TxtLyricsNote.Text = Localization.Get("Lyrics_CopyrightNote");
        TxtCiderHint.Text = Localization.Get("Cider_HowTo");
        TxtMediaInfo.Text = Localization.Get("Media_SourcePriority");
        LblMediaApps.Text = Localization.Get("Media_Apps");
        TxtMediaNote.Text = Localization.Get("Media_Note");
        TxtAbout.Text = Localization.Get("About_Text");

        BtnExport.Content = Localization.Get("Export");
        BtnImport.Content = Localization.Get("Import");
        BtnBrowse.Content = Localization.Get("Browse");
        BtnOpenConfig.Content = Localization.Get("OpenConfigFolder");
        BtnDiagnostics.Content = Localization.Get("Diagnostics");

        TabProductivity.Header = Localization.Get("Settings_Productivity");
        TabUpdate.Header = Localization.Get("Settings_Update");
        LblKeyCaps.Text = Localization.Get("General_KeyCaps");
        LblThemePreset.Text = Localization.Get("Appearance_ThemePreset");
        LblThemeTint.Text = Localization.Get("Appearance_ThemeTint");
        TxtThemeTintNote.Text = Localization.Get("Appearance_ThemeTintNote");
        LblAnimationStyle.Text = Localization.Get("Appearance_AnimStyle");
        LblFontFamily.Text = Localization.Get("Appearance_FontFamily");
        LblFontScale.Text = Localization.Get("Appearance_FontScale");
        LblCornerRadius.Text = Localization.Get("Appearance_CornerRadius");
        ChkCoverTint.Content = Localization.Get("Appearance_CoverTint");
        ChkWave.Content = Localization.Get("Wave_Enabled");
        ChkWaveSync.Content = Localization.Get("Wave_Sync");
        ChkNetCurve.Content = Localization.Get("Appearance_NetCurve");
        LblWaveSensitivity.Text = Localization.Get("Wave_Sensitivity");
        LblWaveHeight.Text = Localization.Get("Wave_Height");
        TxtWaveNote.Text = Localization.Get("Wave_Note");
        LblDndTitle.Text = Localization.Get("Dnd_Title");
        ChkDndManual.Content = Localization.Get("Dnd_Manual");
        ChkDndSchedule.Content = Localization.Get("Dnd_Schedule");
        LblDndStart.Text = Localization.Get("Dnd_Start");
        LblDndEnd.Text = Localization.Get("Dnd_End");
        TxtDndNote.Text = Localization.Get("Dnd_Note");
        LblDndAllowlist.Text = Localization.Get("Dnd_Allowlist");
        LblMeetingTitle.Text = Localization.Get("Meeting_Title");
        ChkMeetingEnabled.Content = Localization.Get("Meeting_Enabled");
        ChkMeetingAutoDnd.Content = Localization.Get("Meeting_AutoDnd");
        LblMeetingKeywords.Text = Localization.Get("Meeting_Keywords");
        TxtMeetingNote.Text = Localization.Get("Meeting_Note");
        LblScreenCapTitle.Text = Localization.Get("ScreenCap_Title");
        ChkScreenCapEnabled.Content = Localization.Get("ScreenCap_Enabled");
        ChkScreenshotNotify.Content = Localization.Get("ScreenCap_Screenshot");
        ChkRecordingNotify.Content = Localization.Get("ScreenCap_Recording");
        TxtScreenCapNote.Text = Localization.Get("ScreenCap_Note");
        LblCalendarTitle.Text = Localization.Get("Calendar_Title");
        ChkCalendarEnabled.Content = Localization.Get("Calendar_Enabled");
        LblCalendarPath.Text = Localization.Get("Calendar_Path");
        BtnCalendarPick.Content = Localization.Get("Calendar_Browse");
        LblCalendarAdvance.Text = Localization.Get("Calendar_Advance");
        TxtCalendarNote.Text = Localization.Get("Calendar_Note");
        LblRssTitle.Text = Localization.Get("Rss_Title");
        ChkRssEnabled.Content = Localization.Get("Rss_Enabled");
        LblRssUrls.Text = Localization.Get("Rss_Urls");
        LblRssInterval.Text = Localization.Get("Rss_Interval");
        TxtRssNote.Text = Localization.Get("Rss_Note");
        LblMailTitle.Text = Localization.Get("Mail_Title");
        ChkMailEnabled.Content = Localization.Get("Mail_Enabled");
        LblMailServer.Text = Localization.Get("Mail_Server");
        ChkMailSsl.Content = Localization.Get("Mail_Ssl");
        LblMailUser.Text = Localization.Get("Mail_User");
        LblMailPass.Text = Localization.Get("Mail_Pass");
        LblMailInterval.Text = Localization.Get("Mail_Interval");
        TxtMailNote.Text = Localization.Get("Mail_Note");


        LblProdClipboard.Text = Localization.Get("Prod_Clipboard");
        ChkClipboardEnabled.Content = Localization.Get("Prod_ClipboardEnabled");
        LblClipboardMax.Text = Localization.Get("Prod_ClipboardMax");
        TxtClipboardNote.Text = Localization.Get("Prod_ClipboardNote");
        ChkCopyToast.Content = Localization.Get("Clipboard_Toast");
        ChkCodeToast.Content = Localization.Get("Clipboard_Code");
        ChkCopyProgress.Content = Localization.Get("Clipboard_Progress");
        LblCopyThreshold.Text = Localization.Get("Clipboard_Threshold");
        TxtCopyNote.Text = Localization.Get("Clipboard_ToastNote");
        LblProdPomodoro.Text = Localization.Get("Prod_Pomodoro");
        ChkPomodoroEnabled.Content = Localization.Get("Prod_PomodoroEnabled");
        LblWorkMinutes.Text = Localization.Get("Prod_WorkMinutes");
        LblBreakMinutes.Text = Localization.Get("Prod_BreakMinutes");
        BtnPomodoroStart.Content = Localization.Get("Pomodoro_StartWork");
        BtnPomodoroBreak.Content = Localization.Get("Pomodoro_StartBreak");
        BtnPomodoroStop.Content = Localization.Get("Pomodoro_Stop");
        LblProdTodo.Text = Localization.Get("Prod_Todo");
        LblProdSchedule.Text = Localization.Get("Prod_Schedule");
        TxtProdNote.Text = Localization.Get("Prod_Note");
        BtnScheduleAdd.Content = Localization.Get("Schedule_Add");
        ChkAutoUpdateCheck.Content = Localization.Get("Update_Enabled");
        BtnCheckUpdate.Content = Localization.Get("Update_CheckNow");
        TxtUpdateNote.Text = Localization.Get("Update_Note");

    }

    // ── 效率工具：番茄钟 / 待办 / 日程 ──
    private void PomodoroStart_Click(object sender, RoutedEventArgs e)
        => _pomodoro?.StartWork(Math.Max(1, _vm.Working.PomodoroWorkMinutes));

    private void PomodoroBreak_Click(object sender, RoutedEventArgs e)
        => _pomodoro?.StartBreak(Math.Max(1, _vm.Working.PomodoroBreakMinutes));

    private void PomodoroStop_Click(object sender, RoutedEventArgs e)
        => _pomodoro?.Stop();

    private void TodoAdd_Click(object sender, RoutedEventArgs e)
    {
        _todo?.Add(TodoInput.Text);
        TodoInput.Clear();
    }

    private void TodoToggle_Click(object sender, RoutedEventArgs e)
    {
        if ((sender as FrameworkElement)?.DataContext is TodoItem i) _todo?.Toggle(i.Id);
    }

    private void TodoRemove_Click(object sender, RoutedEventArgs e)
    {
        if ((sender as FrameworkElement)?.DataContext is TodoItem i) _todo?.Remove(i.Id);
    }

    private void ScheduleAdd_Click(object sender, RoutedEventArgs e)
    {
        var timeText = ScheduleTimeInput.Text.Trim();
        DateTime when;
        if (DateTime.TryParse(timeText, out var parsed)) when = parsed;
        else when = DateTime.Today.Add(TimeSpan.TryParse(timeText, out var ts) ? ts : TimeSpan.MinValue);
        _schedule?.Add(ScheduleTitleInput.Text, when); // 时间过去 / 标题为空时 Add 会忽略
        ScheduleTitleInput.Clear();
        ScheduleTimeInput.Clear();
    }

    private void ScheduleRemove_Click(object sender, RoutedEventArgs e)
    {
        if ((sender as FrameworkElement)?.DataContext is ScheduleItem i) _schedule?.Remove(i.Id);
    }

    private async void CheckUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (_updater is null) return;
        BtnCheckUpdate.IsEnabled = false;
        try
        {
            var found = await _updater.CheckAsync();
            MessageBox.Show(this, found ? Localization.Get("Update_Found") : Localization.Get("Update_None"),
                Localization.Get("Update_Title"), MessageBoxButton.OK, MessageBoxImage.Information);
        }
        finally
        {
            BtnCheckUpdate.IsEnabled = true;
        }
    }

    // ── 组件顺序：横向拖拽排序 ──
    private Point _dragStart;
    private OrderItem? _dragItem;
    private bool _dragActive;

    private void Chip_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _dragStart = e.GetPosition(null);
        _dragItem = (sender as FrameworkElement)?.DataContext as OrderItem;
        _dragActive = true;
    }

    private void Chip_PreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (!_dragActive || _dragItem is null) return;
        var pos = e.GetPosition(null);
        if (Math.Abs(pos.X - _dragStart.X) < 4 && Math.Abs(pos.Y - _dragStart.Y) < 4) return;
        _dragActive = false;
        try { DragDrop.DoDragDrop((DependencyObject)sender, _dragItem, DragDropEffects.Move); }
        catch { /* ignore */ }
    }

    private void OrderStrip_PreviewDragOver(object sender, DragEventArgs e)
    {
        e.Effects = DragDropEffects.Move;
        e.Handled = true;
    }

    private void OrderStrip_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(OrderItem)) && e.Data.GetData(typeof(OrderItem)) is OrderItem item
            && sender is System.Windows.Controls.ItemsControl ic)
        {
            var x = e.GetPosition(ic).X;
            var target = _vm.OrderItems.Count - 1;
            for (var i = 0; i < _vm.OrderItems.Count; i++)
            {
                if (ic.ItemContainerGenerator.ContainerFromIndex(i) is FrameworkElement fe)
                {
                    var mid = fe.TransformToAncestor(ic).Transform(new Point(fe.ActualWidth / 2, 0)).X;
                    if (x < mid) { target = i; break; }
                    target = i;
                }
            }
            _vm.MoveOrderItemTo(item, target);
        }
    }
    private void MediaUp_Click(object sender, RoutedEventArgs e)
    {
        if ((sender as FrameworkElement)?.DataContext is MediaAppRow row) _vm.MoveMediaApp(row, -1);
    }

    private void MediaDown_Click(object sender, RoutedEventArgs e)
    {
        if ((sender as FrameworkElement)?.DataContext is MediaAppRow row) _vm.MoveMediaApp(row, 1);
    }
    private void ColorSwatch_Click(object sender, RoutedEventArgs e)
    {
        if (sender is System.Windows.Controls.Button { Tag: string color })
        {
            _vm.Working.AccentColor = color;
            AccentBox.Text = color;
        }
    }

    private void BrowseLyrics_Click(object sender, RoutedEventArgs e)
    {
        using var dlg = new System.Windows.Forms.FolderBrowserDialog
        {
            Description = Localization.Get("Lyrics_Folder"),
            SelectedPath = _vm.Working.LyricsFolder,
            ShowNewFolderButton = true,
        };
        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            _vm.Working.LyricsFolder = dlg.SelectedPath;
    }

    /// <summary>邮箱密码由 PasswordBox 输入（PasswordBox 不支持绑定），实时写入 Working，随自动保存立即生效。</summary>
    private void MailPass_PasswordChanged(object sender, RoutedEventArgs e)
    {
        _vm.Working.MailPassword = TxtMailPass.Password;
    }

    /// <summary>填充展开卡片模板下拉框（经典/媒体大卡片）。</summary>
    private void InitCardStyle()
    {
        if (CboCardStyle is null) return;
        CboCardStyle.ItemsSource = new[]
        {
            new KeyValuePair<string, string>("Classic", Localization.Get("MediaInfo_CardClassic")),
            new KeyValuePair<string, string>("Hero", Localization.Get("MediaInfo_CardHero")),
        };
        CboCardStyle.DisplayMemberPath = "Value";
        CboCardStyle.SelectedValuePath = "Key";
        CboCardStyle.SelectedValue = _vm.Working.ExpandedCardStyle;
    }

    /// <summary>模板切换即时生效（应用层 Settings.Changed 会刷新展开区块可见性）。</summary>
    private void CardStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CboCardStyle.SelectedValue is string style && style != _vm.Working.ExpandedCardStyle)
            _vm.Working.ExpandedCardStyle = style;
    }

    private void CalendarPick_Click(object sender, RoutedEventArgs e)
    {
        var dlg = new OpenFileDialog { Filter = "iCalendar 文件 (*.ics)|*.ics|所有文件 (*.*)|*.*", DefaultExt = ".ics" };
        if (dlg.ShowDialog() == true)
            _vm.Working.CalendarIcsPath = dlg.FileName;
    }

    private void OpenConfigFolder_Click(object sender, RoutedEventArgs e)
    {
        AppPaths.EnsureDirectories();
        _ = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = "explorer.exe",
            Arguments = $"\"{AppPaths.AppDataDir}\"",
            UseShellExecute = true,
        });
    }

    private async void Diagnostics_Click(object sender, RoutedEventArgs e)
    {
        var text = await DiagnosticsCommand.RunAsync(_service, _cider);
        var win = new Window
        {
            Title = Localization.Get("Diagnostics"),
            Width = 640,
            Height = 480,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Owner = this,
            Content = new System.Windows.Controls.TextBox
            {
                Text = text,
                IsReadOnly = true,
                FontFamily = new System.Windows.Media.FontFamily("Consolas"),
                FontSize = 12,
                TextWrapping = System.Windows.TextWrapping.Wrap,
                VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto,
                Margin = new Thickness(8),
            },
        };
        win.ShowDialog();
    }

    private void Export_Click(object sender, RoutedEventArgs e)
    {
        var dlg = new SaveFileDialog
        {
            FileName = "winisland-settings.json",
            Filter = "JSON|*.json",
            DefaultExt = ".json",
        };
        if (dlg.ShowDialog(this) == true)
        {
            try
            {
                File.WriteAllText(dlg.FileName, _service.Export());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Localization.Get("Settings"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void Import_Click(object sender, RoutedEventArgs e)
    {
        var dlg = new OpenFileDialog { Filter = "JSON|*.json", DefaultExt = ".json" };
        if (dlg.ShowDialog(this) == true)
        {
            try
            {
                var json = File.ReadAllText(dlg.FileName);
                if (_service.TryImport(json))
                {
                    MessageBox.Show(this, Localization.Get("SettingsSaved"), Localization.Get("Settings"),
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show(this, Localization.Get("ImportFailed"), Localization.Get("Settings"),
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Localization.Get("Settings"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    /// <summary>刷新通知历史列表（设置打开 / 历史变化时调用）。</summary>
    private void RefreshHistory()
    {
        if (HistoryList is null || _history is null) return;
        HistoryList.ItemsSource = _history.Entries;
        TxtHistoryEmpty.Visibility = _history.Entries.Count == 0
            ? System.Windows.Visibility.Visible
            : System.Windows.Visibility.Collapsed;
    }

    private void ClearHistory_Click(object sender, RoutedEventArgs e)
    {
        _history?.Clear();
        RefreshHistory();
    }

    /// <summary>通知中心"全部已读"。</summary>
    private void MarkAllRead_Click(object sender, RoutedEventArgs e)
    {
        _history?.MarkAllRead();
        RefreshHistory();
    }

    /// <summary>通知历史单条删除。</summary>
    private void DeleteHistoryItem_Click(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement { DataContext: NotificationHistoryEntry entry })
        {
            _history?.Remove(entry);
        }
    }

    /// <summary>点击历史条目：打开来源应用（12 通知一键处理）。</summary>
    private void HistoryItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (sender is FrameworkElement { DataContext: NotificationHistoryEntry entry })
        {
            OpenSourceApp(entry.Source);
        }
    }

    /// <summary>打开通知来源应用：优先唤起已运行的进程窗口，否则按名称/URL 启动。</summary>
    private static void OpenSourceApp(string? source)
    {
        if (string.IsNullOrWhiteSpace(source)) return;
        var name = source.Trim();
        try
        {
            if (name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            {
                var baseName = Path.GetFileNameWithoutExtension(name);
                foreach (var p in Process.GetProcessesByName(baseName))
                {
                    if (p.MainWindowHandle != IntPtr.Zero)
                    {
                        NativeUser32.SetForegroundWindow(p.MainWindowHandle);
                        return;
                    }
                }
            }
            Process.Start(new ProcessStartInfo(name) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Open notification source failed: {ex.Message}");
        }
    }

    /// <summary>新增一条规则（规则引擎设置页）。</summary>
    private void AddRule_Click(object sender, RoutedEventArgs e) => _vm.AddRule();

    /// <summary>删除指定规则行。</summary>
    private void RuleRemove_Click(object sender, RoutedEventArgs e)
    {
        if ((sender as FrameworkElement)?.DataContext is RuleRow row) _vm.RemoveRule(row);
    }

}

    /// <summary>唤起来源应用窗口所需的少量 Win32 互操作。</summary>
internal static class NativeUser32
{
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    internal static extern bool SetForegroundWindow(IntPtr hWnd);
}

