using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CacheLibrary.UnitTests
{
    [TestClass]
    public class CacheUnitTests
    {
        private DataBase<string> InitializeDataBase(int amountOfElements)
        {
            var generatorRandomStrings = new DataBaseGenerator(amountOfElements);
            var dataBase = new DataBase<string>(generatorRandomStrings.StringDictionary);
            return dataBase;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Capacity_DecreaseValueOfCacheCapacity_ExceptionThrown()
        {
            var dataBase = new DataBase<string>();
            var cache = new CacheBuilder<string>().Build(capacity: 10, slowDataBase: dataBase);
            cache.Capacity = 5;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Capacity_NegativeValue_ExceptionThrown()
        {
            var dataBase = new DataBase<string>();
            var cache = new CacheBuilder<string>().Build(capacity: 10, slowDataBase: dataBase);
            cache.Capacity = -15;
        }

        [TestMethod]
        public void TypesOfStorage_ElementsAreProperlyObtainedFromCacheAndDataBase()
        {
            var dataBase = InitializeDataBase(amountOfElements: 5);
            var cache = new CacheBuilder<string>().Build(capacity: 3, timeOfExpirationInTicks: 1000, slowDataBase: dataBase);
            var listOfId = dataBase.GetListOfId();

            for (var i = 0; i < 5; i++)
                cache.GetElementById(listOfId[i]);

            for (var i = 2; i >= 0; i--)
                cache.GetElementById(listOfId[i]);

            var listOfElementsInCache = cache.GetListOfElementsLocatedInCache();

            const int amountOfElementsInCache = 1;
            Assert.AreEqual(amountOfElementsInCache, listOfElementsInCache.Count);
            const string valueOfElementInCache = "two";
            Assert.AreEqual(valueOfElementInCache, listOfElementsInCache[0].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_NegativeValueOfTimeLimitStoringInCache_ExceptionThrown()
        {
            var dataBase = new DataBase<string>();
            const int negativeValueOfTimeLimitStoringInCache = -5;
            new CacheBuilder<string>().Build(capacity: 10, timeOfExpirationInTicks: negativeValueOfTimeLimitStoringInCache, slowDataBase: dataBase);
        }
    }
}
