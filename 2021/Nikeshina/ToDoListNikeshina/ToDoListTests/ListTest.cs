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
            var list = new ToDoList(new List<Task>(),-1);
            var task = new Task("Win the competitions", 0);
            var testList = new List<Task> { new Task("Win the competitions",StatusOfTask.Todo) };

            list.Add("Win the competitions", StatusOfTask.Todo);

            Assert.AreEqual(1, list.Count());
            CollectionAssert.AreEqual(testList, list.GetListOfTasks());
        }

        [TestMethod]
        public void CountIsZeroAfterRemove()
        {
            var list = new ToDoList(new List<Task>(),-1);

            list.Add("Win the competitions", StatusOfTask.Todo);
            list.Delete(0);

            Assert.AreEqual(0, list.Count());
        }

        [TestMethod]
        public void CountIsZeroAfterListCreating()
        {
            var list = new ToDoList(new List<Task>(),-1);

            Assert.AreEqual(0, list.Count());
            CollectionAssert.AreEqual(new List<Task>(), list.GetListOfTasks());
        }

        [TestMethod]
        public void CompareToDoAfterDifferentOperations()
        {
            var list = new ToDoList(new List<Task>(),-1);
            list.Add("Buy car", StatusOfTask.Todo);
            list.Add("Go to shop", StatusOfTask.Todo);
            list.ChangeStatus(1);
            list.Edit(0, "Pass exams");
            var checkingList = new List<Task> { new Task("Pass exams",StatusOfTask.Todo), new Task("Go to shop", StatusOfTask.InProgress) };

            CollectionAssert.AreEqual(list.GetListOfTasks(), checkingList);
        }
    }
}
