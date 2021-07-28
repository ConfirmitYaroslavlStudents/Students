using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoApi;
using ToDo;

namespace ToDoListTests
{
    [TestClass]
    public class ToDoControllerTests
    {
        [TestMethod]
        public void GetReturnsCorrectList()
        {
            var loaderSaver = new TestLoaderSaver();
            loaderSaver.ToDoList.Add(new Task("wash dishes"));
            var logger = new TestLogger();
            var controller = new ToDoApi.Controllers.ToDoController(loaderSaver, logger);

            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("1. wash dishes  [ ]\r\n", actualToDoList);
        }

        [TestMethod]
        public void TaskExistsAfterPost()
        {
            var loaderSaver = new TestLoaderSaver();
            var logger = new TestLogger();
            var controller = new ToDoApi.Controllers.ToDoController(loaderSaver, logger);

            controller.PostTask("wash dishes");
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("1. wash dishes  [ ]\r\n", actualToDoList);
        }

        [TestMethod]
        public void TaskDoesNotExistAfterDelete()
        {
            var loaderSaver = new TestLoaderSaver();
            loaderSaver.ToDoList.Add(new Task("wash dishes"));
            var logger = new TestLogger();
            var controller = new ToDoApi.Controllers.ToDoController(loaderSaver, logger);

            controller.DeleteTask(1);
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("", actualToDoList);
        }

        [TestMethod]
        public void TaskTextChangesAfterPut()
        {
            var loaderSaver = new TestLoaderSaver();
            loaderSaver.ToDoList.Add(new Task("wash dishes"));
            var logger = new TestLogger();
            var controller = new ToDoApi.Controllers.ToDoController(loaderSaver, logger);

            controller.PutTaskText(new PutTaskTextRequest { TaskNumber = 1, TaskText = "clean the room" });
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("1. clean the room  [ ]\r\n", actualToDoList);
        }

        [TestMethod]
        public void TaskStatusTogglesAfterPut()
        {
            var loaderSaver = new TestLoaderSaver();
            loaderSaver.ToDoList.Add(new Task("wash dishes"));
            var logger = new TestLogger();
            var controller = new ToDoApi.Controllers.ToDoController(loaderSaver, logger);

            controller.PutTaskStatus(1);
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("1. wash dishes  [v]\r\n", actualToDoList);
        }
    }
}
