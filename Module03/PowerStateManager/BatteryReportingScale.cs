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
  public struct BatteryReportingScale
  {
    public ulong Granularity;
    public ulong Capacity;
  }
}
