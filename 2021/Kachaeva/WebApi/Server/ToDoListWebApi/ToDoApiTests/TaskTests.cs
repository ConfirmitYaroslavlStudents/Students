using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace ToDoListTests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void ToStringReturnsCorrectValueIfTaskIsNotDone()
        {
            var task = new Task("wash dishes");

            Assert.AreEqual("wash dishes  [ ]", task.ToString());
        }

        [TestMethod]
        public void ToStringReturnsCorrectValueIfTaskIsDone()
        {
            var task = new Task("wash dishes", true);

            Assert.AreEqual("wash dishes  [v]", task.ToString());
        }
    }
}
