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
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new DeleteCommand();
            command.SetParameters(new[] { "delete", "1" });
            var result = command.PerformCommand(tasks);

            Assert.AreEqual(0, tasks.Count);
        }

        [TestMethod]
        public void CorrectSetParameters()
        {
            var command = new DeleteCommand();
            command.SetParameters(new[] { "delete", "1" });

            Assert.AreEqual(0, command.Index);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenWrongIndex()
        {
            var command = new DeleteCommand();
            command.SetParameters(new[] { "delete", "odin" });
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsNegative()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new DeleteCommand();
            command.SetParameters(new[] { "delete", "-1" });
            command.RunValidate(tasks);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsGreaterThanCount()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new DeleteCommand();
            command.SetParameters(new[] { "delete", "2" });
            command.RunValidate(tasks);
        }
    }
}