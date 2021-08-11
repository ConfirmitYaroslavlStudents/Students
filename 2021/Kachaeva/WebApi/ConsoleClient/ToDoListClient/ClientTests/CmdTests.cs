using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoClient;
using  System.Threading.Tasks;

namespace ClientTests
{
    [TestClass]
    public class CmdTests
    {
        [TestMethod]
        public async Task TaskAddsCorrectly()
        {
            var logger = new FakeToDoLogger();
            var input = new string[] { "add", "wash dishes", "false", "list" };
            var client = new FakeApiClient();
            var cmdInputHandler = new CmdInputHandler(logger, input, client);

            await cmdInputHandler.HandleUsersInput();

            Assert.AreEqual("Задание добавлено под номером 1", logger.Messages[0]);
        }

        [TestMethod]
        public async Task TaskRemovesCorrectly()
        {
            var logger = new FakeToDoLogger();
            var input = new string[] { "add", "wash dishes", "false", "remove", "1", "list" };
            var client = new FakeApiClient();
            var cmdInputHandler = new CmdInputHandler(logger, input, client);

            await cmdInputHandler.HandleUsersInput();

            Assert.AreEqual("Задание удалено", logger.Messages[1]);
        }

        [TestMethod]
        public async Task TaskTextChangesCorrectly()
        {
            var logger = new FakeToDoLogger();
            var input = new string[] { "add", "wash dishes", "false", "text", "1", "clean the room", "list" };
            var client = new FakeApiClient();
            var cmdInputHandler = new CmdInputHandler(logger, input, client);

            await cmdInputHandler.HandleUsersInput();

            Assert.AreEqual("Текст задания обновлен", logger.Messages[1]);
        }

        [TestMethod]
        public async Task TaskStatusChangesCorrectly()
        {
            var logger = new FakeToDoLogger();
            var input = new string[] { "add", "wash dishes", "false", "status", "1", "true", "list" };
            var client = new FakeApiClient();
            var cmdInputHandler = new CmdInputHandler(logger, input, client);

            await cmdInputHandler.HandleUsersInput();

            Assert.AreEqual("Статус задания обновлен", logger.Messages[1]);
        }
        
        [TestMethod]
        public async Task CanNotChooseNonExistentMenuItem()
        {
            var logger = new FakeToDoLogger();
            var input = new string[] { "print" };
            var client = new FakeApiClient();
            var cmdInputHandler = new CmdInputHandler(logger, input, client);

            await cmdInputHandler.HandleUsersInput();

            Assert.AreEqual("Некорректная команда", logger.Messages[0]);
        }

        [TestMethod]
        public async Task CanNotAddEmptyTask()
        {
            var logger = new FakeToDoLogger();
            var input = new string[] { "add", "" };
            var client = new FakeApiClient();
            var cmdInputHandler = new CmdInputHandler(logger, input, client);

            await cmdInputHandler.HandleUsersInput();

            Assert.AreEqual("Нельзя добавить пустое задание", logger.Messages[0]);
        }

        [TestMethod]
        public async Task CanNotInputNotIntegerInNumberChoice()
        {
            var logger = new FakeToDoLogger();
            var input = new string[] { "remove", "wrong input" };
            var client = new FakeApiClient();
            var cmdInputHandler = new CmdInputHandler(logger, input, client);

            await cmdInputHandler.HandleUsersInput();

            Assert.AreEqual("Нужно ввести число", logger.Messages[0]);
        }
    }
}
