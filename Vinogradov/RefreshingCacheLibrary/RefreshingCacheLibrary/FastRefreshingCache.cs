using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace RefreshingCacheLibrary
{
    public class FastRefreshingCache<TKey, TValue>
    {
        private int fastCacheCapacity = 10;
        private int lifetimeInMilliseconds = 1;
        private Dictionary<TKey, TValue> fastCache;
        private Dictionary<TKey, DateTime> additionLog;
        private ICanGetValue<TKey,TValue> _currentSlowDatabase;

        public FastRefreshingCache(ICanGetValue<TKey, TValue> currentSlowDatabase)
        {
            fastCache = new Dictionary<TKey, TValue>();
            additionLog = new Dictionary<TKey, DateTime>();
            _currentSlowDatabase = currentSlowDatabase;
        }

        public TValue GetValue(TKey key)
        {
            var keyForOldest=Refresh();
            if (!fastCache.ContainsKey(key))
            {
                if (fastCache.Count == fastCacheCapacity)
                {
                    additionLog.Remove(keyForOldest);
                    fastCache.Remove(keyForOldest);
                }
            }
            additionLog[key] = DateTime.Now;
            fastCache[key] = _currentSlowDatabase.GetValue(key);
            return fastCache[key];
        }

        public TKey Refresh()
        {
            var now = DateTime.Now;
            DateTime timeOfCreation;
            var listOfKeys = fastCache.Keys.ToList();
            var oldest = new DateTime(9999, 1, 1);
            TKey keyForOldest = default (TKey);
            for (int i = 0; i < listOfKeys.Count; i++)
            {
                timeOfCreation = additionLog[listOfKeys[i]];
                timeOfCreation = timeOfCreation.AddMilliseconds(lifetimeInMilliseconds);
                if (timeOfCreation < now)
                {
                    fastCache.Remove(listOfKeys[i]);
                    additionLog.Remove(listOfKeys[i]);
                }
                else
                {
                    if (timeOfCreation < oldest)
                    {
                        keyForOldest = listOfKeys[i];
                        oldest = additionLog[keyForOldest];
                    }
                }
            }
            return keyForOldest;
        }

        public bool Contains(TKey key)
        {
            return fastCache.ContainsKey(key);
        }
    }
}
