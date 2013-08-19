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
        private const double COUNTDOWNTIMER_INTERVAL = 1000;
        
        public event ElapsedEventHandler TimeOut;

        public TimeSpan RemaningTimeToLock { get; private set; }

        private Timer _countdownTimer;
        private TimeSpan _maxAllowedTime;

        public RemainingTimeController(TimeSpan remainingTime, TimeSpan maxAllowedTime)
        {
            _maxAllowedTime = maxAllowedTime;
            RemaningTimeToLock = CalculateRemainingTimeToLock(remainingTime);
            
            SystemEvents.SessionSwitch += SessionSwitchEvent;

            _countdownTimer = new Timer(COUNTDOWNTIMER_INTERVAL);
            _countdownTimer.Elapsed += DecRemaningSecondsToLock;
            _countdownTimer.Start();
        }

        private TimeSpan CalculateRemainingTimeToLock(TimeSpan remainingTimeToLock)
        {
            var currentDateTime = DateTime.Now;
            var deltaTime = TimeSpan.FromHours(24) - currentDateTime.TimeOfDay;

            if (deltaTime > remainingTimeToLock)
                return remainingTimeToLock;
            else
                return deltaTime + _maxAllowedTime;
        }

        private void DecRemaningSecondsToLock(object o, ElapsedEventArgs e)
        {
            RemaningTimeToLock -= TimeSpan.FromSeconds(1);
            if (RemaningTimeToLock == TimeSpan.Zero)
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
