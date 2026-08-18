using System.IO;
using System.Windows;
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

    public SettingsWindow(SettingsViewModel vm, SettingsService service, CiderMediaProvider? cider)
    {
        _vm = vm;
        _service = service;
        _cider = cider;
        DataContext = vm;
        InitializeComponent();
        ApplyLocalization();
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
        WindowEffects.ApplyDarkMode(hwnd, dark);
        WindowEffects.ApplyAcrylic(hwnd, dark ? Color.FromRgb(0x1B, 0x1B, 0x26) : Color.FromRgb(0xF2, 0xF2, 0xF7), dark ? 0.6 : 0.5);
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
        TabLyrics.Header = Localization.Get("Settings_Lyrics");
        TabCider.Header = Localization.Get("Settings_Cider");
        TabAbout.Header = Localization.Get("Settings_About");

        LblLanguage.Text = Localization.Get("General_Language");
        LblTheme.Text = Localization.Get("Appearance_Theme");
        LblAccent.Text = Localization.Get("Appearance_Accent");
        LblPosition.Text = Localization.Get("Appearance_Position");
        LblMonitor.Text = Localization.Get("Appearance_Monitor");
        LblMonitorIndex.Text = Localization.Get("Appearance_MonitorIndexLabel");
        LblOffsetX.Text = Localization.Get("Appearance_OffsetX");
        LblOffsetY.Text = Localization.Get("Appearance_OffsetY");
        LblOpacity.Text = Localization.Get("Appearance_Opacity");
        LblCompact.Text = Localization.Get("Appearance_Compact");
        LblLyricsFolder.Text = Localization.Get("Lyrics_Folder");
        LblCiderPort.Text = Localization.Get("Cider_Port");
        LblCiderToken.Text = Localization.Get("Cider_Token");

        ChkStartWithWindows.Content = Localization.Get("General_StartWithWindows");
        ChkStartHidden.Content = Localization.Get("General_StartHidden");
        ChkHideWhenNoMedia.Content = Localization.Get("General_HideWhenNoMedia");
        ChkShowWhenPaused.Content = Localization.Get("General_ShowWhenPaused");
        ChkUseSystemVolume.Content = Localization.Get("Media_UseSystemVolume");
        ChkOnlineLyrics.Content = Localization.Get("Lyrics_Online");
        ChkStandaloneLyrics.Content = Localization.Get("Lyrics_StandaloneWindow");
        ChkCiderEnabled.Content = Localization.Get("Cider_Enabled");
        ChkCompactArt.Content = Localization.Get("Appearance_CompactArt");
        ChkCompactTitle.Content = Localization.Get("Appearance_CompactTitle");
        ChkCompactProgress.Content = Localization.Get("Appearance_CompactProgress");

        TxtLyricsNote.Text = Localization.Get("Lyrics_CopyrightNote");
        TxtCiderHint.Text = Localization.Get("Cider_HowTo");
        TxtMediaInfo.Text = Localization.Get("Media_SourcePriority");
        TxtAbout.Text = Localization.Get("About_Text");

        BtnExport.Content = Localization.Get("Export");
        BtnImport.Content = Localization.Get("Import");
        BtnBrowse.Content = Localization.Get("Browse");
        BtnOpenConfig.Content = Localization.Get("OpenConfigFolder");
        BtnDiagnostics.Content = Localization.Get("Diagnostics");
        BtnCancel.Content = Localization.Get("Cancel");
        BtnSave.Content = Localization.Get("Save");
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

    private void Cancel_Click(object sender, RoutedEventArgs e) => Close();

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        _vm.Save();
        DialogResult = true;
        Close();
    }
}
