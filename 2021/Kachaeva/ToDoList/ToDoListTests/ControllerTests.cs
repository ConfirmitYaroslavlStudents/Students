using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoListProject;

namespace ToDoListTests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void MenuDisplaysCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "q" });
            var controller = new Controller(testLoaderSaver, testWriterReader);
            controller.HandleUsersInput();

            Assert.AreEqual("Что вы хотели бы сделать? Введите:", testWriterReader.Messages[0]);
            Assert.AreEqual("1 - просмотреть список", testWriterReader.Messages[1]);
            Assert.AreEqual("2 - добавить задание", testWriterReader.Messages[2]);
            Assert.AreEqual("3 - удалить задание", testWriterReader.Messages[3]);
            Assert.AreEqual("4 - изменить текст задания", testWriterReader.Messages[4]);
            Assert.AreEqual("5 - изменить статус задания", testWriterReader.Messages[5]);
            Assert.AreEqual("q - выйти\r\n", testWriterReader.Messages[6]);
            Assert.AreEqual("", testWriterReader.Messages[7]);
        }

        [TestMethod]
        public void ToDoListIsEmptyAtTheBeginning()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "1", "q" });
            var controller = new Controller(testLoaderSaver, testWriterReader);
            controller.HandleUsersInput();

            Assert.AreEqual("Список пуст\r\n", testWriterReader.Messages[8]);
        }

        [TestMethod]
        public void TaskAddsCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "2", "wash dishes", "1", "q" });
            var controller = new Controller(testLoaderSaver, testWriterReader);
            controller.HandleUsersInput();

            Assert.AreEqual("Введите текст задания", testWriterReader.Messages[8]);
            Assert.AreEqual("1. wash dishes  [ ]\r\n", testWriterReader.Messages[18]);
        }

        [TestMethod]
        public void TaskRemovesCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "2", "wash dishes", "3", "1", "1", "q" });
            var controller = new Controller(testLoaderSaver, testWriterReader);
            controller.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", testWriterReader.Messages[18]);
            Assert.AreEqual("Список пуст\r\n", testWriterReader.Messages[28]);
        }

        [TestMethod]
        public void TaskTextChangesCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "2", "wash dishes", "4", "1", "clean the room", "1", "q" });
            var controller = new Controller(testLoaderSaver, testWriterReader);
            controller.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", testWriterReader.Messages[18]);
            Assert.AreEqual("Введите текст задания", testWriterReader.Messages[20]);
            Assert.AreEqual("1. clean the room  [ ]\r\n", testWriterReader.Messages[30]);
        }

        [TestMethod]
        public void TaskStatusChangesCorrectly()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "2", "wash dishes", "5", "1", "1", "q" });
            var controller = new Controller(testLoaderSaver, testWriterReader);
            controller.HandleUsersInput();

            Assert.AreEqual("Введите номер задания", testWriterReader.Messages[18]);
            Assert.AreEqual("1. wash dishes  [v]\r\n", testWriterReader.Messages[28]);
        }

        [TestMethod]
        public void CanNotChooseNonExistentMenuItem()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "6", "q" });
            var controller = new Controller(testLoaderSaver, testWriterReader);
            controller.HandleUsersInput();

            Assert.AreEqual("Некорректная команда\r\n", testWriterReader.Messages[8]);
        }

        [TestMethod]
        public void CanNotAddEmptyTask()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "2", "", "q" });
            var controller = new Controller(testLoaderSaver, testWriterReader);
            controller.HandleUsersInput();

            Assert.AreEqual("Нельзя добавить пустое задание\r\n", testWriterReader.Messages[10]);
        }

        [TestMethod]
        public void CanNotInputNotIntegerInNumberChoice()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "3", "wrong input", "q" });
            var controller = new Controller(testLoaderSaver, testWriterReader);
            controller.HandleUsersInput();

            Assert.AreEqual("Нужно ввести число\r\n", testWriterReader.Messages[10]);
        }

        [TestMethod]
        public void CanNotChooseNonExistentTaskNumber()
        {
            var testLoaderSaver = new TestLoaderSaver();
            var testWriterReader = new TestWriterReader(new List<string> { "3", "1", "q" });
            var controller = new Controller(testLoaderSaver, testWriterReader);
            controller.HandleUsersInput();

            Assert.AreEqual("Задания с таким номером не существует\r\n", testWriterReader.Messages[10]);
        }
    }
}
