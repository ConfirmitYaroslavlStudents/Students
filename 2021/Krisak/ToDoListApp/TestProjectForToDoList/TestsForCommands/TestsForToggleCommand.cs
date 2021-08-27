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

            var command = new ToggleStatusCommand{TaskId = 0,Status = StatusTask.ToDo};

            var result = command.PerformCommand(tasks);

            Assert.AreEqual("[0] world", result[0].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenIdNotExist()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new ToggleStatusCommand() { TaskId = 1,Status = StatusTask.ToDo};
            command.PerformCommand(tasks);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenLimitOfNotesIsExceeded_InProgressStatus()
        {
            var tasks = new List<Task>
                    {
                        new Task { Status = StatusTask.IsProgress, TaskId = 1},
                        new Task { Status = StatusTask.IsProgress, TaskId = 2},
                        new Task { Status = StatusTask.IsProgress, TaskId = 3},
                        new Task { Status = StatusTask.ToDo, TaskId = 4},
                    };

            var command = new ToggleStatusCommand{TaskId = 4,Status = StatusTask.IsProgress};
            command.PerformCommand(tasks);
        }

        [TestMethod]
        public void WrongStatusToggle_FromToDoToProgress()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.ToDo } };

            var command = new ToggleStatusCommand{TaskId = 0,Status = StatusTask.IsProgress};
            command.PerformCommand(tasks);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void StatusToggle_FromToDoToDone()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.ToDo } };

            var command = new ToggleStatusCommand { TaskId = 0, Status = StatusTask.Done };
            command.PerformCommand(tasks);
        }

        [TestMethod]
        public void StatusToggle_FromProgressToToDo()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new ToggleStatusCommand { TaskId = 0, Status = StatusTask.ToDo };
            command.PerformCommand(tasks);
        }

        [TestMethod]
        public void StatusToggle_FromProgressToDone()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new ToggleStatusCommand { TaskId = 0, Status = StatusTask.Done };
            command.PerformCommand(tasks);
        }

        [TestMethod]
        public void StatusToggle_FromDoneToToDo()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.Done } };

            var command = new ToggleStatusCommand { TaskId = 0, Status = StatusTask.ToDo };
            command.PerformCommand(tasks);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void StatusToggle_FromDoneToProgress()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.Done } };

            var command = new ToggleStatusCommand { TaskId = 0, Status = StatusTask.IsProgress };
            command.PerformCommand(tasks);
        }
    }
}