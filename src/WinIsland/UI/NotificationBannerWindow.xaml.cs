using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using WinIsland.Services;

namespace WinIsland.UI;

/// <summary>
/// 右上角弹出的玻璃通知横幅。
/// 弹出：整个窗口从屏幕右侧滑入 + 淡入；消失：反向滑出到右侧 + 淡出。
/// </summary>
public partial class NotificationBannerWindow : Window
{
    private readonly DispatcherTimer _closeTimer;
    private readonly System.Windows.Forms.Screen _screen;
    private readonly int _stackIndex;
    private double _finalLeft;
    private bool _closing;

    public NotificationBannerWindow(string title, string body, string glyph, int timeoutSeconds,
        System.Windows.Forms.Screen screen, int stackIndex)
    {
        _screen = screen;
        _stackIndex = stackIndex;
        InitializeComponent();
        TitleText.Text = title;
        BodyText.Text = body;
        IconText.Text = glyph;

        var area = _screen.WorkingArea;
        _finalLeft = area.Right - Width - 24;
        Left = area.Right + 8;      // 起始：完全在屏幕右侧外
        Card.Opacity = 0;

        ContentRendered += (_, _) =>
        {
            Top = area.Top + 16 + _stackIndex * (ActualHeight + 12);
            AnimateIn();
        };

        _closeTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(Math.Max(1, timeoutSeconds)) };
        _closeTimer.Tick += (_, _) => CloseWithAnimation();
        _closeTimer.Start();
    }

    /// <summary>从右侧滑入 + 淡入（放慢，约 0.5s）。</summary>
    private void AnimateIn()
    {
        var sb = new Storyboard();
        var smooth = new CubicEase { EasingMode = EasingMode.EaseOut };

        var animLeft = new DoubleAnimation(_finalLeft, TimeSpan.FromMilliseconds(480)) { EasingFunction = smooth };
        Storyboard.SetTarget(animLeft, this);
        Storyboard.SetTargetProperty(animLeft, new PropertyPath(Window.LeftProperty));

        var animO = new DoubleAnimation(1, TimeSpan.FromMilliseconds(320)) { EasingFunction = smooth };
        Storyboard.SetTarget(animO, Card);
        Storyboard.SetTargetProperty(animO, new PropertyPath(UIElement.OpacityProperty));

        sb.Children.Add(animLeft);
        sb.Children.Add(animO);
        sb.Begin();
    }

    /// <summary>反向动画滑出到右侧 + 淡出，然后关闭。</summary>
    private void CloseWithAnimation()
    {
        if (_closing) return;
        _closing = true;
        _closeTimer.Stop();

        var area = _screen.WorkingArea;
        var sb = new Storyboard();
        var easeIn = new CubicEase { EasingMode = EasingMode.EaseIn };

        var animLeft = new DoubleAnimation(area.Right + 8, TimeSpan.FromMilliseconds(420)) { EasingFunction = easeIn };
        Storyboard.SetTarget(animLeft, this);
        Storyboard.SetTargetProperty(animLeft, new PropertyPath(Window.LeftProperty));

        var animO = new DoubleAnimation(0, TimeSpan.FromMilliseconds(280)) { EasingFunction = easeIn };
        Storyboard.SetTarget(animO, Card);
        Storyboard.SetTargetProperty(animO, new PropertyPath(UIElement.OpacityProperty));

        sb.Children.Add(animLeft);
        sb.Children.Add(animO);
        sb.Completed += (_, _) => Close();
        sb.Begin();
    }

    private void Card_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => CloseWithAnimation();

    protected override void OnClosed(EventArgs e)
    {
        _closeTimer.Stop();
        base.OnClosed(e);
    }
}
