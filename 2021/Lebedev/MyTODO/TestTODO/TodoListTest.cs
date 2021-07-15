using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ToDoTest
{
    [TestClass]
    public class TodoListTest
    {
        [TestMethod]
        public void AddNullItem()
        {
            MyTODO.ToDoList list = new MyTODO.ToDoList(null);

            Assert.ThrowsException<ArgumentException>(() => list.Add(null));

            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void AddEmptyItem()
        {
            MyTODO.ToDoList list = new MyTODO.ToDoList(null);

            Assert.ThrowsException<ArgumentException>(() => list.Add(""));

            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void AddItem()
        {
            MyTODO.ToDoList list = new MyTODO.ToDoList(null);

            list.Add("A");
            list.Add("A");
            list.Add("B");

            Assert.AreEqual(list[0].Name,"A");
            Assert.AreEqual(list[1].Name, "A");
            Assert.AreEqual(list[2].Name, "B");
        }
    }
}
