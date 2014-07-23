using System;
using RefreshingCacheLibrary;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var myDatabase = new SlowDatabase();
            var myRefreshingCache = new FastRefreshingCache<int, string>(myDatabase);
            string currentValue;
            for (int i = 0; i < 12; i++)
            {
                currentValue = myRefreshingCache.GetValue(i);
            }
            var result = myRefreshingCache.Contains(0);
            Console.ReadLine();
        }
    }
}
