using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList;

namespace TestsForToDoList
{
    [TestClass]
    public class TestsForHandlingEnteredCommandInCMD
    {
        [TestMethod]
        public void CorrectHandlingCommandAddOneWords()
        {
            var notes = new List<Note>();

            HandlingEnteredCommandInCMD.HandleInput(notes, new string[] { "add", "world" });
            HandlingEnteredCommandInCMD.HandleInput(notes, new string[] { "add", "war" });

            Assert.AreEqual(notes[0].ToString(), "world");
            Assert.AreEqual(notes[1].ToString(), "war");
        }

        [TestMethod]
        public void CorrectHandlingCommanAddSeveralWords()
        {
            var notes = new List<Note>();

            HandlingEnteredCommandInCMD.HandleInput(notes, new string[] { "add", "world", "or", "war" });

            Assert.AreEqual(notes[0].ToString(), "world or war");
        }

        [TestMethod]
        public void CorrectHandlingCommandEditOneWord()
        {
            var notes = new List<Note>
            {
                new Note{Text = "war"},
                new Note{Text = "world"}
            };

            HandlingEnteredCommandInCMD.HandleInput(notes, new string[] { "edit","1", "world" });
            HandlingEnteredCommandInCMD.HandleInput(notes, new string[] { "edit","2", "war" });

            Assert.AreEqual(notes[0].ToString(), "world");
            Assert.AreEqual(notes[1].ToString(), "war");
        }

        [TestMethod]
        public void CorrectHandlingCommandEditSeveralWords()
        {
            var notes = new List<Note> { new Note { Text = "" } };

            HandlingEnteredCommandInCMD.HandleInput(notes, new string[] { "edit","1", "world", "or", "war" });

            Assert.AreEqual(notes[0].ToString(), "world or war");
        }

        [TestMethod]
        public void CorrectHandlingCommandToggle()
        {
            var notes = new List<Note>
            {
                new Note{Text = "world"},
                new Note{Text = "war", isCompletedFlag = true}
            };

            HandlingEnteredCommandInCMD.HandleInput(notes, new string[] { "toggle", "1" });
            HandlingEnteredCommandInCMD.HandleInput(notes, new string[] { "toggle", "2" });

            Assert.AreEqual(notes[0].ToString(), "world [X]");
            Assert.AreEqual(notes[1].ToString(), "war");
        }

        [TestMethod]
        public void CorrectHandlingCommandDelete()
        {
            var notes = new List<Note>
            {
                new Note{Text = "world"},
                new Note{Text = "war"},
                new Note {Text = "peace"}
            };

            HandlingEnteredCommandInCMD.HandleInput(notes, new string[] { "delete", "1" });
            HandlingEnteredCommandInCMD.HandleInput(notes, new string[] { "delete", "2" });

            Assert.AreEqual(notes.Count, 1);
            Assert.AreEqual(notes[0].ToString(), "war");
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongCommand()
        {
            var notes = new List<Note>();

            HandlingEnteredCommandInCMD.HandleInput(notes, new string[] { "IDoNothing" });
        }
        
            [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringEmptyCommand()
        {
            var notes = new List<Note>();

            HandlingEnteredCommandInCMD.HandleInput(notes, new string[] { });
        }
    }
}
