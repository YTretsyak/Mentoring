using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace PowerStateManager
{
  public class PowerStateManager
  {
    #region Public properties

    public DateTime LastSleepTime
        => GetTime(InformationLevelConstants.LastSleepTime);

    public DateTime LastWakeTime
        => GetTime(InformationLevelConstants.LastWakeTime);

    public SystemBatteryState SystemBatteryState
        => GetSystemInformation<SystemBatteryState>(InformationLevelConstants.SystemBatteryState);

    public SystemPowerInformation SystemPowerInformation
        => GetSystemInformation<SystemPowerInformation>(InformationLevelConstants.SystemPowerInformation);

    public bool AddHibernateFile
        => CallToReserveHibernateFile(true);

    public bool RemoveHibernateFile
        => CallToReserveHibernateFile(false);

    public bool Suspend
      => Sleep();

    #endregion

    #region Private methods

    private DateTime GetTime(int informationLevel)
    {
      long ticks = 0;

      CallNtPowerInformation<ulong>(informationLevel, buffer => ticks = Marshal.ReadInt64(buffer));

      var startupTime = PowerStateManagerInternal.GetTickCount64() * 10000;

      return DateTime.UtcNow - TimeSpan.FromTicks((long)startupTime) + TimeSpan.FromTicks(ticks);
    }


    private T GetSystemInformation<T>(int informationLevel)
    {
      var information = default(T);

      CallNtPowerInformation<T>(informationLevel, buffer => information = Marshal.PtrToStructure<T>(buffer));

      return information;
    }

    private void CallNtPowerInformation<T>(int informationLevel, Action<IntPtr> readOutputBuffer)
    {
      var outputBufferSize = Marshal.SizeOf<T>();
      var outputBuffer = Marshal.AllocHGlobal(outputBufferSize);

      PowerStateManagerInternal.CallNtPowerInformation(informationLevel, IntPtr.Zero, 0, outputBuffer, (uint)outputBufferSize);

      readOutputBuffer(outputBuffer);

      Marshal.FreeHGlobal(outputBuffer);
    }

    private bool CallToReserveHibernateFile(bool value)
    {
      var size = Marshal.SizeOf(typeof(int));
      IntPtr pBool = Marshal.AllocHGlobal(size);
      Marshal.WriteInt32(pBool, 0, Convert.ToInt32(value));

      PowerStateManagerInternal.CallNtPowerInformation(InformationLevelConstants.SystemReserveHiberFile, pBool,
        (uint)size, IntPtr.Zero, 0);

      Marshal.FreeHGlobal(pBool);

      SystemPowerCapabilities spc;
      PowerStateManagerInternal.GetPwrCapabilities(out spc);

      return spc.HiberFilePresent;
    }

    private bool Sleep()
    {
      return PowerStateManagerInternal.SetSuspendState(false, false, false);
    }

    #endregion

    #region DLL Imports

    private class PowerStateManagerInternal
    {
      /// <summary>
      /// Retrieves the number of milliseconds that have elapsed since the system was started.
      /// </summary>
      [DllImport("kernel32")]
      public static extern ulong GetTickCount64();

      /// <summary>
      /// Sets or retrieves power information.
      /// </summary>
      [DllImport("powrprof.dll")]
      public static extern uint CallNtPowerInformation(
          int informationLevel, IntPtr inputBuffer, uint inputBufferSize, [Out] IntPtr outputBuffer, uint outputBufferSize);

      /// <summary>
      /// Suspends the system by shutting power down
      /// </summary>
      /// <returns></returns>
      [DllImport("powrprof.dll")]
      public static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

      [DllImport("powrprof.dll")]
      public static extern bool GetPwrCapabilities(out SystemPowerCapabilities systemPowerCapabilites);
    }

    #endregion
  }
}
