using System;
using System.Runtime.InteropServices;

namespace PowerStateManager.COM
{
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IPowerStateManagerCOM
    {
        DateTime GetLastSleepTime();

        DateTime GetLastWakeTime();
    }
}
