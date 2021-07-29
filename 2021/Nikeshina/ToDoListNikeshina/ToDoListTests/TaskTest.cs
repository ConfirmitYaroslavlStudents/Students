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
            var task = new Task("clean room", true);

            Assert.AreEqual("clean room True", task.ToString());
        }
        [TestMethod]
        public void printAfterChangeStatus()
        {
            var task = new Task("clean room", true);

            task.ChangeStatus();

            Assert.AreEqual("clean room False", task.ToString());
        }

        [TestMethod]
        public void PrintAfterChangeStatus()
        {
            var task = new Task("clean room", true);

            task.ChangeName("not clean room");

            Assert.AreEqual("not clean room True", task.ToString());
        }
    }
}
