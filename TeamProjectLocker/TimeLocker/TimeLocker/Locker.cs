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

        private const int DEFAULT_TIMEOUT = 7200;
        private RemainingTimeController _timeController;

        public Locker()
        {
            _timeController = new RemainingTimeController(DEFAULT_TIMEOUT);
            _timeController.TimeOut += Lock;
        }

        public int GetRemainingTime()
        {
            return _timeController.RemaningSecondsToLock;
        }

        private void Lock(object o, EventArgs e)
        {
            LockWorkStation();
        }
    }
}
