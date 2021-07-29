using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ToDoList;
using System.Collections.Generic;

namespace TestsForToDoList
{
    [TestClass]
    public class TestsForPerformerCommands
    {
        [TestMethod]
        public void CorrectPerformanceCommandAdd()
        {
            var notes = new List<Note>();
            var command = new List<object> { "add", "world or war" };
            PerformerCommands.Add(notes, command);
            Assert.AreEqual(notes[0].ToString(), "world or war");
        }

        [TestMethod]
        public void CorrectPerformanceCommandEdit()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var command = new List<object> { "edit", 0, "peace" };
            PerformerCommands.Edit(notes,command);
            Assert.AreEqual(notes[0].ToString(), "peace");
        }

        [TestMethod]
        public void CorrectPerformanceCommandToggleIsCompletedNote()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var command = new List<object> { "toggle", 0};
            PerformerCommands.Toggle(notes, command);
            Assert.AreEqual(notes[0].ToString(), "world [X]");
        }

        [TestMethod]
        public void CountListAfterDeleteNote()
        {
            var notes = new List<Note>();
            var command = new List<object> { "add", "world" };
            PerformerCommands.Add(notes, command);
            command = new List<object> { "add", "war" };
            PerformerCommands.Add(notes, command);
            command = new List<object> { "delete", 1 };
            PerformerCommands.Delete(notes, command);
            command = new List<object> { "delete", 0 };
            PerformerCommands.Delete(notes, command);
            Assert.AreEqual(notes.Count, 0);
        }

        [TestMethod]
        public void ItemsListAfterDeleteNote()
        {
            var notes = new List<Note>();
            var command = new List<object> { "add", "world" };
            PerformerCommands.Add(notes, command);
            command = new List<object> { "add", "war" };
            PerformerCommands.Add(notes, command);
            command = new List<object> { "delete", 1 };
            PerformerCommands.Delete(notes, command);
            Assert.AreEqual(notes[0].ToString(), "world");
        }

        [TestMethod]
        public void CorrectPerformanceCommandRollback()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var command = new List<object> { "rollback", 1 };
            var rollback = new Stack<List<object>>();
           rollback.Push( new List<object> { "add", 0, new Note { Text = "peace" } });

            PerformerCommands.Rollback(notes, rollback, command);
            Assert.AreEqual(notes[0].ToString(), "peace");
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringInvalidIndexCommandEdit()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var command = new List<object> { "edit", 1, "peace" };
            PerformerCommands.Edit(notes, command);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringInvalidIndexCommandToggle()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var command = new List<object> { "toggle", 1 };
            PerformerCommands.Toggle(notes, command);
        }
        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringInvalidIndexCommandDelete()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var command = new List<object> { "delete", 1 };
            PerformerCommands.Delete(notes, command);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringInvalidIndexCommandRollback()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var command = new List<object> { "rollback", 1 };
            var rollback = new Stack<List<object>>();
            PerformerCommands.Rollback(notes, rollback, command);
        }
    }
}
