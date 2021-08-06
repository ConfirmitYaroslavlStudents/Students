using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestProjectForToDoLibrary
{
    [TestClass]
    public class TestsForRollbackCommandCreator
    {
        [TestMethod]
        public void CorrectCreateAddRollbackСommand()
        {
            var task = new Task();
            var tasks = new List<Task> {task};
            var rollbackCommandCreator = new RollbackCommandCreator(tasks);

            var partsCommand = new string[] {"delete", "1"};
            var result = rollbackCommandCreator.AddNewCommand(partsCommand) as AddRollbackCommand;

            Assert.AreSame(result.Tasks, tasks);
            Assert.AreSame(result.Task, task);
            Assert.AreEqual(result.Index, 0);
        }

        [TestMethod]
        public void CorrectCreateEditRollbackСommand()
        {
            var tasks = new List<Task> {new Task {Text = "war"}};
            var rollbackCommandCreator = new RollbackCommandCreator(tasks);

            var partsCommand = new string[] {"edit", "1", "world"};
            var result = rollbackCommandCreator.AddNewCommand(partsCommand) as EditRollbackCommand;

            Assert.AreSame(result.Tasks, tasks);
            Assert.AreEqual(result.Text, "war");
            Assert.AreEqual(result.Index, 0);
        }

        [TestMethod]
        public void CorrectCreateToggleRollbackCommand()
        {
            var tasks = new List<Task> {new Task {Text = "war"}};
            var rollbackCommandCreator = new RollbackCommandCreator(tasks);

            var partsCommand = new string[] {"toggle", "1", "2"};
            var result = rollbackCommandCreator.AddNewCommand(partsCommand) as ToggleRollbackCommand;

            Assert.AreSame(result.Tasks, tasks);
            Assert.AreEqual(result.Index, 0);
            Assert.AreEqual(result.Status, StatusTask.ToDo);
        }

        [TestMethod]
        public void CreateDeleteRollbackCommand()
        {
            var tasks = new List<Task> { };
            var rollbackCommandCreator = new RollbackCommandCreator(tasks);

            var partsCommand = new string[] {"add", "world"};
            var result = rollbackCommandCreator.AddNewCommand(partsCommand) as DeleteRollbackCommand;

            Assert.AreSame(result.Tasks, tasks);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongIndex_CreateAddRollbackСommand()
        {
            var tasks = new List<Task>();
            var rollbackCommandCreator = new RollbackCommandCreator(tasks);

            var partsCommand = new string[] {"delete", "odin"};
            rollbackCommandCreator.AddNewCommand(partsCommand);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongIndex_CreateEditRollbackСommand()
        {
            var tasks = new List<Task>();
            var rollbackCommandCreator = new RollbackCommandCreator(tasks);

            var partsCommand = new string[] {"edit", "odin", "word"};
            rollbackCommandCreator.AddNewCommand(partsCommand);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongIndex_CreateToggleRollbackCommand()
        {
            var tasks = new List<Task>();
            var rollbackCommandCreator = new RollbackCommandCreator(tasks);

            var partsCommand = new string[] {"toggle", "odin", "2"};
            rollbackCommandCreator.AddNewCommand(partsCommand);
        }
    }
}