using System;
using System.Runtime.InteropServices;

namespace DDCMonitorManager.Core
{
    public class NativeStructures
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct PHYSICAL_MONITOR
        {
            public IntPtr hPhysicalMonitor;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szPhysicalMonitorDescription;
        }

        public enum MC_DISPLAY_TECHNOLOGY_TYPE
        {
            MC_SHADOW_MASK_CATHODE_RAY_TUBE,

            MC_APERTURE_GRILL_CATHODE_RAY_TUBE,

            MC_THIN_FILM_TRANSISTOR,

            MC_LIQUID_CRYSTAL_ON_SILICON,

            MC_PLASMA,

            MC_ORGANIC_LIGHT_EMITTING_DIODE,

            MC_ELECTROLUMINESCENT,

            MC_MICROELECTROMECHANICAL,

            MC_FIELD_EMISSION_DEVICE,
        }
    }
}
