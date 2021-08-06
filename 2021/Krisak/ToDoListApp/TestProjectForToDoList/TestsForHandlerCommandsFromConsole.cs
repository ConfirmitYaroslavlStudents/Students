using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.CommandHandler;

namespace TestProjectForToDoLibrary
{
    [TestClass]
    public class TestsForHandlerCommandsFromConsole
    {
        [TestMethod]
        public void CorrectHandleAddCommand_OneWord()
        {
            var task = new List<Task>();
            var handlerCommandsFromConsole = new HandlerCommandsFromConsole(task);

            handlerCommandsFromConsole.Handling(new string[] { "add", "world" });

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleAddCommand_SeveralWords()
        {
            var task = new List<Task>();
            var handlerCommandsFromConsole = new HandlerCommandsFromConsole(task);

            handlerCommandsFromConsole.Handling(new string[] { "add", "world", "or", "war" });

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world or war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleEditCommand_OneWord()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            var handlerCommandsFromConsole = new HandlerCommandsFromConsole(task);

            handlerCommandsFromConsole.Handling(new string[] { "edit", "1", "world" });

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleEditCommand_SeveralWords()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            var handlerCommandsFromConsole = new HandlerCommandsFromConsole(task);

            handlerCommandsFromConsole.Handling(new string[] { "edit", "1", "world", "or", "war" });

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world or war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleToggleCommand()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            var handlerCommandsFromConsole = new HandlerCommandsFromConsole(task);

            handlerCommandsFromConsole.Handling(new string[] { "toggle", "1", "1" });
            handlerCommandsFromConsole.Handling(new string[] { "toggle", "1", "2" });

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
            var handlerCommandsFromConsole = new HandlerCommandsFromConsole(task);

            handlerCommandsFromConsole.Handling(new[] { "delete", "1" });
            handlerCommandsFromConsole.Handling(new[] { "delete", "2" });

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleRollbackAddCommand()
        {
            var task = new List<Task>();
            var handlerCommandsFromConsole = new HandlerCommandsFromConsole(task);

            handlerCommandsFromConsole.Handling(new[] { "add", "world" });
            handlerCommandsFromConsole.Handling(new[] { "rollback", "1" });

            Assert.AreEqual(0, task.Count);
        }

        [TestMethod]
        public void CorrectHandleRollbackEdit()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            var handlerCommandsFromConsole = new HandlerCommandsFromConsole(task);

            handlerCommandsFromConsole.Handling(new[] { "edit", "1", "world" });
            handlerCommandsFromConsole.Handling(new[] { "rollback", "1" });

            Assert.AreEqual("war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleRollbackToggleCommand()
        {
            var task = new List<Task> { new Task { Text = "war", Status = StatusTask.IsProgress } };
            var handlerCommandsFromConsole = new HandlerCommandsFromConsole(task);

            handlerCommandsFromConsole.Handling(new[] { "toggle", "1", "2" });
            handlerCommandsFromConsole.Handling(new[] { "rollback", "1" });

            Assert.AreEqual("war []", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleRollbackDeleteCommand()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            var handlerCommandsFromConsole = new HandlerCommandsFromConsole(task);

            handlerCommandsFromConsole.Handling(new[] { "delete", "1" });
            handlerCommandsFromConsole.Handling(new[] { "rollback", "1" });

            Assert.AreEqual("war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandlingRollbackCommand_OneRollback()
        {
            var task = new List<Task> { };
            var handlerCommandsFromConsole = new HandlerCommandsFromConsole(task);

            handlerCommandsFromConsole.Handling(new[] { "add", "world" });
            handlerCommandsFromConsole.Handling(new[] { "add", "war" });
            handlerCommandsFromConsole.Handling(new[] { "toggle", "2", "1" });
            handlerCommandsFromConsole.Handling(new[] { "add", "peace", "or", "war" });
            handlerCommandsFromConsole.Handling(new[] { "delete", "2" });
            handlerCommandsFromConsole.Handling(new[] { "edit","2","war" });
            handlerCommandsFromConsole.Handling(new[] { "toggle", "1","0" });
            handlerCommandsFromConsole.Handling(new[] { "edit", "1", "war" });
            handlerCommandsFromConsole.Handling(new[] { "delete", "2" });

            handlerCommandsFromConsole.Handling(new[] { "rollback","9"});

            Assert.AreEqual(task.Count, 0);
        }

        [TestMethod]
        public void CorrectHandleRollbackCommand_SeveralRollback()
        {
            var tasks = new List<Task> { new Task{Text = "war"}};
            var handlerCommandsFromConsole = new HandlerCommandsFromConsole(tasks);

            handlerCommandsFromConsole.Handling(new [] { "toggle", "1","1" });
            handlerCommandsFromConsole.Handling(new [] { "add", "world" });
            handlerCommandsFromConsole.Handling(new [] { "edit","2","peace" });
            handlerCommandsFromConsole.Handling(new [] { "toggle", "2", "1" });

            handlerCommandsFromConsole.Handling(new[] {"rollback", "2"});
            
            handlerCommandsFromConsole.Handling(new [] { "delete", "1" });
            handlerCommandsFromConsole.Handling(new [] { "delete", "1" });

            handlerCommandsFromConsole.Handling(new[] {"rollback", "1"});

            handlerCommandsFromConsole.Handling(new [] { "edit", "1", "peace" });
            handlerCommandsFromConsole.Handling(new [] { "toggle", "1", "1" });

            handlerCommandsFromConsole.Handling(new[] { "rollback", "5" });

            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual("war", tasks[0].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongCommand()
        {
            var task = new List<Task> { };
            var handlerCommandsFromConsole = new HandlerCommandsFromConsole(task);
            handlerCommandsFromConsole.Handling(new []{"IDoNothing"});
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringEmptyCommand()
        {
            var task = new List<Task> { };
            var handlerCommandsFromConsole = new HandlerCommandsFromConsole(task);
            handlerCommandsFromConsole.Handling(new string[] {});
        }
    }
}