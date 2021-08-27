using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList.TestsForCommands
{
    [TestClass]
    public class TestsForRollbackToggleCommand
    {
        [TestMethod]
        public void CorrectPerformCommand()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var rollbackCommand = new RollbackToggleStatusCommand{TaskId = 0,Status = StatusTask.IsProgress};

            tasks[0].Status = StatusTask.Done;

            var result = rollbackCommand.PerformCommand(tasks);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("[0] world ○", result[0].ToString());
        }
    }
}