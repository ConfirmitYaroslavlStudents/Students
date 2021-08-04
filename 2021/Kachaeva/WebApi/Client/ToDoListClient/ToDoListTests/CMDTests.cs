using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoClient;

namespace ToDoListTests
{
    [TestClass]
    public class CMDTests
    {
        [TestMethod]
        public async void TaskAddsCorrectly()
        {
            var logger = new FakeLogger();
            var input = new string[] { "add", "wash dishes", "list" };
            var CMDInputHandler = new CMDInputHandler(logger, input);

            await CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Задание добавлено", logger.Messages[0]);
            Assert.AreEqual("1. wash dishes  [ ]\r\n", logger.Messages[1]);
        }

        [TestMethod]
        public void TaskRemovesCorrectly()
        {
            var logger = new FakeLogger();
            var input = new string[] { "add", "wash dishes", "remove", "1", "list" };
            var CMDInputHandler = new CMDInputHandler(logger, input);

            CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Задание удалено", logger.Messages[1]);
            Assert.AreEqual("Список пуст", logger.Messages[2]);
        }

        [TestMethod]
        public void TaskTextChangesCorrectly()
        {
            var logger = new FakeLogger();
            var input = new string[] { "add", "wash dishes", "text", "1", "clean the room", "list" };
            var CMDInputHandler = new CMDInputHandler(logger, input);

            CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Текст задания изменен", logger.Messages[1]);
            Assert.AreEqual("1. clean the room  [ ]\r\n", logger.Messages[2]);
        }

        [TestMethod]
        public void TaskStatusChangesCorrectly()
        {
            var logger = new FakeLogger();
            var input = new string[] { "add", "wash dishes", "status", "1", "list" };
            var CMDInputHandler = new CMDInputHandler(logger, input);

            CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Статус задания изменен", logger.Messages[1]);
            Assert.AreEqual("1. wash dishes  [v]\r\n", logger.Messages[2]);
        }

        [TestMethod]
        public void ListSavesAndLoadsCorrectly()
        {
            var logger = new FakeLogger();
            var input = new string[] {"add", "wash dishes", "q"};
            var input2 = new string[] {"list", "q"};
            var menuInputHandler = new CMDInputHandler(logger, input);

            menuInputHandler.HandleUsersInput();
            menuInputHandler = new CMDInputHandler(logger, input2);
            menuInputHandler.HandleUsersInput();

            Assert.AreEqual("1. wash dishes  [ ]\r\n", logger.Messages[2]);
        }

        [TestMethod]
        public void CanNotChooseNonExistentMenuItem()
        {
            var logger = new FakeLogger();
            var input = new string[] { "print" };
            var CMDInputHandler = new CMDInputHandler(logger, input);

            CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Некорректная команда", logger.Messages[0]);
        }

        [TestMethod]
        public void CanNotAddEmptyTask()
        {
            var logger = new FakeLogger();
            var input = new string[] { "add", "" };
            var CMDInputHandler = new CMDInputHandler(logger, input);

            CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Нельзя добавить пустое задание", logger.Messages[0]);
        }

        [TestMethod]
        public void CanNotInputNotIntegerInNumberChoice()
        {
            var logger = new FakeLogger();
            var input = new string[] { "remove", "wrong input" };
            var CMDInputHandler = new CMDInputHandler(logger, input);

            CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Нужно ввести число", logger.Messages[0]);
        }

        [TestMethod]
        public void CanNotChooseNonExistentTaskNumber()
        {
            var logger = new FakeLogger();
            var input = new string[] { "remove", "1" };
            var CMDInputHandler = new CMDInputHandler(logger, input);

            CMDInputHandler.HandleUsersInput();

            Assert.AreEqual("Задания с таким номером не существует", logger.Messages[0]);
        }
    }
}
