using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoListProject;

namespace ToDoListTests
{
    [TestClass]
    public class ToDoListTests
    {
        [TestMethod]
        public void CountIsZeroAfterToDoListCreation()
        {
            var toDoList = new ToDoList();

            Assert.AreEqual(0, toDoList.Count);
        }

        [TestMethod]
        public void TasksExistAfterAddition()
        {
            var toDoList = new ToDoList();

            toDoList.Add(new Task("wash dishes"));
            toDoList.Add(new Task("clean the room"));

            Assert.AreEqual(2, toDoList.Count);
            Assert.AreEqual("wash dishes", toDoList[0].Text);
            Assert.AreEqual("clean the room", toDoList[1].Text);
        }

        [TestMethod]
        public void TasksDoNotExistAfterRemove()
        {
            var toDoList = new ToDoList();

            toDoList.Add(new Task("wash dishes"));
            toDoList.Add(new Task("clean the room"));
            toDoList.Remove(0);

            Assert.AreEqual(1, toDoList.Count);
            Assert.AreEqual("clean the room", toDoList[0].Text);
        }

        [TestMethod]
        public void ToStringReturnsCorrectValue()
        {
            var toDoList = new ToDoList();

            toDoList.Add(new Task("wash dishes"));
            toDoList.Add(new Task("clean the room"));
            toDoList[1].ChangeStatus();

            Assert.AreEqual("1. wash dishes  [ ]\r\n2. clean the room  [v]\r\n", toDoList.ToString());
        }
    }
}
