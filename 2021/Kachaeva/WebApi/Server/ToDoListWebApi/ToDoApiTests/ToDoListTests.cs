using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;

namespace ToDoListTests
{
    [TestClass]
    public class ToDoListTests
    {
        [TestMethod]
        public void ToStringReturnsCorrectValue()
        {
            var toDoList = new ToDoList();

            toDoList.Add(new Task("wash dishes", false));
            toDoList.Add(new Task("clean the room", true));

            Assert.AreEqual("1. wash dishes  [ ]\r\n2. clean the room  [v]\r\n", toDoList.ToString());
        }
    }
}
