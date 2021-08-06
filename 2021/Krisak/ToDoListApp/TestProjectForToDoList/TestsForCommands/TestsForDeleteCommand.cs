using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList.TestsForCommands
{
    [TestClass]
    public class TestsForDeleteCommand
    {
        [TestMethod]
        public void CorrectPerformCommand()
        {
            var tasks = new List<Task> { new Task { Text = "war" },new Task{Text = "world"} };

            var command = new DeleteCommand { Index = 0, Tasks = tasks};
            command.PerformCommand();

            Assert.AreEqual(1,tasks.Count);
            Assert.AreEqual("world", tasks[0].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsNegative()
        {
            var command = new DeleteCommand() { Index = -1 };
            command.PerformCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsGreaterThanCount()
        {
            var tasks = new List<Task> { new Task() };

            var command = new DeleteCommand() { Index = 1, Tasks = tasks };
            command.PerformCommand();
        }
    }
}