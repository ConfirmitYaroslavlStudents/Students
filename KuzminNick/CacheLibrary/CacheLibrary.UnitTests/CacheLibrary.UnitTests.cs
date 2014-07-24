using System;
using System.Security.Policy;
using CacheLibraryWithoutTimers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CacheLibrary.UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void CacheLibraryWithoutTimers_CorrectnessGettingFromDataBaseAndAddingInCache()
        {
            var generatorRandomStrings = new GeneratorRandomStrings(20);
            Assert.AreEqual(20, generatorRandomStrings.GetCount());

            var dataBase = new DataBase<string>();
            dataBase.InitializeDataBase(generatorRandomStrings.StringDictionary);

            var cacheWithoutTimers = new CacheWithoutTimers<string>(dataBase);
            var listOfIdentifiers = dataBase.GetListOfIdentifiers();
            var typeStorageForCurrentElement = String.Empty;
            foreach (var identifier in listOfIdentifiers)
            {
                var element = cacheWithoutTimers.GetItemByIdentifier(identifier, ref typeStorageForCurrentElement);
                Console.WriteLine("{0} - {1}", element, typeStorageForCurrentElement);
            }

            dataBase.Clear();
            var identifierTestElement = "Test".GetHashCode().ToString();
            var testElement = new Element<string>(identifierTestElement, "Test");
            dataBase.AddItem(testElement);

            var testElementGettingFromDataBase = cacheWithoutTimers.GetItemByIdentifier(identifierTestElement, ref typeStorageForCurrentElement);
            Console.WriteLine("{0} - {1}", testElementGettingFromDataBase, typeStorageForCurrentElement);
        }
    }
}
