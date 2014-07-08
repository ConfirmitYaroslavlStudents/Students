using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeLocker;

namespace TimeLockerTests
{
	[TestClass]
	public class RemainingTimeControllerTests
	{
		[TestMethod]
		public void CulculateCorrectTime()
		{
			var expectedTime = TimeSpan.FromMinutes(1);
			// Запускать не позже, чем за минуту до полуночи!!!
			var remainingTimeController = new RemainingTimeController(expectedTime);
			remainingTimeController.StopTimer();

			Assert.AreEqual(expectedTime, remainingTimeController.RemainingTimeToLock);
		}

		[TestMethod]
		public void CorrectHandlingOfMidnight()
		{
			var time = TimeSpan.FromHours(25);
			var remainingTimeController = new RemainingTimeController(time);
			remainingTimeController.StopTimer();

			var maxAllowedTime = Locktimer.GetMaxAllowedTime();
			var expectedTime = TimeSpan.FromSeconds((int)(TimeSpan.FromHours(24) - DateTime.Now.TimeOfDay).TotalSeconds) + maxAllowedTime;
			var actualTime = TimeSpan.FromSeconds((int)(remainingTimeController.RemainingTimeToLock.TotalSeconds));

			Assert.AreEqual(expectedTime, actualTime);
		}

		[TestMethod]
		public void StopTimerWorks()
		{
			var remainingTimeController = new RemainingTimeController(TimeSpan.FromMinutes(30));

			remainingTimeController.StopTimer();
			var firstRemainingTime = remainingTimeController.RemainingTimeToLock;
			Thread.Sleep(3000);
			var secondRemainingTime = remainingTimeController.RemainingTimeToLock;

			Assert.AreEqual(firstRemainingTime, secondRemainingTime);
		}

		[TestMethod]
		public void StartTimerWorks()
		{
			var remainingTimeController = new RemainingTimeController(TimeSpan.FromMinutes(30));

			remainingTimeController.StopTimer();
			remainingTimeController.StartTimer();
			var firstRemainingTime = remainingTimeController.RemainingTimeToLock;
			Thread.Sleep(3000);
			var secondRemainingTime = remainingTimeController.RemainingTimeToLock;

			Assert.AreNotEqual(firstRemainingTime, secondRemainingTime);
		}
	}
}
