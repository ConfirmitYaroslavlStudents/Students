using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.CommandHandler;

namespace TestProjectForToDoLibrary
{
    [TestClass]
    public class TestsForHandlerCommandsFromConsole
    {
        private TaskStorage _storage = new TaskStorage();

        [TestMethod]
        public void CorrectHandleAddCommand_OneWord()
        {
            var handlerCommandsFromConsole = new CommandsFromConsoleHandler(_storage);

            handlerCommandsFromConsole.CommandHandler(new [] { "add", "world" });

            var task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleAddCommand_SeveralWords()
        {
            var handlerCommandsFromConsole = new CommandsFromConsoleHandler(_storage);

            handlerCommandsFromConsole.CommandHandler(new string[] { "add", "world", "or", "war" });

            var task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world or war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleEditCommand_OneWord()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            _storage.Set(task);

            var handlerCommandsFromConsole = new CommandsFromConsoleHandler(_storage);

            handlerCommandsFromConsole.CommandHandler(new string[] { "edit", "1", "world" });

            task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleEditCommand_SeveralWords()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            _storage.Set(task);

            var handlerCommandsFromConsole = new CommandsFromConsoleHandler(_storage);

            handlerCommandsFromConsole.CommandHandler(new string[] { "edit", "1", "world", "or", "war" });

             task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world or war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleToggleCommand()
        {
            var task = new List<Task> 
            { 
                new Task{Text = "world"},
                new Task{Text = "war"},
            };
            _storage.Set(task);

            var handlerCommandsFromConsole = new CommandsFromConsoleHandler(_storage);

            handlerCommandsFromConsole.CommandHandler(new string[] { "toggle", "1", "1" });
            handlerCommandsFromConsole.CommandHandler(new string[] { "toggle", "1", "2" });
            handlerCommandsFromConsole.CommandHandler(new string[] { "toggle", "2", "1" });

            task = _storage.Get();

            Assert.AreEqual(2, task.Count);
            Assert.AreEqual("world [X]", task[0].ToString());
            Assert.AreEqual("war []", task[1].ToString());
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

            var handlerCommandsFromConsole = new CommandsFromConsoleHandler(_storage);

            handlerCommandsFromConsole.CommandHandler(new[] { "delete", "1" });
            handlerCommandsFromConsole.CommandHandler(new[] { "delete", "2" });

            task = _storage.Get();

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleRollbackAddCommand()
        {
            var handlerCommandsFromConsole = new CommandsFromConsoleHandler(_storage);

            handlerCommandsFromConsole.CommandHandler(new[] { "add", "world" });
            handlerCommandsFromConsole.CommandHandler(new[] { "rollback", "1" });

            var task = _storage.Get();

            Assert.AreEqual(0, task.Count);
        }

        [TestMethod]
        public void CorrectHandleRollbackEdit()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            _storage.Set(task);

            var handlerCommandsFromConsole = new CommandsFromConsoleHandler(_storage);

            handlerCommandsFromConsole.CommandHandler(new[] { "edit", "1", "world" });
            handlerCommandsFromConsole.CommandHandler(new[] { "rollback", "1" });

            task = _storage.Get();
            Assert.AreEqual("war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleRollbackToggleCommand()
        {
            var task = new List<Task> { new Task { Text = "war", Status = StatusTask.IsProgress } };
            _storage.Set(task);

            var handlerCommandsFromConsole = new CommandsFromConsoleHandler(_storage);

            handlerCommandsFromConsole.CommandHandler(new[] { "toggle", "1", "2" });
            handlerCommandsFromConsole.CommandHandler(new[] { "rollback", "1" });

            task = _storage.Get();

            Assert.AreEqual("war []", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleRollbackDeleteCommand()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            _storage.Set(task);

            var handlerCommandsFromConsole = new CommandsFromConsoleHandler(_storage);

            handlerCommandsFromConsole.CommandHandler(new[] { "delete", "1" });
            handlerCommandsFromConsole.CommandHandler(new[] { "rollback", "1" });

            task = _storage.Get();

            Assert.AreEqual("war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandlingRollbackCommand_OneRollback()
        {
            var handlerCommandsFromConsole = new CommandsFromConsoleHandler(_storage);

            handlerCommandsFromConsole.CommandHandler(new[] { "add", "world" });
            handlerCommandsFromConsole.CommandHandler(new[] { "add", "war" });
            handlerCommandsFromConsole.CommandHandler(new[] { "toggle", "2", "1" });
            handlerCommandsFromConsole.CommandHandler(new[] { "add", "peace", "or", "war" });
            handlerCommandsFromConsole.CommandHandler(new[] { "delete", "2" });
            handlerCommandsFromConsole.CommandHandler(new[] { "edit", "2", "war" });
            handlerCommandsFromConsole.CommandHandler(new[] { "toggle", "1", "0" });
            handlerCommandsFromConsole.CommandHandler(new[] { "edit", "1", "war" });
            handlerCommandsFromConsole.CommandHandler(new[] { "delete", "2" });

            handlerCommandsFromConsole.CommandHandler(new[] { "rollback", "9" });

            var task = _storage.Get();

            Assert.AreEqual(task.Count, 0);
        }

        [TestMethod]
        public void CorrectHandleRollbackCommand_SeveralRollback()
        {
            var tasks = new List<Task> { new Task { Text = "war" } };
            _storage.Set(tasks);

            var handlerCommandsFromConsole = new CommandsFromConsoleHandler(_storage);

            handlerCommandsFromConsole.CommandHandler(new[] { "toggle", "1", "1" });
            handlerCommandsFromConsole.CommandHandler(new[] { "add", "world" });
            handlerCommandsFromConsole.CommandHandler(new[] { "edit", "2", "peace" });
            handlerCommandsFromConsole.CommandHandler(new[] { "toggle", "2", "1" });

            handlerCommandsFromConsole.CommandHandler(new[] { "rollback", "2" });

            handlerCommandsFromConsole.CommandHandler(new[] { "delete", "1" });
            handlerCommandsFromConsole.CommandHandler(new[] { "delete", "1" });

            handlerCommandsFromConsole.CommandHandler(new[] { "rollback", "1" });

            handlerCommandsFromConsole.CommandHandler(new[] { "edit", "1", "peace" });
            handlerCommandsFromConsole.CommandHandler(new[] { "toggle", "1", "1" });

            handlerCommandsFromConsole.CommandHandler(new[] { "rollback", "5" });

            tasks = _storage.Get();

            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual("war", tasks[0].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongCommand()
        {
            var handlerCommandsFromConsole = new CommandsFromConsoleHandler(_storage);
            handlerCommandsFromConsole.CommandHandler(new[] { "IDoNothing" });
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringEmptyCommand()
        {
            var handlerCommandsFromConsole = new CommandsFromConsoleHandler(_storage);
            handlerCommandsFromConsole.CommandHandler(new string[] { });
        }
    }
    }