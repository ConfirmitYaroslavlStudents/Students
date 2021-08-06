using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList.TestsForCommands
{
    [TestClass]
    public class TestsForEditCommand
    {
        [TestMethod]
        public void CorrectPerformCommand()
        {
            var tasks = new List<Task> { new Task { Text = "war" }};

            var command = new EditCommand {Index = 0, Tasks = tasks, Text = "world"};
            command.PerformCommand();
            
            Assert.AreEqual("world", tasks[0].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsNegative()
        {
            var command = new EditCommand {Index = -1};
            command.PerformCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsGreaterThanCount()
        {
            var tasks = new List<Task> {new Task()};

            var command = new EditCommand { Index = 1,Tasks = tasks};
            command.PerformCommand();
        }
    }
}