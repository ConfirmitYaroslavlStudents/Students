using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.CommandHandler;

namespace TestProjectForToDoLibrary
{
    [TestClass]
    public class TestsForHandlerCommandFromCmd
    {
        [TestMethod]
        public void CorrectHandleAddCommand_OneWord()
        {
            var task = new List<Task>();
            var handlerCommandFromCmd = new HandlerCommandFromCmd(task);

            handlerCommandFromCmd.Handling(new [] { "add", "world" });

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleAddCommand_SeveralWords()
        {
            var task = new List<Task>();
            var handlerCommandFromCmd = new HandlerCommandFromCmd(task);

            handlerCommandFromCmd.Handling(new [] { "add", "world", "or", "war" });

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world or war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleEditCommand_OneWord()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            var handlerCommandFromCmd = new HandlerCommandFromCmd(task);

            handlerCommandFromCmd.Handling(new [] { "edit", "1", "world" });

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleEditCommand_SeveralWords()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            var handlerCommandFromCmd = new HandlerCommandFromCmd(task);

            handlerCommandFromCmd.Handling(new [] { "edit", "1", "world", "or", "war" });

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("world or war", task[0].ToString());
        }

        [TestMethod]
        public void CorrectHandleToggleCommand()
        {
            var task = new List<Task> { new Task { Text = "war" } };
            var handlerCommandFromCmd = new HandlerCommandFromCmd(task);

            handlerCommandFromCmd.Handling(new [] { "toggle", "1", "1" });
            handlerCommandFromCmd.Handling(new [] { "toggle", "1", "2" });

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
            var handlerCommandFromCmd = new HandlerCommandFromCmd(task);

            handlerCommandFromCmd.Handling(new[] { "delete", "1" });
            handlerCommandFromCmd.Handling(new[] { "delete", "2" });

            Assert.AreEqual(1, task.Count);
            Assert.AreEqual("war", task[0].ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongCommand()
        {
            var task = new List<Task> { };
            var handlerCommandFromCmd = new HandlerCommandFromCmd(task);
            handlerCommandFromCmd.Handling(new[] { "IDoNothing" });
        }
    }
}