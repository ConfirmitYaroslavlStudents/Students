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
            var tasks = new List<Task> { new Task { Text = "war" } };

            var command = new ToggleCommand() { Index = 0, Tasks = tasks, Status = StatusTask.IsProgress};
            command.PerformCommand();

            Assert.AreEqual("war []", tasks[0].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsNegative()
        {
            var command = new ToggleCommand() { Index = -1 };
            command.PerformCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenEnteredIndexIsGreaterThanCount()
        {
            var tasks = new List<Task> { new Task() };

            var command = new ToggleCommand() { Index = 1, Tasks = tasks };
            command.PerformCommand();
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

            var command = new ToggleCommand() { Index = 3, Tasks = tasks, Status = StatusTask.IsProgress };
            command.PerformCommand();
        }

        [TestMethod]
        public void StatusToggle_FromToDoToProgress()
        {
            var tasks = new List<Task> { new Task { Status = StatusTask.ToDo},};
            var command = new ToggleCommand() { Index = 0, Tasks = tasks, Status = StatusTask.IsProgress };

            command.PerformCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void StatusToggle_FromToDoToDone()
        {
            var tasks = new List<Task> {new Task { Status = StatusTask.ToDo},};
            var command = new ToggleCommand() { Index = 0, Tasks = tasks, Status = StatusTask.Done };

            command.PerformCommand();
        }
        [TestMethod]
        public void StatusToggle_FromProgressToToDo()
        {
            var tasks = new List<Task> {new Task { Status = StatusTask.IsProgress},};
            var command = new ToggleCommand() { Index = 0, Tasks = tasks, Status = StatusTask.ToDo };

            command.PerformCommand();
        }

        [TestMethod]
        public void StatusToggle_FromProgressToDone()
        {
            var tasks = new List<Task> {new Task { Status = StatusTask.IsProgress},};
            var command = new ToggleCommand() { Index = 0, Tasks = tasks, Status = StatusTask.Done };

            command.PerformCommand();
        }

        [TestMethod]
        public void StatusToggle_FromDoneToToDo()
        {
            var tasks = new List<Task> {new Task { Status = StatusTask.Done}};
            var command = new ToggleCommand() { Index = 0, Tasks = tasks, Status = StatusTask.ToDo };

            command.PerformCommand();
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void StatusToggle_FromDoneToProgress()
        {
            var tasks = new List<Task> {new Task { Status = StatusTask.Done},};
            var command = new ToggleCommand() { Index = 0, Tasks = tasks, Status = StatusTask.IsProgress };

            command.PerformCommand();
        }
    }
}