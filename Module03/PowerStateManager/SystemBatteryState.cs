using System.Runtime.InteropServices;

namespace PowerStateManager
{
    [ComVisible(true)]
    [StructLayout(LayoutKind.Sequential)]
    public class SystemBatteryState
    {
        public bool AcOnLine;
        public bool BatteryPresent;
        public bool Charging;
        public bool Discharging;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public bool[] Spare1;
        public int MaxCapacity;
        public int RemainingCapacity;
        public int Rate;
        public int EstimatedTime;
        public int DefaultAlert1;
        public int DefaultAlert2;
    }
}
