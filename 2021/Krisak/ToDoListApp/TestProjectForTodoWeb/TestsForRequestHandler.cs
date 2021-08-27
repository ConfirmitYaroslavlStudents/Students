using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProjectForToDoLibrary;
using ToDoLibrary;
using ToDoLibrary.Commands;
using ToDoLibrary.Loggers;
using TodoWeb;

namespace TestProjectForTodoWeb
{
    [TestClass]
    public class TestsForRequestHandler
    {
        private RequestHandler _requestHandler;

        [TestMethod]
        public void SuccessfullyHandleGetRequest()
        {
            var task = new List<Task>
                {
                    new Task{Text = "IFirst"},
                    new Task{Text = "ISecond"},
                    new Task {Text = "IThird"}
                };
            _requestHandler = new RequestHandler(new WebLogger(), new FakeSaver(), new FakeLoader(task));

            var result = _requestHandler.ShowTasks();
            CollectionAssert.AreEqual(task, result, new TaskComparer());
        }

        [TestMethod]
        public void SuccessfullyHandlePostRequest()
        {
            var tasks = new List<Task>
                {
                    new Task{Text = "IFirst"},
                    new Task{Text = "ISecond"},
                };

            var task = new Task { Text = "IThird", TaskId = 1 };

            _requestHandler = new RequestHandler(new WebLogger(), new FakeSaver(), new FakeLoader(tasks));
            var result = _requestHandler.Handle(HttpVerbs.Post, new AddCommand { NewTask = new Task { Text = "IThird" } });

            Assert.AreEqual(201, result.Item1);
            Assert.AreEqual("New task has been successfully added.", result.Item2);

            tasks.Add(task);
            var resultTasks = _requestHandler.ShowTasks();
            CollectionAssert.AreEqual(tasks, resultTasks, new TaskComparer());
        }

        [TestMethod]
        public void WrongHandlePostRequestWithLongTask()
        {
            _requestHandler = new RequestHandler(new WebLogger(), new FakeSaver(), new FakeLoader(new List<Task>()));
            var result = _requestHandler.Handle(HttpVerbs.Post, new AddCommand { NewTask = new Task { Text = new string('o', 51)} });

            Assert.AreEqual(400, result.Item1);
            Assert.AreEqual("Task length must not be more than 50 characters.", result.Item2);
        }

        [TestMethod]
        public void SuccessfullyHandleDeleteRequest()
        {
            var tasks = new List<Task>
                {
                    new Task{Text = "IFirst",TaskId = 1},
                    new Task{Text = "ISecond",TaskId = 2},
                    new Task {Text = "IThird",TaskId = 3}
                };

            _requestHandler = new RequestHandler(new WebLogger(), new FakeSaver(), new FakeLoader(tasks));
            var result = _requestHandler.Handle(HttpVerbs.Delete, new DeleteCommand{TaskId = 3});

            Assert.AreEqual(200, result.Item1);
            Assert.AreEqual("Task has been successfully deleted.", result.Item2);

            tasks.RemoveAt(2);
            var resultTasks = _requestHandler.ShowTasks();
            CollectionAssert.AreEqual(tasks, resultTasks, new TaskComparer());
        }

        [TestMethod]
        public void WrongHandleDeleteRequestWithWrongId()
        {
            _requestHandler = new RequestHandler(new WebLogger(), new FakeSaver(), new FakeLoader(new List<Task>()));
            var result = _requestHandler.Handle(HttpVerbs.Delete, new DeleteCommand { TaskId = 0 });

            Assert.AreEqual(400, result.Item1);
            Assert.AreEqual("Task not found with Id 0.", result.Item2);
        }

        [TestMethod]
        public void SuccessfullyHandlePatchRequest()
        {
            var tasks = new List<Task>
            {
                new Task{Text = "IFirst",TaskId = 1},
                new Task{Text = "ISecond",TaskId = 2},
                new Task {Text = "IThird",TaskId = 3}
            };

            _requestHandler = new RequestHandler(new WebLogger(), new FakeSaver(), new FakeLoader(tasks));
            var command = new EditTaskCommand
            {
                EditTextCommand = new EditTextCommand() { Text = "IFourth", TaskId = 3 },
                ToggleStatusCommand = new ToggleStatusCommand() { Status = StatusTask.IsProgress, TaskId = 3 }
            };
            var result = _requestHandler.Handle(HttpVerbs.Patch,command);

            Assert.AreEqual(200, result.Item1);
            Assert.AreEqual("Task has been successfully changed.", result.Item2);

            tasks[2] = new Task {TaskId = 3, Text = "IFourth", Status = StatusTask.IsProgress};
            var resultTasks = _requestHandler.ShowTasks();
            CollectionAssert.AreEqual(tasks, resultTasks, new TaskComparer());
        }
    }
}