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

            list.Add(new Task("Win the competitions", false));

            Assert.AreEqual(1, list.Count());
        }

        [TestMethod]
        public void CountIsZeroAfterRemove()
        {
            var list = new ToDoList(new List<Task>());

            list.Add(new Task("Win the competitions", false));
            list.Delete(1);

            Assert.AreEqual(0, list.Count());
        }

        [TestMethod]
        public void CountIsZeroAfterListCreating()
        {
            var list = new ToDoList(new List<Task>());

            Assert.AreEqual(0, list.Count());
        }
    }
}
