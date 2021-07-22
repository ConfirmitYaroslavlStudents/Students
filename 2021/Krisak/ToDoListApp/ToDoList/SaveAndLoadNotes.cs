using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ToDoList
{
    public class SaveAndLoadNotes
    {
        private string _fileName;

        public SaveAndLoadNotes(string fileName)
        {
            _fileName = fileName;
        }

        public void Save(List<string> notes)
        {
                File.WriteAllLines(_fileName, notes);
        }

        public List<Note> Load ()
        {
            if (!File.Exists(_fileName))
                throw new FileNotFoundException();

            var notesList = File.ReadAllLines(_fileName);
            List<Note> newNotesList = new List<Note>();

            foreach (var note in notesList)
                newNotesList.Add(ConvertingNote(note));

            return newNotesList;
        }

        private static Note ConvertingNote(string note)
        {
            if (note.IndexOf("X ") == 0)
                return new Note{Text = note.Substring(2), isCompleted = true };
            return new Note { Text = note, isCompleted = false };
        }
    }
}
