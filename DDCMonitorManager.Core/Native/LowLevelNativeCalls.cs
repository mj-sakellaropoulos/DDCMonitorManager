using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DDCMonitorManager.Core.Native
{
    class LowLevelNativeCalls
    {

        [DllImport("dxva2.dll", EntryPoint = "CapabilitiesRequestAndCapabilitiesReply", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CapabilitiesRequestAndCapabilitiesReply(
            IntPtr hMonitor,
            [Out] string pszASCIICapabilitiesString,
            uint dwCapabilitiesStringLengthInCharacters);

        [DllImport("dxva2.dll", EntryPoint = "GetCapabilitiesStringLength", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCapabilitiesStringLength(IntPtr hMonitor, uint pdwCapabilitiesStringLengthInCharacters);

        [DllImport("dxva2.dll", EntryPoint = "GetVCPFeatureAndVCPFeatureReply", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetVCPFeatureAndVCPFeatureReply(
            IntPtr hMonitor,
            byte bVCPCode,
            ref NativeStructures._MC_VCP_CODE_TYPE pvct,
            ref uint pdwCurrentValue,
            ref uint pdwMaximumValue);

        [DllImport("dxva2.dll", EntryPoint = "SaveCurrentSettings", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SaveCurrentSettings(IntPtr hMonitor);

        [DllImport("dxva2.dll", EntryPoint = "SetVCPFeature", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetVCPFeature(IntPtr hMonitor, byte bVCPCode, uint dwNewValue);

    }
}
