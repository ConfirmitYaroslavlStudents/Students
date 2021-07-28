using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ToDo;

namespace ToDoListTests
{
    [TestClass]
    public class FileLoaderSaverTests
    {
        [TestMethod]
        public void EmtyToDoListDoesNotSaves()
        {
            string fileName = "TestToDoList.txt";
            File.Delete(fileName);
            var fileLoaderSaver = new FileLoaderSaver(fileName);

            fileLoaderSaver.Save(new ToDoList());

            Assert.IsFalse(File.Exists(fileName));
        }

        [TestMethod]
        public void ToDoListSavesAndLoadsCorrectly()
        {
            string fileName = "TestToDoList.txt";
            File.Delete(fileName);
            var fileLoaderSaver = new FileLoaderSaver(fileName);
            var toDoList = new ToDoList();

            toDoList.Add(new Task("wash dishes"));
            fileLoaderSaver.Save(toDoList);
            var loadedToDoList = fileLoaderSaver.Load();

            Assert.AreEqual(toDoList.ToString(), loadedToDoList.ToString());
        }
    }
}
