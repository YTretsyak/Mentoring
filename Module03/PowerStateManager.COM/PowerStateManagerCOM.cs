using System;
using System.Runtime.InteropServices;

namespace PowerStateManager.COM
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public sealed class PowerStateManagerCOM : IPowerStateManagerCOM
    {
        private readonly PowerStateManager _powerStateManager = new PowerStateManager();

        public DateTime GetLastSleepTime()
            => _powerStateManager.LastSleepTime;

        public DateTime GetLastWakeTime()
            => _powerStateManager.LastWakeTime;
    }
}
