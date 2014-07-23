using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CacheLib.UnitTests
{
    [TestClass]
    public class CacheUnitTests
    {
        [TestMethod]
        public void Cache_DataAddedAndReceived_ShouldPass()
        {
            var storage = new PiStorage();
            var datetime = new ChangeableTime();
            var cache = new Cache<int, string>(16,1000,10000,storage,datetime);

            cache.Add(1,"One");
            cache.Add(2,"Two");

            Assert.AreEqual("One",cache[1]);
            Assert.AreEqual("Two",cache.Get(2));
        }

        [TestMethod]
        public void Cache_ReceivedBeforeExpired_ShouldPass()
        {
            var storage = new PiStorage();
            var datetime = new ChangeableTime();
            var cache = new Cache<int, string>(16,1000,10000,storage,datetime);

            cache.Add(1, "One");
            cache.Add(2, "Two");

            datetime.AddTime(900);

            Assert.AreEqual("One", cache[1]);
            Assert.AreEqual("Two", cache.Get(2));
        }

        [TestMethod]
        public void Cache_ReceivedAfterSlideExpired_ShouldPass()
        {
            var storage = new PiStorage();
            var datetime = new ChangeableTime();
            var cache = new Cache<int, string>(16,1000,10000,storage,datetime);

            cache.Add(1, "One");
            cache.Add(2, "Two");

            datetime.AddTime(1500);
            cache.Get(1);
            cache.Add(3,"Three");

            Assert.AreEqual("One", cache[1]);
            Assert.AreEqual("Pi", cache.Get(2));
        }

        [TestMethod]
        public void Cache_ReceivedAfterAbsoluteExpired_ShouldPass()
        {
            var storage = new PiStorage();
            var datetime = new ChangeableTime();
            var cache = new Cache<int, string>(16, 1000, 10000, storage, datetime);

            cache.Add(1, "One");
            cache.Add(2, "Two");

            datetime.AddTime(10101);
            cache.Get(1);
            cache.Get(2);
            cache.Add(3, "Three");

            Assert.AreEqual("Pi", cache[1]);
            Assert.AreEqual("Pi", cache.Get(2));
        }

        [TestMethod]
        public void Cache_AddedInFull_ShouldPass()
        {
            var storage = new PiStorage();
            var datetime = new ChangeableTime();
            var cache = new Cache<int, string>(2,1000,10000,storage,datetime);

            cache.Add(1, "One");
            datetime.AddTime(300);
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
            var datetime = new ChangeableTime();
            var cache = new Cache<int, string>(2,1000,10000,storage,datetime);

            cache.Add(1, "One");
            datetime.AddTime(400);
            cache.Add(2, "Two");
            datetime.AddTime(400);
            cache.Add(3, "Three");

            Assert.AreEqual("Pi", cache[1]); //Indexer and Get will add deleted data from storage
            datetime.AddTime(0);
            Assert.AreEqual("Pi", cache.Get(2));
            datetime.AddTime(0);
            Assert.AreEqual("Pi", cache[3]);
        }
    }
}
