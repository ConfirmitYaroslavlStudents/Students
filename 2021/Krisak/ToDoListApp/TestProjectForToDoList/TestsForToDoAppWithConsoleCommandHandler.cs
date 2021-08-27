using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary.CommandHandler;
using ToDoLibrary;
using ToDoLibrary.Storages;

namespace TestProjectForToDoLibrary
{
    [TestClass]
    public class TestsForToDoAppWithConsoleCommandHandler
    {
        private RollbacksStorage _rollback = new RollbacksStorage();
        private FakeLogger _logger = new FakeLogger();
        private ToDoApp _toDoApp;

        private void RunHandle(string[] command)
        {
            var consoleCommandHandler = new ConsoleCommandHandler(_rollback, command);
            _toDoApp.HandleCommand(consoleCommandHandler);
        }

        [TestMethod]
        public void RunEmptyCommand()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new string[] { });

            Assert.AreEqual("Empty command", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunWrongCommand()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "IWrongHaHa" });

            Assert.AreEqual("Unknown command.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunAddCommandWithLongTask()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "add", new string('o', 51) });

            Assert.AreEqual("Task length must not be more than 50 characters.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunEditCommandWithWrongIndex()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "edit", "odin" });

            Assert.AreEqual("Wrong index.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunEditCommandWithLongTask()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "add", "1" });
            RunHandle(new[] { "edit", "1", new string('o', 51) });

            Assert.AreEqual("Task length must not be more than 50 characters.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunEditCommandWithWrongId()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "edit", "1", "" });

            Assert.AreEqual("Task not found with Id 1.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunEditCommandSeveralException()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "edit", "1", new string('o', 51) });
            Assert.AreEqual("Task not found with Id 1.\nTask length must not be more than 50 characters.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunToggleCommandWithWrongIndex()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "toggle", "odin", "1" });
            Assert.AreEqual("Wrong index.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunToggleCommandWithWrongNumberStatus()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "toggle", "1", "odin" });
            Assert.AreEqual("Wrong index.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunToggleCommandWithWrongId()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "toggle", "-1", "1" });
            Assert.AreEqual("Task not found with Id -1.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunToggleCommandWithNumberStatusOutOfRange()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "toggle", "0", "3" });
            Assert.AreEqual("Wrong status number.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunToggleCommandWithWrongToggle()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "add", "" });
            RunHandle(new[] { "toggle", "1", "2" });
            Assert.AreEqual("Status \"Done\" can only be toggled from status \"In Progress\".", _logger.Exception.Message);
            RunHandle(new[] { "toggle", "1", "1" });
            RunHandle(new[] { "toggle", "1", "1" });
            Assert.AreEqual("Status \"In Progress\" can only be toggled from status \"To Do\".", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunToggleCommandWithOutOfStatusLimit()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "add", "" });
            RunHandle(new[] { "add", "" });
            RunHandle(new[] { "add", "" });
            RunHandle(new[] { "add", "" });
            RunHandle(new[] { "toggle", "4", "1" });
            RunHandle(new[] { "toggle", "2", "1" });
            RunHandle(new[] { "toggle", "1", "1" });
            RunHandle(new[] { "toggle", "3", "1" });
            Assert.AreEqual($"Number of tasks in \"In Progress\" status should not exceed 3.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunToggleCommandSeveralException()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "add", "" });
            RunHandle(new[] { "add", "" });
            RunHandle(new[] { "add", "" });
            RunHandle(new[] { "add", "" });
            RunHandle(new[] { "toggle", "2", "1" });
            RunHandle(new[] { "toggle", "4", "1" });
            RunHandle(new[] { "toggle", "2", "2" });
            RunHandle(new[] { "toggle", "1", "1" });
            RunHandle(new[] { "toggle", "3", "1" });
            RunHandle(new[] { "toggle", "2", "1" });
            Assert.AreEqual
                ($"Status \"In Progress\" can only be toggled from status \"To Do\".\nNumber of tasks in \"In Progress\" status should not exceed 3.",
                _logger.Exception.Message);
        }

        [TestMethod]
        public void RunDeleteCommandWithWrongIndex()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "delete", "-1" });
            Assert.AreEqual("Task not found with Id -1.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunRollbackCommandWithCountOutOfRange()
        {
            _toDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "add", "-1" });
            RunHandle(new[] { "rollback", "-1" });
            Assert.AreEqual("Wrong count of rollback steps", _logger.Exception.Message);
        }
    }
}