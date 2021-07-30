using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDo;
using ToDoApi.Controllers;

namespace ToDoListTests
{
    [TestClass]
    public class ToDoControllerTests
    {
        [TestMethod]
        public void GetWorksCorrectlyWithEmptyList()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            var logger = new FakeLogger();
            var controller = new ToDoController(loaderSaver, logger);

            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("Список пуст", logger.Messages[0]);
            Assert.AreEqual("", actualToDoList);
        }

        [TestMethod]
        public void GetReturnsCorrectList()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            loaderSaver.ToDoList.Add(new Task("wash dishes"));
            var logger = new FakeLogger();
            var controller = new ToDoController(loaderSaver, logger);

            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("1. wash dishes  [ ]\r\n", actualToDoList);
        }

        [TestMethod]
        public void TaskExistsAfterPost()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            var logger = new FakeLogger();
            var controller = new ToDoController(loaderSaver, logger);

            controller.PostTask("wash dishes");
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("Задание добавлено", logger.Messages[0]);
            Assert.AreEqual("1. wash dishes  [ ]\r\n", actualToDoList);
        }

        [TestMethod]
        public void TaskDoesNotExistAfterDelete()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            loaderSaver.ToDoList.Add(new Task("wash dishes"));
            var logger = new FakeLogger();
            var controller = new ToDoController(loaderSaver, logger);

            controller.DeleteTask(1);
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("Задание удалено", logger.Messages[0]);
            Assert.AreEqual("", actualToDoList);
        }

        [TestMethod]
        public void TaskTextChangesAfterPatch()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            loaderSaver.ToDoList.Add(new Task("wash dishes"));
            var logger = new FakeLogger();
            var controller = new ToDoController(loaderSaver, logger);

            controller.PatchTaskText(1, "clean the room");
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("Текст задания обновлен", logger.Messages[0]);
            Assert.AreEqual("1. clean the room  [ ]\r\n", actualToDoList);
        }

        [TestMethod]
        public void TaskStatusTogglesAfterPatch()
        {
            var loaderSaver = new FakeLoaderAndSaver();
            loaderSaver.ToDoList.Add(new Task("wash dishes"));
            var logger = new FakeLogger();
            var controller = new ToDoController(loaderSaver, logger);

            controller.PatchTaskStatus(1, true);
            var actualToDoList = controller.GetToDoList();

            Assert.AreEqual("Статус задания обновлен", logger.Messages[0]);
            Assert.AreEqual("1. wash dishes  [v]\r\n", actualToDoList);
        }
    }
}

