using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoListNikeshina;

namespace ToDoListTests
{
    [TestClass]
    public class ListTest
    {
        [TestMethod]
        public void AddNewItem()
        {
            var list = new ToDoList(new List<Task>());
            var task = new Task("Win the competitions", 0);
            var testList = new List<Task> { new Task("Win the competitions",StatusOfTask.Todo) };

            list.Add(task);

            Assert.AreEqual(1, list.Count());
            CollectionAssert.AreEqual(testList, list.GetListOfTasks());
        }

        [TestMethod]
        public void CountIsZeroAfterRemove()
        {
            var list = new ToDoList(new List<Task>());

            list.Add(new Task("Win the competitions", StatusOfTask.Todo));
            list.Delete(1);

            Assert.AreEqual(0, list.Count());
        }

        [TestMethod]
        public void CountIsZeroAfterListCreating()
        {
            var list = new ToDoList(new List<Task>());

            Assert.AreEqual(0, list.Count());
            CollectionAssert.AreEqual(new List<Task>(), list.GetListOfTasks());
        }

        [TestMethod]
        public void CompareToDoAfterDifferentOperations()
        {
            var list = new ToDoList(new List<Task>());
            list.Add(new Task("Buy car", StatusOfTask.Todo));
            list.Add(new Task("Go to shop", StatusOfTask.Todo));
            list.ChangeStatus(2);
            list.Edit(1, "Pass exams");
            var checkingList = new List<Task> { new Task("Pass exams",StatusOfTask.Todo), new Task("Go to shop", StatusOfTask.InProgress) };

            CollectionAssert.AreEqual(list.GetListOfTasks(), checkingList);
        }
    }
}
