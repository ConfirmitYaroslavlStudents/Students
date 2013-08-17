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

        private RemainingTimeController _timeController;


        public Locker(TimeSpan timout)
        {
            _timeController = new RemainingTimeController(timout);
            _timeController.TimeOut += Lock;
        }

        public Locker():this(new TimeSpan(2, 0, 0))
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
