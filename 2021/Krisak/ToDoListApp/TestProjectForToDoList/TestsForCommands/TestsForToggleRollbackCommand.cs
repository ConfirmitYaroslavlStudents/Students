using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList.TestsForCommands
{
    [TestClass]
    public class TestsForToggleRollbackCommand
    {
        [TestMethod]
        public void CorrectPerformCommand()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "1", "2" });

            var rollbackCommand = new ToggleRollbackCommand();
            rollbackCommand.SetParameters(command, tasks);

            tasks[0].Status = StatusTask.Done;

            var result = rollbackCommand.PerformCommand(tasks);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("world []", result[0].ToString());
        }

        [TestMethod]
        public void CorrectSetParameters()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "1", "2" });

            var rollbackCommand = new ToggleRollbackCommand();
            rollbackCommand.SetParameters(command, tasks);

            Assert.AreEqual(0, rollbackCommand.Index);
            Assert.AreEqual(StatusTask.IsProgress, rollbackCommand.Status);
        }
    }
    }