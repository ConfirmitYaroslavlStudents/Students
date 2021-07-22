using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using ToDo;

namespace ToDoListTests
{
    [TestClass]
    public class CMDHandlerTests
    {
        [TestMethod]
        public void TaskAddsCorrectly()
        {
            var fileName = "TestToDoList.txt";
            File.Delete(fileName);
            var fileWorkHandler = new FileWorkHandler(fileName);
            var testWriterReader1 = new TestWriterReader();
            var testWriterReader2 = new TestWriterReader();
            var input1 = new string[] { "add", "wash dishes" };
            var input2 = new string[] { "list" };
            var CMDHandler1 = new CMDHandler(fileWorkHandler, testWriterReader1, input1);

            CMDHandler1.HandleUsersInput();
            var CMDHandler2 = new CMDHandler(fileWorkHandler, testWriterReader2, input2);
            CMDHandler2.HandleUsersInput();

            Assert.AreEqual("Задание добавлено\r\n", testWriterReader1.Messages[0]);
            Assert.AreEqual("1. wash dishes  [ ]\r\n", testWriterReader2.Messages[0]);
        }

        [TestMethod]
        public void TaskRemovesCorrectly()
        {
            var fileName = "TestToDoList.txt";
            File.Delete(fileName);
            var fileWorkHandler = new FileWorkHandler(fileName);
            var testWriterReader1 = new TestWriterReader();
            var testWriterReader2 = new TestWriterReader();
            var testWriterReader3 = new TestWriterReader();
            var input1 = new string[] { "add", "wash dishes" };
            var input2 = new string[] { "remove", "1" };
            var input3 = new string[] { "list" };
            var CMDHandler1 = new CMDHandler(fileWorkHandler, testWriterReader1, input1);
            
            CMDHandler1.HandleUsersInput();
            var CMDHandler2 = new CMDHandler(fileWorkHandler, testWriterReader2, input2);
            CMDHandler2.HandleUsersInput();
            var CMDHandler3 = new CMDHandler(fileWorkHandler, testWriterReader3, input3);
            CMDHandler3.HandleUsersInput();

            Assert.AreEqual("Задание удалено\r\n", testWriterReader2.Messages[0]);
            Assert.AreEqual("Список пуст\r\n", testWriterReader3.Messages[0]);
        }

        [TestMethod]
        public void TaskTextChangesCorrectly()
        {
            var fileName = "TestToDoList.txt";
            File.Delete(fileName);
            var fileWorkHandler = new FileWorkHandler(fileName);
            var testWriterReader1 = new TestWriterReader();
            var testWriterReader2 = new TestWriterReader();
            var testWriterReader3 = new TestWriterReader();
            var input1 = new string[] { "add", "wash dishes" };
            var input2 = new string[] { "text", "1", "clean the room" };
            var input3 = new string[] { "list" };
            var CMDHandler1 = new CMDHandler(fileWorkHandler, testWriterReader1, input1);

            CMDHandler1.HandleUsersInput();
            var CMDHandler2 = new CMDHandler(fileWorkHandler, testWriterReader2, input2);
            CMDHandler2.HandleUsersInput();
            var CMDHandler3 = new CMDHandler(fileWorkHandler, testWriterReader3, input3);
            CMDHandler3.HandleUsersInput();

            Assert.AreEqual("Текст задания изменен\r\n", testWriterReader2.Messages[0]);
            Assert.AreEqual("1. clean the room  [ ]\r\n", testWriterReader3.Messages[0]);
        }

        [TestMethod]
        public void TaskStatusChangesCorrectly()
        {
            var fileName = "TestToDoList.txt";
            File.Delete(fileName);
            var fileWorkHandler = new FileWorkHandler(fileName);
            var testWriterReader1 = new TestWriterReader();
            var testWriterReader2 = new TestWriterReader();
            var testWriterReader3 = new TestWriterReader();
            var input1 = new string[] { "add", "wash dishes" };
            var input2 = new string[] { "status", "1" };
            var input3 = new string[] { "list" };
            var CMDHandler1 = new CMDHandler(fileWorkHandler, testWriterReader1, input1);
            
            CMDHandler1.HandleUsersInput();
            var CMDHandler2 = new CMDHandler(fileWorkHandler, testWriterReader2, input2);
            CMDHandler2.HandleUsersInput();
            var CMDHandler3 = new CMDHandler(fileWorkHandler, testWriterReader3, input3);
            CMDHandler3.HandleUsersInput();

            Assert.AreEqual("Статус задания изменен\r\n", testWriterReader2.Messages[0]);
            Assert.AreEqual("1. wash dishes  [v]\r\n", testWriterReader3.Messages[0]);
        }

        [TestMethod]
        public void CanNotChooseNonExistentMenuItem()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader();
            var input = new string[] { "print" };
            var CMDHandler = new CMDHandler(testLoaderSaver, testWriterReader, input);

            CMDHandler.HandleUsersInput();

            Assert.AreEqual("Некорректная команда\r\n", testWriterReader.Messages[0]);
        }

        [TestMethod]
        public void CanNotAddEmptyTask()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader();
            var input = new string[] { "add", "" };
            var CMDHandler = new CMDHandler(testLoaderSaver, testWriterReader, input);

            CMDHandler.HandleUsersInput();

            Assert.AreEqual("Нельзя добавить пустое задание\r\n", testWriterReader.Messages[0]);
        }

        [TestMethod]
        public void CanNotInputNotIntegerInNumberChoice()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader();
            var input = new string[] { "remove", "wrong input" };
            var CMDHandler = new CMDHandler(testLoaderSaver, testWriterReader, input);

            CMDHandler.HandleUsersInput();

            Assert.AreEqual("Нужно ввести число\r\n", testWriterReader.Messages[0]);
        }

        [TestMethod]
        public void CanNotChooseNonExistentTaskNumber()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader();
            var input = new string[] { "remove", "1" };
            var CMDHandler = new CMDHandler(testLoaderSaver, testWriterReader, input);

            CMDHandler.HandleUsersInput();

            Assert.AreEqual("Задания с таким номером не существует\r\n", testWriterReader.Messages[0]);
        }
    }
}
