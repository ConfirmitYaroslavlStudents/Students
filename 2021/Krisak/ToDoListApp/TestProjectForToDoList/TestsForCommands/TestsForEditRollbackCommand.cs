using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList.TestsForCommands
{
    [TestClass]
    public class TestsForEditRollbackCommand
    {
        [TestMethod]
        public void CorrectPerformCommand()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new EditCommand();
            command.SetParameters(new[] { "edit", "1","war" });

            var rollbackCommand = new EditRollbackCommand();
            rollbackCommand.SetParameters(command, tasks);

            tasks[0].Text = "war";

            var result = rollbackCommand.PerformCommand(tasks);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("world []", result[0].ToString());
        }

        [TestMethod]
        public void CorrectSetParameters()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new EditCommand();
            command.SetParameters(new[] { "edit", "1", "war" });

            var rollbackCommand = new EditRollbackCommand();
            rollbackCommand.SetParameters(command, tasks);

            Assert.AreEqual(0, rollbackCommand.Index);
            Assert.AreEqual("world", rollbackCommand.Text);
        }
    }
}