using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoClient;

namespace ToDoClientTests
{
    [TestClass]
    public class MenuTests
    {
        [TestMethod]
        public async void MenuDisplaysCorrectly()
        {
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Что вы хотели бы сделать? Введите:\r\nlist - просмотреть список\r\n" +
                "add - добавить задание\r\nremove - удалить задание\r\ntext - изменить текст задания\r\n" +
                "status - изменить статус задания\r\nq - выйти\r\n", logger.Messages[0]);
        }

        [TestMethod]
        public async void TaskAddsCorrectly()
        {
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "add", "wash dishes", "list", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Введите текст задания", logger.Messages[1]);
            Assert.AreEqual("Задание добавлено", logger.Messages[2]);
            //Assert.AreEqual("1. wash dishes  [ ]\r\n", logger.Messages[4]);
        }

        [TestMethod]
        public async void TaskRemovesCorrectly()
        {
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "add", "wash dishes", "remove", "1", "list", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", logger.Messages[4]);
            Assert.AreEqual("Задание удалено", logger.Messages[5]);
            //Assert.AreEqual("Список пуст", logger.Messages[7]);
        }

        [TestMethod]
        public async void TaskTextChangesCorrectly()
        {
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "add", "wash dishes", "text", "1", "clean the room", "list", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", logger.Messages[4]);
            Assert.AreEqual("Введите текст задания", logger.Messages[5]);
            Assert.AreEqual("Текст задания изменен", logger.Messages[6]);
            //Assert.AreEqual("1. clean the room  [ ]\r\n", logger.Messages[8]);
        }

        [TestMethod]
        public async void TaskStatusChangesCorrectly()
        {
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "add", "wash dishes", "status", "1", "list", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", logger.Messages[4]);
            Assert.AreEqual("Статус задания изменен", logger.Messages[5]);
            //Assert.AreEqual("1. wash dishes  [v]\r\n", logger.Messages[7]);
        }

        //[TestMethod]
        //public async void ListSavesAndLoadsCorrectly()
        //{
        //    var logger = new FakeLogger();
        //    var reader = new FakeReader(new List<string> { "add", "wash dishes", "q" });
        //    var reader2 = new FakeReader(new List<string> {"list", "q"});
        //    var client = new FakeApiClient();
        //    var menuInputHandler = new MenuInputHandler(logger, reader, client);

        //    await menuInputHandler.HandleUsersInput();
        //    menuInputHandler = new MenuInputHandler(logger, reader2, client);
        //    await menuInputHandler.HandleUsersInput();

        //    Assert.AreEqual("1. wash dishes  [ ]\r\n", logger.Messages[5]);
        //}

        [TestMethod]
        public async void CanNotChooseNonExistentMenuItem()
        {
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "print", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Некорректная команда", logger.Messages[1]);
        }

        [TestMethod]
        public async void CanNotAddEmptyTask()
        {
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "add", "", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Нельзя добавить пустое задание", logger.Messages[2]);
        }

        [TestMethod]
        public async void CanNotInputNotIntegerInNumberChoice()
        {
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "remove", "wrong input", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Нужно ввести число", logger.Messages[2]);
        }

        [TestMethod]
        public async void CanNotChooseNonExistentTaskNumber()
        {
            var logger = new FakeLogger();
            var reader = new FakeReader(new List<string> { "remove", "1", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Задания с таким номером не существует", logger.Messages[2]);
        }
    }
}
