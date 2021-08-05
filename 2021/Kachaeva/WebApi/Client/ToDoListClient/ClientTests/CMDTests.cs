using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoClient;

namespace ClientTests
{
    [TestClass]
    public class CMDTests
    {
        [TestMethod]
        public void TaskAddsCorrectly()
        {
            var logger = new FakeLogger();
            var input = new string[] { "add", "wash dishes", "false", "list" };
            var client = new FakeApiClient();
            var CMDInputHandler = new CMDInputHandler(logger, input, client);

            CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Задание добавлено", logger.Messages[0]);
            //Assert.AreEqual("1. wash dishes  [ ]\r\n", logger.Messages[1]);
        }

        [TestMethod]
        public void TaskRemovesCorrectly()
        {
            var logger = new FakeLogger();
            var input = new string[] { "add", "wash dishes", "false", "remove", "1", "list" };
            var client = new FakeApiClient();
            var CMDInputHandler = new CMDInputHandler(logger, input, client);

            CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Задание удалено", logger.Messages[1]);
            //Assert.AreEqual("Список пуст", logger.Messages[2]);
        }

        [TestMethod]
        public void TaskTextChangesCorrectly()
        {
            var logger = new FakeLogger();
            var input = new string[] { "add", "wash dishes", "false", "text", "1", "clean the room", "list" };
            var client = new FakeApiClient();
            var CMDInputHandler = new CMDInputHandler(logger, input, client);

            CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Текст задания обновлен", logger.Messages[1]);
            //Assert.AreEqual("1. clean the room  [ ]\r\n", logger.Messages[2]);
        }

        [TestMethod]
        public void TaskStatusChangesCorrectly()
        {
            var logger = new FakeLogger();
            var input = new string[] { "add", "wash dishes", "false", "status", "1", "true", "list" };
            var client = new FakeApiClient();
            var CMDInputHandler = new CMDInputHandler(logger, input, client);

            CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Статус задания обновлен", logger.Messages[1]);
            //Assert.AreEqual("1. wash dishes  [v]\r\n", logger.Messages[2]);
        }

        //[TestMethod]
        //public void ListSavesAndLoadsCorrectly()
        //{
        //    var logger = new FakeLogger();
        //    var input = new string[] {"add", "wash dishes", "q"};
        //    var input2 = new string[] {"list", "q"};
        //    var client = new FakeApiClient();
        //    var menuInputHandler = new CMDInputHandler(logger, input, client);

        //    menuInputHandler.HandleUsersInput();
        //    menuInputHandler = new CMDInputHandler(logger, input2, client);
        //    menuInputHandler.HandleUsersInput();

        //    Assert.AreEqual("1. wash dishes  [ ]\r\n", logger.Messages[2]);
        //}

        [TestMethod]
        public void CanNotChooseNonExistentMenuItem()
        {
            var logger = new FakeLogger();
            var input = new string[] { "print" };
            var client = new FakeApiClient();
            var CMDInputHandler = new CMDInputHandler(logger, input, client);

            CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Некорректная команда", logger.Messages[0]);
        }

        [TestMethod]
        public void CanNotAddEmptyTask()
        {
            var logger = new FakeLogger();
            var input = new string[] { "add", "" };
            var client = new FakeApiClient();
            var CMDInputHandler = new CMDInputHandler(logger, input, client);

            CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Нельзя добавить пустое задание", logger.Messages[0]);
        }

        [TestMethod]
        public void CanNotInputNotIntegerInNumberChoice()
        {
            var logger = new FakeLogger();
            var input = new string[] { "remove", "wrong input" };
            var client = new FakeApiClient();
            var CMDInputHandler = new CMDInputHandler(logger, input, client);

            CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Нужно ввести число", logger.Messages[0]);
        }
    }
}
