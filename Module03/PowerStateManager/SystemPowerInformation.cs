using System.Runtime.InteropServices;

namespace PowerStateManager
{
    [ComVisible(true)]
    [StructLayout(LayoutKind.Sequential)]
    public class SystemPowerInformation
    {
        public uint MaxIdlenessAllowed;
        public uint Idleness;
        public uint TimeRemaining;
        public sbyte CoolingMode;
    }
}
