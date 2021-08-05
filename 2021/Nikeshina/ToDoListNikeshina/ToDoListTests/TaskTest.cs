using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoListNikeshina;

namespace ToDoListTests
{
    [TestClass]
    public class TaskTest
    {
        [TestMethod]
        public void PrintTask()
        {
            var task = new Task("clean room", 0);

            Assert.AreEqual("clean room Todo", task.StringFormat());
        }
        [TestMethod]
        public void printAfterChangeStatus()
        {
            var task = new Task("clean room",0);

            task.ChangeStatus();

            Assert.AreEqual("clean room InProgress", task.StringFormat());
        }

        [TestMethod]
        public void PrintAfterChangeStatus()
        {
            var task = new Task("clean room", 2);

            task.ChangeName("not clean room");

            Assert.AreEqual("not clean room Done", task.StringFormat());
        }

        [TestMethod]
        public void CompareTasks()
        {
            var task1 = new Task("do homework", 0);
            var task2 = new Task("do homework", 0);
            Assert.AreEqual(task1, task2);
        }
    }
}
