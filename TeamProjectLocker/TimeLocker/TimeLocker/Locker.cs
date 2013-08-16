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

        //public int RemainingSecondsToLock { get; private set; }
        private RemainingTimeController _timeController;

        public Locker()
        {
            _timeController = new RemainingTimeController(30);
            _timeController.TimeOut += Lock;
        }

        private void Lock(object o, EventArgs e)
        {
            LockWorkStation();
        }
    }
}
