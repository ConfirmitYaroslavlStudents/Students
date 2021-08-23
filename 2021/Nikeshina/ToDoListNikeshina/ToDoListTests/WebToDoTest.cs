using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoListNikeshina;
using WebToDoApp;
using WebToDoApp.Controllers;

namespace ToDoListTests
{
    [TestClass]
    public class WebToDoTest
    {
        ToDoList list = new ToDoList(new List<Task>(), -1);
        FileManager fileManager = new FileManager();

        [TestMethod]
        public void CheckAddingNewItem()
        {
            var controller = new ToDoController(fileManager, list);
            controller.AddToDoItem("Wash dishes");
            controller.AddToDoItem("clean the room");

            CollectionAssert.AreEqual(controller.GetToDoList(), new List<Task> { new Task("Wash dishes", StatusOfTask.Todo), new Task("clean the room", StatusOfTask.Todo) });
        }

        [TestMethod]
        public void CheckDelete()
        {
            var controller = new ToDoController(fileManager, list);
            controller.AddToDoItem("Wash dishes");
            controller.AddToDoItem("clean the room");

            controller.DeleteToDoItem(0);
            CollectionAssert.AreEqual(controller.GetToDoList(), new List<Task> {new Task("clean the room", StatusOfTask.Todo) });
        }

        [TestMethod]
        public void NotaddTaskWithIncorrectDescription()
        {
            var controller = new ToDoController(fileManager, list);
            controller.AddToDoItem("Wash dishes jhbjh jhbjh  hbjhb jkjbjgv h JJHGHJKJL UYGHGV HGVHJj hgj gh jhbjhv hfc hjhgvg fbnb fcfc jh vhdgv ahndj hgv");
            CollectionAssert.AreEqual(controller.GetToDoList(), new List<Task>());
        }

        [TestMethod]
        public void TryToAddSomeTasksInProgress()
        {
            var controller = new ToDoController(fileManager, list);
            for(int i=0;i<4;i++)
            {
                controller.AddToDoItem("i");
                controller.ChangeToDoItem(i, new Task(null, StatusOfTask.InProgress));
            }

            CollectionAssert.AreEqual(controller.GetToDoList(), new List<Task>() { new Task("i",StatusOfTask.InProgress), new Task("i", StatusOfTask.InProgress) , new Task("i", StatusOfTask.InProgress) , new Task("i", StatusOfTask.Todo)});
        }

        [TestMethod]
        public void TryUseIncorrectId()
        {
            var controller = new ToDoController(fileManager, list);
            controller.AddToDoItem("id is 0");
            controller.DeleteToDoItem(1);
            CollectionAssert.AreEqual(controller.GetToDoList(), new List<Task> { new Task("id is 0", StatusOfTask.Todo) });
        }

        [TestMethod]
        public void TryToUseIncorrectnameDuringEdit()
        {
            var controller = new ToDoController(fileManager, list);
            controller.AddToDoItem("id is 0");
            controller.ChangeToDoItem(0, new Task("", StatusOfTask.InProgress));
            CollectionAssert.AreEqual(controller.GetToDoList(), new List<Task> { new Task("id is 0", StatusOfTask.Todo) });
        }
    }
}
