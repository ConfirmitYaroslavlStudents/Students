using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TimeLocker
{
    class LockerLoader
    {
        private static Locker _locker;

        public static Locker GetLocker()
        {
            if (_locker == null)
            {
                StreamReader savedDataFile;
                try
                {
                    savedDataFile = new StreamReader("LastSession.dat");
                    DateTime oldDate = DateTime.Parse(savedDataFile.ReadLine());
                    DateTime curreentDate = DateTime.Now;
                    if (curreentDate.Date > oldDate.Date)
                        _locker = new Locker(Properties.Settings.Default.maxAllowedTime,
                                             Properties.Settings.Default.maxAllowedTime);
                    else
                        _locker = new Locker(TimeSpan.Parse(savedDataFile.ReadLine()),
                                             Properties.Settings.Default.maxAllowedTime);
                }
                catch (FileNotFoundException)
                {
                    _locker = new Locker(Properties.Settings.Default.maxAllowedTime, 
                                         Properties.Settings.Default.maxAllowedTime);
                }
            }

            return _locker;
        }
    }
}
