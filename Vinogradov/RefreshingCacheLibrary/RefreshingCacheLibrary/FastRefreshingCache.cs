using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace RefreshingCacheLibrary
{
    public class FastRefreshingCache<TKey, TValue> : ICanGetValue<TKey, TValue>
    {
        private int _fastCacheCapacity;
        private int _lifetimeInMilliseconds;
        private Dictionary<TKey, TValue> _fastCache;
        private Dictionary<TKey, DateTime> _additionLog;
        private ICanGetValue<TKey,TValue> _currentSlowDatabase;

        public int Count
        {
            get { return _fastCache.Count; }
        }

        public FastRefreshingCache(ICanGetValue<TKey, TValue> currentSlowDatabase, int lifetimeInMilliseconds, int fastCacheCapacity)
        {
            _fastCache = new Dictionary<TKey, TValue>();
            _additionLog = new Dictionary<TKey, DateTime>();
            _currentSlowDatabase = currentSlowDatabase;
            if (lifetimeInMilliseconds <= 0)
            {
                _lifetimeInMilliseconds = 1000;
            }
            else
            {
                _lifetimeInMilliseconds = lifetimeInMilliseconds;
            }
            if (fastCacheCapacity <= 0)
            {
                _fastCacheCapacity = 1000;
            }
            else
            {
                _fastCacheCapacity = fastCacheCapacity;
            }
        }

        public TValue GetValue(TKey key,DateTime now)
        {
            Refresh(true,now);
            _additionLog[key] = now;
            _fastCache[key] = _currentSlowDatabase.GetValue(key,now);
            return _fastCache[key];
        }

        private void Refresh(bool removeOldest, DateTime now)
        {
            DateTime timeOfCreation;
            var listOfKeys = _fastCache.Keys.ToList();
            var oldest = new DateTime(9999, 1, 1);
            TKey keyForOldest = default (TKey);
            for (int i = 0; i < listOfKeys.Count; i++)
            {
                timeOfCreation = _additionLog[listOfKeys[i]];
                timeOfCreation = timeOfCreation.AddMilliseconds(_lifetimeInMilliseconds);
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
