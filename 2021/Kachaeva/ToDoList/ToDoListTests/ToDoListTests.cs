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

            toDoList.Add("wash dishes");
            toDoList.Add("clean the room");

            Assert.AreEqual(2, toDoList.Count);
            Assert.AreEqual("wash dishes", toDoList[0].text);
            Assert.AreEqual("clean the room", toDoList[1].text);
        }

        [TestMethod]
        public void TasksDoNotExistAfterRemove()
        {
            var toDoList = new ToDoList();

            toDoList.Add("wash dishes");
            toDoList.Add("clean the room");
            toDoList.Remove(0);

            Assert.AreEqual(1, toDoList.Count);
            Assert.AreEqual("clean the room", toDoList[0].text);
        }


    }
}
