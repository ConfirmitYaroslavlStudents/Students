using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TimeLocker
{
    class DataSaver
    {
        public static void SaveSessionData(TimeSpan remainingTime)
        {
            var currentDate = DateTime.Now;
            using (var sessionData = new StreamWriter("LastSession.dat", false))
            {
                sessionData.WriteLine(currentDate.Date.ToString());
                sessionData.WriteLine(remainingTime.ToString(@"hh\:mm\:ss"));
            }
        }
    }
}
