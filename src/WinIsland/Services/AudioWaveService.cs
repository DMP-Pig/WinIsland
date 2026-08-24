using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace WinIsland.Services;

/// <summary>
/// 播放波纹：优先用 WASAPI 环回（loopback）采集系统输出声音的真实电平，
/// 失败（无声卡/权限/格式）时自动降级为“播放状态驱动”的柔和模拟波纹。
/// 全部为系统 API（P/Invoke），无第三方依赖；后台采集线程任何异常都不会影响主程序。
/// </summary>
public sealed class AudioWaveService : IDisposable
{
    // WASAPI 常量
    private const int AUDCLNT_SHAREMODE_SHARED = 0;
    private const int AUDCLNT_STREAMFLAGS_LOOPBACK = 0x00020000;
    private const int WAVE_FORMAT_PCM = 1;
    private const int WAVE_FORMAT_IEEE_FLOAT = 3;

    private static readonly Guid CLSID_MMDeviceEnumerator = new("BCDE0395-E52F-467C-8E3D-C4579291692E");
    private static readonly Guid IID_IMMDeviceEnumerator = new("A95664D2-9614-4F35-A746-DE8DB63617E6");
    private static readonly Guid IID_IAudioClient = new("1CB9AD4C-DBFA-4C32-B178-C2F568A703B2");
    private static readonly Guid IID_IAudioCaptureClient = new("C8ADBD64-E71E-48A0-A4DE-185C395CD317");

    private volatile bool _running;
    private Thread? _thread;
    private readonly object _gate = new();
    private double _level;              // 0..1 平滑后的波纹强度
    private bool _playing;
    private readonly Random _rng = new();
    private double _simTarget;

    /// <summary>当前波纹强度（0..1），UI 每帧轮询。</summary>
    public double Level { get { lock (_gate) return _level; } }

    /// <summary>是否启用了真实音频采集（false = 模拟降级）。</summary>
    public bool LiveCapture { get; private set; }

    /// <summary>由媒体状态驱动：播放为 true、暂停/停止为 false。</summary>
    public void SetPlaying(bool playing)
    {
        _playing = playing;
    }

    public void Start()
    {
        if (_running) return;
        _running = true;
        _thread = new Thread(Loop) { IsBackground = true, Name = "AudioWave" };
        _thread.Start();
    }

    public void Stop()
    {
        _running = false;
    }

    private void Loop()
    {
        try
        {
            if (TryWasapiLoop())
            {
                LiveCapture = true;
                AppLogger.Info("Audio wave: live WASAPI loopback capture active");
                return;
            }
        }
        catch (Exception ex)
        {
            AppLogger.Debug($"Audio wave WASAPI failed: {ex.Message}");
        }

        LiveCapture = false;
        AppLogger.Info("Audio wave: using simulated waveform (no audio capture)");
        SimulateLoop();
    }

    // ── 模拟降级：播放时柔和波动，暂停时衰减到 0 ──────────────────
    private void SimulateLoop()
    {
        while (_running)
        {
            lock (_gate)
            {
                var now = _playing && (DateTime.UtcNow.Ticks & 0x1FFFFF) < 0x15555; // ~三分之二时间波动
                if (_playing && _simTarget <= 0) _simTarget = 0.22 + _rng.NextDouble() * 0.45;
                if (now || !_playing)
                {
                    _simTarget = Math.Max(0, Math.Min(1,
                        _simTarget + (_rng.NextDouble() - 0.5) * 0.16 + (_playing ? 0 : -0.35)));
                }
                // 指数平滑：起音快、衰减慢，视觉连贯
                var t = _level + (_simTarget - _level) * (_playing ? 0.28 : 0.12);
                _level = t < 0.012 ? 0 : t;
            }
            Thread.Sleep(33); // ~30fps
        }
    }

