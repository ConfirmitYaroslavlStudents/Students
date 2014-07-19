using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefreshingCacheLibrary
{
    public class FastRefreshingCache<TKey, TValue> : ICanGetValue<TKey, TValue>
    {
        private int fastCacheCapacity = 10;
        private int liveTime = 10;
        private Dictionary<TKey, TValue> fastCache;
        private Dictionary<TKey, DateTime> additionLog;
        private SlowDatabase<TKey, TValue> currentSlowDatabase;

        public FastRefreshingCache()
        {
            fastCache = new Dictionary<TKey, TValue>();
            additionLog = new Dictionary<TKey, DateTime>();
            currentSlowDatabase = new SlowDatabase<TKey, TValue>();
        }

        public TValue GetValue(TKey key)
        {
            if (!fastCache.ContainsKey(key))
            {
                if(fastCache.Count==fastCacheCapacity)
                fastCache[key] = currentSlowDatabase.GetValue(key);
            }
            additionLog[key] = DateTime.Now;
            return fastCache[key];
        }

        public void Refresh()
        {
            var now = DateTime.Now;
            var timeOfCreation = new DateTime();
            var listOfKeys = fastCache.Keys.ToList();
            for (int i = 0; i < listOfKeys.Count; i++)
            {
                timeOfCreation = additionLog[listOfKeys[i]];
                timeOfCreation = timeOfCreation.AddSeconds(liveTime);
                if (timeOfCreation < now)
                {
                    fastCache.Remove(listOfKeys[i]);
                    additionLog.Remove(listOfKeys[i]);
                }
            }
        }

        public bool Contains(TKey key)
        {
            return fastCache.ContainsKey(key);
        }
    }
}
