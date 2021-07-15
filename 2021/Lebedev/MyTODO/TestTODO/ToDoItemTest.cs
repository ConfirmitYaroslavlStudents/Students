using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ToDoTest
{
    [TestClass]
    public class ToDoItemTest
    {
        [TestMethod]
        public void CompleteAndDelete()
        {
            MyTODO.ToDoItem subject1, subject2;
            subject1 = new MyTODO.ToDoItem("A");
            subject2 = new MyTODO.ToDoItem("A");

            subject1.Complete();
            subject2.Delete();

            Assert.AreEqual((int)MyTODO.States.completed, subject1.State);
            Assert.AreEqual((int)MyTODO.States.deleted, subject2.State);
        }

        [TestMethod]
        public void ChangeStateDeny()
        {
            MyTODO.ToDoItem subject1;
            subject1 = new MyTODO.ToDoItem("A");
            subject1.Delete();

            subject1.Complete();

            Assert.AreEqual((int)MyTODO.States.deleted, subject1.State);
        }

        [TestMethod]
        public void ChangeName()
        {
            MyTODO.ToDoItem subject1;
            subject1 = new MyTODO.ToDoItem("A");

            subject1.ChangeName("B");

            Assert.AreEqual(subject1.Name, "B");
        }

        [TestMethod]
        public void ChangeNameDeny()
        {
            MyTODO.ToDoItem subject1, subject2;
            subject1 = new MyTODO.ToDoItem("A");
            subject2 = new MyTODO.ToDoItem("B");

            Assert.ThrowsException<ArgumentException> (()=>subject1.ChangeName(""));
            Assert.ThrowsException<ArgumentException>(() => subject2.ChangeName(null));

            Assert.AreEqual(subject1.Name, "A");
            Assert.AreEqual(subject2.Name, "B");
        }
    }
}
