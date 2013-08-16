using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Win32;

namespace TimeLocker
{
    class RemainingTimeController
    {
        private const double DEFAULT_INTERVAL = 1000;
        
        public event ElapsedEventHandler TimeOut;

        public int RemaningSecondsToLock { get; private set; }
        private System.Timers.Timer _countdownTimer;

        public RemainingTimeController(int secondsToLock)
        {
            RemaningSecondsToLock = secondsToLock;

            _countdownTimer = new System.Timers.Timer(DEFAULT_INTERVAL);
            _countdownTimer.Elapsed += DecRemaningSecondsToLock;
            _countdownTimer.Start();

            SystemEvents.SessionSwitch += SessionSwitchEvent;
        }

        private void DecRemaningSecondsToLock(object o, ElapsedEventArgs e)
        {
            RemaningSecondsToLock--;
            if (RemaningSecondsToLock == 0)
            {
                ChangeTimerEvent();
            }
        }

        private void ChangeTimerEvent()
        {
            _countdownTimer.Elapsed -= DecRemaningSecondsToLock;
            _countdownTimer.Elapsed += TimeOut; ;
        }

        private void SessionSwitchEvent(object o, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                _countdownTimer.Stop();
                return;
            }

            if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                _countdownTimer.Start();
                return;
            }
        }
    }
}
