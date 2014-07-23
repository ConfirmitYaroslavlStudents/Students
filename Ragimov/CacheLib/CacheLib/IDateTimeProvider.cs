using System;

namespace CacheLib
{
    public interface IDateTimeProvider
    {
        DateTime GetTime();
    }

    public class ChangeableTime : IDateTimeProvider
    {
        public DateTime Time { get; set; }

        public ChangeableTime()
        {
            Time = DateTime.Now;
        }

        public void AddTime(int milliseconds)
        {
            Time = Time.AddMilliseconds(milliseconds);
        }

        public DateTime GetTime()
        {
            return Time;
        }
    }
}
