using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace TimeLocker
{
    class Locker
    {
        [DllImport("user32.dll", EntryPoint = "LockWorkStation")]
        static extern bool LockWorkStation();

        public TimeSpan MaxAllowedTime { get; private set; }

        private RemainingTimeController _timeController;
        private DataSaver _saver;

        public Locker(TimeSpan remainingTime, TimeSpan maxAllowedTime)
        {
            MaxAllowedTime = maxAllowedTime;

            _saver = new DataSaver();

            SystemEvents.SessionEnding += SaveData;

            _timeController = new RemainingTimeController(remainingTime, MaxAllowedTime);
            _timeController.TimeOut += Lock;
        }

        public Locker():this(new TimeSpan(2, 0, 0), new TimeSpan(2, 0, 0))
        {
        }

        public TimeSpan GetRemainingTime()
        {
            return _timeController.RemaningTimeToLock;
        }

        private void SaveData(object o, EventArgs e)
        {
            if (_timeController.RemaningTimeToLock < MaxAllowedTime)
                _saver.SaveSessionData(_timeController.RemaningTimeToLock);
            else
                _saver.SaveSessionData(_timeController.RemaningTimeToLock - MaxAllowedTime);
        }

        private void Lock(object o, EventArgs e)
        {
            LockWorkStation();
        }
    }
}
