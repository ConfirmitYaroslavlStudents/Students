using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList
{
    [TestClass]
    public class TestsForCommandCreator
    {
        [TestMethod]
        public void CorrectCreateAddСommand_OneWord()
        {
            var tasks = new List<Task>();
            var commandCreator = new CommandCreator(tasks);

            var partsCommand = new string[] { "add", "world" };
            var result = commandCreator.CreateAddСommand(partsCommand) as AddCommand;
            Assert.AreSame(result.Tasks, tasks);
            Assert.AreEqual(result.NewTask.ToString(), "world");
        }

        [TestMethod]
        public void CorrectCreateAddСommand_SeveralWords()
        {
            var tasks = new List<Task>();
            var commandCreator = new CommandCreator(tasks);

            var partsCommand = new string[] { "add", "world", "or", "war" };
            var result = commandCreator.CreateAddСommand(partsCommand) as AddCommand;
            Assert.AreSame(result.Tasks, tasks);
            Assert.AreEqual(result.NewTask.ToString(), "world or war");
        }

        [TestMethod]
        public void CorrectCreateEditСommand_OneWord()
        {
            var tasks = new List<Task>();
            var commandCreator = new CommandCreator(tasks);

            var partsCommand = new string[] { "edit", "1", "world" };
            var result = commandCreator.CreateEditСommand(partsCommand) as EditCommand;
            Assert.AreSame(result.Tasks, tasks);
            Assert.AreEqual(result.Text, "world");
            Assert.AreEqual(result.Index, 0);
        }

        [TestMethod]
        public void CorrectCreateEditСommand_SeveralWords()
        {
            var tasks = new List<Task>();
            var commandCreator = new CommandCreator(tasks);

            var partsCommand = new string[] { "edit", "1", "world", "or", "war" };
            var result = commandCreator.CreateEditСommand(partsCommand) as EditCommand;
            Assert.AreSame(result.Tasks, tasks);
            Assert.AreEqual(result.Text, "world or war");
            Assert.AreEqual(result.Index, 0);
        }

        [TestMethod]
        public void CorrectCreateToggleСommand()
        {
            var tasks = new List<Task>();
            var commandCreator = new CommandCreator(tasks);

            var partsCommand = new string[] { "toggle", "1", "0" };
            var result = commandCreator.CreateToggleСommand(partsCommand) as ToggleCommand;
            Assert.AreSame(result.Tasks, tasks);
            Assert.AreEqual(result.Index, 0);
            Assert.AreEqual(result.Status, StatusTask.ToDo);
        }

        [TestMethod]
        public void CorrectConvertTaskStatusFromInt()
        {
            var tasks = new List<Task>();
            var commandCreator = new CommandCreator(tasks);

            var partsCommand = new string[] { "toggle", "1", "0" };
            var result1 = commandCreator.CreateToggleСommand(partsCommand) as ToggleCommand;
            partsCommand = new string[] { "toggle", "1", "1" };
            var result2 = commandCreator.CreateToggleСommand(partsCommand) as ToggleCommand;
            partsCommand = new string[] { "toggle", "1", "2" };
            var result3 = commandCreator.CreateToggleСommand(partsCommand) as ToggleCommand;

            Assert.AreEqual(result1.Status, StatusTask.ToDo);
            Assert.AreEqual(result2.Status, StatusTask.IsProgress);
            Assert.AreEqual(result3.Status, StatusTask.Done);
        }

        [TestMethod]
        public void CorrectCreateDeleteСommand()
        {
            var tasks = new List<Task>();
            var commandCreator = new CommandCreator(tasks);

            var partsCommand = new string[] { "delete", "1" };
            var result = commandCreator.CreateDeleteСommand(partsCommand) as DeleteCommand;

            Assert.AreSame(result.Tasks, tasks);
            Assert.AreEqual(result.Index, 0);
        }

        [TestMethod]
        public void CorrectCreateRollbackСommand()
        {
            var tasks = new List<Task>();
            var commandCreator = new CommandCreator(tasks);
            var rollback = new Rollback(tasks);

            var partsCommand = new string[] { "rollback", "1" };
            var result = commandCreator.CreateRollbackСommand(partsCommand,rollback) as RollbackCommand;

            Assert.AreSame(result.Rollback, rollback);
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongIndex_CreateEditСommand()
        {
            var tasks = new List<Task>();
            var commandCreator = new CommandCreator(tasks);

            var partsCommand = new string[] { "edit", "odin" };
            commandCreator.CreateEditСommand(partsCommand);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongIndex_CreateToggleСommand()
        {
            var tasks = new List<Task>();
            var commandCreator = new CommandCreator(tasks);

            var partsCommand = new string[] { "toggle", "odin", "1" };
            commandCreator.CreateToggleСommand(partsCommand);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongNumberStatus_CreateToggleСommand()
        {
            var tasks = new List<Task>();
            var commandCreator = new CommandCreator(tasks);

            var partsCommand = new string[] { "toggle", "1", "odin" };
            commandCreator.CreateToggleСommand(partsCommand);
        }
        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongIndex_CreateDeleteСommand()
        {
            var tasks = new List<Task>();
            var commandCreator = new CommandCreator(tasks);

            var partsCommand = new string[] { "delete", "odin" };
            commandCreator.CreateDeleteСommand(partsCommand);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringInvalidCount_CreateRollbackСommand()
        {
            var tasks = new List<Task>();
            var commandCreator = new CommandCreator(tasks);
            var rollback = new Rollback(tasks);

            var partsCommand = new string[] { "rollback", "odin" };
            commandCreator.CreateRollbackСommand(partsCommand, rollback);
        }
    }
}
