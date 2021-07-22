using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDo;

namespace ToDoListTests
{
    [TestClass]
    public class ConsoleHadlerTests
    {
        [TestMethod]
        public void MenuDisplaysCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "q" });
            var consoleHandler = new ConsoleHandler(testLoaderSaver, testWriterReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Что вы хотели бы сделать? Введите:", testWriterReader.Messages[0]);
            Assert.AreEqual("list - просмотреть список", testWriterReader.Messages[1]);
            Assert.AreEqual("add - добавить задание", testWriterReader.Messages[2]);
            Assert.AreEqual("remove - удалить задание", testWriterReader.Messages[3]);
            Assert.AreEqual("text - изменить текст задания", testWriterReader.Messages[4]);
            Assert.AreEqual("status - изменить статус задания", testWriterReader.Messages[5]);
            Assert.AreEqual("q - выйти\r\n", testWriterReader.Messages[6]);
            Assert.AreEqual("", testWriterReader.Messages[7]);
        }

        [TestMethod]
        public void TaskAddsCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "add", "wash dishes", "list", "q" });
            var consoleHandler = new ConsoleHandler(testLoaderSaver, testWriterReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Введите текст задания", testWriterReader.Messages[8]);
            Assert.AreEqual("Задание добавлено\r\n", testWriterReader.Messages[10]);
            Assert.AreEqual("1. wash dishes  [ ]\r\n", testWriterReader.Messages[19]);
        }

        [TestMethod]
        public void TaskRemovesCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "add", "wash dishes", "remove", "1", "list", "q" });
            var consoleHandler = new ConsoleHandler(testLoaderSaver, testWriterReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", testWriterReader.Messages[19]);
            Assert.AreEqual("Задание удалено\r\n", testWriterReader.Messages[21]);
            Assert.AreEqual("Список пуст\r\n", testWriterReader.Messages[30]);
        }

        [TestMethod]
        public void TaskTextChangesCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "add", "wash dishes", "text", "1", "clean the room", "list", "q" });
            var consoleHandler = new ConsoleHandler(testLoaderSaver, testWriterReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", testWriterReader.Messages[19]);
            Assert.AreEqual("Введите текст задания", testWriterReader.Messages[21]);
            Assert.AreEqual("Текст задания изменен\r\n", testWriterReader.Messages[23]);
            Assert.AreEqual("1. clean the room  [ ]\r\n", testWriterReader.Messages[32]);
        }

        [TestMethod]
        public void TaskStatusChangesCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "add", "wash dishes", "status", "1", "list", "q" });
            var consoleHandler = new ConsoleHandler(testLoaderSaver, testWriterReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", testWriterReader.Messages[19]);
            Assert.AreEqual("Статус задания изменен\r\n", testWriterReader.Messages[21]);
            Assert.AreEqual("1. wash dishes  [v]\r\n", testWriterReader.Messages[30]);
        }

        [TestMethod]
        public void CanNotChooseNonExistentMenuItem()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "print", "q" });
            var consoleHandler = new ConsoleHandler(testLoaderSaver, testWriterReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Некорректная команда\r\n", testWriterReader.Messages[8]);
        }

        [TestMethod]
        public void CanNotAddEmptyTask()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "add", "", "q" });
            var consoleHandler = new ConsoleHandler(testLoaderSaver, testWriterReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Нельзя добавить пустое задание\r\n", testWriterReader.Messages[10]);
        }

        [TestMethod]
        public void CanNotInputNotIntegerInNumberChoice()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "remove", "wrong input", "q" });
            var consoleHandler = new ConsoleHandler(testLoaderSaver, testWriterReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Нужно ввести число\r\n", testWriterReader.Messages[10]);
        }

        [TestMethod]
        public void CanNotChooseNonExistentTaskNumber()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "remove", "1", "q" });
            var consoleHandler = new ConsoleHandler(testLoaderSaver, testWriterReader);

            consoleHandler.HandleUsersInput();

            Assert.AreEqual("Задания с таким номером не существует\r\n", testWriterReader.Messages[10]);
        }
    }
}
