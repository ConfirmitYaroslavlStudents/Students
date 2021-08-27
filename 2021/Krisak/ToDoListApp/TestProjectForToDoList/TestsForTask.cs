using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;

namespace TestProjectForToDoLibrary
{

    [TestClass]
    public class TestsForTask
    {
        [TestMethod]
        public void CorrectToDoStatusToString()
        {
            var task = new Task { Text = "I ToDo", Status = StatusTask.ToDo, TaskId = 1 };
            Assert.AreEqual("[1] I ToDo", task.ToString());
        }

        [TestMethod]
        public void CorrectInProgressStatusToString()
        {
            var task = new Task { Text = "I InProgress", Status = StatusTask.IsProgress, TaskId = 1 };
            Assert.AreEqual("[1] I InProgress ○", task.ToString());
        }

        [TestMethod]
        public void CorrectDoneStatusToString()
        {
            var task = new Task { Text = "I Done", Status = StatusTask.Done, TaskId = 1 };
            Assert.AreEqual("[1] I Done ●", task.ToString());
        }
    }
}
