using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CacheLibrary.UnitTests
{
    [TestClass]
    public class CacheUnitTests
    {
        private static DataBase<string> GenerateDataBaseOfRandomStringElements(int amountOfElements)
        {
            var generatorRandomStrings = new GeneratorRandomStrings(amountOfElements);
            var dataBase = new DataBase<string>();
            dataBase.InitializeDataBase(generatorRandomStrings.StringDictionary);
            return dataBase;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Capacity_DecreaseValueOfCacheCapacity_ExceptionThrow()
        {
            var dataBase = new DataBase<string>();
            var cache = new Cache<string>(10, dataBase);
            cache.Capacity = 5;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Capacity_NegativeValue_ExceptionThrow()
        {
            var dataBase = new DataBase<string>();
            var cache = new Cache<string>(10, dataBase);
            cache.Capacity = -15;
        }
        //This test is large and hard for reading and have so many code duplications
        [TestMethod]
        public void TypesOfStorage_CorrectGettingElementsFromCacheAndDataBase()
        {
            var dataBase = GenerateDataBaseOfRandomStringElements(3);
            var cache = new Cache<string>(2, dataBase);
            var listOfId = dataBase.GetListOfId();
            var elements = new List<Element<string>>();

            foreach (var identifier in listOfId)
                elements.Add(cache.GetElementById(identifier));

            var expectedTypeStorageOfFirstElement = TypesOfStorage.DataBase;
            var expectedTypeStorageOfSecondElement = TypesOfStorage.DataBase;
            var expectedTypeStorageOfThirdElement = TypesOfStorage.DataBase;

            var actualTypeStorageOfFirstElement = elements[0].TypeOfStorage;
            var actualTypeStorageOfSecondElement = elements[1].TypeOfStorage;
            var actualTypeStorageOfThirdElement = elements[2].TypeOfStorage;

            Assert.AreEqual(expectedTypeStorageOfFirstElement, actualTypeStorageOfFirstElement, "First Element From Data Base");
            Assert.AreEqual(expectedTypeStorageOfSecondElement, actualTypeStorageOfSecondElement, "Second Element From Data Base");
            Assert.AreEqual(expectedTypeStorageOfThirdElement, actualTypeStorageOfThirdElement, "Third Element From Data Base");

            RepeatedRequestToLastTwoElements(cache, listOfId);

            expectedTypeStorageOfFirstElement = TypesOfStorage.DataBase;
            expectedTypeStorageOfSecondElement = TypesOfStorage.Cache;
            expectedTypeStorageOfThirdElement = TypesOfStorage.Cache;

            actualTypeStorageOfFirstElement = elements[0].TypeOfStorage;
            actualTypeStorageOfSecondElement = elements[1].TypeOfStorage;
            actualTypeStorageOfThirdElement = elements[2].TypeOfStorage;

            Assert.AreEqual(expectedTypeStorageOfFirstElement, actualTypeStorageOfFirstElement, "First Element From Data Base");
            Assert.AreEqual(expectedTypeStorageOfSecondElement, actualTypeStorageOfSecondElement, "Second Element From Cache");
            Assert.AreEqual(expectedTypeStorageOfThirdElement, actualTypeStorageOfThirdElement, "Third Element From Cache");
        }

        private static void RepeatedRequestToLastTwoElements(Cache<string> cache, List<string> listOfId)
        {
            //Why do we need this variable?
            var someInformationAboutSecondElement = cache.GetElementById(listOfId[1]);
            var someInformationAboutThirdElement = cache.GetElementById(listOfId[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_NegativeValueOfTimeLimitStoringInCache_ExceptionThrow()
        {
            var dataBase = new DataBase<string>();
            var negativeValueOfTimeLimitStoringInCache = -5; //Should convert to constant
            var cache = new Cache<string>(10, negativeValueOfTimeLimitStoringInCache, dataBase);
        }
    }
}
