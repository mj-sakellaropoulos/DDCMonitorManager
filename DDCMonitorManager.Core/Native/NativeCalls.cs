using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DDCMonitorManager.Core.Native
{
    public class NativeCalls
    {
        [DllImport("user32.dll", EntryPoint = "MonitorFromWindow", SetLastError = true)]
        public static extern IntPtr MonitorFromWindow(
            [In] IntPtr hwnd, uint dwFlags);

        [DllImport("dxva2.dll", EntryPoint = "GetNumberOfPhysicalMonitorsFromHMONITOR", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(
            IntPtr hMonitor, ref uint pdwNumberOfPhysicalMonitors);

        [DllImport("dxva2.dll", EntryPoint = "GetPhysicalMonitorsFromHMONITOR", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPhysicalMonitorsFromHMONITOR(
            IntPtr hMonitor,
            uint dwPhysicalMonitorArraySize,
            [Out] NativeStructures.PHYSICAL_MONITOR[] pPhysicalMonitorArray);

        [DllImport("dxva2.dll", EntryPoint = "DestroyPhysicalMonitors", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyPhysicalMonitors(
            uint dwPhysicalMonitorArraySize, [Out] NativeStructures.PHYSICAL_MONITOR[] pPhysicalMonitorArray);

        [DllImport("dxva2.dll", EntryPoint = "GetMonitorTechnologyType", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorTechnologyType(
            IntPtr hMonitor, ref NativeStructures.MC_DISPLAY_TECHNOLOGY_TYPE pdtyDisplayTechnologyType);

        [DllImport("dxva2.dll", EntryPoint = "GetMonitorCapabilities", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorCapabilities(
            IntPtr hMonitor, ref uint pdwMonitorCapabilities, ref uint pdwSupportedColorTemperatures);

        [DllImport("dxva2.dll", EntryPoint = "SetMonitorBrightness", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetMonitorBrightness(
            IntPtr hMonitor, short brightness);

        [DllImport("dxva2.dll", EntryPoint = "GetMonitorBrightness", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorBrightness(
            IntPtr hMonitor, ref short pdwMinimumBrightness, ref short pdwCurrentBrightness, ref short pdwMaximumBrightness);

    }

    public class MonitorEnumerator
    {

        public MonitorEnumerator()
        {
            GetAllPhysicalMonitors();
        }

        public List<IntPtr> HMONITORS;
        public List<NativeStructures.PHYSICAL_MONITOR> PMONITORS;

        public delegate void lpfnEnumProc(IntPtr hMonitor, IntPtr hdc, IntPtr lpRect, uint lParam);

        private lpfnEnumProc enumCallback;

        private void EnumCallbackHandler(IntPtr hMonitor, IntPtr hdc, IntPtr lpRect, uint lParam)
        {
            HMONITORS.Add(hMonitor);
        }

        [DllImport("user32.dll", EntryPoint = "EnumDisplayMonitors", SetLastError = true)]
        public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, lpfnEnumProc lpfnEnum, uint dwData);

        public List<IntPtr> GetAllHMonitorHandles()
        {
            HMONITORS = new List<IntPtr>();
            enumCallback = new lpfnEnumProc(EnumCallbackHandler);
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, enumCallback, 0);
            return HMONITORS;
        }

        public List<NativeStructures.PHYSICAL_MONITOR> GetAllPhysicalMonitors()
        {
            
            GetAllHMonitorHandles();
            int totalMonitors = HMONITORS.Count;
            NativeStructures.PHYSICAL_MONITOR[] pMonArray = new NativeStructures.PHYSICAL_MONITOR[totalMonitors];

            List<NativeStructures.PHYSICAL_MONITOR> retList = new List<NativeStructures.PHYSICAL_MONITOR>();

            foreach (var intPtr in HMONITORS)
            {
                var tempAr = new NativeStructures.PHYSICAL_MONITOR[1];
                NativeCalls.GetPhysicalMonitorsFromHMONITOR(intPtr, 1, tempAr);
                retList.Add(tempAr[0]);
            }

            PMONITORS = retList;
            return retList;
        }

    }
}