using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoListProject;

namespace ToDoListTests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void TextIsCorrectAfterChange()
        {
            var task = new Task("wash dishes");

            task.ChangeText("clean the room");

            Assert.AreEqual("clean the room", task.Text);
        }

        [TestMethod]
        public void StatusIsCorrectAfterChange()
        {
            var task = new Task("wash dishes");

            task.ChangeStatus();

            Assert.AreEqual(true, task.IsDone);
        }

        [TestMethod]
        public void ToStringReturnsCorrectValue()
        {
            var task = new Task("wash dishes");

            Assert.AreEqual("wash dishes  [ ]", task.ToString());
        }
    }
}
