using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace RefreshingCache
{
    public class RefreshingCache<TKey, TValue>
    {
        private const int MaxCacheSize = 16;
        private const int MaxTimeLimit = 500;

        private class Entry
        {
            public TValue Value { get; private set; }
            public long LastAccessTime { get; set; }

            public Entry(TValue value, TKey key, TimerCallback handler)
            {
                Value = value;
                var delayTime = new TimeSpan(0, 0, 0, 0, MaxTimeLimit);
                var intervalTime = new TimeSpan(0, 0, 0, 0, 0);
                new Timer(handler, key, delayTime, intervalTime);
            }
        }

        private readonly IComputer<TKey, TValue> _computer;
        private readonly Dictionary<TKey, Entry> _data;
        private readonly Stopwatch _mainTimer;

        public TValue this[TKey key]
        {
            get
            {
                if (!_data.ContainsKey(key))
                {
                    AddData(key, _computer.GetData(key));
                }
                _data[key].LastAccessTime = _mainTimer.ElapsedMilliseconds;
                return _data[key].Value;
            }
        }

        public int Count
        {
            get { return _data.Count; }
        }

        public RefreshingCache(IComputer<TKey, TValue> computer)
        {
            _data = new Dictionary<TKey, Entry>();

            if (computer == null)
            {
                throw new ArgumentNullException("computer");
            }
            _computer = computer;
            _mainTimer = new Stopwatch();
            _mainTimer.Start();
        }

        private void AddData(TKey key, TValue value)
        {
            if (_data.Count == MaxCacheSize)
            {
                var minKey = GetMinimalLastAccessTime();
                RemoveEntry(minKey);
            }
            _data.Add(key, new Entry(value, key, TimerOnTickHandler));
        }

        private TKey GetMinimalLastAccessTime()
        {
            var min = _data.First().Value.LastAccessTime;
            TKey minKey = _data.First().Key;

            foreach (var keyValuePair in _data)
            {
                if (min > keyValuePair.Value.LastAccessTime)
                {
                    min = keyValuePair.Value.LastAccessTime;
                    minKey = keyValuePair.Key;
                }
            }

            return minKey;
        }

        private void TimerOnTickHandler(object state)
        {
            var key = (TKey)state;
            RemoveEntry(key);
        }

        private void RemoveEntry(TKey key)
        {
            _data.Remove(key);
        }
    }
}
