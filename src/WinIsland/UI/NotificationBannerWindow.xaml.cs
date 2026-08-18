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
/// 不加亚克力 DWM 效果（避免透明窗口出现黑色大块），仅半透明卡片；
/// 整个窗口从右侧滑入，避免卡片被窗口边界裁切。
/// </summary>
public partial class NotificationBannerWindow : Window
{
    private readonly DispatcherTimer _closeTimer;
    private readonly System.Windows.Forms.Screen _screen;
    private readonly int _stackIndex;
    private double _finalLeft;

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
        // 初始在屏幕右侧外（不可见），ContentRendered 后滑入
        _finalLeft = area.Right - Width - 24; // 距右边缘 24px（不贴边）
        Left = area.Right + 8 - Width;        // 起始位置：完全在屏幕右侧外
        Card.Opacity = 0;

        ContentRendered += (_, _) =>
        {
            Top = area.Top + 8 + _stackIndex * (ActualHeight + 10);
            AnimateIn();
        };

        _closeTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(Math.Max(1, timeoutSeconds)) };
        _closeTimer.Tick += (_, _) => { _closeTimer.Stop(); Close(); };
        _closeTimer.Start();
    }

    /// <summary>整个窗口从右侧滑入 + 卡片淡入。</summary>
    private void AnimateIn()
    {
        var sb = new Storyboard();
        var smooth = new CubicEase { EasingMode = EasingMode.EaseOut };

        var animLeft = new DoubleAnimation(_finalLeft, TimeSpan.FromMilliseconds(300)) { EasingFunction = smooth };
        Storyboard.SetTarget(animLeft, this);
        Storyboard.SetTargetProperty(animLeft, new PropertyPath(Window.LeftProperty));

        var animO = new DoubleAnimation(1, TimeSpan.FromMilliseconds(220)) { EasingFunction = smooth };
        Storyboard.SetTarget(animO, Card);
        Storyboard.SetTargetProperty(animO, new PropertyPath(UIElement.OpacityProperty));

        sb.Children.Add(animLeft);
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
