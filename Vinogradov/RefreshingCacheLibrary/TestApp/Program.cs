using System;
using RefreshingCacheLibrary;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var myDatabase = new SlowDatabase();
            ICanGetValue<int, string> myRefreshingCache = new FastRefreshingCache<int, string>(myDatabase, 1000, 10);
            var before = myRefreshingCache.Contains(10);
            var currentValue = myRefreshingCache.GetValue(10, new DateTime());
            var after = myRefreshingCache.Contains(10);
            bool result = false;
            if (before == false && after == true)
            {
                result = true;
            }
            Console.ReadLine();
        }
    }
}
