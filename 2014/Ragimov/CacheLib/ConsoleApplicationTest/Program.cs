using System;
using CacheLib;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var storage = new PiStorage();
            var datetime = new ChangeableTime();
            var cache = new Cache<int, string>(2, 1000, 10000, storage, datetime);

            cache.Add(1, "One");
            datetime.AddTime(400);
            cache.Add(2, "Two");
            datetime.AddTime(400);
            cache.Add(3, "Three");

            Console.WriteLine("Pi" + cache[1]); //Indexer and Get will add deleted data from storage
            datetime.AddTime(0);
            Console.WriteLine("Pi" + cache.Get(2));
            datetime.AddTime(0);
            Console.WriteLine("Pi" + cache[3]);

            Console.ReadKey();
        }
    }
}
