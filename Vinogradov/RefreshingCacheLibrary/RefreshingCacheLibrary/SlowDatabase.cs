using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefreshingCacheLibrary
{
    internal class SlowDatabase<TKey, TValue> : ICanGetValue<TKey, TValue>
    {
        private Dictionary<TKey, TValue> slowBigCache;

        public SlowDatabase()
        {
            slowBigCache = new Dictionary<TKey, TValue>();
        }

        public TValue GetValue(TKey key)
        {
            if (slowBigCache.ContainsKey(key))
            {
                return slowBigCache[key];
            }
            else
            {
                return default (TValue);
            }
        }
    }
}
