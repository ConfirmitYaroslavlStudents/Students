using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList.TestsForCommands
{
    [TestClass]
    public class TestsForAddCommand
    {
        [TestMethod]
        public void CorrectCreateIdWhenListEntry()
        {
            var tasks = new List<Task>();

            var command = new AddCommand { NewTask = new Task { Text = "war" } };
            command.PerformCommand(tasks);

            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual("[1] war", tasks[0].ToString());
        }

        [TestMethod]
        public void CorrectPerformCommand()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress, TaskId = 33 } };

            var command = new AddCommand{NewTask = new Task{Text = "war"} };
            command.PerformCommand(tasks);

            Assert.AreEqual(2, tasks.Count);
            Assert.AreEqual("[33] world ○", tasks[0].ToString());
            Assert.AreEqual("[34] war",tasks[1].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionValidateWithLongTask()
        {
            var command = new AddCommand{NewTask = new Task{Text = new string('o', 51) } };
            command.PerformCommand(new List<Task>());
        }
    }
}