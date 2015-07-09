using System;
using System.Collections.Generic;
using System.Linq;

namespace RefreshingCache
{
    public class RefreshingCache<TKey, TValue> : IDataStorage<TKey, TValue>
    {
        private readonly ITime _cacheTime;
        private readonly IDataStorage<TKey, TValue> _dataStorage;
        private readonly Dictionary<TKey, Entry> _data;
        private readonly int _maxCacheSize;

        public class Entry
        {
            private readonly long _creationTime;

            // ReSharper disable once StaticFieldInGenericType
            public static long ExpirationTime;

            public TValue Value { get; private set; }
            public long LastAccessTime { get; set; }

            public Entry(TValue value, ITime time)
            {
                Value = value;
                _creationTime = time.CurrentTime;
            }

            public bool IsExpired(ITime time)
            {
                return (time.CurrentTime - _creationTime >= ExpirationTime);
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                if (!_data.ContainsKey(key))
                {
                    AddData(key, _dataStorage.GetData(key));
                }
                _data[key].LastAccessTime = _cacheTime.CurrentTime;
                return _data[key].Value;
            }
        }

        public int Count
        {
            get { return _data.Count; }
        }

        public RefreshingCache(int maxCacheSize, long expirationTime, IDataStorage<TKey, TValue> dataStorage, ITime cacheTime)
        {
            _maxCacheSize = maxCacheSize;
            Entry.ExpirationTime = expirationTime;

            _data = new Dictionary<TKey, Entry>();

            if (dataStorage == null)
            {
                throw new ArgumentNullException("dataStorage");
            }

            if (cacheTime == null)
            {
                throw new ArgumentNullException("cacheTime");
            }

            _dataStorage = dataStorage;
            _cacheTime = cacheTime;
        }
        
        public TValue GetData(TKey key)
        {
            return this[key];
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
            _data.Add(key, new Entry(value, _cacheTime));
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
    }
}
