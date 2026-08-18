using System.IO;
using System.Text.Json;
using System.Windows;
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
    private string _lastAppliedJson;

    public SettingsWindow(SettingsViewModel vm, SettingsService service, CiderMediaProvider? cider,
        NotificationHistoryService? history = null)
    {
        _vm = vm;
        _service = service;
        _cider = cider;
        _history = history;
        DataContext = vm;
        InitializeComponent();
        ApplyLocalization();
        RefreshHistory();
        if (_history is not null) _history.Changed += (_, _) => RefreshHistory();

        // 即时生效：轮询检测 Working 变化并立即应用（无保存按钮）
        _lastAppliedJson = JsonSerializer.Serialize(_vm.Working);
        _autoApply = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(300) };
        _autoApply.Tick += (_, _) => AutoApply();
        _autoApply.Start();

        // 语言切换后立即刷新界面文案
        Localization.LanguageChanged += (_, _) => ApplyLocalization();

        // 关闭时兜底保存最后一次改动
        Closed += (_, _) => { try { _vm.Save(); } catch { } };
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
            Add("HoverBrush", new SolidColorBrush(Color.FromArgb(0x30, 0xFF, 0xFF, 0xFF)));
            Add("ControlBgBrush", new SolidColorBrush(Color.FromArgb(0x24, 0xFF, 0xFF, 0xFF)));
            Add("ControlBorderBrush", new SolidColorBrush(Color.FromArgb(0x3C, 0xFF, 0xFF, 0xFF)));
            Add("TrackBrush", new SolidColorBrush(Color.FromArgb(0x5E, 0xFF, 0xFF, 0xFF)));
            Add("ScrollTrackBrush", new SolidColorBrush(Color.FromArgb(0x2A, 0xFF, 0xFF, 0xFF)));
            Add("ScrollThumbBrush", new SolidColorBrush(Color.FromArgb(0x9A, 0xFF, 0xFF, 0xFF)));
        }
        else
        {
            Add("GlassBgBrush", new SolidColorBrush(Color.FromArgb(0xE6, 0xF7, 0xF7, 0xFB)));
            Add("CardBgBrush", new SolidColorBrush(Color.FromArgb(0x8C, 0xFF, 0xFF, 0xFF)));
            Add("GlassBorderBrush", new SolidColorBrush(Color.FromArgb(0x6E, 0xFF, 0xFF, 0xFF)));
            Add("TextPrimaryBrush", new SolidColorBrush(Color.FromRgb(0x1D, 0x1D, 0x24)));
            Add("TextSecondaryBrush", new SolidColorBrush(Color.FromArgb(0xB0, 0x48, 0x48, 0x52)));
            Add("HoverBrush", new SolidColorBrush(Color.FromArgb(0x18, 0x00, 0x00, 0x00)));
            Add("ControlBgBrush", new SolidColorBrush(Color.FromArgb(0xD9, 0xFF, 0xFF, 0xFF)));
            Add("ControlBorderBrush", new SolidColorBrush(Color.FromArgb(0x30, 0x00, 0x00, 0x00)));
            Add("TrackBrush", new SolidColorBrush(Color.FromArgb(0x50, 0x00, 0x00, 0x00)));
            Add("ScrollTrackBrush", new SolidColorBrush(Color.FromArgb(0x18, 0x00, 0x00, 0x00)));
            Add("ScrollThumbBrush", new SolidColorBrush(Color.FromArgb(0x66, 0x00, 0x00, 0x00)));
        }

        Add("AccentBrush", new SolidColorBrush(accent));
        Add("AccentSoftBrush", new SolidColorBrush(Color.FromArgb(0x3D, accent.R, accent.G, accent.B)));
    }

    private void TitleBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.ButtonState == System.Windows.Input.MouseButtonState.Pressed)
        {
            try { DragMove(); } catch { /* ignore */ }
        }
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();
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
        ChkReduceMotion.Content = Localization.Get("General_ReduceMotion");
        ChkGlobalHotkeys.Content = Localization.Get("General_GlobalHotkeys");
        TxtHotkeysHint.Text = Localization.Get("General_HotkeysHint");
        LblLowBattery.Text = Localization.Get("General_LowBattery");
        TxtLowBatteryHint.Text = Localization.Get("General_LowBatteryHint");
        LblHistory.Text = Localization.Get("Notifications_History");
        TxtHistoryEmpty.Text = Localization.Get("Notifications_HistoryEmpty");
        BtnClearHistory.Content = Localization.Get("Notifications_HistoryClear");
        ChkUseSystemVolume.Content = Localization.Get("Media_UseSystemVolume");
        ChkOnlineLyrics.Content = Localization.Get("Lyrics_Online");
        ChkStandaloneLyrics.Content = Localization.Get("Lyrics_StandaloneWindow");
        ChkCiderEnabled.Content = Localization.Get("Cider_Enabled");
        ChkBluetoothNotify.Content = Localization.Get("Notifications_Bluetooth");
        ChkNotifyTakeover.Content = Localization.Get("Notifications_Takeover");
        LblNotifyTimeout.Text = Localization.Get("Notifications_Timeout");
        TxtNotifyNote.Text = Localization.Get("Notifications_Note");
        ChkCompactArt.Content = Localization.Get("Appearance_CompactArt");
        ChkCompactTitle.Content = Localization.Get("Appearance_CompactTitle");
        ChkCompactProgress.Content = Localization.Get("Appearance_CompactProgress");

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

}
