using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace CacheLib.UnitTests
{
    [TestClass]
    public class CacheUnitTests
    {
        [TestMethod]
        public void Cache_DataAddedAndReceived_ShouldPass()
        {
            var storage = new PiStorage();
            var cache = new Cache<int, string>(16,1000,storage);

            cache.Add(1,"One");
            cache.Add(2,"Two");

            Assert.AreEqual("One",cache[1]);
            Assert.AreEqual("Two",cache.Get(2));
        }

        [TestMethod]
        public void Cache_ReceivedBeforeExpired_ShouldPass()
        {
            var storage = new PiStorage();
            var cache = new Cache<int, string>(16, 1000, storage);

            cache.Add(1, "One");
            cache.Add(2, "Two");

            Thread.Sleep(900);

            Assert.AreEqual("One", cache[1]);
            Assert.AreEqual("Two", cache.Get(2));
        }

        [TestMethod]
        public void Cache_ReceivedAfterExpired_ShouldPass()
        {
            var storage = new PiStorage();
            var cache = new Cache<int, string>(16, 1000, storage);

            cache.Add(1, "One");
            cache.Add(2, "Two");

            Thread.Sleep(1500);

            Assert.AreEqual("Pi", cache[1]);
            Assert.AreEqual("Pi", cache.Get(2));
        }

        [TestMethod]
        public void Cache_AddedInFull_ShouldPass()
        {
            var storage = new PiStorage();
            var cache = new Cache<int, string>(2, 1000, storage);

            cache.Add(1, "One");
            Thread.Sleep(10);
            cache.Add(2, "Two");

            cache.Add(3,"Three");

            Assert.AreEqual("Two", cache.Get(2));
            Assert.AreEqual("Three", cache[3]);
            Assert.AreEqual("Pi", cache.Get(1));
        }

        [TestMethod]
        public void Cache_AddedDeletedDataFromStorage_ShouldPass()
        {
            var storage = new PiStorage();
            var cache = new Cache<int, string>(2, 1000, storage);

            cache.Add(1, "One");
            Thread.Sleep(10);
            cache.Add(2, "Two");
            Thread.Sleep(10);
            cache.Add(3, "Three");

            Assert.AreEqual("Pi", cache[1]); //Indexer and Get will add deleted data from storage
            Assert.AreEqual("Pi", cache.Get(2));
            Assert.AreEqual("Pi", cache[3]);
        }
    }
}
