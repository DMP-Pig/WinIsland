using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace WinIsland.Services;

/// <summary>
/// 全局快捷键（Win32 RegisterHotKey）。
/// 通过一个不可见的 0x0 窗口接收 WM_HOTKEY，不抢占焦点；
/// 受设置 GlobalHotkeysEnabled 控制（默认开启）。
/// 快捷键：Ctrl+Alt+P 播放/暂停 · Ctrl+Alt+← 上一首 · Ctrl+Alt+→ 下一首 · Ctrl+Alt+I 显示/隐藏
/// </summary>
public sealed class GlobalHotkeyService : IDisposable
{
    public const int WM_HOTKEY = 0x0312;
    private const int MOD_CONTROL = 0x0002;
    private const int MOD_ALT = 0x0001;
    private const int MOD_NOREPEAT = 0x4000; // 按住不重复触发
    private const int VK_P = 0x50;
    private const int VK_LEFT = 0x25;
    private const int VK_RIGHT = 0x27;
    private const int VK_I = 0x49;

    private readonly Window _host;
    private HwndSource? _source;
    private IntPtr _hwnd;
    private readonly int[] _ids = { 0xC011, 0xC012, 0xC013, 0xC014 };
    private bool _registered;
    private bool _disposed;

    public GlobalHotkeyService()
    {
        _host = new Window
        {
            WindowStyle = WindowStyle.None,
            ResizeMode = ResizeMode.NoResize,
            ShowInTaskbar = false,
            ShowActivated = false,
            Width = 0,
            Height = 0,
            Left = -10000,
            Top = -10000,
            Opacity = 0,
            AllowsTransparency = false,
        };
        _host.SourceInitialized += (_, _) =>
        {
            _hwnd = new WindowInteropHelper(_host).Handle;
            _source = HwndSource.FromHwnd(_hwnd);
            _source?.AddHook(WndProc);
            if (_registered) RegisterAll();
        };
        _host.Show(); // 不可见窗口：必须 Show 一次才有 HWND，但永远不可见、不抢焦点
    }

    /// <summary>播放/暂停（Ctrl+Alt+P）。</summary>
    public event Action? PlayPausePressed;
    /// <summary>下一首（Ctrl+Alt+→）。</summary>
    public event Action? NextPressed;
    /// <summary>上一首（Ctrl+Alt+←）。</summary>
    public event Action? PreviousPressed;
    /// <summary>显示/隐藏灵动岛（Ctrl+Alt+I）。</summary>
    public event Action? ToggleVisibilityPressed;

    /// <summary>启用或禁用全部快捷键（设置变更时调用）。</summary>
    public void SetEnabled(bool enabled)
    {
        if (enabled) RegisterAll();
        else UnregisterAll();
    }

    private void RegisterAll()
    {
        if (_disposed || _hwnd == IntPtr.Zero) return;
        Register(0, VK_P, PlayPausePressed);
        Register(1, VK_RIGHT, NextPressed);
        Register(2, VK_LEFT, PreviousPressed);
        Register(3, VK_I, ToggleVisibilityPressed);
        _registered = true;
    }

    private void Register(int idx, int vk, Action? handler)
    {
        if (handler is null || _ids[idx] == 0) return;
        if (!RegisterHotKey(_hwnd, _ids[idx], (uint)(MOD_CONTROL | MOD_ALT | MOD_NOREPEAT), (uint)vk))
        {
            AppLogger.Warn($"RegisterHotKey failed (vk=0x{vk:X2}, id=0x{_ids[idx]:X4}): {Marshal.GetLastWin32Error()}");
            _ids[idx] = 0; // 失败后不再重复注册，避免占用
        }
    }

    private void UnregisterAll()
    {
        if (_hwnd == IntPtr.Zero) return;
        foreach (var id in _ids)
        {
            if (id != 0) UnregisterHotKey(_hwnd, id);
        }
        _registered = false;
    }

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg != WM_HOTKEY) return IntPtr.Zero;
        var id = wParam.ToInt32();
        if (id == _ids[0]) PlayPausePressed?.Invoke();
        else if (id == _ids[1]) NextPressed?.Invoke();
        else if (id == _ids[2]) PreviousPressed?.Invoke();
        else if (id == _ids[3]) ToggleVisibilityPressed?.Invoke();
        handled = true;
        return IntPtr.Zero;
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        try
        {
            UnregisterAll();
            _source?.RemoveHook(WndProc);
            _host.Close();
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"GlobalHotkeyService dispose failed: {ex.Message}");
        }
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
}
