using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.CommandHandler;

namespace TestProjectForToDoLibrary
{
    [TestClass]
    public class TestsForToDoAppWithCmdCommandHandler
    {
        private FakeLogger _logger = new FakeLogger();
        private ToDoApp _ToDoApp;

        private void RunHandle(string[] command)
        {
            var cmdCommandHandler = new CmdCommandHandler(command);
            _ToDoApp.HandleCommand(cmdCommandHandler);
        }

        [TestMethod]
        public void RunWrongCommand()
        {
            _ToDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new [] { "IWrongHaHa" });
            Assert.AreEqual("Unknown command.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunAddCommandWithLongTask()
        {
            _ToDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new [] { "add", new string('o', 51) });
            Assert.AreEqual("Task length must not be more than 50 characters.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunEditCommandWithWrongIndex()
        {
            _ToDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "edit", "odin" });
            
            Assert.AreEqual("Wrong index.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunEditCommandWithLongTask()
        {
            _ToDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "add", "1" });
            RunHandle(new[] { "edit", "1", new string('o', 51) });
            Assert.AreEqual("Task length must not be more than 50 characters.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunEditCommandWithIndexOutOfRange()
        {
            _ToDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "edit", "-1", "" });
            Assert.AreEqual("Task not found with Id -1.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunToggleCommandWithWrongIndex()
        { 
            _ToDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "toggle", "odin", "1" });
            Assert.AreEqual("Wrong index.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunToggleCommandWithWrongNumberStatus()
        {
            _ToDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "toggle", "1", "odin" });
            RunHandle(new[] { "add", "1" });
            Assert.AreEqual("Wrong index.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunToggleCommandWithWrongId()
        {
            _ToDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "toggle", "-1", "1" });
            Assert.AreEqual("Task not found with Id -1.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunToggleCommandWithNumberStatusOutOfRange()
        {
            _ToDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "toggle", "0", "3" });
            Assert.AreEqual("Wrong status number.", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunToggleCommandWithWrongToggle()
        {
            _ToDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "add", "" });
            RunHandle(new[] { "toggle", "1", "2" });
            Assert.AreEqual("Status \"Done\" can only be toggled from status \"In Progress\".", _logger.Exception.Message);
        }

        [TestMethod]
        public void RunToggleCommandWithOutOfStatusLimit()
        {
            _ToDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
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
        public void RunDeleteCommandWithIndexOutOfRange()
        {
            _ToDoApp = new ToDoApp(_logger, new FakeSaver(), new FakeLoader());
            RunHandle(new[] { "delete", "-1" });
            Assert.AreEqual("Task not found with Id -1.", _logger.Exception.Message);
        }
    }
}