    // ── WASAPI 环回采集 ────────────────────────────────────────────
    private bool TryWasapiLoop()
    {
        if (CoInitializeEx(IntPtr.Zero, 0) < 0) return false; // COINIT_MULTITHREADED

        IntPtr enumerator = IntPtr.Zero, device = IntPtr.Zero, client = IntPtr.Zero, capture = IntPtr.Zero;
        try
        {
            var clsidEnum = CLSID_MMDeviceEnumerator;
            var iidEnum = IID_IMMDeviceEnumerator;
            if (CoCreateInstance(ref clsidEnum, IntPtr.Zero, 23, ref iidEnum, out enumerator) < 0)
                return false;
            var devEnum = (IMMDeviceEnumerator)Marshal.GetObjectForIUnknown(enumerator);
            devEnum.GetDefaultAudioEndpoint(0 /*eRender*/, 1 /*eMultimedia*/, out device);
            if (device == IntPtr.Zero) return false;

            var mmDevice = (IMMDevice)Marshal.GetObjectForIUnknown(device);
            var iidClient = IID_IAudioClient;
            mmDevice.Activate(ref iidClient, 23 /*CLSCTX_ALL*/, IntPtr.Zero, out client);
            if (client == IntPtr.Zero) return false;

            var audioClient = (IAudioClient)Marshal.GetObjectForIUnknown(client);
            audioClient.GetMixFormat(out var fmtPtr);
            if (fmtPtr == IntPtr.Zero) return false;
            var fmt = Marshal.PtrToStructure<WAVEFORMATEX>(fmtPtr);
            if (fmt.nChannels == 0 || fmt.nSamplesPerSec == 0) return false;

            // 环回模式：200ms 缓冲
            const long bufferHns = 200 * 10000; // 200ms
            audioClient.Initialize(AUDCLNT_SHAREMODE_SHARED, AUDCLNT_STREAMFLAGS_LOOPBACK, bufferHns, 0, fmtPtr, IntPtr.Zero);
            var iidCapture = IID_IAudioCaptureClient;
            audioClient.GetService(ref iidCapture, out capture);
            if (capture == IntPtr.Zero) return false;
            var cap = (IAudioCaptureClient)Marshal.GetObjectForIUnknown(capture);

            audioClient.Start();
            try
            {
                var blockAlign = Math.Max(1, (int)fmt.nBlockAlign);
                var channels = fmt.nChannels;
                var step = fmt.wBitsPerSample / 8;           // 每样本字节（2 或 4）
                if (step < 2) step = 2;

                while (_running)
                {
                    uint packet = 0;
                    if (cap.GetNextPacketSize(out packet) < 0 || packet == 0)
                    {
                        Thread.Sleep(20);
                        continue;
                    }
                    if (cap.GetBuffer(out var dataPtr, out uint frames, out _, out _, out _) < 0 || frames == 0 || dataPtr == IntPtr.Zero)
                    {
                        cap.ReleaseBuffer(0);
                        Thread.Sleep(20);
                        continue;
                    }

                    var totalBytes = (int)(frames * blockAlign);
                    totalBytes = Math.Min(totalBytes, 1 << 20); // 防异常大包
                    var bytes = new byte[totalBytes];
                    Marshal.Copy(dataPtr, bytes, 0, totalBytes);
                    cap.ReleaseBuffer(frames);

                    double level = ComputeLevel(bytes, fmt, channels, step, totalBytes);
                    lock (_gate)
                    {
                        // 起音快、释放慢
                        var alpha = level > _level ? 0.55 : 0.18;
                        _level = _level + (level - _level) * alpha;
                    }
                }
            }
            finally
            {
                try { audioClient.Stop(); } catch { }
            }
            return true;
        }
        finally
        {
            if (capture != IntPtr.Zero) Marshal.Release(capture);
            if (client != IntPtr.Zero) Marshal.Release(client);
            if (device != IntPtr.Zero) Marshal.Release(device);
            if (enumerator != IntPtr.Zero) Marshal.Release(enumerator);
        }
    }

