using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTODO;

namespace ToDoTest
{
    [TestClass]
    public class ToDoItemTest
    {
        [TestMethod]
        public void CompleteAndDelete()
        {
            var subject1 = new ToDoItem("A");
            var subject2 = new ToDoItem("A");

            subject1.Complete();
            subject2.Delete();

            Assert.IsTrue(subject1.Completed);
            Assert.IsTrue(subject2.Deleted);
        }

        [TestMethod]
        public void ChangeStateDeny()
        {
            var subject1 = new ToDoItem("A");
            subject1.Delete();

            subject1.Complete();

            Assert.IsTrue(subject1.Deleted);
        }

        [TestMethod]
        public void ChangeName()
        {
            var subject1 = new ToDoItem("A");

            subject1.ChangeName("B");

            Assert.AreEqual(subject1.Name, "B");
        }

        [TestMethod]
        public void ChangeNameDeny()
        {
            var subject1 = new ToDoItem("A");
            var subject2 = new ToDoItem("B");

            Assert.ThrowsException<ArgumentException> (() => subject1.ChangeName(""));
            Assert.ThrowsException<ArgumentException>(() => subject2.ChangeName(null));

            Assert.AreEqual(subject1.Name, "A");
            Assert.AreEqual(subject2.Name, "B");
        }
    }
}
