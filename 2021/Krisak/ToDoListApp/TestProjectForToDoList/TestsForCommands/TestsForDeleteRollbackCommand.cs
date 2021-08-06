using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList.TestsForCommands
{
    [TestClass]
    public class TestsForDeleteRollbackCommand
    {
        [TestMethod]
        public void CorrectPerformCommand()
        {
            var tasks = new List<Task> { new Task{Text = "world"},new Task { Text = "war" } };

            var command = new DeleteRollbackCommand() { Tasks = tasks};
            command.PerformCommand();

            Assert.AreEqual(1,tasks.Count);
            Assert.AreEqual("world", tasks[0].ToString());
        }
    }
}