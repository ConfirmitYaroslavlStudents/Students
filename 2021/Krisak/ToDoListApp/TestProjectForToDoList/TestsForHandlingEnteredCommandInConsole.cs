using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList;

namespace TestsForToDoList
{
    [TestClass]
    public class TestsForHandlingEnteredCommandInConsole
    {
        [TestMethod]
        public void CorrectHandlingCommandAddOneWord()
        {
            var notes = new List<Note>();
            var handlingEnteredCommandInConsole = new HandlingEnteredCommandInConsole();

            handlingEnteredCommandInConsole.HandleInput(notes, "add world" );
            handlingEnteredCommandInConsole.HandleInput(notes, "add war");

            Assert.AreEqual(notes[0].ToString(), "world");
            Assert.AreEqual(notes[1].ToString(), "war");
        }

        [TestMethod]
        public void CorrectHandlingCommandAddSeveralWords()
        {
            var notes = new List<Note>();
            var handlingEnteredCommandInConsole = new HandlingEnteredCommandInConsole();

            handlingEnteredCommandInConsole.HandleInput(notes, "add world or war");

            Assert.AreEqual(notes[0].ToString(), "world or war");
        }

        [TestMethod]
        public void CorrectHandlingCommandEditOneWord()
        {
            var notes = new List<Note> { new Note { Text = "war" } };
            var handlingEnteredCommandInConsole = new HandlingEnteredCommandInConsole();

            handlingEnteredCommandInConsole.HandleInput(notes, "edit 1 world");

            Assert.AreEqual(notes[0].ToString(), "world");
        }

        [TestMethod]
        public void CorrectHandlingCommandEditSeveralWords()
        {
            var notes = new List<Note> { new Note { Text = "" } };
            var handlingEnteredCommandInConsole = new HandlingEnteredCommandInConsole();

            handlingEnteredCommandInConsole.HandleInput(notes, "edit 1 world or war");

            Assert.AreEqual(notes[0].ToString(), "world or war");
        }

        [TestMethod]
        public void CorrectParseCommandToggle()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var handlingEnteredCommandInConsole = new HandlingEnteredCommandInConsole();

            handlingEnteredCommandInConsole.HandleInput(notes, "toggle 1");

            Assert.AreEqual(notes[0].ToString(), "world [X]");
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

            var handlingEnteredCommandInConsole = new HandlingEnteredCommandInConsole();

            handlingEnteredCommandInConsole.HandleInput(notes, "delete 1");
            handlingEnteredCommandInConsole.HandleInput(notes, "delete 2");

            Assert.AreEqual(notes.Count, 1);
            Assert.AreEqual(notes[0].ToString(), "war");
        }

        [TestMethod]
        public void CorrectHandlingCommandRollbackAdd()
        {
            var notes = new List<Note>{ new Note { Text = "world" } };
            var handlingEnteredCommandInConsole = new HandlingEnteredCommandInConsole();

            handlingEnteredCommandInConsole.HandleInput(notes, "delete 1");
            handlingEnteredCommandInConsole.HandleInput(notes, "rollback 1");

            Assert.AreEqual(notes[0].ToString(), "world");
        }

        [TestMethod]
        public void CorrectHandlingCommandRollbackEdit()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var handlingEnteredCommandInConsole = new HandlingEnteredCommandInConsole();

            handlingEnteredCommandInConsole.HandleInput(notes, "edit 1 war");
            handlingEnteredCommandInConsole.HandleInput(notes, "rollback 1");

            Assert.AreEqual(notes[0].ToString(), "world");
        }

        [TestMethod]
        public void CorrectHandlingCommandRollbackToggle()
        {
            var notes = new List<Note> { new Note { Text = "world" } };
            var handlingEnteredCommandInConsole = new HandlingEnteredCommandInConsole();

            handlingEnteredCommandInConsole.HandleInput(notes, "toggle 1");
            handlingEnteredCommandInConsole.HandleInput(notes, "rollback 1");

            Assert.AreEqual(notes[0].ToString(), "world");
        }

        [TestMethod]
        public void CorrectHandlingCommandRollbackDelete()
        {
            var notes = new List<Note> { };
            var handlingEnteredCommandInConsole = new HandlingEnteredCommandInConsole();

            handlingEnteredCommandInConsole.HandleInput(notes, "add world");
            handlingEnteredCommandInConsole.HandleInput(notes, "rollback 1");

            Assert.AreEqual(notes.Count, 0);
        }

        [TestMethod]
        public void CorrectHandlingCommandRollbackSeveralCommand()
        {
            var notes = new List<Note> { };
            var handlingEnteredCommandInConsole = new HandlingEnteredCommandInConsole();

            handlingEnteredCommandInConsole.HandleInput(notes, "add world");
            handlingEnteredCommandInConsole.HandleInput(notes, "add war");
            handlingEnteredCommandInConsole.HandleInput(notes, "toggle 2");
            handlingEnteredCommandInConsole.HandleInput(notes, "add peace or war");
            handlingEnteredCommandInConsole.HandleInput(notes, "delete 2");
            handlingEnteredCommandInConsole.HandleInput(notes, "edit 2 war");
            handlingEnteredCommandInConsole.HandleInput(notes, "toggle 1");
            handlingEnteredCommandInConsole.HandleInput(notes, "edit 1 war");
            handlingEnteredCommandInConsole.HandleInput(notes, "delete 2");

            handlingEnteredCommandInConsole.HandleInput(notes, "rollback 9");

            Assert.AreEqual(notes.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringWrongCommand()
        {
            var notes = new List<Note>();
            var handlingEnteredCommandInConsole = new HandlingEnteredCommandInConsole();

            handlingEnteredCommandInConsole.HandleInput(notes, "IDoNothing");
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEnteredCommandException))]
        public void ExceptionEnteringEmptyCommand()
        {
            var notes = new List<Note>();
            var handlingEnteredCommandInConsole = new HandlingEnteredCommandInConsole();

            handlingEnteredCommandInConsole.HandleInput(notes, "");
        }
    }
}

