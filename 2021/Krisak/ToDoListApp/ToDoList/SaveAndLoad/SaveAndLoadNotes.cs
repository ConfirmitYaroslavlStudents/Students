using System.Collections.Generic;
using System.IO;

namespace ToDoLibrary
{
    public class SaveAndLoadNotes
    {
        private readonly string _fileName;

        public SaveAndLoadNotes(string fileName)
        {
            _fileName = fileName;
        }

        public void Save(List<Task> notes)
        {
            File.WriteAllLines(_fileName, ConvertTaskToSaveAndLoad.ConvertNoteToSave(notes));
        }

        public List<Task> Load()
        {
            if (!File.Exists(_fileName))
                throw new FileNotFoundException("Saved data was not found. New list created.");

            var lines = File.ReadAllLines(_fileName);

            return new List<Task>(ConvertTaskToSaveAndLoad.ConvertLinesAfterLoading(lines));
        }
    }
}
