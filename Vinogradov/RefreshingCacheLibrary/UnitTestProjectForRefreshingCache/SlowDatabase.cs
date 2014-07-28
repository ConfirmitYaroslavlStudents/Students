using System;
using System.Collections.Generic;
using RefreshingCacheLibrary;

namespace UnitTestProjectForRefreshingCache
{
    public class SlowDatabase : ICanGetValue<int, string>
    {
        private Dictionary<int, string> slowBigCache;

        public SlowDatabase()
        {
            slowBigCache = new Dictionary<int, string>();
            slowBigCache[0] = "zero";
            slowBigCache[1] = "one";
            slowBigCache[2] = "two";
            slowBigCache[3] = "three";
            slowBigCache[4] = "four";
            slowBigCache[5] = "five";
            slowBigCache[6] = "six";
            slowBigCache[7] = "seven";
            slowBigCache[8] = "eight ";
            slowBigCache[9] = "nine";
            slowBigCache[10] = "Allons-Y";
            slowBigCache[11] = "Geronimo";
        }


        public string GetValue(int key, DateTime now)
        {
            if (slowBigCache.ContainsKey(key))
            {
                return slowBigCache[key];
            }
            else
            {
                return default(string);
            }
        }

        public bool Contains(int key)
        {
            return slowBigCache.ContainsKey(key);
        }
    }
}
