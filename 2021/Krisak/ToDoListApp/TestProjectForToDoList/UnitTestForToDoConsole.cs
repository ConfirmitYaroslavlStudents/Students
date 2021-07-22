using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList;
using System.IO;

namespace TestProjectForToDoList
{
    [TestClass]
    public class UnitTestForToDoConsole
    {
        TestToDoConsole _todo = new TestToDoConsole();
        [TestMethod]
        public void CorrectCommandAddingOneWord()
        {
            _todo.SelectionChoise(new string[] { "add", "world" });
            Assert.AreEqual(_todo._notes[0].ToString(), "world");
        }

        [TestMethod]
        public void CorrectCommandAddingSeveralWords()
        {
            _todo.SelectionChoise(new string[] { "add", "world or war" });
            Assert.AreEqual(_todo._notes[0].ToString(), "world or war");
        }

        [TestMethod]
        public void CorrectCommandEditingOneWord()
        {
            _todo.SelectionChoise(new string[] { "add", "world" });
            _todo.SelectionChoise(new string[] { "edit", "1", "peace" });
            Assert.AreEqual(_todo._notes[0].ToString(), "peace");
        }

        [TestMethod]
        public void CorrectCommandEditingSeveralWords()
        {
            _todo.SelectionChoise(new string[] { "add", "world or war" });
            _todo.SelectionChoise(new string[] { "edit", "1", "world and peace" });
            Assert.AreEqual(_todo._notes[0].ToString(), "world and peace");
        }

        [TestMethod]
        public void CorrectCommandToggleIsCompletedNote()
        {
            _todo.SelectionChoise(new string[] { "add", "world" });
            _todo.SelectionChoise(new string[] { "toggle", "1" });
            Assert.AreEqual(_todo._notes[0].ToString(), "X world");
        }

        [TestMethod]
        public void CountListAfterDeleteNote()
        {
            _todo.SelectionChoise(new string[] { "add", "world" });
            _todo.SelectionChoise(new string[] { "add", "war" });
            _todo.SelectionChoise(new string[] { "add", "peace" });
            _todo.SelectionChoise(new string[] { "delete", "2" });
            Assert.AreEqual(_todo._notes.Count, 2);
        }

        [TestMethod]
        public void ItemsListAfterDeleteNote()
        {
            _todo.SelectionChoise(new string[] { "add", "world" });
            _todo.SelectionChoise(new string[] { "add", "war" });
            _todo.SelectionChoise(new string[] { "add", "peace" });
            _todo.SelectionChoise(new string[] { "delete", "2" });
            Assert.AreEqual(_todo._notes[0].ToString(), "world");
            Assert.AreEqual(_todo._notes[1].ToString(), "peace");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExceptionEnteringInvalidCommand()
        {
            _todo.SelectionChoise(new string[] { "edit","0" });
        }
    }
    public class TestToDoConsole
    {
        SaveAndLoadNotes _saveAndLoad = new SaveAndLoadNotes("MyNotes.txt");
        public List<Note> _notes = new List<Note>();
        Logger _logger = new Logger();
        bool isWork;
        string[] _formatInputHelp = new string[]
        {
            "Input format:",
            "Сreating new note: \"add *notes*\"",
            "Edit note: \"edit 00 *new notes*\"",
            "Toggle progress status note: \"toggle 00\"",
            "Delete note: \"delete 00\"",
            "Show all list: \"show\"",
            "Exit: \"bye\"",
        };

        public TestToDoConsole()
        {
            LoadNotes();
        }

        public void DoWork()
        {
            isWork = true;

            while (isWork)
            {
                DisplayFormatInputHelp();
                try
                {
                    DisplaySeparator();

                    var choise = Console.ReadLine().Split(" ");

                    DisplaySeparator();

                    SelectionChoise(choise);
                    SaveNotes();
                }
                catch
                {
                    _logger.Log("You entered wrong command!");
                }
            }

            Console.WriteLine("Bye Bye ^-^");
            Console.ReadKey();
        }

        public void DoWork(string[] command)
        {
            try
            {
                SelectionChoise(command);
                SaveNotes();
            }
            catch
            {
                _logger.Log("You entered wrong command!");
            }
        }

       public void SelectionChoise(string[] choise)
        {
            var command = choise[0];
            switch (command)
            {
                case "add":
                    Add(ConvertNoteToString(1, choise));
                    break;

                case "edit":
                    Edit(CorrectnessEnteringIndex(choise[1]), ConvertNoteToString(2, choise));
                    break;

                case "toggle":
                    ToggleIsCompleted(CorrectnessEnteringIndex(choise[1]));
                    break;

                case "delete":
                    Delete(CorrectnessEnteringIndex(choise[1]));
                    break;
                case "show":
                    DisplayAllNotes();
                    break;

                case "bye":
                    isWork = false;
                    break;

                default: { throw new ArgumentException(); }
            }
        }

        void Add(string note)
        {
            _notes.Add(new Note { Text = note, isCompleted = false });
        }
        void Edit(int index, string newNote)
        {
            _notes[index].Text = newNote;
        }
        string ConvertNoteToString(int index, string[] part)
        {
            var newNote = new StringBuilder("");

            for (var i = index; i < part.Length; i++)
                newNote.Append(part[i] + " ");

            return newNote.ToString().Trim();
        }

        void ToggleIsCompleted(int index)
        {
            _notes[index].isCompleted = !_notes[index].isCompleted;
        }

        void Delete(int index)
        {
            _notes.RemoveAt(index);
        }

        int CorrectnessEnteringIndex(string inputIndex)
        {
            var result = 0;
            int.TryParse(inputIndex, out result);
            if (result <= 0 || result > _notes.Count)
                throw new ArgumentException();

            return result - 1;
        }

        private void SaveNotes()
        {
            var newNotesList = new List<string>();
            foreach (var note in _notes)
                newNotesList.Add(note.ToString());

            _saveAndLoad.Save(newNotesList);
        }

        private void LoadNotes()
        {
            try
            {
                _notes = _saveAndLoad.Load();
            }
            catch
            {
                _notes = new List<Note>();
                _logger.Log("Saved data was not found. New list created.");
            }
        }

        void DisplayAllNotes()
        {
            for (var i = 0; i < _notes.Count; i++)
                Console.WriteLine("{0}. {1}", i + 1, _notes[i].ToString());
            DisplaySeparator();
        }

        void DisplayFormatInputHelp()
        {
            for (var i = 0; i < _formatInputHelp.Length; i++)
                Console.WriteLine(_formatInputHelp[i]);
        }

        void DisplaySeparator()
        {
            Console.WriteLine("---------------");
        }
    }
}
