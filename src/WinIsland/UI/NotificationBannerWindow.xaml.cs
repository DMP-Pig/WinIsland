using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using WinIsland.Services;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;

namespace WinIsland.UI;

/// <summary>右上角弹出的玻璃通知横幅，自动消失，点击关闭。</summary>
public partial class NotificationBannerWindow : Window
{
    private readonly DispatcherTimer _closeTimer;

    public NotificationBannerWindow(string title, string body, string glyph, int timeoutSeconds,
        System.Windows.Forms.Screen screen, int stackIndex)
    {
        InitializeComponent();
        TitleText.Text = title;
        BodyText.Text = body;
        IconText.Text = glyph;

        // 定位到屏幕右上角（带堆叠偏移）
        Loaded += (_, _) =>
        {
            var area = screen.WorkingArea;
            Left = area.Right - ActualWidth - 8;
            Top = area.Top + 8 + stackIndex * (ActualHeight + 10);
            AnimateIn();
        };

        _closeTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(Math.Max(1, timeoutSeconds)) };
        _closeTimer.Tick += (_, _) => { _closeTimer.Stop(); Close(); };
        _closeTimer.Start();

        // 液态玻璃模糊
        SourceInitialized += (_, _) =>
        {
            try
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                WindowEffects.ApplyDarkMode(hwnd, true);
                WindowEffects.ApplyAcrylic(hwnd, Color.FromRgb(0x1B, 0x1B, 0x26), 0.7);
            }
            catch { /* 尽力而为 */ }
        };
    }

    private void AnimateIn()
    {
        var tx = new TranslateTransform(40, 0);
        Card.RenderTransform = tx;
        Card.RenderTransformOrigin = new Point(0.5, 0.5);
        var sb = new Storyboard();
        var anim = new DoubleAnimation(0, TimeSpan.FromMilliseconds(320))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
        };
        Storyboard.SetTarget(anim, tx);
        Storyboard.SetTargetProperty(anim, new PropertyPath(TranslateTransform.XProperty));
        sb.Children.Add(anim);
        sb.Begin();
    }

    private void Card_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => Close();

    protected override void OnClosed(EventArgs e)
    {
        _closeTimer.Stop();
        base.OnClosed(e);
    }
}
