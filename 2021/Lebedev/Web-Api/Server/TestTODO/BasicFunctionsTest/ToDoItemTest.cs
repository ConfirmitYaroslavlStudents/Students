using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTODO;

namespace TodoServerTest.BasicFunctionsTest
{
    [TestClass]
    public class ToDoItemTest
    {
        [TestMethod]
        public void CompleteAndDelete()
        {
            var subject1 = new ToDoItem(0, "A");
            var subject2 = new ToDoItem(0, "A");

            subject1.SetCompletedTrue();
            subject2.SetDeletedTrue();

            Assert.IsTrue((bool)subject1.Completed);
            Assert.IsTrue((bool)subject2.Deleted);
        }

        [TestMethod]
        public void ChangeStateDeny()
        {
            var subject1 = new ToDoItem(0, "A");
            subject1.SetDeletedTrue();

            subject1.SetCompletedTrue();

            Assert.IsTrue((bool)subject1.Deleted);
            Assert.IsFalse((bool)subject1.Completed);
        }

        [TestMethod]
        public void ChangeName()
        {
            var subject1 = new ToDoItem(0, "A");

            subject1.ChangeName("B");

            Assert.AreEqual(subject1.Name, "B");
        }

        [TestMethod]
        public void ChangeNameDeny()
        {
            var subject1 = new ToDoItem(0, "A");
            var subject2 = new ToDoItem(0, "B");

            Assert.ThrowsException<ArgumentException> (() => subject1.ChangeName(""));
            Assert.ThrowsException<ArgumentException>(() => subject2.ChangeName(null));

            Assert.AreEqual(subject1.Name, "A");
            Assert.AreEqual(subject2.Name, "B");
        }
    }
}
