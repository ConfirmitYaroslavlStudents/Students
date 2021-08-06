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
            var tasks = new List<Task> { new Task { Text = "world" }, new Task { Text = "war" } };
            var task = new Task { Text = "or" };

            var command = new AddRollbackCommand() { Index = 1, Tasks = tasks, Task = task};
            command.PerformCommand();

            Assert.AreEqual(3,tasks.Count);
            Assert.AreEqual("or", tasks[1].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsNegative()
        {
            var command = new AddRollbackCommand() { Index = -1 };
            command.PerformCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsGreaterThanCount()
        {
            var tasks = new List<Task> { };

            var command = new AddRollbackCommand() { Index = 1, Tasks = tasks };
            command.PerformCommand();
        }
    }
}