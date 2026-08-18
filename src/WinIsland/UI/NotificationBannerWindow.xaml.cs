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
            Left = area.Right - ActualWidth - 18; // 整体左移，不贴屏幕右边缘
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

    /// <summary>弹出动画：从右侧滑入 + 轻微放大 + 淡入（iOS 风格，快速弹出）。</summary>
    private void AnimateIn()
    {
        var tg = new TransformGroup();
        var tx = new TranslateTransform(90, 0);
        var sc = new ScaleTransform(0.9, 0.9);
        tg.Children.Add(sc);
        tg.Children.Add(tx);
        Card.RenderTransform = tg;
        Card.RenderTransformOrigin = new Point(1.0, 0.5);
        Card.Opacity = 0;

        var spring = new SpringEase { Damping = 14, Stiffness = 300, Mass = 1 };
        var smooth = new CubicEase { EasingMode = EasingMode.EaseOut };

        var sb = new Storyboard();

        var animX = new DoubleAnimation(0, TimeSpan.FromMilliseconds(260)) { EasingFunction = smooth };
        Storyboard.SetTarget(animX, tx);
        Storyboard.SetTargetProperty(animX, new PropertyPath(TranslateTransform.XProperty));

        var animSx = new DoubleAnimation(1, TimeSpan.FromMilliseconds(300)) { EasingFunction = spring };
        Storyboard.SetTarget(animSx, sc);
        Storyboard.SetTargetProperty(animSx, new PropertyPath(ScaleTransform.ScaleXProperty));

        var animSy = new DoubleAnimation(1, TimeSpan.FromMilliseconds(300)) { EasingFunction = spring };
        Storyboard.SetTarget(animSy, sc);
        Storyboard.SetTargetProperty(animSy, new PropertyPath(ScaleTransform.ScaleYProperty));

        var animO = new DoubleAnimation(1, TimeSpan.FromMilliseconds(180)) { EasingFunction = smooth };
        Storyboard.SetTarget(animO, Card);
        Storyboard.SetTargetProperty(animO, new PropertyPath(UIElement.OpacityProperty));

        sb.Children.Add(animX);
        sb.Children.Add(animSx);
        sb.Children.Add(animSy);
        sb.Children.Add(animO);
        sb.Begin();
    }

    private void Card_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => Close();

    protected override void OnClosed(EventArgs e)
    {
        _closeTimer.Stop();
        base.OnClosed(e);
    }
}
