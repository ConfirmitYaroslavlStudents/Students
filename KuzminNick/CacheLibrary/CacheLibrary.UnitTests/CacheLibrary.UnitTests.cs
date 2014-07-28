using System;
using CacheLibraryWithoutTimers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CacheLibrary.UnitTests
{
    [TestClass]
    public class UnitTests
    {
        private static DataBase<string> GenerateDataBaseOfRandomStringElements(int amountOfElements)
        {
            var generatorRandomStrings = new GeneratorRandomStrings(amountOfElements);
            var dataBase = new DataBase<string>();
            dataBase.InitializeDataBase(generatorRandomStrings.StringDictionary);
            return dataBase;
        }

        [TestMethod]
        public void CacheUnitTest_CorrectnessOfAddingElementsInCache()
        {
            var dataBase = GenerateDataBaseOfRandomStringElements(12);
            var cacheWithoutTimers = new CacheWithoutTimers<string>(5, dataBase);
            var listOfIdentifiers = dataBase.GetListOfIdentifiers();
            Console.WriteLine("----------------");

            foreach (var identifier in listOfIdentifiers)
            {
                Console.WriteLine("id = '{0}', value = '{1}'",
                         identifier, cacheWithoutTimers.GetElementById(identifier));
            }
            Console.WriteLine("----------------");

            foreach (var identifier in listOfIdentifiers)
            {
                Console.WriteLine("id = '{0}', value = '{1}'",
                         identifier, cacheWithoutTimers.GetElementById(identifier));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CacheUnitTest_ExceptionOf_UncorrectChangingCapacityValueOfCache()
        {
            var dataBase = new DataBase<string>();
            var cacheWithoutTimers = new CacheWithoutTimers<string>(10, dataBase);
            cacheWithoutTimers.Capacity = 5;
        }

        [TestMethod]
        public void CacheUnitTest_CorrectnessOfAddingElementsInCache_WithCapacityEqualOne()
        {
            var dataBase = GenerateDataBaseOfRandomStringElements(12);
            var cacheWithoutTimers = new CacheWithoutTimers<string>(1, dataBase);
            var listOfIdentifiers = dataBase.GetListOfIdentifiers();
            foreach (var identifier in listOfIdentifiers)
            {
                Console.WriteLine("id = '{0}', value = '{1}'",
                    identifier, cacheWithoutTimers.GetElementById(identifier));
            }
        }
    }
}
