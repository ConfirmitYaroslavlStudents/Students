using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ToDo;

namespace ToDoListTests
{
    [TestClass]
    public class FileWorkHandlerTests
    {
        [TestMethod]
        public void EmtyToDoListDoesNotSaves()
        {
            string fileName = "TestToDoList.txt";
            File.Delete(fileName);
            var fileWorkHandler = new FileLoaderSaver(fileName);

            fileWorkHandler.Save(new ToDoList());

            Assert.IsFalse(File.Exists(fileName));
        }

        [TestMethod]
        public void ToDoListSavesCorrectly()
        {
            string fileName = "TestToDoList.txt";
            File.Delete(fileName);
            var fileWorkHandler = new FileLoaderSaver(fileName);
            var toDoList = new ToDoList();

            toDoList.Add(new Task("wash dishes"));
            fileWorkHandler.Save(toDoList);

            Assert.AreEqual("1. wash dishes  [ ]\r\n",File.ReadAllText(fileName));
        }

        [TestMethod]
        public void ToDoListLoadsCorrectly()
        {
            string fileName = "TestToDoList.txt";
            File.Delete(fileName);
            var fileWorkHandler = new FileLoaderSaver(fileName);
            var toDoList = new ToDoList();

            toDoList.Add(new Task("wash dishes"));
            fileWorkHandler.Save(toDoList);
            var loadedToDoList = fileWorkHandler.Load();
            

            Assert.AreEqual(toDoList.ToString(),loadedToDoList.ToString());
        }
    }
}
