using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessorsLib
{
    public class TimeCounter
    {
        public bool Trace { get; set; }
        public Stopwatch watch;
        Dictionary<string, TimeSpan> timeData;

        public string TotalTime
        {
            get
            {
                var total = String.Empty;
                var totalTime = new TimeSpan();
                foreach (var time in timeData)
                {
                    total += time.Key + " : " + time.Value + "\n";
                    totalTime += time.Value;
                }
                total += "Total time : " + totalTime;
                return total;
            }
        }

        public TimeCounter(bool trace)
        {
            Trace = trace;
            watch = new Stopwatch();
            timeData = new Dictionary<string, TimeSpan>();
        }

        public void CountTime(Action action, string timeTag)
        {
            if (!Trace)
            {
                action();
                return;
            }

            watch.Start();
            action();
            watch.Stop();

            RecordTime(watch.Elapsed, timeTag);

            watch.Reset();
        }

        public void RecordTime(TimeSpan time, string timeTag)
        {
            if (timeData.ContainsKey(timeTag))
            {
                timeData[timeTag] += time;
            }
            else
            {
                timeData.Add(timeTag, time);
            }
        }
    }
}
