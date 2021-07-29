using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList;

namespace TestsForToDoList
{
    [TestClass]
   public class TestsForPerformerRollbackCommands
    {
        [TestMethod]
        public void CorrectPerformanceCommandAdd()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var command = new List<object> { "add", 1, new Note { Text = "peace"} };

            PerformerRollbackCommands.Add(notes, command);
            Assert.AreEqual(notes[1].ToString(), "peace");
        }

        [TestMethod]
        public void CorrectPerformanceCommandEdit()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var command = new List<object> { "edit", 0 ,"peace"};

            PerformerRollbackCommands.Edit(notes, command);
            Assert.AreEqual(notes[0].ToString(), "peace");
        }

        [TestMethod]
        public void CorrectPerformanceCommandToggle()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var command = new List<object> { "toggle", 0 };

            PerformerRollbackCommands.Toggle(notes, command);
            Assert.AreEqual(notes[0].ToString(), "world [X]");
        }

        [TestMethod]
        public void CorrectPerformanceCommandDelete()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var command = new List<object> { "delete", 0 };

            PerformerRollbackCommands.Delete(notes);
            Assert.AreEqual(notes.Count,0);
        }
    }
}
