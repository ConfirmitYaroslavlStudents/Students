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
            var testList = new List<Task> { new Task("Win the competitions",0) };

            list.Add(task);

            Assert.AreEqual(1, list.Count());
            CollectionAssert.AreEqual(testList, list.GetListOfTask());
        }

        [TestMethod]
        public void CountIsZeroAfterRemove()
        {
            var list = new ToDoList(new List<Task>());

            list.Add(new Task("Win the competitions", 0));
            list.Delete(1);

            Assert.AreEqual(0, list.Count());
        }

        [TestMethod]
        public void CountIsZeroAfterListCreating()
        {
            var list = new ToDoList(new List<Task>());

            Assert.AreEqual(0, list.Count());
            CollectionAssert.AreEqual(new List<Task>(), list.GetListOfTask());
        }

        [TestMethod]
        public void CompareToDoAfterDifferentOperations()
        {
            var list = new ToDoList(new List<Task>());
            list.Add(new Task("Buy car", 0));
            list.Add(new Task("Go to shop", 0));
            list.ChangeStatus(2);
            list.Edit(1, "Pass exams");
            var checkingList = new List<Task> { new Task("Pass exams",0), new Task("Go to shop", 1) };

            CollectionAssert.AreEqual(list.GetListOfTask(), checkingList);
        }
    }
}
