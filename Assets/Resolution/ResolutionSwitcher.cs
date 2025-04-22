using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ResolutionSwitcher : MonoBehaviour {
    // Win32 常量
    private const int ENUM_CURRENT_SETTINGS   = -1;
    private const int CDS_TEST                = 0x02;
    private const int CDS_UPDATEREGISTRY      = 0x01;
    private const int CDS_FULLSCREEN          = 0x04;
    private const int DISP_CHANGE_SUCCESSFUL  = 0;

    // 保存启动前的分辨率
    private DEVMODE originalMode;

    void Awake() {
        // 获取并保存当前模式
        originalMode = GetCurrentMode();
        // 尝试切换到 1920×1080
        bool ok = TrySetMode(1920, 1080, originalMode.dmDisplayFrequency);
        if (!ok)
            Debug.LogError("无法切换到 1920×1080 分辨率");
        else
            Debug.Log("已切换到 1920×1080 分辨率");
    }

    void OnApplicationQuit() {
        // 恢复为启动时的分辨率
        int ret = NativeMethods.ChangeDisplaySettingsExA(
            null, ref originalMode, IntPtr.Zero, 0, IntPtr.Zero);
        if (ret == DISP_CHANGE_SUCCESSFUL)
            Debug.Log("已恢复原始分辨率");
        else
            Debug.LogError($"恢复分辨率失败，返回码 {ret}");
    }

    // 获取当前桌面模式
    private DEVMODE GetCurrentMode() {
        var dm = DEVMODE.Create();
        NativeMethods.EnumDisplaySettingsExA(
            null, ENUM_CURRENT_SETTINGS, ref dm, 0);
        return dm;
    }

    // 测试并应用新模式
    private bool TrySetMode(int w, int h, int freq) {
        var dm = DEVMODE.Create();
        dm.dmPelsWidth        = w;
        dm.dmPelsHeight       = h;
        dm.dmDisplayFrequency = freq;
        dm.dmFields           = DEVMODE.DM_PELSWIDTH 
                             | DEVMODE.DM_PELSHEIGHT 
                             | DEVMODE.DM_DISPLAYFREQUENCY;

        // 先测试
        int test = NativeMethods.ChangeDisplaySettingsExA(
            null, ref dm, IntPtr.Zero,
            CDS_TEST | CDS_UPDATEREGISTRY, IntPtr.Zero);
        if (test != DISP_CHANGE_SUCCESSFUL) return false;

        // 正式应用
        int apply = NativeMethods.ChangeDisplaySettingsExA(
            null, ref dm, IntPtr.Zero,
            CDS_UPDATEREGISTRY | CDS_FULLSCREEN, IntPtr.Zero);
        return apply == DISP_CHANGE_SUCCESSFUL;
    }

    // Win32 API 和 DEVMODE 结构定义
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DEVMODE {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmDeviceName;
        public ushort dmSpecVersion, dmDriverVersion, dmSize, dmDriverExtra;
        public uint   dmFields;
        public int    dmPositionX, dmPositionY;
        public int    dmPelsWidth, dmPelsHeight;
        public int    dmDisplayFrequency;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmFormName;

        public const uint DM_PELSWIDTH           = 0x80000;
        public const uint DM_PELSHEIGHT          = 0x100000;
        public const uint DM_DISPLAYFREQUENCY    = 0x400000;

        public static DEVMODE Create() {
            var dm = new DEVMODE();
            dm.dmDeviceName = new string(new char[32]);
            dm.dmFormName   = new string(new char[32]);
            dm.dmSize       = (ushort)Marshal.SizeOf<DEVMODE>();
            return dm;
        }
    }

    internal static class NativeMethods {
        private const string USER32 = "user32.dll";

        [DllImport(USER32, CharSet = CharSet.Ansi)]
        public static extern bool EnumDisplaySettingsExA(
            string deviceName, int modeNum, ref DEVMODE devMode, int flags);

        [DllImport(USER32, CharSet = CharSet.Ansi)]
        public static extern int ChangeDisplaySettingsExA(
            string   deviceName,
            ref DEVMODE devMode,
            IntPtr   hwnd,
            int      flags,
            IntPtr   lParam);
    }
}
