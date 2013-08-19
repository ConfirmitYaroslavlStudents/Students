using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace TimeLocker
{
    class Locker
    {
        [DllImport("user32.dll", EntryPoint = "LockWorkStation")]
        static extern bool LockWorkStation();

        public TimeSpan MaxAllowedTime { get; private set; }

        private RemainingTimeController _timeController;

        public Locker(TimeSpan remainingTime, TimeSpan maxAllowedTime)
        {
            MaxAllowedTime = maxAllowedTime;
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

        private void Lock(object o, EventArgs e)
        {
            LockWorkStation();
        }
    }
}
