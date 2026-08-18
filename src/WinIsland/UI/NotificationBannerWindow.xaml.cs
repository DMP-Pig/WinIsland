using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using WinIsland.Services;
using Point = System.Windows.Point;

namespace WinIsland.UI;

/// <summary>
/// 右上角弹出的玻璃通知横幅（macOS 风格：原位淡入 + 轻微放大回弹）。
/// 不加亚克力 DWM（避免透明窗口黑块），卡片完整显示、不贴边。
/// </summary>
public partial class NotificationBannerWindow : Window
{
    private readonly DispatcherTimer _closeTimer;
    private readonly System.Windows.Forms.Screen _screen;
    private readonly int _stackIndex;

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
        // 窗口直接停在目标位置（距右边缘 24px，不贴边）
        Left = area.Right - Width - 24;
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

    /// <summary>macOS 风格：原位淡入 + 从右上角轻微放大回弹 + 轻微下落。</summary>
    private void AnimateIn()
    {
        var tg = new TransformGroup();
        var sc = new ScaleTransform(0.92, 0.92);
        var ty = new TranslateTransform(0, -8);
        tg.Children.Add(sc);
        tg.Children.Add(ty);
        Card.RenderTransform = tg;
        Card.RenderTransformOrigin = new Point(1, 0); // 右上角为缩放原点

        var spring = new SpringEase { Damping = 15, Stiffness = 320, Mass = 1 };
        var smooth = new CubicEase { EasingMode = EasingMode.EaseOut };

        var sb = new Storyboard();

        var animSx = new DoubleAnimation(1, TimeSpan.FromMilliseconds(320)) { EasingFunction = spring };
        Storyboard.SetTarget(animSx, sc);
        Storyboard.SetTargetProperty(animSx, new PropertyPath(ScaleTransform.ScaleXProperty));

        var animSy = new DoubleAnimation(1, TimeSpan.FromMilliseconds(320)) { EasingFunction = spring };
        Storyboard.SetTarget(animSy, sc);
        Storyboard.SetTargetProperty(animSy, new PropertyPath(ScaleTransform.ScaleYProperty));

        var animTy = new DoubleAnimation(0, TimeSpan.FromMilliseconds(260)) { EasingFunction = smooth };
        Storyboard.SetTarget(animTy, ty);
        Storyboard.SetTargetProperty(animTy, new PropertyPath(TranslateTransform.YProperty));

        var animO = new DoubleAnimation(1, TimeSpan.FromMilliseconds(200)) { EasingFunction = smooth };
        Storyboard.SetTarget(animO, Card);
        Storyboard.SetTargetProperty(animO, new PropertyPath(UIElement.OpacityProperty));

        sb.Children.Add(animSx);
        sb.Children.Add(animSy);
        sb.Children.Add(animTy);
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
