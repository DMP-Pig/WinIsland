using System.Runtime.InteropServices;

namespace WinIsland.Services;

/// <summary>
/// Controls the *system* master volume through the Windows CoreAudio COM API
/// (IMMDeviceEnumerator / IAudioEndpointVolume). Used for non-Cider sources when
/// the user opts into system-volume control.
/// </summary>
public static class SystemVolume
{
    private static readonly Guid IID_IMMDeviceEnumerator = new("A95664D2-9614-4F35-A746-DE8DB63617E6");
    private static readonly Guid IID_IAudioEndpointVolume = new("5CDF2C82-841E-4546-9722-0CF74078229A");
    private static readonly Guid CLSID_MMDeviceEnumerator = new("BCDE0395-E52F-467C-8E3D-C4579291692E");
    private static readonly Guid GUID_NULL = Guid.Empty;

    private static IAudioEndpointVolume? GetEndpointVolume()
    {
        var enumerator = Activator.CreateInstance(
            Type.GetTypeFromCLSID(CLSID_MMDeviceEnumerator, server: null) ?? throw new InvalidOperationException("MMDeviceEnumerator not available"))
            as IMMDeviceEnumerator ?? throw new InvalidOperationException("MMDeviceEnumerator activation failed");
        try
        {
            enumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eConsole, out var device);
            if (device is null) return null;
            try
            {
                device!.Activate(IID_IAudioEndpointVolume, CLSCTX.CLSCTX_ALL, IntPtr.Zero, out var volume);
                return volume as IAudioEndpointVolume;
            }
            finally
            {
                Marshal.ReleaseComObject(device);
            }
        }
        finally
        {
            Marshal.ReleaseComObject(enumerator);
        }
    }

    public static double? GetVolume()
    {
        try
        {
            var vol = GetEndpointVolume();
            if (vol is null) return null;
            try
            {
                vol.GetMasterVolumeLevelScalar(out var level);
                return Math.Clamp(level, 0, 1);
            }
            finally
            {
                Marshal.ReleaseComObject(vol!);
            }
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"SystemVolume.Get failed: {ex.Message}");
            return null;
        }
    }

    public static void SetVolume(double value01)
    {
        try
        {
            var vol = GetEndpointVolume();
            if (vol is null) return;
            try
            {
                var context = Guid.Empty;
                vol.SetMasterVolumeLevelScalar((float)Math.Clamp(value01, 0, 1), ref context);
            }
            finally
            {
                Marshal.ReleaseComObject(vol!);
            }
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"SystemVolume.Set failed: {ex.Message}");
        }
    }

    public static bool IsMuted()
    {
        try
        {
            var vol = GetEndpointVolume();
            if (vol is null) return false;
            try
            {
                vol.GetMute(out var muted);
                return muted;
            }
            finally
            {
                Marshal.ReleaseComObject(vol!);
            }
        }
        catch { return false; }
    }

    // ── COM interfaces ─────────────────────────────────────────

    [ComImport, Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
    private class MMDeviceEnumeratorComObject { }

    [ComImport, Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMMDeviceEnumerator
    {
        [PreserveSig] int EnumAudioEndpoints(EDataFlow dataFlow, uint dwStateMask, out IntPtr devices);
        [PreserveSig] int GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice? device);
        [PreserveSig] int GetDevice(string id, out IMMDevice device);
        [PreserveSig] int RegisterEndpointNotificationCallback(IntPtr client);
        [PreserveSig] int UnregisterEndpointNotificationCallback(IntPtr client);
    }

    [ComImport, Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMMDevice
    {
        [PreserveSig] int Activate(in Guid iid, CLSCTX clsCtx, IntPtr activationParams, [MarshalAs(UnmanagedType.IUnknown)] out object? interfacePtr);
        [PreserveSig] int OpenPropertyStore(uint stgmAccess, out IntPtr properties);
        [PreserveSig] int GetId(out IntPtr id);
        [PreserveSig] int GetState(out uint state);
    }

    [ComImport, Guid("5CDF2C82-841E-4546-9722-0CF74078229A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IAudioEndpointVolume
    {
        [PreserveSig] int RegisterControlChangeNotify(IntPtr notify);
        [PreserveSig] int UnregisterControlChangeNotify(IntPtr notify);
        [PreserveSig] int GetChannelCount(out uint count);
        [PreserveSig] int SetMasterVolumeLevel(float level, ref Guid eventContext);
        [PreserveSig] int SetMasterVolumeLevelScalar(float level, ref Guid eventContext);
        [PreserveSig] int GetMasterVolumeLevel(out float level);
        [PreserveSig] int GetMasterVolumeLevelScalar(out float level);
        [PreserveSig] int SetChannelVolumeLevel(uint channel, float level, ref Guid eventContext);
        [PreserveSig] int SetChannelVolumeLevelScalar(uint channel, float level, ref Guid eventContext);
        [PreserveSig] int GetChannelVolumeLevel(uint channel, out float level);
        [PreserveSig] int GetChannelVolumeLevelScalar(uint channel, out float level);
        [PreserveSig] int SetMute(bool mute, ref Guid eventContext);
        [PreserveSig] int GetMute(out bool mute);
    }

    private enum EDataFlow { eRender = 0, eCapture = 1, eAll = 2 }
    private enum ERole { eConsole = 0, eMultimedia = 1, eCommunications = 2 }
    private enum CLSCTX : uint { CLSCTX_ALL = 0x17 }
}


