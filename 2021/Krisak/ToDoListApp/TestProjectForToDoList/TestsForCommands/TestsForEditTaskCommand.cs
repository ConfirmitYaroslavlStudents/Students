using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.Commands;

namespace TestsForToDoList.TestsForCommands
{
    [TestClass]
    public class TestsForEditTaskCommand
    {
        [TestMethod]
        public void ListNotChangeIfParametersNotSet()
        {
            var tasks = new List<Task> {new Task {Text = "IOne"}};
            var command = new EditTaskCommand();
            var result = command.PerformCommand(new List<Task>(tasks));
            CollectionAssert.AreEqual(tasks,result);
        }

        [TestMethod]
        public void CorrectPerformCommand()
        {
            var tasks = new List<Task> { new Task { Text = "world", Status = StatusTask.IsProgress } };

            var command = new EditTaskCommand
            {
                EditTextCommand = new EditTextCommand(){Text = "war", TaskId = 0},
                ToggleStatusCommand = new ToggleStatusCommand(){Status = StatusTask.Done, TaskId = 0}
            };
            var result = command.PerformCommand(new List<Task>(tasks));

            Assert.AreEqual("[0] war ●", result[0].ToString());
        }
    }
}