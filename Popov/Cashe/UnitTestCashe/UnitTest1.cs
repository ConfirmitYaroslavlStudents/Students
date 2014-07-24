
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cache;
using System.Diagnostics;

namespace UnitTestCashe
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEqualsKeys()
        {
            var temp = new Cache<int, string>(new TestClassForCache());
            Assert.AreEqual(temp[1],"1 HZ");
                
        }

        [TestMethod]
        public void TestSpeed()
        {
            var testCache = new Cache<int, string>(new TestClassForCache());
            var timewatch = new Stopwatch();
            string temp = null;
            timewatch.Start();
            for (var i = 0; i < 10; ++i)
            {
                temp = testCache[i];
            }
            var firstTime = timewatch.Elapsed;
            timewatch.Restart();
            for (var i = 0; i < 10; ++i)
            {
                temp = testCache[i];
            }
            var secondTime = timewatch.Elapsed;
            Assert.AreEqual(firstTime.CompareTo(secondTime) == 1, true);
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
            var cache = new Cache<int, string>(new TestClassForCache());
            var temp = cache[10];
            Assert.AreEqual(cache.GetCount(),1);
        }

        [TestMethod]
        public void TestTimeLive()
        {
            var cache = new Cache<int, string>(new TestClassForCache(),10,new TimeSpan(0,0,2));
            Assert.AreEqual(cache.TimeLive, new TimeSpan(0,0,2));
        }
        

    }
}
