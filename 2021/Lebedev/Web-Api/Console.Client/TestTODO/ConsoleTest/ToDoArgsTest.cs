using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTODO;
using ToDoClient.Controllers;

namespace TodoClientTest.ConsoleTest
{
    [TestClass]
    public class ToDoArgsTest
    {
        private ToDoList _list;
        private ToDoArgs _todoArgs;
        [TestInitialize]
        public void Initializer()
        {
            _list = new ToDoList();
            _todoArgs = new ToDoArgs(new TestConnector(_list));
        }

        [TestMethod]
        public void AddTest()
        {

            _todoArgs.WorkWithArgs("add \"new item\"".Split());

            Assert.AreEqual("new item", _list[0].Name);
        }

        [TestMethod]
        public void AddEmptyStringThrowsExceptionTest()
        {

            Assert.ThrowsException<ArgumentException>(() => _todoArgs.WorkWithArgs("add \"\"".Split()));

            Assert.AreEqual(0, _list.Count);
        }

        [TestMethod]
        public void AddWithoutStringThrowsExceptionTest()
        {

            Assert.ThrowsException<ArgumentException>(() => _todoArgs.WorkWithArgs("add".Split()));

            Assert.AreEqual(0, _list.Count);
        }

        [TestMethod]
        public void ChangeNameTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            _todoArgs.WorkWithArgs("-cn 0 \"not new\"".Split());

            Assert.AreEqual("not new", _list[0].Name);
        }

        [TestMethod]
        public void ChangeNameWithoutArgsDontWorkTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            _todoArgs.WorkWithArgs("-cn".Split());

            Assert.AreEqual("new item", _list[0].Name);
        }

        [TestMethod]
        public void ChangeNameToEmptyStringThrowsExceptionTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            Assert.ThrowsException<ArgumentException>(() => _todoArgs.WorkWithArgs("-cn 0 \"\"".Split()));

            Assert.AreEqual(1, _list.Count);
        }

        [TestMethod]
        public void ChangeNameWithoutStringDontWorkTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            _todoArgs.WorkWithArgs("-cn 0".Split());

            Assert.AreEqual(1, _list.Count);
        }

        [TestMethod]
        public void ChangeWithoutIndexDontWorkTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            _todoArgs.WorkWithArgs("-cn \"not new\"".Split());

            Assert.AreEqual("new item", _list[0].Name);
        }

        [TestMethod]
        public void ChangeWithIndexLessThan0DontWorkTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            _todoArgs.WorkWithArgs("-cn -1 \"not new\"".Split());

            Assert.AreEqual("new item", _list[0].Name);
        }

        [TestMethod]
        public void ChangeWithIndexMoreThanAmountOfItemsDontWorkTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            _todoArgs.WorkWithArgs("-cn 2 \"not new\"".Split());

            Assert.AreEqual("new item", _list[0].Name);
        }

        [TestMethod]
        public void CompleteTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            _todoArgs.WorkWithArgs("-co 0".Split());

            Assert.IsTrue(_list[0].Completed);
        }

        [TestMethod]
        public void CompleteWithoutArgsDontWorkTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            _todoArgs.WorkWithArgs("-co".Split());

            Assert.IsFalse(_list[0].Completed);
        }

        [TestMethod]
        public void CompleteWithIndexLessThan0DontWorkTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            _todoArgs.WorkWithArgs("-co -1".Split());

            Assert.IsFalse(_list[0].Completed);
        }

        [TestMethod]
        public void CompleteWithIndexMoreThanAmountOfItemsDontWorkTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            _todoArgs.WorkWithArgs("-co 2".Split());

            Assert.IsFalse(_list[0].Completed);
        }

        [TestMethod]
        public void DeleteTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            _todoArgs.WorkWithArgs("-d 0".Split());

            Assert.IsTrue(_list[0].Deleted);
        }

        [TestMethod]
        public void DeleteWithoutArgsDontWorkTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            _todoArgs.WorkWithArgs("-d".Split());

            Assert.IsFalse(_list[0].Deleted);
        }

        [TestMethod]
        public void DeleteWithIndexLessThan0DontWorkTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            _todoArgs.WorkWithArgs("-d -1".Split());

            Assert.IsFalse(_list[0].Deleted);
        }

        [TestMethod]
        public void DeleteWithIndexMoreThanAmountOfItemsDontWorkTest()
        {
            _todoArgs.WorkWithArgs("-a \"new item\"".Split());

            _todoArgs.WorkWithArgs("-d 2".Split());

            Assert.IsFalse(_list[0].Deleted);
        }

        [TestMethod]
        public void AddChangeCompleteDeleteTest()
        {

            _todoArgs.WorkWithArgs("-a \"new item\" -cn 0 \"not new\" -co 0 -d 0".Split());

            Assert.AreEqual("not new", _list[0].Name);
            Assert.IsTrue(_list[0].Completed);
            Assert.IsTrue(_list[0].Deleted);
        }
    }
}
