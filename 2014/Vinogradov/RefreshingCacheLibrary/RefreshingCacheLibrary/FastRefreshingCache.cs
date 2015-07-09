using System;
using System.Collections.Generic;
using System.Linq;

namespace RefreshingCacheLibrary
{
    public class FastRefreshingCache<TKey, TValue> : IDataStorage<TKey, TValue>
    {
        private const int defaultFastCacheCapacity = 10;
        private const int defaultLifetime = 1000;
        private readonly int _fastCacheCapacity;
        private readonly int _lifetime;
        private readonly Dictionary<TKey, TValue> _fastCache;
        private readonly Dictionary<TKey, int> _additionLog;
        private readonly IDataStorage<TKey,TValue> _currentSlowDatabase;
        private ITime _time;

        public int Count
        {
            get { return _fastCache.Count; }
        }

        public FastRefreshingCache(ITime time,IDataStorage<TKey, TValue> currentSlowDatabase, int lifetimeInMilliseconds, int fastCacheCapacity)
        {
            _time = time;
            _fastCache = new Dictionary<TKey, TValue>();
            _additionLog = new Dictionary<TKey, int>();
            _currentSlowDatabase = currentSlowDatabase;

            _lifetime = lifetimeInMilliseconds <= 0 ? defaultLifetime : lifetimeInMilliseconds;
            _fastCacheCapacity = fastCacheCapacity <= 0 ? defaultFastCacheCapacity : fastCacheCapacity;
        }

        public FastRefreshingCache(ITime time, IDataStorage<TKey, TValue> currentSlowDatabase):
            this(time,currentSlowDatabase,defaultLifetime,defaultFastCacheCapacity)
        { }
        public TValue GetValue(TKey key)
        {
            Refresh(true);
            _additionLog[key] = _time.CurrentTime;
            _fastCache[key] = _currentSlowDatabase.GetValue(key);
            return _fastCache[key];
        }

        private void Refresh(bool removeOldest)
        {
            int timeOfCreation;
            var oldest = _time.MaxTime;
            var listOfKeys = _fastCache.Keys.ToList();
            var now = _time.CurrentTime;

            TKey keyForOldest = default (TKey);
            for (int i = 0; i < listOfKeys.Count; i++)
            {
                timeOfCreation = _additionLog[listOfKeys[i]];
                timeOfCreation += _lifetime;
                if (timeOfCreation < now)
                {
                    _fastCache.Remove(listOfKeys[i]);
                    _additionLog.Remove(listOfKeys[i]);
                }
                else
                {
                    if (timeOfCreation < oldest)
                    {
                        keyForOldest = listOfKeys[i];
                        oldest = _additionLog[keyForOldest];
                    }
                }
            }
            if (_fastCache.Count == _fastCacheCapacity && removeOldest)
            {
                _fastCache.Remove(keyForOldest);
                _additionLog.Remove(keyForOldest);
            }
        }

        public bool Contains(TKey key)
        {
            return _fastCache.ContainsKey(key);
        }
    }
}
