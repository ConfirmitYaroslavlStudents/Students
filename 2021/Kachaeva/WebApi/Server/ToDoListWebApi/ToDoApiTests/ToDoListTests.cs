using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoApiDependencies;

namespace ToDoApiTests
{
    [TestClass]
    public class ToDoListTests
    {
        [TestMethod]
        public void ToStringReturnsCorrectValue()
        {
            var toDoList = new ToDoList {new ToDoTask("wash dishes", false, new List<string> { "home", "important" }), new ToDoTask("clean the room", true, new List<string> { "home"}) };

            Assert.AreEqual("1. wash dishes  [ ] home important \r\n2. clean the room  [v] home \r\n", toDoList.ToString());
        }
    }
}