    /// <summary>计算一包样本的平均电平（0..1）。支持 PCM16 与 IEEE float32。</summary>
    private static double ComputeLevel(byte[] bytes, WAVEFORMATEX fmt, ushort channels, int step, int length)
    {
        if (length <= 0) return 0;
        var isFloat = fmt.wFormatTag == WAVE_FORMAT_IEEE_FLOAT;
        var count = length / step;
        if (count <= 0) return 0;

        double sum = 0;
        if (isFloat)
        {
            for (var i = 0; i + 4 <= bytes.Length; i += 4)
            {
                var f = BitConverter.ToSingle(bytes, i);
                if (float.IsNaN(f) || float.IsInfinity(f)) f = 0;
                sum += Math.Abs(f);
            }
            return Math.Min(1, sum / Math.Max(1, bytes.Length / 4));
        }

        // PCM16
        for (var i = 0; i + 2 <= bytes.Length; i += 2)
        {
            var s = BitConverter.ToInt16(bytes, i);
            sum += Math.Abs(s / 32768.0);
        }
        return Math.Min(1, sum / Math.Max(1, bytes.Length / 2));
    }

    public void Dispose() => Stop();

    // ── COM 接口（vtable 索引自 IUnknown 后开始）──
    [ComImport, Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMMDeviceEnumerator
    {
        void EnumAudioEndpoints(int dataFlow, int stateMask, out IntPtr devices);
        void GetDefaultAudioEndpoint(int dataFlow, int role, out IntPtr device);
        void GetDevice(string id, out IntPtr device);
        void RegisterEndpointNotificationCallback(IntPtr client);
        void UnregisterEndpointNotificationCallback(IntPtr client);
    }

    [ComImport, Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMMDevice
    {
        void Activate(ref Guid iid, int clsCtx, IntPtr activationParams, out IntPtr iface);
        void OpenPropertyStore(int stgmAccess, out IntPtr properties);
        void GetId(out IntPtr id);
        void GetState(out int state);
    }

    [ComImport, Guid("1CB9AD4C-DBFA-4C32-B178-C2F568A703B2"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IAudioClient
    {
        void Initialize(int shareMode, int streamFlags, long bufferDurationHns, long periodicityHns, IntPtr waveFormat, IntPtr audioSessionGuid);
        void GetBufferSize(out uint numBufferFrames);
        void GetStreamLatency(out long latency);
        void GetCurrentPadding(out uint numPaddingFrames);
        void IsFormatSupported(int shareMode, IntPtr format, out IntPtr closestMatch);
        void GetMixFormat(out IntPtr deviceFormat);
        void GetDevicePeriod(out long defaultPeriod, out long minPeriod);
        void Start();
        void Stop();
        void Reset();
        void SetEventHandle(IntPtr eventHandle);
        void GetService(ref Guid iid, out IntPtr service);
    }

    [ComImport, Guid("C8ADBD64-E71E-48A0-A4DE-185C395CD317"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IAudioCaptureClient
    {
        int GetBuffer(out IntPtr data, out uint frames, out uint flags, out ulong devicePosition, out ulong qpcPosition);
        void ReleaseBuffer(uint frames);
        int GetNextPacketSize(out uint frames);
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct WAVEFORMATEX
    {
        public ushort wFormatTag;
        public ushort nChannels;
        public uint nSamplesPerSec;
        public uint nAvgBytesPerSec;
        public ushort nBlockAlign;
        public ushort wBitsPerSample;
        public ushort cbSize;
    }

    [DllImport("ole32.dll")]
    private static extern int CoInitializeEx(IntPtr pvReserved, uint dwCoInit);

    [DllImport("ole32.dll")]
    private static extern int CoCreateInstance(ref Guid rclsid, IntPtr pUnkOuter, uint dwClsContext, ref Guid riid, out IntPtr ppv);
}
