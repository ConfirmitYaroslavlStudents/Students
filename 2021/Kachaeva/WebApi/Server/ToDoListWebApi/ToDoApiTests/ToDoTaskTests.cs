using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoApiDependencies;

namespace ToDoApiTests
{
    [TestClass]
    public class ToDoTaskTests
    {
        [TestMethod]
        public void ToStringReturnsCorrectValueIfTaskIsNotDone()
        {
            var toDoTask = new ToDoTask("wash dishes", false);

            Assert.AreEqual("0. wash dishes  [ ]", toDoTask.ToString());
        }

        [TestMethod]
        public void ToStringReturnsCorrectValueIfTaskIsDone()
        {
            var toDoTask = new ToDoTask("wash dishes", true);

            Assert.AreEqual("0. wash dishes  [v]", toDoTask.ToString());
        }
    }
}
