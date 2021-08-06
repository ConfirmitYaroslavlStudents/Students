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
        public void CorrectPerformCommand()
        {
            var task1 = new Task {Text = "world",Status = StatusTask.ToDo};
            var task2 = new Task() {Text = "war"};
            var tasks = new List<Task> { task1};

            var command = new AddCommand {NewTask = task2, Tasks = tasks};
            command.PerformCommand();

            Assert.AreEqual(2, tasks.Count);
            Assert.AreEqual(task1.ToString(),tasks[0].ToString());
            Assert.AreEqual(task2.ToString(),tasks[1].ToString());
        }
    }
}