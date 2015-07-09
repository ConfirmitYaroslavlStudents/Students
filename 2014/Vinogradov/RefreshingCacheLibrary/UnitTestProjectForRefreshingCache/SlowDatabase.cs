using System.Collections.Generic;
using RefreshingCacheLibrary;

namespace UnitTestProjectForRefreshingCache
{
    public class SlowDatabase : IDataStorage<int, string>
    {
        private readonly Dictionary<int, string> _slowBigCache;

        public SlowDatabase()
        {
            _slowBigCache = new Dictionary<int, string>();
            _slowBigCache[0] = "zero";
            _slowBigCache[1] = "one";
            _slowBigCache[2] = "two";
            _slowBigCache[3] = "three";
            _slowBigCache[4] = "four";
            _slowBigCache[5] = "five";
            _slowBigCache[6] = "six";
            _slowBigCache[7] = "seven";
            _slowBigCache[8] = "eight ";
            _slowBigCache[9] = "nine";
            _slowBigCache[10] = "Allons-Y";
            _slowBigCache[11] = "Geronimo";
        }


        public string GetValue(int key)
        {
            return _slowBigCache.ContainsKey(key) ? _slowBigCache[key] : default(string);
        }

        public bool Contains(int key)
        {
            return _slowBigCache.ContainsKey(key);
        }
    }
}
