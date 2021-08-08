using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ToDoApiDependencies;

namespace ToDoApiTests
{
    [TestClass]
    public class FileLoaderAndSaverTests
    {
        private const string FileName = "TestToDoList.txt";
        private readonly FileLoaderAndSaver _fileLoaderAndSaver = new FileLoaderAndSaver(FileName);

        [TestInitialize]
        public void InitializeTests()
        {
            File.Delete(FileName);
        }

        [TestMethod]
        public void EmptyToDoListDoesNotSaves()
        {
            _fileLoaderAndSaver.Save(new ToDoList());

            Assert.IsFalse(File.Exists(FileName));
        }

        [TestMethod]
        public void ToDoListSavesAndLoadsCorrectly()
        {
            var toDoList = new ToDoList {new ToDoTask("wash dishes", false)};

            _fileLoaderAndSaver.Save(toDoList);
            var loadedToDoList = _fileLoaderAndSaver.Load();

            Assert.AreEqual(toDoList.ToString(), loadedToDoList.ToString());
        }
    }
}
