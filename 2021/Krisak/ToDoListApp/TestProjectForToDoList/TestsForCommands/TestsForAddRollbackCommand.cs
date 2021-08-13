using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList.TestsForCommands
{
    [TestClass]
    public class TestsForAddRollbackCommand
    {
        [TestMethod]
        public void CorrectPerformCommand()
        {
            var tasks = new List<Task> {new Task {Text = "world", Status = StatusTask.IsProgress}};

            var command = new DeleteCommand();
            command.SetParameters(new[] {"delete", "1"});

            var rollbackCommand = new AddRollbackCommand();
            rollbackCommand.SetParameters(command, tasks);

            tasks.Clear();

            var result = rollbackCommand.PerformCommand(tasks);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("world []", result[0].ToString());
        }

        [TestMethod]
        public void CorrectSetParameters()
        {
            var tasks = new List<Task> {new Task {Text = "world", Status = StatusTask.IsProgress}};

            var command = new DeleteCommand();
            command.SetParameters(new[] {"delete", "1"});

            var rollbackCommand = new AddRollbackCommand();
            rollbackCommand.SetParameters(command, tasks);

            Assert.AreEqual(0, rollbackCommand.Index);
            Assert.AreEqual("world []", rollbackCommand.Task.ToString());
        }
    }
}