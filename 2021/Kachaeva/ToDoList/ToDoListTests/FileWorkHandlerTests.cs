using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using ToDoListProject;

namespace ToDoListTests
{
    [TestClass]
    public class FileWorkHandlerTests
    {
        [TestMethod]
        public void ToDoListSavesCorrectly()
        {
            string fileName = "TestToDoList.txt";
            File.Delete(fileName);
            var fileWorkHandler = new FileWorkHandler(fileName);
            var testWriterReader = new TestWriterReader(new List<string> { "2", "wash dishes", "q" });
            var controller = new Controller(fileWorkHandler, testWriterReader);
            controller.HandleUsersInput();

            Assert.AreEqual("1. wash dishes  [ ]\r\n",File.ReadAllText(fileName));
        }

        [TestMethod]
        public void ToDoListLoadsCorrectly()
        {
            string fileName = "TestToDoList.txt";
            File.Delete(fileName);
            var fileWorkHandler = new FileWorkHandler(fileName);
            var testWriterReader = new TestWriterReader(new List<string> { "2", "wash dishes", "q" });
            var controller = new Controller(fileWorkHandler, testWriterReader);
            controller.HandleUsersInput();
            testWriterReader = new TestWriterReader(new List<string> { "1", "q" });
            controller = new Controller(fileWorkHandler, testWriterReader);
            controller.HandleUsersInput();

            Assert.AreEqual("1. wash dishes  [ ]\r\n", testWriterReader.Messages[8]);
        }
    }
}
