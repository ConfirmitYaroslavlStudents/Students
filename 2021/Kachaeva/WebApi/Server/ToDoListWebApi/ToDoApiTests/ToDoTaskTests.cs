using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoApiDependencies;
using System.Collections.Generic;

namespace ToDoApiTests
{
    [TestClass]
    public class ToDoTaskTests
    {
        [TestMethod]
        public void ToStringReturnsCorrectValueIfTaskIsNotDone()
        {
            var toDoTask = new ToDoTask("wash dishes", false, new List<string> { "home", "important"});

            Assert.AreEqual("0. wash dishes  [ ] home important ", toDoTask.ToString());
        }

        [TestMethod]
        public void ToStringReturnsCorrectValueIfTaskIsDone()
        {
            var toDoTask = new ToDoTask("wash dishes", true, new List<string> { "home", "important" });

            Assert.AreEqual("0. wash dishes  [v] home important ", toDoTask.ToString());
        }
    }
}
