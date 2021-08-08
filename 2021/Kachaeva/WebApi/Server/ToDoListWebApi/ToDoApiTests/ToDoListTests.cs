using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoApiDependencies;

namespace ToDoApiTests
{
    [TestClass]
    public class ToDoListTests
    {
        [TestMethod]
        public void ToStringReturnsCorrectValue()
        {
            var toDoList = new ToDoList {new ToDoTask("wash dishes", false), new ToDoTask("clean the room", true)};

            Assert.AreEqual("1. wash dishes  [ ]\r\n2. clean the room  [v]\r\n", toDoList.ToString());
        }
    }
}
