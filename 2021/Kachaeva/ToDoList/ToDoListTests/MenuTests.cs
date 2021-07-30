using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDo;

namespace ToDoListTests
{
    [TestClass]
    public class MenuTests
    {
        [TestMethod]
        public void MenuDisplaysCorrectly()
        {
            var loaderAndSaver = new FakeLoaderAndSaver();
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "q" });
            var menuInputHandler = new MenuInputHandler(loaderAndSaver, logger, reader);

            menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Что вы хотели бы сделать? Введите:\r\nlist - просмотреть список\r\n" +
                "add - добавить задание\r\nremove - удалить задание\r\ntext - изменить текст задания\r\n" +
                "status - изменить статус задания\r\nq - выйти\r\n", logger.Messages[0]);
        }

        [TestMethod]
        public void TaskAddsCorrectly()
        {
            var loaderAndSaver = new FakeLoaderAndSaver();
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "add", "wash dishes", "list", "q" });
            var menuInputHandler = new MenuInputHandler(loaderAndSaver, logger, reader);

            menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Введите текст задания", logger.Messages[1]);
            Assert.AreEqual("Задание добавлено", logger.Messages[2]);
            Assert.AreEqual("1. wash dishes  [ ]\r\n", logger.Messages[4]);
        }

        [TestMethod]
        public void TaskRemovesCorrectly()
        {
            var loaderAndSaver = new FakeLoaderAndSaver();
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "add", "wash dishes", "remove", "1", "list", "q" });
            var menuInputHandler = new MenuInputHandler(loaderAndSaver, logger, reader);

            menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", logger.Messages[4]);
            Assert.AreEqual("Задание удалено", logger.Messages[5]);
            Assert.AreEqual("Список пуст", logger.Messages[7]);
        }

        [TestMethod]
        public void TaskTextChangesCorrectly()
        {
            var loaderAndSaver = new FakeLoaderAndSaver();
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "add", "wash dishes", "text", "1", "clean the room", "list", "q" });
            var menuInputHandler = new MenuInputHandler(loaderAndSaver, logger, reader);

            menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", logger.Messages[4]);
            Assert.AreEqual("Введите текст задания", logger.Messages[5]);
            Assert.AreEqual("Текст задания изменен", logger.Messages[6]);
            Assert.AreEqual("1. clean the room  [ ]\r\n", logger.Messages[8]);
        }

        [TestMethod]
        public void TaskStatusChangesCorrectly()
        {
            var loaderAndSaver = new FakeLoaderAndSaver();
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "add", "wash dishes", "status", "1", "list", "q" });
            var menuInputHandler = new MenuInputHandler(loaderAndSaver, logger, reader);

            menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", logger.Messages[4]);
            Assert.AreEqual("Статус задания изменен", logger.Messages[5]);
            Assert.AreEqual("1. wash dishes  [v]\r\n", logger.Messages[7]);
        }

        [TestMethod]
        public void ListSavesAndLoadsCorrectly()
        {
            var loaderAndSaver = new FakeLoaderAndSaver();
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "add", "wash dishes", "q" });
            var reader2 = new FakeReader(new List<string> {"list", "q"});
            var menuInputHandler = new MenuInputHandler(loaderAndSaver, logger, reader);

            menuInputHandler.HandleUsersInput();
            menuInputHandler = new MenuInputHandler(loaderAndSaver, logger, reader2);
            menuInputHandler.HandleUsersInput();

            Assert.AreEqual("1. wash dishes  [ ]\r\n", logger.Messages[5]);
        }

        [TestMethod]
        public void CanNotChooseNonExistentMenuItem()
        {
            var loaderAndSaver = new FakeLoaderAndSaver();
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "print", "q" });
            var menuInputHandler = new MenuInputHandler(loaderAndSaver, logger, reader);

            menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Некорректная команда", logger.Messages[1]);
        }

        [TestMethod]
        public void CanNotAddEmptyTask()
        {
            var loaderAndSaver = new FakeLoaderAndSaver();
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "add", "", "q" });
            var menuInputHandler = new MenuInputHandler(loaderAndSaver, logger, reader);

            menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Нельзя добавить пустое задание", logger.Messages[2]);
        }

        [TestMethod]
        public void CanNotInputNotIntegerInNumberChoice()
        {
            var loaderAndSaver = new FakeLoaderAndSaver();
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "remove", "wrong input", "q" });
            var menuInputHandler = new MenuInputHandler(loaderAndSaver, logger, reader);

            menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Нужно ввести число", logger.Messages[2]);
        }

        [TestMethod]
        public void CanNotChooseNonExistentTaskNumber()
        {
            var loaderAndSaver = new FakeLoaderAndSaver();
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "remove", "1", "q" });
            var menuInputHandler = new MenuInputHandler(loaderAndSaver, logger, reader);

            menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Задания с таким номером не существует", logger.Messages[2]);
        }
    }
}
