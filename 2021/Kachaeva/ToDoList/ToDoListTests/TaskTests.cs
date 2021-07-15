using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoListProject;

namespace ToDoListTests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void TaskIsCorrectAfterCreation()
        {
            var task = new Task("wash dishes");

            Assert.AreEqual("wash dishes", task.text);
            Assert.AreEqual(false, task.isDone);
        }

        [TestMethod]
        public void TextIsCorrectAfterChange()
        {
            var task = new Task("wash dishes");

            task.ChangeText("clean the room");

            Assert.AreEqual("clean the room", task.text);
        }

        [TestMethod]
        public void StatusIsCorrectAfterChange()
        {
            var task = new Task("wash dishes");

            task.ChangeStatus();

            Assert.AreEqual(true, task.isDone);
        }
    }
}
