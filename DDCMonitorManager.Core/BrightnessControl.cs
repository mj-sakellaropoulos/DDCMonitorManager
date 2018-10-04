using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DDCMonitorManager.Core.Native;

namespace DDCMonitorManager.Core
{
    public class BrightnessControl
    {
        private List<NativeStructures.PHYSICAL_MONITOR> PhysicalMonitors;
        private uint pdwNumberOfPhysicalMonitors;
        private MonitorEnumerator monitorEnumerator;

        public BrightnessControl()
        {
            monitorEnumerator = new MonitorEnumerator();
            PhysicalMonitors = monitorEnumerator.PMONITORS;
            pdwNumberOfPhysicalMonitors = (uint)PhysicalMonitors.Count;
        }

        //private void GetMonitorCapabilities(int monitorNumber)
        //{
        //    uint pdwMonitorCapabilities = 0u;
        //    uint pdwSupportedColorTemperatures = 0u;
        //    var monitorCapabilities = NativeCalls.GetMonitorCapabilities(pPhysicalMonitorArray[monitorNumber].hPhysicalMonitor, ref pdwMonitorCapabilities, ref pdwSupportedColorTemperatures);
        //    Debug.WriteLine(pdwMonitorCapabilities);
        //    Debug.WriteLine(pdwSupportedColorTemperatures);
        //    int lastWin32Error = Marshal.GetLastWin32Error();
        //    NativeStructures.MC_DISPLAY_TECHNOLOGY_TYPE type = NativeStructures.MC_DISPLAY_TECHNOLOGY_TYPE.MC_SHADOW_MASK_CATHODE_RAY_TUBE;
        //    var monitorTechnologyType = NativeCalls.GetMonitorTechnologyType(pPhysicalMonitorArray[monitorNumber].hPhysicalMonitor, ref type);
        //    Debug.WriteLine(type);
        //    lastWin32Error = Marshal.GetLastWin32Error();
        //}

        public bool SetBrightness(short brightness,int monitorNumber)
        {
            var brightnessWasSet = HighLevelNativeCalls.SetMonitorBrightness(PhysicalMonitors[monitorNumber].hPhysicalMonitor, (short)brightness);
            if (brightnessWasSet)
                Debug.WriteLine("Brightness set to " + (short)brightness);
            int lastWin32Error = Marshal.GetLastWin32Error();
            return brightnessWasSet;
        }

        public BrightnessInfo GetBrightnessCapabilities(int monitorNumber)
        {
            short current = -1, minimum = -1, maximum = -1;
            HighLevelNativeCalls.GetMonitorBrightness(PhysicalMonitors[monitorNumber].hPhysicalMonitor,ref minimum,ref current,ref maximum);
            return new BrightnessInfo { Minimum = minimum, Maximum = maximum, Current = current};
        }

        //public void DestroyMonitors()
        //{
        //    var destroyPhysicalMonitors = NativeCalls.DestroyPhysicalMonitors(pdwNumberOfPhysicalMonitors, pPhysicalMonitorArray);
        //}

        public uint GetMonitorCount()
        {
            return pdwNumberOfPhysicalMonitors;
        }
    }
}
