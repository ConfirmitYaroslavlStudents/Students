using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ToDoTest
{
    [TestClass]
    public class ToDoListTest
    {
        [TestMethod]
        public void AddNullItem()
        {
            var list = new MyTODO.ToDoList(null);

            Assert.ThrowsException<ArgumentException>(() => list.Add(null));

            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void AddEmptyItem()
        {
            var list = new MyTODO.ToDoList(null);

            Assert.ThrowsException<ArgumentException>(() => list.Add(""));

            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void AddItem()
        {
            var list = new MyTODO.ToDoList(null);

            list.Add("A");
            list.Add("A");
            list.Add("B");

            Assert.AreEqual(list[0].Name,"A");
            Assert.AreEqual(list[1].Name, "A");
            Assert.AreEqual(list[2].Name, "B");
        }
    }
}
