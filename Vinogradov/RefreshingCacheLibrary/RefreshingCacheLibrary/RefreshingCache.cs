using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace RefreshingCacheLibrary
{
    public class RefreshingCache<TKey, TValue> where TKey : IComparable
    {
        private System.Timers.Timer timeLord;

        private int timeBeforeRefresh = 1000;
        private int numberOflivesBeforeItemMove = 10;
        private int maxFastCacheSize = 10;
        public Dictionary<TKey, TValue> FastCache { get; private set; }
        private Dictionary<TKey, int> livesOfItemsInFastCache;
        public Dictionary<TKey, TValue> SlowCache { get; private set; }
        //==========================================================================
        public RefreshingCache()
        {
            FastCache = new Dictionary<TKey, TValue>();
            livesOfItemsInFastCache = new Dictionary<TKey, int>();
            SlowCache = new Dictionary<TKey, TValue>();

            timeLord = new System.Timers.Timer(timeBeforeRefresh);
            timeLord.Elapsed += new ElapsedEventHandler(ReduceByOne);
            timeLord.Start();
        }
        //==========================================================================
        public void Add(TKey key, TValue value)
        {
            if (FastCache.Count == maxFastCacheSize)
            {
                MoveMostOldKey();
            }
            FastCache[key] = value;
            livesOfItemsInFastCache[key] = numberOflivesBeforeItemMove;
        }
        private void MoveMostOldKey()
        {
            var listOfKeys = FastCache.Keys.ToList();
            var tempKey = listOfKeys[0];
            var min = livesOfItemsInFastCache[tempKey];
            for (int i = 1; i < listOfKeys.Count; i++)
            {
                if (livesOfItemsInFastCache[listOfKeys[i]] < min)
                {
                    tempKey = listOfKeys[i];
                    min = livesOfItemsInFastCache[tempKey];
                }
            }
            SlowCache[tempKey] = FastCache[tempKey];
            FastCache.Remove(tempKey);
            livesOfItemsInFastCache.Remove(tempKey);
        }
        //==========================================================================
        private void ReduceByOne(object o, ElapsedEventArgs e)
        {
            var listOfKeys = FastCache.Keys.ToList();
            for (int i = 0; i < listOfKeys.Count; i++)
            {
                if (livesOfItemsInFastCache[listOfKeys[i]] > 0)
                {
                    --livesOfItemsInFastCache[listOfKeys[i]];
                }
                else
                {
                    SlowCache[listOfKeys[i]] = FastCache[listOfKeys[i]];
                    FastCache.Remove(listOfKeys[i]);
                    livesOfItemsInFastCache.Remove(listOfKeys[i]);
                }
            }

        }
        //==========================================================================
        public TValue GetValue(TKey key)
        {
            if (FastCache.ContainsKey(key))
            {
                livesOfItemsInFastCache[key] = numberOflivesBeforeItemMove;
                return FastCache[key];
            }
            else
            {
                if (SlowCache.ContainsKey(key))
                {
                    FastCache[key] = SlowCache[key];
                    livesOfItemsInFastCache[key] = numberOflivesBeforeItemMove;
                    SlowCache.Remove(key);
                    return FastCache[key];
                }
                //else
                //{
                //    throw new Exception(string.Format("not found {0}",key));
                //}
            }
        }
    }
}
