using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefreshingCacheLibrary;

namespace UnitTestProjectForRefreshingCache
{
    [TestClass]
    public class RefreshingCacheTests
    {
        [TestMethod]
        public void ContainsElement_True()
        {
            var myDatabase = new SlowDatabase();
            var alphaMachine = new TheTimeMachine();
            IDataStorage<int, string> myRefreshingCache = new FastRefreshingCache<int, string>(alphaMachine, myDatabase);
            myRefreshingCache.GetValue(10);
            var result = myRefreshingCache.Contains(10);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ContainsElement_False()
        {
            var myDatabase = new SlowDatabase();
            var alphaMachine = new TheTimeMachine();
            IDataStorage<int, string> myRefreshingCache = new FastRefreshingCache<int, string>(alphaMachine, myDatabase);
            myRefreshingCache.GetValue(10);
            var result = myRefreshingCache.Contains(11);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ContainsFirstAfterGetingAll_False()
        {
            var myDatabase = new SlowDatabase();
            var alphaMachine = new TheTimeMachine();
            IDataStorage<int, string> myRefreshingCache = new FastRefreshingCache<int, string>(alphaMachine, myDatabase);
            for (int i = 0; i < 12; i++)
            {
                myRefreshingCache.GetValue(i);
            }
            var result = myRefreshingCache.Contains(0);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void VerifyTheLocationOfTheRequestedItem_ElementIsMovedToTheCache()
        {
            var myDatabase = new SlowDatabase();
            var alphaMachine = new TheTimeMachine();
            IDataStorage<int, string> myRefreshingCache = new FastRefreshingCache<int, string>(alphaMachine, myDatabase);
            var before = myRefreshingCache.Contains(10);
            myRefreshingCache.GetValue(10);
            var after = myRefreshingCache.Contains(10);
            Assert.AreEqual(true, !before && after);
        }

        [TestMethod]
        public void ItemShouldBeRemovedAtTheExpirationOfStandardPeriod()
        {
            var myDatabase = new SlowDatabase();
            var alphaMachine = new TheTimeMachine();
            IDataStorage<int, string> myRefreshingCache = new FastRefreshingCache<int, string>(alphaMachine, myDatabase);
            myRefreshingCache.GetValue(0);
            alphaMachine.ChangeTo(1500);
            myRefreshingCache.GetValue(5);
            var zero = myRefreshingCache.Contains(0);
            var five = myRefreshingCache.Contains(5);
            Assert.AreEqual(true, !zero && five);
        }
    }
}
