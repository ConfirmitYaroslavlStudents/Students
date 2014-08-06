using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CacheLib.UnitTests
{
    [TestClass]
    public class CacheUnitTests
    {
        [TestMethod]
        public void Cache_DataAddedAndReceived_ShouldPass()
        {
            var storage = new AsteriskStorage();
            //datetime -> dateTime
            var datetime = new ChangeableTime(); //this magic number to const
            var cache = new Cache<int, string>(16,1000,10000,storage,datetime);// needs more space

            cache.Get(1);
            cache.Get(2);

            Assert.AreEqual("1",cache[1]);
            Assert.AreEqual("2",cache.Get(2));
        }

        [TestMethod]
        public void Cache_ReceivedBeforeExpired_ShouldPass()
        {
            var storage = new AsteriskStorage();
            //datetime -> dateTime
            var datetime = new ChangeableTime();//this magic number to const
            var cache = new Cache<int, string>(16,1000,10000,storage,datetime);// needs more space

            cache.Get(1);
            cache.Get(2);

            datetime.AddTime(900);

            Assert.AreEqual("1", cache[1]);
            Assert.AreEqual("2", cache.Get(2));
        }

        [TestMethod]
        public void Cache_ReceivedAfterSlideExpired_ShouldPass()
        {
            var storage = new AsteriskStorage();
            //datetime -> dateTime
            var datetime = new ChangeableTime();//this magic number to const
            var cache = new Cache<int, string>(16,1000,10000,storage,datetime);// needs more space

            cache.Get(1);
            cache.Get(2);

            datetime.AddTime(1500);
            cache.Get(1);

            cache.Get(3);

            Assert.AreEqual("1", cache[1]);
            Assert.AreEqual("2*", cache.Get(2));
        }

        [TestMethod]
        public void Cache_ReceivedAfterAbsoluteExpired_ShouldPass()
        {
            var storage = new AsteriskStorage();
            //datetime -> dateTime
            var datetime = new ChangeableTime();//this magic number to const
            var cache = new Cache<int, string>(16, 1000, 10000, storage, datetime);

            cache.Get(1);
            cache.Get(2);

            datetime.AddTime(10101);
            cache.Get(1);
            cache.Get(2);

            cache.Get(3);

            Assert.AreEqual("1*", cache[1]);
            Assert.AreEqual("2*", cache.Get(2));
        }

        [TestMethod]
        public void Cache_AddedInFull_ShouldPass()
        {
            var storage = new AsteriskStorage();
            //datetime -> dateTime
            var datetime = new ChangeableTime();//this magic number to const
            var cache = new Cache<int, string>(2,1000,10000,storage,datetime);// needs more space

            cache.Get(1);
            datetime.AddTime(300);

            cache.Get(2);

            cache.Get(3);

            Assert.AreEqual("2", cache.Get(2));
            Assert.AreEqual("3", cache[3]);
            Assert.AreEqual("1*", cache.Get(1));
        }

        [TestMethod]
        public void Cache_AddedDeletedDataFromStorage_ShouldPass()
        {
            var storage = new AsteriskStorage();
            //datetime -> dateTime
            var datetime = new ChangeableTime();//this magic number to const
            var cache = new Cache<int, string>(2,1000,10000,storage,datetime);// needs more space

            cache.Get(1);
            datetime.AddTime(400);
            cache.Get(2);
            datetime.AddTime(400);

            cache.Get(3);

            Assert.AreEqual("1*", cache[1]); //Indexer and Get will add deleted data from storage
            datetime.AddTime(0);
            Assert.AreEqual("2*", cache.Get(2));
            datetime.AddTime(0);
            Assert.AreEqual("3*", cache[3]);
        }
    }
}
