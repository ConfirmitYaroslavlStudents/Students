using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ToDo;

namespace ToDoListTests
{
    [TestClass]
    public class FileLoaderAndSaverTests
    {
        [TestMethod]
        public void EmtyToDoListDoesNotSaves()
        {
            string fileName = "TestToDoList.txt";
            File.Delete(fileName);
            var fileLoaderAndSaver = new FileLoaderAndSaver(fileName);

            fileLoaderAndSaver.Save(new ToDoList());

            Assert.IsFalse(File.Exists(fileName));
        }

        [TestMethod]
        public void ToDoListSavesAndLoadsCorrectly()
        {
            string fileName = "TestToDoList.txt";
            File.Delete(fileName);
            var fileLoaderAndSaver = new FileLoaderAndSaver(fileName);
            var toDoList = new ToDoList();

            toDoList.Add(new Task("wash dishes"));
            fileLoaderAndSaver.Save(toDoList);
            var loadedToDoList = fileLoaderAndSaver.Load();

            Assert.AreEqual(toDoList.ToString(), loadedToDoList.ToString());
        }
    }
}
