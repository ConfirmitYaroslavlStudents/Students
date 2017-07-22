using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TimeMeasurer
{
    public class TimeMeasurer
    {
        private readonly Stopwatch _stopwatch;
        private readonly Dictionary<string, TimeSpan> _measurements;

        public bool Trace { get; set; }

        public TimeMeasurer(bool trace = false)
        {
            Trace = trace;
            _stopwatch = new Stopwatch();
            _measurements = new Dictionary<string, TimeSpan>();
        }

        public void Measure(Action target, string tag = "NoName")
        {
            if (!Trace)
            {
                target();
                return;
            }

            _stopwatch.Start();
            target();
            _stopwatch.Stop();

            RecordMeasurement(tag, _stopwatch.Elapsed);

            _stopwatch.Reset();
        }

        public TResult Measure<TResult>(Func<TResult> target, string tag = "NoName")
        {
            if (!Trace)
            {
                return target();
            }

            _stopwatch.Start();
            TResult result = target();
            _stopwatch.Stop();

            RecordMeasurement(tag, _stopwatch.Elapsed);

            _stopwatch.Reset();

            return result;
        }

        private void RecordMeasurement(string tag, TimeSpan time)
        {
            if (!_measurements.ContainsKey(tag))
            {
                _measurements.Add(tag, time);
            }
            else
            {
                _measurements[tag] += time;
            }
        }

        public string GetResults()
        {
            TimeSpan totalTime = new TimeSpan();
            string result = string.Empty;

            foreach (var item in _measurements)
            {
                totalTime += item.Value;
                result += $"{item.Key} - {item.Value.TotalMilliseconds} ms\n";
            }

            result += $"Total time - {totalTime.TotalMilliseconds} ms";

            return result;
        }
    }
}