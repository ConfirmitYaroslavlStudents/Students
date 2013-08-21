using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;

namespace TimeLocker
{
    class Locker
    {
        [DllImport("user32.dll", EntryPoint = "LockWorkStation")]
        static extern bool LockWorkStation();

        public TimeSpan MaxAllowedTime { get; private set; }

        private RemainingTimeController _timeController;
        private static Locker _instance;

        public static Locker GetInstance()
        {
            if (_instance == null)
            {
                try
                {
                    LoadDataFromFile();
                }
                catch (FileNotFoundException)
                {
                    CreateDefaultLocker();
                }
            }

            return _instance;
        }

        private static void LoadDataFromFile()
        {
            using (var savedDataFile = new StreamReader("LastSession.dat"))
            {
                DateTime oldDate = DateTime.Parse(savedDataFile.ReadLine());
                DateTime curreentDate = DateTime.Now;

                if (curreentDate.Date > oldDate.Date)
                    CreateDefaultLocker();
                else
                {
                    var remainingTime = TimeSpan.Parse(savedDataFile.ReadLine());
                    if (remainingTime < Properties.Settings.Default.maxAllowedTime)
                        _instance = new Locker(remainingTime,
                                             Properties.Settings.Default.maxAllowedTime);
                    else
                        CreateDefaultLocker();
                }
            }
        }

        private static void CreateDefaultLocker()
        {
            _instance = new Locker(Properties.Settings.Default.maxAllowedTime,
                             Properties.Settings.Default.maxAllowedTime);
        }

        private Locker(TimeSpan remainingTime, TimeSpan maxAllowedTime)
        {
            MaxAllowedTime = maxAllowedTime;

            SystemEvents.SessionSwitch += SessionSwitchEvent;
            SystemEvents.SessionEnding += SaveData;

            _timeController = new RemainingTimeController(remainingTime, MaxAllowedTime);
            _timeController.TimeOut += Lock;
        }


        public TimeSpan GetRemainingTime()
        {
            return _timeController.RemaningTimeToLock;
        }


        private void SaveData(object o, EventArgs e)
        {
            if (_timeController.RemaningTimeToLock < MaxAllowedTime)
                DataSaver.SaveSessionData(_timeController.RemaningTimeToLock);
            else
                DataSaver.SaveSessionData(_timeController.RemaningTimeToLock - MaxAllowedTime);
        }

        private void Lock(object o, EventArgs e)
        {
            LockWorkStation();
        }

        private void SessionSwitchEvent(object o, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                _timeController.StopTimer();
                return;
            }

            if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                _timeController.StartTimer();
                return;
            }
        }
    }
}
