using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProjectForToDoLibrary;
using ToDoLibrary;
using ToDoLibrary.Loggers;
using TodoWeb;
using TodoWeb.Controllers;

namespace TestProjectForTodoWeb
{
    [TestClass]
    public class TestsForTodoController
    {
        private RequestHandler _requestHandler;
        private WebMediator _mediator;
        private TodoController _todoController;

        [TestMethod]
        public void SuccessfullyPerformeShowTasks()
        {
            var tasks = new List<Task>
                {
                    new Task{Text = "IFirst"},
                    new Task{Text = "ISecond"},
                    new Task {Text = "IThird"}
                };
            _requestHandler = new RequestHandler(new WebLogger(), new FakeSaver(), new FakeLoader(tasks));
            _mediator = new WebMediator(_requestHandler);
            _todoController = new TodoController(_mediator);

            var result = _todoController.ShowTasks().ToList();
            CollectionAssert.AreEqual(tasks, result, new TaskComparer());
        }

        [TestMethod]
        public void SuccessfullyPerformAddNewTask_WithoutStatus()
        {
            var tasks = new List<Task>();

            _requestHandler = new RequestHandler(new WebLogger(), new FakeSaver(), new FakeLoader(tasks));
            _mediator = new WebMediator(_requestHandler);
            _todoController = new TodoController(_mediator);

            var result = (ObjectResult)_todoController.AddNewTask("world",null).Result;

            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual("New task has been successfully added.", result.Value);

            tasks.Add(new Task{TaskId = 1,Text = "world"});
            var resultTasks = _todoController.ShowTasks().ToList();
            CollectionAssert.AreEqual(tasks, resultTasks, new TaskComparer());
        }

        [TestMethod]
        public void SuccessfullyPerformAddNewTask_WithStatus()
        {
            var tasks = new List<Task>();

            _requestHandler = new RequestHandler(new WebLogger(), new FakeSaver(), new FakeLoader(tasks));
            _mediator = new WebMediator(_requestHandler);
            _todoController = new TodoController(_mediator);

            var result = (ObjectResult)_todoController.AddNewTask("world", "2").Result;

            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual("New task has been successfully added.", result.Value);

            tasks.Add(new Task { TaskId = 1, Text = "world" ,Status = StatusTask.Done});
            var resultTasks = _todoController.ShowTasks().ToList();
            CollectionAssert.AreEqual(tasks, resultTasks, new TaskComparer());
        }

        [TestMethod]
        public void WrongPerformAddNewTask_TextNull()
        {
            _requestHandler = new RequestHandler(new WebLogger(), new FakeSaver(), new FakeLoader(new List<Task>()));
            _mediator = new WebMediator(_requestHandler);
            _todoController = new TodoController(_mediator);

            var result = (ObjectResult)_todoController.AddNewTask(null, null).Result;

            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void WrongPerformAddNewTask_WrongStatus()
        {
            var tasks = new List<Task>();

            _requestHandler = new RequestHandler(new WebLogger(), new FakeSaver(), new FakeLoader(tasks));
            _mediator = new WebMediator(_requestHandler);
            _todoController = new TodoController(_mediator);

            var result = (ObjectResult)_todoController.AddNewTask("world", "da").Result;

            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual("New task has been successfully added.", result.Value);

            tasks.Add(new Task { TaskId = 1, Text = "world"});
            var resultTasks = _todoController.ShowTasks().ToList();
            CollectionAssert.AreEqual(tasks, resultTasks, new TaskComparer());
        }

        [TestMethod]
        public void SuccessfullyPerformeDeleteTask()
        {
            var tasks = new List<Task>
                {
                    new Task{Text = "IFirst",TaskId = 1},
                    new Task{Text = "ISecond",TaskId = 2},
                    new Task {Text = "IThird",TaskId = 3}
                };

            _requestHandler = new RequestHandler(new WebLogger(), new FakeSaver(), new FakeLoader(tasks));
            _mediator = new WebMediator(_requestHandler);
            _todoController = new TodoController(_mediator);

            var result = (ObjectResult)_todoController.DeleteTask(2).Result;

            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Task has been successfully deleted.", result.Value);

            tasks.RemoveAt(1);
            var resultTasks = _todoController.ShowTasks().ToList();
            CollectionAssert.AreEqual(tasks, resultTasks, new TaskComparer());
        }

        [TestMethod]
        public void WrongPerformeDeleteTaskWithWrong()
        {
            _requestHandler = new RequestHandler(new WebLogger(), new FakeSaver(), new FakeLoader(new List<Task>()));
            _mediator = new WebMediator(_requestHandler);
            _todoController = new TodoController(_mediator);

            var result = (ObjectResult)_todoController.DeleteTask(20).Result;

            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Task not found with Id 20.", result.Value);
        }

        [TestMethod]
        public void SuccessfullyPerformeEditTask()
        {
            var tasks = new List<Task>
            {
                new Task{Text = "IFirst",TaskId = 1},
                new Task{Text = "ISecond",TaskId = 2},
                new Task {Text = "IThird",TaskId = 3}
            };

            _requestHandler = new RequestHandler(new WebLogger(), new FakeSaver(), new FakeLoader(tasks));
            _mediator = new WebMediator(_requestHandler);
            _todoController = new TodoController(_mediator);

            var result = (ObjectResult)_todoController.EditTask("IFourth", "1", 2).Result;

            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Task has been successfully changed.", result.Value);

            tasks[1] = (new Task { TaskId = 2, Text = "IFourth", Status = StatusTask.IsProgress });
            var resultTasks = _todoController.ShowTasks().ToList();
            CollectionAssert.AreEqual(tasks, resultTasks, new TaskComparer());
        }
    }
}