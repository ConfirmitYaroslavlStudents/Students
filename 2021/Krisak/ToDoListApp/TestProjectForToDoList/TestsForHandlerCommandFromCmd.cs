using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.CommandHandler;

namespace TestProjectForToDoLibrary
{
    [TestClass]
    public class TestsForHandlerCommandFromCmd
    {
        private TaskStorage _storage = new TaskStorage();

        [TestMethod]
        public void CorrectHandleAddCommand_OneWord()
        {
            var handlerCommandFromCmd = new CommandFromCmdHandler(_storage);

            handlerCommandFromCmd.CommandHandler(new[] { "add", "world" });

            var task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleAddCommand_SeveralWords()
        {
            var handlerCommandFromCmd = new CommandFromCmdHandler(_storage);

            handlerCommandFromCmd.CommandHandler(new[] { "add", "world", "or", "war" });

            var task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world or war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleEditCommand_OneWord()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            _storage.Set(task);

            var handlerCommandFromCmd = new CommandFromCmdHandler(_storage);

            handlerCommandFromCmd.CommandHandler(new[] { "edit", "1", "world" });

            task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleEditCommand_SeveralWords()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            _storage.Set(task);

            var handlerCommandFromCmd = new CommandFromCmdHandler(_storage);

            handlerCommandFromCmd.CommandHandler(new[] { "edit", "1", "world", "or", "war" });

            task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world or war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleToggleCommand()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            _storage.Set(task);

            var handlerCommandFromCmd = new CommandFromCmdHandler(_storage);

            handlerCommandFromCmd.CommandHandler(new[] { "toggle", "1", "1" });
            handlerCommandFromCmd.CommandHandler(new[] { "toggle", "1", "2" });

            task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("war [X]", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleDeleteCommand()
        {
            var task = new List<Task>
                    {
                        new Task{Text = "world"},
                        new Task{Text = "war"},
                        new Task {Text = "peace"}
                    };
            _storage.Set(task);

            var handlerCommandFromCmd = new CommandFromCmdHandler(_storage);

            handlerCommandFromCmd.CommandHandler(new[] { "delete", "1" });
            handlerCommandFromCmd.CommandHandler(new[] { "delete", "2" });

            task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("war", task[0].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongCommand()
        {
            var handlerCommandFromCmd = new CommandFromCmdHandler(_storage);
            handlerCommandFromCmd.CommandHandler(new[] { "IDoNothing" });
        }
    }
}