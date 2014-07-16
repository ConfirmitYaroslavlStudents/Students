using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CacheLib.UnitTests
{
    [TestClass]
    public class CacheUnitTests
    {
        [TestMethod]
        public void CacheUnitTest_AddingElementsInCache()
        {
            var cache = new Cache<string>();
            var list = new List<Element<string>>();
            for (var i = 0; i < 10; i++)
            {
                var randomString = Path.GetRandomFileName();
                var keyElement = randomString.GetHashCode();
                var randomElement = new Element<string>(keyElement, randomString);
                cache.AddItem(randomElement);
            }

            cache.AddItem(new Element<string>("Test".GetHashCode(), "Test"));
            Assert.AreEqual("Test", cache.GetItem(list, "Test".GetHashCode()).Value);
        }

        [TestMethod]
        public void CacheUnitTest_AddingElements_FromSlowCollection_InCache()
        {
            var cache = new Cache<string>();
            var list = new List<Element<string>>();
            for (var i = 0; i < 9; i++)
            {
                var randomString = Path.GetRandomFileName();
                var keyElement = randomString.GetHashCode();
                var randomElement = new Element<string>(keyElement, randomString);
                list.Add(randomElement);
            }

            cache.AddItem(new Element<string>("Test".GetHashCode(), "Test"));
            Assert.AreEqual("Test", cache.GetItem(list, "Test".GetHashCode()).Value);
        }

        [TestMethod]
        public void CacheUnitTest_AddingElements_ExceedingMaxLimitOfCache()
        {
            var cache = new Cache<string>(10);
            var list = new List<Element<string>>();
            for (var i = 0; i < 16; i++)
            {
                var randomString = Path.GetRandomFileName();
                var keyElement = randomString.GetHashCode();
                var randomElement = new Element<string>(keyElement, randomString);
                list.Add(randomElement);
            }

            foreach (var element in list)
            {
                cache.AddItem(element);
            }

            Assert.AreEqual(10, cache.CurrentSize);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CacheUnitTest_GettingElementNotIncludedInCache()
        {
            var cache = new Cache<string>(10);
            var list = new List<Element<string>>();
            for (var i = 0; i < 5; i++)
            {
                var randomString = Path.GetRandomFileName();
                var keyElement = randomString.GetHashCode();
                var randomElement = new Element<string>(keyElement, randomString);
                list.Add(randomElement);
            }

            foreach (var element in list)
            {
                cache.AddItem(element);
            }

            cache.GetItem(list, "Not Included Item".GetHashCode());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CacheUnitTest_UncorrectChangingOfCapacityProperty()
        {
            var cache = new Cache<string>(10);
            cache.Capacity = 5;
        }

        [TestMethod]
        public void CacheUnitTest_AutoRemovingLeastUsedItem_FromCache()
        {
            var cache = new Cache<string>();
            var list = new List<Element<string>>();
            for (var i = 0; i < 10; i++)
            {
                var keyElement = i.ToString().GetHashCode();
                var item = new Element<string>(keyElement, i.ToString());
                list.Add(item);
            }

            foreach (var element in list)
            {
                cache.AddItem(element);
            }

            for (var i = 0; i < 9; i++)
            {
                Console.WriteLine(i);
                cache.GetItem(list, i.ToString().GetHashCode()); // additionly, it increases 'frequenceUsage'-value of Element<T>
            }

            cache.AddItem(new Element<string>("9 will be replaced as least used item in cache".GetHashCode(), "9 will be replaced as least used item in cache"));
            Assert.AreEqual("|0|1|2|3|4|5|6|7|8|9 will be replaced as least used item in cache", cache.GetAllElementsCacheInStringFormat());
        }
    }
}
