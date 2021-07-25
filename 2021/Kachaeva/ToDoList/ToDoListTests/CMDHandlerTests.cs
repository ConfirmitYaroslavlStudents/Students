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
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var input = new string[] { "add", "wash dishes", "list" };
            var CMDHandler = new CMDHandler(testLoaderSaver, testLogger, input);

            CMDHandler.HandleUsersInput();

            Assert.AreEqual("Задание добавлено", testLogger.Messages[0]);
            Assert.AreEqual("1. wash dishes  [ ]\r\n", testLogger.Messages[1]);
        }

        [TestMethod]
        public void TaskRemovesCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var input = new string[] { "add", "wash dishes", "remove", "1", "list" };
            var CMDHandler = new CMDHandler(testLoaderSaver, testLogger, input);

            CMDHandler.HandleUsersInput();

            Assert.AreEqual("Задание удалено", testLogger.Messages[1]);
            Assert.AreEqual("Список пуст", testLogger.Messages[2]);
        }

        [TestMethod]
        public void TaskTextChangesCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var input = new string[] { "add", "wash dishes", "text", "1", "clean the room", "list" };
            var CMDHandler = new CMDHandler(testLoaderSaver, testLogger, input);

            CMDHandler.HandleUsersInput();

            Assert.AreEqual("Текст задания изменен", testLogger.Messages[1]);
            Assert.AreEqual("1. clean the room  [ ]\r\n", testLogger.Messages[2]);
        }

        [TestMethod]
        public void TaskStatusChangesCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var input = new string[] { "add", "wash dishes", "status", "1", "list" };
            var CMDHandler = new CMDHandler(testLoaderSaver, testLogger, input);

            CMDHandler.HandleUsersInput();

            Assert.AreEqual("Статус задания изменен", testLogger.Messages[1]);
            Assert.AreEqual("1. wash dishes  [v]\r\n", testLogger.Messages[2]);
        }

        [TestMethod]
        public void CanNotChooseNonExistentMenuItem()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var input = new string[] { "print" };
            var CMDHandler = new CMDHandler(testLoaderSaver, testLogger, input);

            CMDHandler.HandleUsersInput();

            Assert.AreEqual("Некорректная команда", testLogger.Messages[0]);
        }

        [TestMethod]
        public void CanNotAddEmptyTask()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var input = new string[] { "add", "" };
            var CMDHandler = new CMDHandler(testLoaderSaver, testLogger, input);

            CMDHandler.HandleUsersInput();

            Assert.AreEqual("Нельзя добавить пустое задание", testLogger.Messages[0]);
        }

        [TestMethod]
        public void CanNotInputNotIntegerInNumberChoice()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var input = new string[] { "remove", "wrong input" };
            var CMDHandler = new CMDHandler(testLoaderSaver, testLogger, input);

            CMDHandler.HandleUsersInput();

            Assert.AreEqual("Нужно ввести число", testLogger.Messages[0]);
        }

        [TestMethod]
        public void CanNotChooseNonExistentTaskNumber()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var input = new string[] { "remove", "1" };
            var CMDHandler = new CMDHandler(testLoaderSaver, testLogger, input);

            CMDHandler.HandleUsersInput();

            Assert.AreEqual("Задания с таким номером не существует", testLogger.Messages[0]);
        }
    }
}
