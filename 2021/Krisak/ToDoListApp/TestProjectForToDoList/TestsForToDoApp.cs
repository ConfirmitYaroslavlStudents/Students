using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;

namespace TestProjectForToDoLibrary
{
    [TestClass]
    public class TestsForToDoApp
    {
        private FakeUserInput _userInput = new FakeUserInput();

        [TestMethod]
        public void LoadWithFileNotFound()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);

            Assert.AreEqual("Saved data was not found. New list created.", logger.Message);
        }

        [TestMethod]
        public void WorkWithConsole_GetEmptyCommand()
        {
            var  logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new []{"bye"});
            _userInput.Commands.Push(new string[] { });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Empty command",logger.Message);
        }

        [TestMethod]
        public void WorkWithConsole_GetWrongCommand()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new string[] {"IWrongHaHa" });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Unknown command.", logger.Message);
        }

        [TestMethod]
        public void WorkWithConsole_GetAddCommandWithLongTask()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new string[] { "add", new string('o', 51) });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Task length must not be more than 50 characters.", logger.Message);
        }

        [TestMethod]
        public void WorkWithConsole_GetEditCommandWithWrongIndex()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new [] { "edit","odin" });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Wrong index.", logger.Message);
        }

        [TestMethod]
        public void WorkWithConsole_GetEditCommandWithLongTask()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new [] { "edit","1", new string('o', 51)});

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Task length must not be more than 50 characters.", logger.Message);
        }

        [TestMethod]
        public void WorkWithConsole_GetEditCommandWithIndexOutOfRange()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new[] {"edit", "-1", ""});

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Task not found with number -1.", logger.Message);

            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new[] { "edit", "100", "" });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Task not found with number 100.", logger.Message);
        }

        [TestMethod]
        public void WorkWithConsole_GetToggleCommandWithWrongIndex()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new[] { "toggle", "odin","1" });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Wrong index.", logger.Message);
        }

        [TestMethod]
        public void WorkWithConsole_GetToggleCommandWithWrongNumberStatus()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new[] { "toggle", "1", "odin" });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Wrong number.", logger.Message);
        }

        [TestMethod]
        public void WorkWithConsole_GetToggleCommandWithIndexOutOfRange()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new[] { "toggle", "-1", "1" });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Task not found with number -1.", logger.Message);

            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new[] { "toggle", "100", "1" });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Task not found with number 100.", logger.Message);
        }

        [TestMethod]
        public void WorkWithConsole_GetToggleCommandWithNumberStatusOutOfRange()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new[] { "toggle", "0", "3" });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Wrong status number.", logger.Message);
        }

        [TestMethod]
        public void WorkWithConsole_GetToggleCommandWithWrongToggle()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new[] { "toggle", "1", "2" });
            _userInput.Commands.Push(new[] { "add", "" });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Status \"Done\" can only be toggled from status \"In Progress\".", logger.Message);

            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new[] { "toggle", "1", "1" });
            _userInput.Commands.Push(new[] { "toggle", "1", "1" });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Status \"In Progress\" can only be toggled from status \"To Do\".", logger.Message);
        }

        [TestMethod]
        public void WorkWithConsole_GetToggleCommandWithOutOfStatusLimit()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new[] { "toggle", "4", "1" });
            _userInput.Commands.Push(new[] { "toggle", "2", "1" });
            _userInput.Commands.Push(new[] { "toggle", "1", "1" });
            _userInput.Commands.Push(new[] { "toggle", "3", "1" });
            _userInput.Commands.Push(new[] { "add", "" });
            _userInput.Commands.Push(new[] { "add", "" });
            _userInput.Commands.Push(new[] { "add", "" });
            _userInput.Commands.Push(new[] { "add", "" });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual($"Number of tasks in \"In Progress\" status should not exceed 3.", logger.Message);
        }

        [TestMethod]
        public void WorkWithConsole_GetDeleteCommandWithIndexOutOfRange()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new[] { "delete", "-1" });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Task not found with number -1.", logger.Message);

            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new[] { "delete", "100" });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual("Task not found with number 100.", logger.Message);
        }

        [TestMethod]
        public void WorkWithConsole_GetRollbackCommandWithCountOutOfRange()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "bye" });
            _userInput.Commands.Push(new[] { "rollback", "-1" });

            toDo.WorkWithConsole(_userInput);

            Assert.AreEqual( "Wrong count of rollback steps", logger.Message);
        }

        [TestMethod]
        public void WorkWithCmd_GetWrongCommand()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new string[] { "IWrongHaHa" });

            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Unknown command.", logger.Message);
        }

        [TestMethod]
        public void WorkWithCmd_GetAddCommandWithLongTask()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new string[] { "add", new string('o', 51) });

            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Task length must not be more than 50 characters.", logger.Message);
        }

        [TestMethod]
        public void WorkWithCmde_GetEditCommandWithWrongIndex()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "edit", "odin" });

            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Wrong index.", logger.Message);
        }

        [TestMethod]
        public void WorkWithCmd_GetEditCommandWithLongTask()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "edit", "1", new string('o', 51) });

            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Task length must not be more than 50 characters.", logger.Message);
        }

        [TestMethod]
        public void WorkWithCmd_GetEditCommandWithIndexOutOfRange()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);

            _userInput.Commands.Push(new[] { "edit", "100", "" });
            _userInput.Commands.Push(new[] { "edit", "-1", "" });

            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Task not found with number -1.", logger.Message);

            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Task not found with number 100.", logger.Message);
        }

        [TestMethod]
        public void WorkWithCmd_GetToggleCommandWithWrongIndex()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "toggle", "odin", "1" });

            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Wrong index.", logger.Message);
        }

        [TestMethod]
        public void WorkWithCmd_GetToggleCommandWithWrongNumberStatus()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "toggle", "1", "odin" });

            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Wrong number.", logger.Message);
        }

        [TestMethod]
        public void WorkWithCmd_GetToggleCommandWithIndexOutOfRange()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "toggle", "100", "1" });
            _userInput.Commands.Push(new[] { "toggle", "-1", "1" });

            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Task not found with number -1.", logger.Message);

            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Task not found with number 100.", logger.Message);
        }

        [TestMethod]
        public void WorkWithCmd_GetToggleCommandWithNumberStatusOutOfRange()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "toggle", "0", "3" });

            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Wrong status number.", logger.Message);
        }

        [TestMethod]
        public void WorkWithCmd_GetToggleCommandWithWrongToggle()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);

            _userInput.Commands.Push(new[] { "toggle", "1", "1" });
            _userInput.Commands.Push(new[] { "toggle", "1", "1" });
            _userInput.Commands.Push(new[] { "toggle", "1", "2" });
            _userInput.Commands.Push(new[] { "add", "" });

            toDo.WorkWithCmd(_userInput);
            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Status \"Done\" can only be toggled from status \"In Progress\".", logger.Message);

            toDo.WorkWithCmd(_userInput);
            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Status \"In Progress\" can only be toggled from status \"To Do\".", logger.Message);
        }

        [TestMethod]
        public void WorkWithCmd_GetToggleCommandWithOutOfStatusLimit()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "toggle", "4", "1" });
            _userInput.Commands.Push(new[] { "toggle", "2", "1" });
            _userInput.Commands.Push(new[] { "toggle", "1", "1" });
            _userInput.Commands.Push(new[] { "toggle", "3", "1" });
            _userInput.Commands.Push(new[] { "add", "" });
            _userInput.Commands.Push(new[] { "add", "" });
            _userInput.Commands.Push(new[] { "add", "" });
            _userInput.Commands.Push(new[] { "add", "" });

            toDo.WorkWithCmd(_userInput);
            toDo.WorkWithCmd(_userInput);
            toDo.WorkWithCmd(_userInput);
            toDo.WorkWithCmd(_userInput);
            toDo.WorkWithCmd(_userInput);
            toDo.WorkWithCmd(_userInput);
            toDo.WorkWithCmd(_userInput);
            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual($"Number of tasks in \"In Progress\" status should not exceed 3.", logger.Message);
        }

        [TestMethod]
        public void WorkWithCmd_GetDeleteCommandWithIndexOutOfRange()
        {
            var logger = new FakeLogger();
            var toDo = new ToDoApp(logger);
            _userInput.Commands.Push(new[] { "delete", "100" });
            _userInput.Commands.Push(new[] { "delete", "-1" });

            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Task not found with number -1.", logger.Message);

            toDo.WorkWithCmd(_userInput);

            Assert.AreEqual("Task not found with number 100.", logger.Message);
        }
    }
}