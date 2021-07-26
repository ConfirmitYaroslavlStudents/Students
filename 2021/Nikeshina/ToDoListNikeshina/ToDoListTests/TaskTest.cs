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
            var task = new Task("wash hands", true);

            Assert.AreEqual("wash hands True", task.ToString());
        }
        [TestMethod]
        public void printAfterChangeStatus()
        {
            var task = new Task("wash hands", true);

            task.ChangeStatus();

            Assert.AreEqual("wash hands False", task.ToString());
        }

        [TestMethod]
        public void PrintAfterChangeStatus()
        {
            var task = new Task("wash hands", true);

            task.ChangeName("not wash hands");

            Assert.AreEqual("not wash hands True", task.ToString());
        }
    }
}
