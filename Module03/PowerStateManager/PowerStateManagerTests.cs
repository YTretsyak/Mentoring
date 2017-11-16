using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PowerStateManager
{
  [TestClass]
  public class PowerStateManagerTests
  {
    [TestMethod]
    public void GetLastSleepTimeTest()
    {
      var powerstateManager = new PowerStateManager();

      var lastSleepTime = powerstateManager.LastSleepTime;
    }

    [TestMethod]
    public void GetLastWakeTimeTest()
    {
      var powerstateManager = new PowerStateManager();

      var lastSleepTime = powerstateManager.LastWakeTime;
    }

    [TestMethod]
    public void GetSystemBatteryState()
    {
      var powerManager = new PowerStateManager();

      var powerManagerState = powerManager.SystemBatteryState;
    }

    [TestMethod]
    public void GetSystemPowerInformation()
    {
      var powerManager = new PowerStateManager();

      var powerSystemInformation = powerManager.SystemPowerInformation;
    }

    [TestMethod]
    public void AddHibFileTest()
    {
      var powerManager = new PowerStateManager();

      var res = powerManager.AddHibernateFile;

      Assert.AreEqual(true, res);
    }

    [TestMethod]
    public void RemoveHibFileTest()
    {
      var powerManager = new PowerStateManager();

      var res = powerManager.RemoveHibernateFile;

      Assert.AreEqual(false, res);
    }

    [TestMethod]
    public void SleepTest()
    {
      var powerManager = new PowerStateManager();

      var res = powerManager.Suspend;

      Assert.AreEqual(true, res);
    }
  }
}
