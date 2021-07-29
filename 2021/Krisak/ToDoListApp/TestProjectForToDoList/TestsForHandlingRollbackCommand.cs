using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList;

namespace TestsForToDoList
{
    [TestClass]
    public class TestsForHandlingRollbackCommand
    {
        [TestMethod]
        public void StackRemovDataAfterRollback()
        {
            var notes = new List<Note>();
            var rollback = new Stack<List<object>>();
            rollback.Push(new List<object> { "add", 0, new Note { Text = "world", isCompletedFlag = true } });
            rollback.Push(new List<object> { "add", 0, new Note { Text = "peace" } });

            HandlingRollbackCommand.Rollback(notes, rollback, 2);
            Assert.AreEqual(rollback.Count, 0);
        }

        [TestMethod]
        public void CorrectHandlingCommandAdd()
        {
            var notes = new List<Note>();
            var rollback = new Stack<List<object>>();
            rollback.Push(new List<object> { "add", 0, new Note { Text = "world", isCompletedFlag = true } });
            rollback.Push(new List<object> { "add", 0, new Note { Text = "peace" } });

            HandlingRollbackCommand.Rollback(notes, rollback, 2);
            Assert.AreEqual(notes[0].ToString(), "world [X]");
            Assert.AreEqual(notes[1].ToString(), "peace");
        }

        [TestMethod]
        public void CorrectHandlingCommandEdit()
        {
            var notes = new List<Note> { new Note { Text = "world", isCompletedFlag = true } };
            var rollback = new Stack<List<object>>();

            rollback.Push(new List<object> { "edit", 1, "war" });
            rollback.Push(new List<object> { "edit", 0, "peace" });
            rollback.Push(new List<object> { "add", 1, new Note { Text = "world" } });

            HandlingRollbackCommand.Rollback(notes, rollback, 3);
            Assert.AreEqual(notes[0].ToString(), "peace [X]");
            Assert.AreEqual(notes[1].ToString(), "war");
        }

        [TestMethod]
        public void CorrectHandlingCommandToggle()
        {
            var notes = new List<Note>
            {
                new Note { Text = "peace" } ,
                new Note { Text = "war", isCompletedFlag = true }
            };
            var rollback = new Stack<List<object>>();

            rollback.Push(new List<object> { "toggle", 0 });
            rollback.Push(new List<object> { "toggle", 1 });

            HandlingRollbackCommand.Rollback(notes, rollback, 2);
            Assert.AreEqual(notes[0].ToString(), "peace [X]");
            Assert.AreEqual(notes[1].ToString(), "war");
        }

        [TestMethod]
        public void CorrectHandlingCommandDelete()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var rollback = new Stack<List<object>>();

            rollback.Push(new List<object> { "delete", 0 });

            HandlingRollbackCommand.Rollback(notes, rollback, 1);
            Assert.AreEqual(notes.Count, 0);
        }

        [TestMethod]
        public void CorrectHandlingDifferentCommands()
        {
            var notes = new List<Note> { new Note { Text = "peace" } };
            var rollback = new Stack<List<object>>();

            rollback.Push(new List<object> { "toggle", 0 });
            rollback.Push(new List<object> { "edit", 0, "peace" });
            rollback.Push(new List<object> { "delete", 1 });
            rollback.Push(new List<object> { "delete", 2 });
            rollback.Push(new List<object> { "toggle", 0 });
            rollback.Push(new List<object> { "add", 2,new Note { Text = "peace",isCompletedFlag = true} });
            rollback.Push(new List<object> { "edit", 1, "world" });
            rollback.Push(new List<object> { "add", 0, new Note { Text = "war"} });

            HandlingRollbackCommand.Rollback(notes, rollback, 8);
            Assert.AreEqual(notes[0].ToString(), "peace");
        }
    }
}
