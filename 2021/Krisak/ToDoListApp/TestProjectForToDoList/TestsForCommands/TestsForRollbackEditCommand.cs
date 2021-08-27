using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList.TestsForCommands
{
    [TestClass]
    public class TestsForRollbackEditCommand
    {
        [TestMethod]
        public void CorrectPerformCommand()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var rollbackCommand = new RollbackEditCommand{TaskId = 0,Text = "world"};

            tasks[0].Text = "war";

            var result = rollbackCommand.PerformCommand(tasks);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("[0] world ○", result[0].ToString());
        }
    }
}