using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeLocker;

namespace TimeLockerTests
{
	[TestClass]
	public class LastSessionSynchronizerTests
	{
		[TestMethod]
		public void SaveSessionDataCreateFile()
		{
			if (File.Exists(LastSessionSynchronizer.LAST_SESSION_DATA_WAY))
				File.Delete(LastSessionSynchronizer.LAST_SESSION_DATA_WAY);

			LastSessionSynchronizer.SaveSessionData(TimeSpan.FromHours(2));
			var fileExists = File.Exists(LastSessionSynchronizer.LAST_SESSION_DATA_WAY);

			Assert.AreEqual(true, fileExists);
		}

		[TestMethod]
		public void SaveSessionDataSaveCorrectDate()
		{
			LastSessionSynchronizer.SaveSessionData(TimeSpan.FromHours(2));
			DateTime savedDate;
			using (var lastSessionDataFile = new StreamReader(LastSessionSynchronizer.LAST_SESSION_DATA_WAY))
				savedDate = DateTime.Parse(lastSessionDataFile.ReadLine());

			Assert.AreEqual(DateTime.Now.Date, savedDate);
		}

		[TestMethod]
		public void SaveSessionDataSaveCorrectRemainingTime()
		{
			var expectedTime = TimeSpan.FromHours(2);

			LastSessionSynchronizer.SaveSessionData(expectedTime);
			TimeSpan savedTime;
			using (var lastSessionDataFile = new StreamReader(LastSessionSynchronizer.LAST_SESSION_DATA_WAY))
			{
				lastSessionDataFile.ReadLine();
				savedTime = TimeSpan.Parse(lastSessionDataFile.ReadLine());
			}

			Assert.AreEqual(expectedTime, savedTime);
		}

		[TestMethod]
		public void GetAllowedTimeReturnMaxAllowedTimeWhenFileNotExists()
		{
			if (File.Exists(LastSessionSynchronizer.LAST_SESSION_DATA_WAY))
				File.Delete(LastSessionSynchronizer.LAST_SESSION_DATA_WAY);

			var allowedTime = LastSessionSynchronizer.GetAllowedTime();
			var maxAllowedTime = Locktimer.GetMaxAllowedTime();

			Assert.AreEqual(maxAllowedTime, allowedTime);
		}

		[TestMethod]
		public void GetAllowedTimeReturnMaxAllowedTimeWhenRemainingTimeLargerThenMaxAllowedTime()
		{
			TimeSpan savingTime = Locktimer.GetMaxAllowedTime() + TimeSpan.FromMinutes(30);
			LastSessionSynchronizer.SaveSessionData(savingTime);

			var allowedTime = LastSessionSynchronizer.GetAllowedTime();
			var maxAllowedTime = Locktimer.GetMaxAllowedTime();

			Assert.AreEqual(maxAllowedTime, allowedTime);
		}

		[TestMethod]
		public void GetAllowedTimeReturnSavedTimeWhenRemainingTimeLessThenMaxAllowedTime()
		{
			var expectedTime = TimeSpan.FromMilliseconds(Locktimer.GetMaxAllowedTime().TotalMilliseconds / 2);
			LastSessionSynchronizer.SaveSessionData(expectedTime);

			var allowedTime = LastSessionSynchronizer.GetAllowedTime();

			Assert.AreEqual(expectedTime, allowedTime);
		}

		[TestMethod]
		public void GetAllowedTimeReturnMaxAllowedTimeWhenSavedDateLessThanCurrentDate()
		{
			var notExpectedTime = TimeSpan.FromMilliseconds(Locktimer.GetMaxAllowedTime().TotalMilliseconds / 2);
			using (var sessionData = new StreamWriter(LastSessionSynchronizer.LAST_SESSION_DATA_WAY, false))
			{
				sessionData.WriteLine(DateTime.Now.Subtract(TimeSpan.FromDays(1)).Date.ToString());
				sessionData.WriteLine(notExpectedTime.ToString(@"hh\:mm\:ss"));
			}

			var allowedTime = LastSessionSynchronizer.GetAllowedTime();
			var maxAllowedTime = Locktimer.GetMaxAllowedTime();

			Assert.AreEqual(maxAllowedTime, allowedTime);
		}
	}
}
