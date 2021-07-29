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

        public void Save(List<Note> notes)
        {
            File.WriteAllLines(_fileName, ConvertNoteToSaveAndLoad.ConvertNoteToSave(notes));
        }

        public List<Note> Load ()
        {
            if (!File.Exists(_fileName))
                throw new FileNotFoundException("Saved data was not found. New list created.");

            var lines = File.ReadAllLines(_fileName);

            return new List<Note>(ConvertNoteToSaveAndLoad.ConvertLinesAfterLoading(lines));
        }
    }
}
