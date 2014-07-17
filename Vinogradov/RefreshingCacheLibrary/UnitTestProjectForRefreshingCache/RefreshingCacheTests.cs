using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

using RefreshingCacheLibrary;

namespace UnitTestProjectForRefreshingCache
{
    [TestClass]
    public class RefreshingCacheTests
    {
        [TestMethod]
        public void AddAndCount()
        {
            var tempRefreshingCache = new RefreshingCache<int, string>();
            tempRefreshingCache.Add(0, "hello");
            tempRefreshingCache.Add(1, "world");
            var result = tempRefreshingCache.FastCache.Keys.Count;
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void AddElevenAndCount()
        {
            var tempRefreshingCache = new RefreshingCache<int, string>();
            for (int i = 0; i < 11; i++)
            {
                tempRefreshingCache.Add(i, i.ToString() + "A");
            }
            var result = tempRefreshingCache.FastCache.Count;
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void AddSleepAddSleepCount()
        {
            var tempRefreshingCache = new RefreshingCache<int, string>();
            tempRefreshingCache.Add(0, "A");
            Thread.Sleep(5000);
            tempRefreshingCache.Add(1, "B");
            Thread.Sleep(10000);
            var result = tempRefreshingCache.FastCache.Count;
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void AddSleepAddSleepGetCount()
        {
            var tempRefreshingCache = new RefreshingCache<int, string>();
            tempRefreshingCache.Add(0, "A");
            Thread.Sleep(5000);
            tempRefreshingCache.Add(1, "B");
            Thread.Sleep(10000);
            //var valueString = tempRefreshingCache.Get(0);
            var result = tempRefreshingCache.FastCache.Count;
            Assert.AreEqual(2, result);
        }
    }
}
