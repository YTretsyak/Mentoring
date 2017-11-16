using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace SuspendManager.COM
{
    [ComVisible(true)]
    [Guid("89BCB740-6119-101A-BCB7-00DD011235AF")]
    [ClassInterface(ClassInterfaceType.None)]
    public class SuspendManager : ISuspendManager
    {
      private readonly PowerStateManager.PowerStateManager psm = new PowerStateManager.PowerStateManager();

      public bool Sleep()
      {
        var res = psm.Suspend;
        return res;
      }
    }
}
