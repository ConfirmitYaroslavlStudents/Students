using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTODO;
using Newtonsoft.Json;
using ToDoHost.Controllers;

namespace ToDoTest
{
    [TestClass]
    public class ToDoApiTest
    {
        [TestMethod]
        public void GettingElement()
        {
            var todo = new ToDoList()
            {
                new ToDoItem("A")
            };
            var controller = new ToDoListController(null) { Todo = todo };

            var result = controller.GetItem(0);

            Assert.AreEqual("A", result.Name);
        }

        [TestMethod]
        public void GettingElementWithIndexLessThanZero()
        {
            var todo = new ToDoList()
            {
                new ToDoItem("A")
            };
            var controller = new ToDoListController(null) { Todo = todo };

            var result = controller.GetItem(-1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GettingElementWithIndexMoreThanAmount()
        {
            var todo = new ToDoList()
            {
                new ToDoItem("A")
            };
            var controller = new ToDoListController(null) {Todo = todo};

            var result = controller.GetItem(1);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void PostingElement()
        {
            var item = new ToDoItem("bill", true, true);
            var serealized = JsonConvert.SerializeObject(item);
            var todo = new ToDoList()
            {
                new ToDoItem("A")
            };
            var controller = new ToDoListController(null) { Todo = todo };

            var result = controller.PostItem(serealized);

            Assert.AreEqual("Post Completed", result);
            Assert.AreEqual(todo[1].Name, item.Name);
            Assert.AreEqual(todo[1].Completed, item.Completed);
            Assert.AreEqual(todo[1].Deleted, item.Deleted);
        }

        [TestMethod]
        public void DeletingElement()
        {
            var todo = new ToDoList()
            {
                new ToDoItem("A")
            };
            var controller = new ToDoListController(null) { Todo = todo };

            var result = controller.DeleteItem(0);

            Assert.AreEqual("Delete Completed", result);
            Assert.IsTrue(todo[0].Deleted);
        }

        [TestMethod]
        public void DeletingElementWithIndexLessThanZero()
        {
            var todo = new ToDoList()
            {
                new ToDoItem("A")
            };
            var controller = new ToDoListController(null) { Todo = todo };

            var result = controller.DeleteItem(-1);

            Assert.AreEqual("Delete Failed", result);
        }

        [TestMethod]
        public void DeletingElementWithIndexMoreThanAmount()
        {
            var todo = new ToDoList()
            {
                new ToDoItem("A")
            };
            var controller = new ToDoListController(null) { Todo = todo };

            var result = controller.DeleteItem(1);

            Assert.AreEqual("Delete Failed", result);
        }
    }
}
