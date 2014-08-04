using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cache;


namespace UnitTestCashe
{
    [TestClass]
    public class TestCache
    {
        [TestMethod]
        public void TestEqualsKeys()
        {
            var temp = new Cache<int, string>(new TestClassForCache());
            Assert.AreEqual(temp[1], "1 HZ");

        }
        
        [TestMethod]
        public void TestCapacity()
        {
            var testCache = new Cache<int, string>(new TestClassForCache());
            Assert.AreEqual(testCache.Capacity, testCache.DefaultCapacity);
        }

        [TestMethod]
        public void TestCount()
        {
            const int capacity = 10;
            var cache = new Cache<int, string>(new TestClassForCache(),capacity,TimeSpan.MaxValue);
            var temp = cache[capacity];
            Assert.AreEqual(cache.GetCount(), 1);
        }

        [TestMethod]
        public void TestTimeLive()
        {
            const int capacity = 10;
            var cache = new Cache<int, string>(new TestClassForCache(), capacity, new TimeSpan(0, 0, 2));
            Assert.AreEqual(cache.TimeLive, new TimeSpan(0, 0, 2));
        }

        [TestMethod]
        public void TestIncludeCache()
        {
            const int capacity = 10;
            var cache = new Cache<int, string>(new TestClassForCache(), capacity, TimeSpan.MaxValue);
            var temp = cache[capacity];
            for (var i = 1; i <= capacity; ++i)
            {
                temp = cache[capacity];
            }
            Assert.AreEqual((cache as IGetNumberIncludeCache).NumberIncludeCahce, capacity);
        }

        [TestMethod]
        public void TestIncludeStorage()
        {
            const int capacity = 10;
            var cache = new Cache<int, string>(new TestClassForCache(), capacity, TimeSpan.MaxValue);
            for (var i = 1; i <= capacity; ++i)
            {
                var temp = cache[i];
            }
            Assert.AreEqual((cache as IGetNumberIncludeCache).NumberIncludeStorage, capacity);
        }

        [TestMethod]
        public void TestOverflowCache()
        {
            const int capcity = 10;
            var cache = new Cache<int, string>(new TestClassForCache(), capcity, TimeSpan.MaxValue);
            var temp = cache[0];
            for (var i = 1; i <= capcity; ++i)
            {
                temp = cache[i];
            }
            Assert.IsFalse((cache as ICheckCantainsKeyInCache<int>).ContainsInCache(0));
        }

        [TestMethod]
        public void TestOldElemets()
        {
           
            const int capacity = 10;
            const int numberOldElements = 7;
            var cache = new Cache<int, string>(new TestClassForCache(), capacity, TimeSpan.MaxValue);
            for (var i = 1; i <= capacity; ++i)
            {
                var temp = cache[i];
            }
           
            (cache as IMakeElementsInCacheOld).MakeElementsOld(numberOldElements);
            Assert.AreEqual(cache.GetCount(), capacity - numberOldElements);

        }


    }
}
