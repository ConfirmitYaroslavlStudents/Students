using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDo;

namespace ToDoListTests
{
    [TestClass]
    public class MenuCommandHadlerTests
    {
        [TestMethod]
        public void MenuDisplaysCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var testReader = new TestReader(new List<string> { "q" });
            var consoleHandler = new MenuCommandHandler(testLoaderSaver, testLogger, testReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Что вы хотели бы сделать? Введите:\r\nlist - просмотреть список\r\n" +
                "add - добавить задание\r\nremove - удалить задание\r\ntext - изменить текст задания\r\n" +
                "status - изменить статус задания\r\nq - выйти\r\n", testLogger.Messages[0]);
        }

        [TestMethod]
        public void TaskAddsCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var testReader = new TestReader(new List<string> { "add", "wash dishes", "list", "q" });
            var consoleHandler = new MenuCommandHandler(testLoaderSaver, testLogger, testReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Введите текст задания", testLogger.Messages[1]);
            Assert.AreEqual("Задание добавлено", testLogger.Messages[2]);
            Assert.AreEqual("1. wash dishes  [ ]\r\n", testLogger.Messages[4]);
        }

        [TestMethod]
        public void TaskRemovesCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var testReader = new TestReader(new List<string> { "add", "wash dishes", "remove", "1", "list", "q" });
            var consoleHandler = new MenuCommandHandler(testLoaderSaver, testLogger, testReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", testLogger.Messages[4]);
            Assert.AreEqual("Задание удалено", testLogger.Messages[5]);
            Assert.AreEqual("Список пуст", testLogger.Messages[7]);
        }

        [TestMethod]
        public void TaskTextChangesCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var testReader = new TestReader(new List<string> { "add", "wash dishes", "text", "1", "clean the room", "list", "q" });
            var consoleHandler = new MenuCommandHandler(testLoaderSaver, testLogger, testReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", testLogger.Messages[4]);
            Assert.AreEqual("Введите текст задания", testLogger.Messages[5]);
            Assert.AreEqual("Текст задания изменен", testLogger.Messages[6]);
            Assert.AreEqual("1. clean the room  [ ]\r\n", testLogger.Messages[8]);
        }

        [TestMethod]
        public void TaskStatusChangesCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var testReader = new TestReader(new List<string> { "add", "wash dishes", "status", "1", "list", "q" });
            var consoleHandler = new MenuCommandHandler(testLoaderSaver, testLogger, testReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", testLogger.Messages[4]);
            Assert.AreEqual("Статус задания изменен", testLogger.Messages[5]);
            Assert.AreEqual("1. wash dishes  [v]\r\n", testLogger.Messages[7]);
        }

        [TestMethod]
        public void CanNotChooseNonExistentMenuItem()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var testReader = new TestReader(new List<string> { "print", "q" });
            var consoleHandler = new MenuCommandHandler(testLoaderSaver, testLogger, testReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Некорректная команда", testLogger.Messages[1]);
        }

        [TestMethod]
        public void CanNotAddEmptyTask()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var testReader = new TestReader(new List<string> { "add", "", "q" });
            var consoleHandler = new MenuCommandHandler(testLoaderSaver, testLogger, testReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Нельзя добавить пустое задание", testLogger.Messages[2]);
        }

        [TestMethod]
        public void CanNotInputNotIntegerInNumberChoice()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var testReader = new TestReader(new List<string> { "remove", "wrong input", "q" });
            var consoleHandler = new MenuCommandHandler(testLoaderSaver, testLogger, testReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Нужно ввести число", testLogger.Messages[2]);
        }

        [TestMethod]
        public void CanNotChooseNonExistentTaskNumber()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testLogger = new TestLogger();
            var testReader = new TestReader(new List<string> { "remove", "1", "q" });
            var consoleHandler = new MenuCommandHandler(testLoaderSaver, testLogger, testReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Задания с таким номером не существует", testLogger.Messages[2]);
        }
    }
}
