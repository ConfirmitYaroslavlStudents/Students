using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using RefreshingCacheLibrary;

namespace UnitTestProjectForRefreshingCache
{
    [TestClass]
    public class RefreshingCacheTests
    {
        //Names of tests must be refactored, naming constants should be introduced
        [TestMethod]
        public void ContainsTrue()
        {
            var myDatabase = new SlowDatabase();
            ICanGetValue<int, string> myRefreshingCache = new FastRefreshingCache<int, string>(myDatabase,1000,10);
            myRefreshingCache.GetValue(10,DateTime.Now);
            var result = myRefreshingCache.Contains(10);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ContainsFalse()
        {
            var myDatabase = new SlowDatabase();
            ICanGetValue<int, string> myRefreshingCache = new FastRefreshingCache<int, string>(myDatabase, 1000, 10);
            var stringOne = myRefreshingCache.GetValue(10,DateTime.Now);
            var result = myRefreshingCache.Contains(11);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GetAllAndContainsFirst()
        {
            var myDatabase = new SlowDatabase();
            ICanGetValue<int, string> myRefreshingCache = new FastRefreshingCache<int, string>(myDatabase, 1000, 10);
            for (int i = 0; i < 12; i++)
            {
                myRefreshingCache.GetValue(i,DateTime.Now);
            }
            var result = myRefreshingCache.Contains(0);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void WhereWasElement()
        {
            var myDatabase = new SlowDatabase();
            ICanGetValue<int, string> myRefreshingCache = new FastRefreshingCache<int, string>(myDatabase, 1000, 10);
            var before = myRefreshingCache.Contains(10);
            var currentValue = myRefreshingCache.GetValue(10,DateTime.Now);
            var after = myRefreshingCache.Contains(10);
            bool result=false;
            if (before == false && after == true)
            {
                result = true;
            }
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void StorageLife()
        {
            var myDatabase = new SlowDatabase();
            ICanGetValue<int, string> myRefreshingCache = new FastRefreshingCache<int, string>(myDatabase, 1000, 10);
            myRefreshingCache.GetValue(0, DateTime.Now);
            myRefreshingCache.GetValue(5, DateTime.Now.AddMilliseconds(1500));
            var zero = myRefreshingCache.Contains(0);
            var five = myRefreshingCache.Contains(5);
            bool result = false;
            if (zero == false && five == true)
            {
                result = true;
            }
            Assert.AreEqual(true, result);
        }
    }
}
