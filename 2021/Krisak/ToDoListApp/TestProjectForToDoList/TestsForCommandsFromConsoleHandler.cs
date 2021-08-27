using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.CommandHandler;
using ToDoLibrary.Storages;

namespace TestProjectForToDoLibrary
{
    [TestClass]
    public class TestsForCommandsFromConsoleHandler
    {
        private TasksStorage _storage = new TasksStorage();
        private RollbacksStorage _rollback = new RollbacksStorage();

        private void RunHandle(string[] command)
        {
            var consoleCommandHandler = new ConsoleCommandHandler(_rollback, command);
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
            var task = new List<Task>
                {
                    new Task{Text = "world", TaskId = 1},
                    new Task{Text = "war", TaskId = 2},
                };
            _storage.Set(task);

            RunHandle(new[] { "toggle", "1", "1" });
            RunHandle(new[] { "toggle", "1", "2" });
            RunHandle(new[] { "toggle", "2", "1" });

            task = _storage.Get();

            Assert.AreEqual(2, task.Count);
            Assert.AreEqual("[1] world ●", task[0].ToString());
            Assert.AreEqual("[2] war ○", task[1].ToString());
        }

        [TestMethod]
        public void CorrectHandleDeleteCommand()
        {
            var task = new List<Task>
                            {
                                new Task{Text = "world", TaskId =1},
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
        public void CorrectHandleRollbackAddCommand()
        {
            RunHandle(new[] { "add", "world" });
            RunHandle(new[] { "rollback", "1" });
            var task = _storage.Get();

            Assert.AreEqual(0, task.Count);
        }

        [TestMethod]
        public void CorrectHandleRollbackEdit()
        {
            var task = new List<Task> { new Task { Text = "war", TaskId = 1 } };
            _storage.Set(task);

            RunHandle(new[] { "edit", "1", "world" });
            RunHandle(new[] { "rollback", "1" });
            
            task = _storage.Get();
            Assert.AreEqual("[1] war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleRollbackToggleCommand()
        {
            var task = new List<Task> { new Task { Text = "war", Status = StatusTask.IsProgress, TaskId = 1 } };
            _storage.Set(task);

            RunHandle(new[] { "toggle", "1", "2" });
            RunHandle(new[] { "rollback", "1" });
            
            task = _storage.Get();

            Assert.AreEqual("[1] war ○", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleRollbackDeleteCommand()
        {
            var task = new List<Task> { new Task { Text = "war", TaskId = 1 } };
            _storage.Set(task);

            RunHandle(new[] { "delete", "1" });
            RunHandle(new[] { "rollback", "1" });
            
            task = _storage.Get();

            Assert.AreEqual("[1] war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandlingRollbackCommand_OneRollback()
        {
            RunHandle(new[] { "add", "world" });
            RunHandle(new[] { "add", "war" });
            RunHandle(new[] { "toggle", "2", "1" });
            RunHandle(new[] { "add", "peace", "or", "war" });
            RunHandle(new[] { "delete", "2" });
            RunHandle(new[] { "edit", "3", "war" });
            RunHandle(new[] { "toggle", "1", "0" });
            RunHandle(new[] { "edit", "1", "war" });
            RunHandle(new[] { "delete", "3" });

            RunHandle(new[] { "rollback", "9" });

            var task = _storage.Get();

            Assert.AreEqual(task.Count, 0);
        }

        [TestMethod]
        public void CorrectHandleRollbackCommand_SeveralRollback()
        {
            var tasks = new List<Task> { new Task { Text = "war" , TaskId = 1} };
            _storage.Set(tasks);

            RunHandle(new[] { "toggle", "1", "1" });
            RunHandle(new[] { "add", "world" });
            RunHandle(new[] { "edit", "2", "peace" });
            RunHandle(new[] { "toggle", "2", "1" });
            RunHandle(new[] { "rollback", "2" });
            RunHandle(new[] { "delete", "1" });
            RunHandle(new[] { "delete", "2" });
            RunHandle(new[] { "rollback", "1" });
            RunHandle(new[] { "edit", "2", "peace" });
            RunHandle(new[] { "toggle", "2", "1" });
            RunHandle(new[] { "rollback", "5" });

            tasks = _storage.Get();

            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual("[1] war", tasks[0].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongCommand()
        {
            RunHandle(new[] { "IDoNothing" });
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringEmptyCommand()
        {
            RunHandle(new string[] { });
        }
    }
}