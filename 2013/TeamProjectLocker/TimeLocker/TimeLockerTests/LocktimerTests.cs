using System;
using System.Threading;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeLocker;

namespace TimeLockerTests
{
	[TestClass]
	public class LocktimerTests
	{
		[TestMethod]
		public void RemainingTimeIsChanging()
		{
			var windowsLocker = new WindowsLocker();
			var locktimer = new Locktimer(windowsLocker);

			var firstRemainingTime = locktimer.GetRemainingTime();
			Thread.Sleep(3000);
			var secondRemainingTime = locktimer.GetRemainingTime();

			Assert.AreNotEqual(firstRemainingTime, secondRemainingTime);
		}

		[TestMethod]
		public void RemainingTimeReturnTimeThatIsNotLargerThanMaxAllowedTime()
		{
			LastSessionSynchronizer.SaveSessionData(TimeSpan.FromHours(25));
			var windowsLocker = new WindowsLocker();
			var locktimer = new Locktimer(windowsLocker);

			var remainingTime = locktimer.GetRemainingTime();
			var maxAllowedTime = Locktimer.GetMaxAllowedTime();
			var remainingTimeIsNotLargerThanMaxAllowedTime = remainingTime <= maxAllowedTime;

			Assert.AreEqual(true, remainingTimeIsNotLargerThanMaxAllowedTime);
		}
	}
}
