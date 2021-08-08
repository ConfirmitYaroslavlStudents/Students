using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoClient;
using System.Threading.Tasks;

namespace ClientTests
{
    [TestClass]
    public class MenuTests
    {
        [TestMethod]
        public async Task MenuDisplaysCorrectly()
        {
            var logger = new FakeToDoLogger();
            var reader = new FakeToDoReader(new List<string> { "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Что вы хотели бы сделать? Введите:\r\nlist - просмотреть список\r\n" +
                "add - добавить задание\r\nremove - удалить задание\r\ntext - обновить текст задания\r\n" +
                "status - обновить статус задания\r\nq - выйти\r\n", logger.Messages[0]);
        }

        [TestMethod]
        public async Task TaskAddsCorrectly()
        {
            var logger = new FakeToDoLogger();
            var reader = new FakeToDoReader(new List<string> { "add", "wash dishes", "false", "list", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Введите текст задания", logger.Messages[1]);
            Assert.AreEqual("Введите статус задания", logger.Messages[2]);
            Assert.AreEqual("Задание добавлено", logger.Messages[3]);
        }

        [TestMethod]
        public async Task TaskRemovesCorrectly()
        {
            var logger = new FakeToDoLogger();
            var reader = new FakeToDoReader(new List<string> { "add", "wash dishes", "false", "remove", "1", "list", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", logger.Messages[5]);
            Assert.AreEqual("Задание удалено", logger.Messages[6]);
        }

        [TestMethod]
        public async Task TaskTextChangesCorrectly()
        {
            var logger = new FakeToDoLogger();
            var reader = new FakeToDoReader(new List<string> { "add", "wash dishes", "false", "text", "1", "clean the room", "list", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", logger.Messages[5]);
            Assert.AreEqual("Введите текст задания", logger.Messages[6]);
            Assert.AreEqual("Текст задания обновлен", logger.Messages[7]);
        }

        [TestMethod]
        public async Task TaskStatusChangesCorrectly()
        {
            var logger = new FakeToDoLogger();
            var reader = new FakeToDoReader(new List<string> { "add", "wash dishes", "false", "status", "1", "true", "list", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", logger.Messages[5]);
            Assert.AreEqual("Введите статус задания", logger.Messages[6]);
            Assert.AreEqual("Статус задания обновлен", logger.Messages[7]);
        }

        [TestMethod]
        public async Task CanNotChooseNonExistentMenuItem()
        {
            var logger = new FakeToDoLogger();
            var reader = new FakeToDoReader(new List<string> { "print", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Некорректная команда", logger.Messages[1]);
        }

        [TestMethod]
        public async Task CanNotAddEmptyTask()
        {
            var logger = new FakeToDoLogger();
            var reader = new FakeToDoReader(new List<string> { "add", "", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Нельзя добавить пустое задание", logger.Messages[2]);
        }

        [TestMethod]
        public async Task CanNotInputNotIntegerInNumberChoice()
        {
            var logger = new FakeToDoLogger();
            var reader = new FakeToDoReader(new List<string> { "remove", "wrong input", "q" });
            var client = new FakeApiClient();
            var menuInputHandler = new MenuInputHandler(logger, reader, client);

            await menuInputHandler.HandleUsersInput();

            Assert.AreEqual("Нужно ввести число", logger.Messages[2]);
        }
    }
}
