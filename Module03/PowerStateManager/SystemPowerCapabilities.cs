using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PowerStateManager
{
  [ComVisible(true)]
  [StructLayout(LayoutKind.Sequential)]
  public struct SystemPowerCapabilities
  {
    [MarshalAs(UnmanagedType.I1)]
    public bool PowerButtonPresent;
    [MarshalAs(UnmanagedType.I1)]
    public bool SleepButtonPresent;
    [MarshalAs(UnmanagedType.I1)]
    public bool LidPresent;
    [MarshalAs(UnmanagedType.I1)]
    public bool SystemS1;
    [MarshalAs(UnmanagedType.I1)]
    public bool SystemS2;
    [MarshalAs(UnmanagedType.I1)]
    public bool SystemS3;
    [MarshalAs(UnmanagedType.I1)]
    public bool SystemS4;
    [MarshalAs(UnmanagedType.I1)]
    public bool SystemS5;
    [MarshalAs(UnmanagedType.I1)]
    public bool HiberFilePresent;
    [MarshalAs(UnmanagedType.I1)]
    public bool FullWake;
    [MarshalAs(UnmanagedType.I1)]
    public bool VideoDimPresent;
    [MarshalAs(UnmanagedType.I1)]
    public bool ApmPresent;
    [MarshalAs(UnmanagedType.I1)]
    public bool UpsPresent;
    [MarshalAs(UnmanagedType.I1)]
    public bool ThermalControl;
    [MarshalAs(UnmanagedType.I1)]
    public bool ProcessorThrottle;
    public byte ProcessorMinThrottle;
    public byte ProcessorMaxThrottle;
    public bool FastSystemS4;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public byte[] spare2;
    [MarshalAs(UnmanagedType.I1)]
    public bool DiskSpinDown;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public byte[] spare3;
    [MarshalAs(UnmanagedType.I1)]
    public bool SystemBatteriesPresent;
    [MarshalAs(UnmanagedType.I1)]
    public bool BatteriesAreShortTerm;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public BatteryReportingScale[] BatteryScale;
    public SystemPowerState AcOnLineWake;
    public SystemPowerState SoftLidWake;
    public SystemPowerState RtcWake;
    public SystemPowerState MinDeviceWakeState;
    public SystemPowerState DefaultLowLatencyWake;
  }

  public enum SystemPowerState
  {
    PowerSystemUnspecified = 0,
    PowerSystemWorking = 1,
    PowerSystemSleeping1 = 2,
    PowerSystemSleeping2 = 3,
    PowerSystemSleeping3 = 4,
    PowerSystemHibernate = 5,
    PowerSystemShutdown = 6,
    PowerSystemMaximum = 7
  }
}
