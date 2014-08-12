using System;
using System.Collections.Generic;
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
            var temp = new Cache<int, string>(new TestClasses(), new TimeForCache());
            Assert.AreEqual(temp[1], "1");
        }
        
        [TestMethod]
        public void TestCapacity()
        {
            var testCache = new Cache<int, string>(new TestClasses(), new TimeForCache());
            Assert.AreEqual(testCache.Capacity, testCache.DefaultCapacity);
        }

        [TestMethod]
        public void TestCount()
        {
            const int capacity = 10;
            var cache = new Cache<int, string>(new TestClasses(), new TimeForCache(), capacity);
            var temp = cache[capacity];
            Assert.AreEqual(cache.GetCount(), 1);
        }

        [TestMethod]
        public void TestTimeLive()
        {
            const int capacity = 10;
            var cache = new Cache<int, string>(new TestClasses(), new TimeForCache(), capacity);
            Assert.AreEqual(cache.TimeLive, new TimeForCache().TimeLive);
        }

        [TestMethod]
        public void TestIncludeCache()
        {
            const int capacity = 10;
            var cache = new Cache<int, string>(new TestClasses(), new TimeForCache(), capacity);
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
            var cache = new Cache<int, string>(new TestClasses(), new TimeForCache(), capacity);
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
            var cache = new Cache<int, string>(new TestClasses(), new TimeForCacheOverflowElement(), capcity);
            var temp = cache[0];
            for (var i = 1; i <= capcity; ++i)
            {
                temp = cache[i];
            }
            Assert.IsFalse(cache.ContainsInCache(0));
        }

        [TestMethod]
        public void TestOldElemets()
        {
           
            const int capacity = 10;
            
            var cache = new Cache<int, string>(new TestClasses(), new TimeForCacheOldElement(), capacity);
            for (var i = 1; i <= capacity; ++i)
            {
                var temp = cache[i];
            }
           
            Assert.AreEqual(cache.GetCount(), 0);

        }


    }
}
