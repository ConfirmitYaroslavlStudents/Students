using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList.TestsForCommands
{
    [TestClass]
    public class TestsForToggleCommand
    {
        [TestMethod]
        public void CorrectPerformCommand()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "1", "0" });
            var result = command.PerformCommand(tasks);

            Assert.AreEqual("world", result[0].ToString());
        }

        [TestMethod]
        public void CorrectSetParameters()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "1", "0" });

            Assert.AreEqual(0, command.Index);
            Assert.AreEqual(StatusTask.ToDo, command.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenWrongIndex()
        {
            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "odin", "1" });
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenWrongStatus()
        {
            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "1", "3" });
        }


        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsNegative()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "-1", "2" });
            command.RunValidate(tasks);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsGreaterThanCount()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "2", "2" });
            command.RunValidate(tasks);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenLimitOfNotesIsExceeded_InProgressStatus()
        {
            var tasks = new List<Task>
                {
                    new Task { Status = StatusTask.IsProgress},
                    new Task { Status = StatusTask.IsProgress},
                    new Task { Status = StatusTask.IsProgress},
                    new Task { Status = StatusTask.ToDo},
                };

            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "4", "2" });
            command.RunValidate(tasks);
        }

        [TestMethod]
        public void WrongStatusToggle_FromToDoToProgress()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.ToDo } };

            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "1", "1" });
            command.RunValidate(tasks);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void StatusToggle_FromToDoToDone()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.ToDo } };

            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "1", "2" });
            command.RunValidate(tasks);
        }

        [TestMethod]
        public void StatusToggle_FromProgressToToDo()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "1", "0" });
            command.RunValidate(tasks);
        }

        [TestMethod]
        public void StatusToggle_FromProgressToDone()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "1", "2" });
            command.RunValidate(tasks);
        }

        [TestMethod]
        public void StatusToggle_FromDoneToToDo()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.Done } };

            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "1", "0" });
            command.RunValidate(tasks);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void StatusToggle_FromDoneToProgress()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.Done } };

            var command = new ToggleCommand();
            command.SetParameters(new[] { "toggle", "1", "1" });
            command.RunValidate(tasks);
        }
    }
}