using System;
using System.Collections.Generic;
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
    private DispatcherTimer _closeTimer;
    private readonly System.Windows.Forms.Screen _screen;
    private readonly int _stackIndex;
    private double _finalLeft;
    private bool _closing;
    private int _foldCount = 1;
    private readonly bool _progressMode;

    /// <summary>折叠键：来源 + 标题，相同的活动横幅复用更新（11 通知折叠）。</summary>
    public string FoldKey { get; }

    public NotificationBannerWindow(string title, string body, string glyph, int timeoutSeconds,
        System.Windows.Forms.Screen screen, int stackIndex, string foldKey = "",
        bool progressMode = false, IReadOnlyList<(string Label, Action Callback)>? actions = null)
    {
        _screen = screen;
        _stackIndex = stackIndex;
        FoldKey = foldKey;
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

        _progressMode = progressMode;
        if (progressMode)
            ProgressHost.Visibility = Visibility.Visible; // 进度模式：先显示进度，完成后由 Complete() 接管

        // #9 通知操作按钮：动态添加横向按钮，点击后先执行回调再收起横幅
        if (actions is not null)
        {
            foreach (var (label, callback) in actions)
            {
                if (string.IsNullOrWhiteSpace(label)) continue;
                var btn = new System.Windows.Controls.Button
                {
                    Content = label,
                    Style = (System.Windows.Style)FindResource("BannerActionButton"),
                };
                btn.Click += (_, _) =>
                {
                    try { callback?.Invoke(); }
                    catch (Exception ex) { AppLogger.Warn($"Banner action failed: {ex.Message}"); }
                    CloseWithAnimation();
                };
                ActionsHost.Children.Add(btn);
            }
            if (ActionsHost.Children.Count > 0) ActionsHost.Visibility = Visibility.Visible;
        }

        _closeTimer = new DispatcherTimer();
        _closeTimer.Tick += (_, _) => CloseWithAnimation();
        if (!progressMode)
        {
            _closeTimer.Interval = TimeSpan.FromSeconds(Math.Max(1, timeoutSeconds));
            _closeTimer.Start();
        }
    }

    /// <summary>进度模式：更新进度条（0~1）。</summary>
    public void SetProgress(double value01)
    {
        try
        {
            if (!_progressMode) return;
            if (ProgressHost.ActualWidth <= 0) ProgressHost.UpdateLayout();
            ProgressFill.Width = Math.Max(0, ProgressHost.ActualWidth) * Math.Clamp(value01, 0, 1);
        }
        catch { /* 布局未就绪时忽略 */ }
    }

    /// <summary>进度完成：切换为结果文案并开始自动关闭计时。</summary>
    public void Complete(string title, string body, string glyph, int timeoutSeconds)
    {
        if (_progressMode)
        {
            TitleText.Text = title;
            BodyText.Text = body;
            IconText.Text = glyph;
            ProgressHost.Visibility = Visibility.Collapsed;
        }
        _closeTimer.Stop();
        _closeTimer.Interval = TimeSpan.FromSeconds(Math.Max(1, timeoutSeconds));
        _closeTimer.Start();
    }

    /// <summary>折叠更新：同来源同标题的新通知复用本横幅，刷新文本并重置计时。</summary>
    public void Refresh(string title, string body, string glyph, int timeoutSeconds)
    {
        if (_progressMode)
        {
            // 进度横幅命中折叠：直接重置为新进度（不累加数量）
            TitleText.Text = title;
            IconText.Text = glyph;
            BodyText.Text = body;
            SetProgress(0);
            return;
        }
        _foldCount++;
        TitleText.Text = title;
        IconText.Text = glyph;
        BodyText.Text = _foldCount > 1 ? body + $"  (+{_foldCount})" : body;
        _closeTimer.Stop();
        _closeTimer.Interval = TimeSpan.FromSeconds(Math.Max(1, timeoutSeconds));
        _closeTimer.Start();
        Topmost = true;
    }

    /// <summary>从右侧滑入 + 淡入（放慢，约 0.5s）。</summary>
    private void AnimateIn()
    {
        var sb = new Storyboard();
        var smooth = new CubicEase { EasingMode = EasingMode.EaseOut };

        var animLeft = new DoubleAnimation(_finalLeft, TimeSpan.FromMilliseconds(560)) { EasingFunction = smooth };
        Storyboard.SetTarget(animLeft, this);
        Storyboard.SetTargetProperty(animLeft, new PropertyPath(Window.LeftProperty));

        var animO = new DoubleAnimation(1, TimeSpan.FromMilliseconds(420)) { EasingFunction = smooth };
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

        var animLeft = new DoubleAnimation(area.Right + 8, TimeSpan.FromMilliseconds(500)) { EasingFunction = easeIn };
        Storyboard.SetTarget(animLeft, this);
        Storyboard.SetTargetProperty(animLeft, new PropertyPath(Window.LeftProperty));

        var animO = new DoubleAnimation(0, TimeSpan.FromMilliseconds(380)) { EasingFunction = easeIn };
        Storyboard.SetTarget(animO, Card);
        Storyboard.SetTargetProperty(animO, new PropertyPath(UIElement.OpacityProperty));

        sb.Children.Add(animLeft);
        sb.Children.Add(animO);
        sb.Completed += (_, _) => Close();
        sb.Begin();
    }

    private void Card_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        // #9 点击按钮不收起（按钮自己在回调后收起）
        if (IsActionButton(e.OriginalSource)) return;
        CloseWithAnimation();
    }

    /// <summary>判断点击源是否位于操作按钮上。</summary>
    private static bool IsActionButton(object source)
    {
        var d = source as DependencyObject;
        while (d is not null)
        {
            if (d is System.Windows.Controls.Button) return true;
            d = d is System.Windows.Media.Visual or System.Windows.Media.Media3D.Visual3D
                ? System.Windows.Media.VisualTreeHelper.GetParent(d)
                : System.Windows.LogicalTreeHelper.GetParent(d);
        }
        return false;
    }

    protected override void OnClosed(EventArgs e)
    {
        _closeTimer.Stop();
        base.OnClosed(e);
    }
}
