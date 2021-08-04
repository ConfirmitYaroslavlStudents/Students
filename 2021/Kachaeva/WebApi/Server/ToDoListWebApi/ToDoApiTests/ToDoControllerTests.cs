using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;
using ToDoApi.Controllers;

namespace ToDoApiTests
{
    [TestClass]
    public class ToDoControllerTests
    {
        [TestMethod]
        public void GetWorksCorrectlyWithEmptyList()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            var controller = new ToDoController(loaderSaver);

            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("", actualToDoList);
        }

        [TestMethod]
        public void GetReturnsCorrectList()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            loaderSaver.ToDoList.Add(new Task("wash dishes", false));
            var controller = new ToDoController(loaderSaver);

            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("1. wash dishes  [ ]\r\n", actualToDoList);
        }

        [TestMethod]
        public void TaskExistsAfterPost()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            var controller = new ToDoController(loaderSaver);

            controller.PostTask(new Task("wash dishes", false));
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("1. wash dishes  [ ]\r\n", actualToDoList);
        }

        [TestMethod]
        public void TaskDoesNotExistAfterDelete()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            loaderSaver.ToDoList.Add(new Task("wash dishes", false));
            var controller = new ToDoController(loaderSaver);

            controller.DeleteTask(1);
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("", actualToDoList);
        }

        [TestMethod]
        public void TaskTextChangesAfterPatch()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            loaderSaver.ToDoList.Add(new Task("wash dishes" , false));
            var controller = new ToDoController(loaderSaver);

            controller.PatchTask(1, new Task("clean the room"));
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("1. clean the room  [ ]\r\n", actualToDoList);
        }

        [TestMethod]
        public void TaskStatusTogglesAfterPatch()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            loaderSaver.ToDoList.Add(new Task("wash dishes", false));
            var controller = new ToDoController(loaderSaver);

            controller.PatchTask(1, new Task(true));
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("1. wash dishes  [v]\r\n", actualToDoList);
        }
    }
}

