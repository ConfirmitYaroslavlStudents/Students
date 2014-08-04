using System;
using System.Collections.Generic;
using System.Linq;

namespace RefreshingCache
{
    public class RefreshingCache<TKey, TValue> : IComputer<TKey, TValue>
    {
        private readonly int _maxCacheSize;
        private readonly long _expirationTime;

        private class Entry
        {
            private readonly long _creationTime;
            private readonly long _expirationTime;

            public TValue Value { get; private set; }
            public long LastAccessTime { get; set; }

            public Entry(TValue value, long expirationTime, ITime time)
            {
                Value = value;
                _expirationTime = expirationTime;
                _creationTime = time.CurrentTime;
            }

            public bool IsExpired(ITime time)
            {
                return (time.CurrentTime - _creationTime >= _expirationTime);
            }
        }

        private readonly ITime _cacheTime;
        private readonly IComputer<TKey, TValue> _computer;
        private readonly Dictionary<TKey, Entry> _data;

        public TValue this[TKey key]
        {
            get
            {
                if (!_data.ContainsKey(key))
                {
                    AddData(key, _computer.GetData(key));
                }
                _data[key].LastAccessTime = _cacheTime.CurrentTime;
                return _data[key].Value;
            }
        }

        public int Count
        {
            get { return _data.Count; }
        }

        public RefreshingCache(int maxCacheSize, long expirationTime, IComputer<TKey, TValue> computer, ITime cacheTime)
        {
            _maxCacheSize = maxCacheSize;
            _expirationTime = expirationTime;

            _data = new Dictionary<TKey, Entry>();

            if (computer == null)
            {
                throw new ArgumentNullException("computer");
            }

            if (cacheTime == null)
            {
                throw new ArgumentNullException("cacheTime");
            }

            _computer = computer;
            _cacheTime = cacheTime;
        }

        private void AddData(TKey key, TValue value)
        {
            if (_data.Count == _maxCacheSize)
            {
                if (!RemoveExpirationData())
                {
                    var minKey = GetLeastRecentlyUsedKey();
                    RemoveEntry(minKey);
                }
            }
            _data.Add(key, new Entry(value, _expirationTime, _cacheTime));
        }

        private bool RemoveExpirationData()
        {
            foreach (var keyValuePair in _data)
            {
                if (keyValuePair.Value.IsExpired(_cacheTime))
                {
                    RemoveEntry(keyValuePair.Key);
                    return true;
                }
            }

            return false;
        }

        private TKey GetLeastRecentlyUsedKey()
        {
            var min = _cacheTime.CurrentTime;
            var minKey = _data.First().Key;

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

        private void RemoveEntry(TKey key)
        {
            _data.Remove(key);
        }

        public TValue GetData(TKey key)
        {
            return this[key];
        }
    }
}
