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
            var tasks = new List<Task> { new Task { Text = "war" } };


            var command = new ToggleRollbackCommand() { Index = 0, Tasks = tasks, Status = StatusTask.Done };
            command.PerformCommand();

            Assert.AreEqual("war [X]", tasks[0].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsNegative()
        {
            var command = new ToggleRollbackCommand() { Index = -1 };
            command.PerformCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsGreaterThanCount()
        {
            var tasks = new List<Task> { new Task() };

            var command = new ToggleRollbackCommand() { Index = 1, Tasks = tasks };
            command.PerformCommand();
        }
    }
}