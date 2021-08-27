using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList.TestsForCommands
{
    [TestClass]
    public class TestsForEditTextCommand
    {
        [TestMethod]
        public void CorrectPerformCommand()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new EditTextCommand { TaskId = 0, Text = "war" };
            var result = command.PerformCommand(tasks);

            Assert.AreEqual("[0] war ○", result[0].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionWhenIdNotExist()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new EditTextCommand() { TaskId = 1, Text = "war"};
            command.PerformCommand(tasks);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionValidateWithLongTask()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress, TaskId = 1} };

            var command = new EditTextCommand{TaskId = 1, Text = new string('o', 51) };
            command.PerformCommand(tasks);
        }
    }
}