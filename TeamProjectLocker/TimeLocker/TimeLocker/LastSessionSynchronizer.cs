using System;
using System.IO;

namespace TimeLocker
{
	public static class LastSessionSynchronizer
    {
		public const string LAST_SESSION_DATA_WAY = "LastSession.dat";

        public static void SaveSessionData(TimeSpan remainingTime)
        {
            var currentDate = DateTime.Now;

			using (var sessionData = new StreamWriter(LAST_SESSION_DATA_WAY, false))
            {
                sessionData.WriteLine(currentDate.Date.ToString());
                sessionData.WriteLine(remainingTime.ToString(@"hh\:mm\:ss"));
            }
        }

		public static TimeSpan GetAllowedTime()
		{
			if (File.Exists(LAST_SESSION_DATA_WAY))
				using (var lastSessionDataFile = new StreamReader(LAST_SESSION_DATA_WAY))
				{
					var oldDate = DateTime.Parse(lastSessionDataFile.ReadLine());
					var curreentDate = DateTime.Now;

					if (curreentDate.Date > oldDate.Date)
						return Properties.Settings.Default.MaxAllowedTime;
					else
					{
						var remainingTime = TimeSpan.Parse(lastSessionDataFile.ReadLine());

						if (remainingTime < Properties.Settings.Default.MaxAllowedTime)
							return remainingTime;
						else
							return Properties.Settings.Default.MaxAllowedTime;
					}
				}
			else
				return Properties.Settings.Default.MaxAllowedTime;
		}
    }
}
