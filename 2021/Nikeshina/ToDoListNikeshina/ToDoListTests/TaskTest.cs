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
            var task = new Task("clean room", StatusOfTask.Todo);

            Assert.AreEqual("0 clean room Todo", task.StringFormat());
        }
        [TestMethod]
        public void printAfterChangeStatus()
        {
            var task = new Task("clean room", StatusOfTask.Todo);

            task.ChangeStatus();

            Assert.AreEqual("0 clean room InProgress", task.StringFormat());
        }

        [TestMethod]
        public void PrintAfterChangeStatus()
        {
            var task = new Task("clean room", StatusOfTask.Done);

            task.ChangeName("not clean room");

            Assert.AreEqual("0 not clean room Done", task.StringFormat());
        }

        [TestMethod]
        public void CompareTasks()
        {
            var task1 = new Task("do homework", StatusOfTask.Todo);
            var task2 = new Task("do homework", StatusOfTask.Todo);
            Assert.AreEqual(task1, task2);
        }
    }
}
