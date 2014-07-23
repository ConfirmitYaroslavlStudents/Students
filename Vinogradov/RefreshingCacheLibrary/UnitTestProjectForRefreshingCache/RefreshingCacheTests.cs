using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using RefreshingCacheLibrary;

namespace UnitTestProjectForRefreshingCache
{
    [TestClass]
    public class RefreshingCacheTests
    {
        [TestMethod]
        public void ContainsTrue()
        {
            var myDatabase = new SlowDatabase();
            var myRefreshingCache = new FastRefreshingCache<int, string>(myDatabase);
            var stringOne = myRefreshingCache.GetValue(0);
            var result = myRefreshingCache.Contains(0);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ContainsFalse()
        {
            var myDatabase = new SlowDatabase();
            var myRefreshingCache = new FastRefreshingCache<int, string>(myDatabase);
            var stringOne = myRefreshingCache.GetValue(0);
            var result = myRefreshingCache.Contains(1);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GetAllAndContainsFirst()
        {
            var myDatabase = new SlowDatabase();
            var myRefreshingCache = new FastRefreshingCache<int, string>(myDatabase);
            string currentValue;
            for (int i = 0; i < 12; i++)
            {
                currentValue = myRefreshingCache.GetValue(i);
            }
            var result = myRefreshingCache.Contains(0);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void MoveElement()
        {
            var myDatabase = new SlowDatabase();
            var myRefreshingCache = new FastRefreshingCache<int, string>(myDatabase);
            var currentValue = myRefreshingCache.GetValue(10); ;
            for (int i = 0; i < 99; i++)
            {
                myRefreshingCache.Refresh();
            }
            var result = myRefreshingCache.Contains(0);
            Assert.AreEqual(false, result);
        }
    }
}
