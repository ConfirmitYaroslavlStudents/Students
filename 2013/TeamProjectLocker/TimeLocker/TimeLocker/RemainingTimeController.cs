using System;
using System.Timers;

namespace TimeLocker
{
    public sealed class RemainingTimeController
    {
        const double COUNTDOWNTIMER_INTERVAL = 1000;

		Timer _countdownTimer;

        public TimeSpan RemainingTimeToLock { get; private set; }

		public event ElapsedEventHandler TimeOut;

        public RemainingTimeController(TimeSpan remainingTime)
        {
            RemainingTimeToLock = CalculateRemainingTimeToLock(remainingTime);

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
                return deltaTime + Properties.Settings.Default.MaxAllowedTime;
        }

        public void StartTimer()
        {
            _countdownTimer.Start();
        }

        public void StopTimer()
        {
            _countdownTimer.Stop();
        }

        private void DecRemaningSecondsToLock(object o, ElapsedEventArgs e)
        {
            RemainingTimeToLock -= TimeSpan.FromSeconds(1);

            if (RemainingTimeToLock == TimeSpan.Zero)
                ChangeTimerEvent();
        }

        private void ChangeTimerEvent()
        {
            _countdownTimer.Elapsed -= DecRemaningSecondsToLock;
            _countdownTimer.Elapsed += TimeOut;
        }        
    }
}
