using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTODO.Controllers;
using MyTODO;
namespace ToDoTest
{
    [TestClass]
    public class ToDoArgsTest
    {
        [TestMethod]
        public void AddTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);

            todoArgs.WorkWithArgs("add \"new item\"".Split());

            Assert.AreEqual("new item", list[0].name);
        }

        [TestMethod]
        public void AddEmptyStringThrowsExceptionTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);

            Assert.ThrowsException<ArgumentException>(() => todoArgs.WorkWithArgs("add \"\"".Split()));

            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void AddWithoutStringThrowsExceptionTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);

            Assert.ThrowsException<ArgumentException>(() => todoArgs.WorkWithArgs("add".Split()));

            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void ChangeNameTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            todoArgs.WorkWithArgs("-cn 0 \"not new\"".Split());

            Assert.AreEqual("not new", list[0].name);
        }

        [TestMethod]
        public void ChangeNameWithoutArgsDontWorkTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            todoArgs.WorkWithArgs("-cn".Split());

            Assert.AreEqual("new item", list[0].name);
        }

        [TestMethod]
        public void ChangeNameToEmptyStringThrowsExceptionTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            Assert.ThrowsException<ArgumentException>(() => todoArgs.WorkWithArgs("-cn 0 \"\"".Split()));

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void ChangeNameWithoutStringDontWorkTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            todoArgs.WorkWithArgs("-cn 0".Split());

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void ChangeWithoutIndexDontWorkTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            todoArgs.WorkWithArgs("-cn \"not new\"".Split());

            Assert.AreEqual("new item", list[0].name);
        }

        [TestMethod]
        public void ChangeWithIndexLessThan0DontWorkTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            todoArgs.WorkWithArgs("-cn -1 \"not new\"".Split());

            Assert.AreEqual("new item", list[0].name);
        }

        [TestMethod]
        public void ChangeWithIndexMoreThanAmountOfItemsDontWorkTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            todoArgs.WorkWithArgs("-cn 2 \"not new\"".Split());

            Assert.AreEqual("new item", list[0].name);
        }

        [TestMethod]
        public void CompleteTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            todoArgs.WorkWithArgs("-co 0".Split());

            Assert.IsTrue(list[0].completed);
        }

        [TestMethod]
        public void CompleteWithoutArgsDontWorkTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            todoArgs.WorkWithArgs("-co".Split());

            Assert.IsFalse(list[0].completed);
        }

        [TestMethod]
        public void CompleteWithIndexLessThan0DontWorkTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            todoArgs.WorkWithArgs("-co -1".Split());

            Assert.IsFalse(list[0].completed);
        }

        [TestMethod]
        public void CompleteWithIndexMoreThanAmountOfItemsDontWorkTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            todoArgs.WorkWithArgs("-co 2".Split());

            Assert.IsFalse(list[0].completed);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            todoArgs.WorkWithArgs("-d 0".Split());

            Assert.IsTrue(list[0].deleted);
        }

        [TestMethod]
        public void DeleteWithoutArgsDontWorkTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            todoArgs.WorkWithArgs("-d".Split());

            Assert.IsFalse(list[0].deleted);
        }

        [TestMethod]
        public void DeleteWithIndexLessThan0DontWorkTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            todoArgs.WorkWithArgs("-d -1".Split());

            Assert.IsFalse(list[0].deleted);
        }

        [TestMethod]
        public void DeleteWithIndexMoreThanAmountOfItemsDontWorkTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);
            todoArgs.WorkWithArgs("-a \"new item\"".Split());

            todoArgs.WorkWithArgs("-d 2".Split());

            Assert.IsFalse(list[0].deleted);
        }

        [TestMethod]
        public void AddChangeCompleteDeleteTest()
        {
            var list = new ToDoList();
            var todoArgs = new ToDoArgs(list);

            todoArgs.WorkWithArgs("-a \"new item\" -cn 0 \"not new\" -co 0 -d 0".Split());

            Assert.AreEqual("not new", list[0].name);
            Assert.IsTrue(list[0].completed);
            Assert.IsTrue(list[0].deleted);
        }
    }
}
