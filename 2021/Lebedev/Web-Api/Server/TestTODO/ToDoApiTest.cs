using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTODO;
using Newtonsoft.Json;
using System;
using System.Net;
using ToDoHost.Controllers;

namespace ToDoTest
{
    class BadLogger : ILogger<ToDoListController>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
        }
    }
    [TestClass]
    public class ToDoApiTest
    {
        [TestMethod]
        public void GettingElement()
        {
            var todo = new ToDoList()
            {
                new ToDoItem(0, "A")
            };
            var controller = new ToDoListController(new BadLogger()) { Todo = todo };

            var result = controller.GetItem(0);

            Assert.AreEqual("A", result.name);
        }

        [TestMethod]
        public void GettingElementWithIndexLessThanZero()
        {
            var todo = new ToDoList()
            {
                new ToDoItem(0, "A")
            };
            var controller = new ToDoListController(new BadLogger()) { Todo = todo };

            Assert.ThrowsException<WebException>(() => controller.GetItem(-1));
        }

        [TestMethod]
        public void GettingElementWithIndexMoreThanAmount()
        {
            var todo = new ToDoList()
            {
                new ToDoItem(0, "A")
            };
            var controller = new ToDoListController(new BadLogger()) {Todo = todo};

            Assert.ThrowsException<WebException>(() => controller.GetItem(1));
        }

        [TestMethod]
        public void PostingElement()
        {
            var item = new ToDoItem(0,"bill");
            var todo = new ToDoList()
            {
                new ToDoItem(0,"A")
            };
            var controller = new ToDoListController(new BadLogger()) { Todo = todo };

            var result = controller.PostItem("bill");

            Assert.AreEqual("Post Completed", result);
            Assert.AreEqual(todo[1].name, item.name);
            Assert.AreEqual(todo[1].completed, item.completed);
            Assert.AreEqual(todo[1].deleted, item.deleted);
        }

        [TestMethod]
        public void DeletingElement()
        {
            var todo = new ToDoList()
            {
                new ToDoItem(0, "A")
            };
            var controller = new ToDoListController(new BadLogger()) { Todo = todo };

            var result = controller.DeleteItem(0);

            Assert.AreEqual("Delete Completed", result);
            Assert.IsTrue(todo[0].deleted);
        }

        [TestMethod]
        public void DeletingElementWithIndexLessThanZero()
        {
            var todo = new ToDoList()
            {
                new ToDoItem(0, "A")
            };
            var controller = new ToDoListController(new BadLogger()) { Todo = todo };

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => controller.DeleteItem(-1));
        }

        [TestMethod]
        public void DeletingElementWithIndexMoreThanAmount()
        {
            var todo = new ToDoList()
            {
                new ToDoItem(0, "A")
            };
            var controller = new ToDoListController(new BadLogger()) { Todo = todo };

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => controller.DeleteItem(1));
        }
    }
}
