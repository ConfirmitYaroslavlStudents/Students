using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TimeLocker
{
    class RemainingTimeController
    {
        private const double DEFAULT_INTERVAL = 1000;

        public event EventHandler TimeOut;

        private int _remaningSecondsToLock;
        private Timer _countdownTimer;

        public RemainingTimeController(int secondsToLock)
        {
            _remaningSecondsToLock = secondsToLock;

            _countdownTimer = new Timer(DEFAULT_INTERVAL);
            _countdownTimer.Elapsed += DecRemaningSecondsToLock;
            _countdownTimer.Start();
        }

        private void DecRemaningSecondsToLock(object o, ElapsedEventArgs e)
        {
            _remaningSecondsToLock--;
            if (_remaningSecondsToLock == 0)
            {
                _countdownTimer.Stop();
                if (TimeOut != null)
                    TimeOut(new object(), new EventArgs());
            }
        }
    }
}
