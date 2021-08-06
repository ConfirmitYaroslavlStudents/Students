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
            var task = new Task { Text = "I ToDo", Status = StatusTask.ToDo};
            Assert.AreEqual("I ToDo",task.ToString());
        }

        [TestMethod]
        public void CorrectInProgressStatusToString()
        {
            var task = new Task { Text = "I InProgress", Status = StatusTask.IsProgress };
            Assert.AreEqual("I InProgress []", task.ToString());
        }

        [TestMethod]
        public void CorrectDoneStatusToString()
        {
            var task = new Task { Text = "I Done", Status = StatusTask.Done};
            Assert.AreEqual("I Done [X]", task.ToString());
        }
    }
}
