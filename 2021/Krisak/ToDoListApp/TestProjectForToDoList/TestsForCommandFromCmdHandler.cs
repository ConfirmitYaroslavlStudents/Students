using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.CommandHandler;
using ToDoLibrary.Storages;

namespace TestProjectForToDoLibrary
{
    [TestClass]
    public class TestsForCommandFromCmdHandler
    {
        private TasksStorage _storage = new TasksStorage();

        private void RunHandle(string[] command)
        {
            var consoleCommandHandler = new CmdCommandHandler(command);
            consoleCommandHandler.SetStorage(_storage);
            consoleCommandHandler.Run();
        }

        [TestMethod]
        public void CorrectHandleAddCommand_OneWord()
        {
            RunHandle(new[] { "add", "world" });

            var task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("[1] world", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleAddCommand_SeveralWords()
        {
            RunHandle(new[] { "add", "world", "or", "war" });

            var task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("[1] world or war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleEditCommand_OneWord()
        {
            var task = new List<Task> { new Task { Text = "war", TaskId = 1 } };
            _storage.Set(task);

            RunHandle(new[] { "edit", "1", "world" });

            task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("[1] world", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleEditCommand_SeveralWords()
        {
            var task = new List<Task> { new Task { Text = "war", TaskId = 1 } };
            _storage.Set(task);

            RunHandle(new[] { "edit", "1", "world", "or", "war" });

            task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("[1] world or war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleToggleCommand()
        {
            var task = new List<Task> { new Task { Text = "war", TaskId = 1 } };
            _storage.Set(task);

            RunHandle(new[] { "toggle", "1", "1" });
            RunHandle(new[] { "toggle", "1", "2" });

            task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("[1] war ●", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleDeleteCommand()
        {
            var task = new List<Task>
                        {
                            new Task{Text = "world", TaskId =  1},
                            new Task{Text = "war", TaskId = 2},
                            new Task {Text = "peace", TaskId = 3}
                        };
            _storage.Set(task);

            RunHandle(new[] { "delete", "1" });
            RunHandle(new[] { "delete", "3" });

            task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("[2] war", task[0].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongCommand()
        {

            RunHandle(new[] { "IDoNothing" });
        }
    }
}