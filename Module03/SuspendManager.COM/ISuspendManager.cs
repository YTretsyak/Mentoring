using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuspendManager.COM
{
  [ComVisible(true)]
  [Guid("89BCB740-6119-101A-BCB7-00DD010655AF")]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  public interface ISuspendManager
  {
    bool Sleep();
  }
}
