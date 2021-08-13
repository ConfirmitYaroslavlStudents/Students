using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ToDoLibrary
{
    public class TasksSaverAndLoader
    {
        private readonly string _fileName;

        public TasksSaverAndLoader(string fileName)
        {
            _fileName = fileName;
        }

        public void Save(IEnumerable<Task> tasks)
        {

            File.WriteAllLines(_fileName,TaskToSaveAndLoadConverter.ConvertNoteToSave(tasks));
        }

        public List<Task> Load()
        {
            if (!File.Exists(_fileName))
                throw new FileNotFoundException("Saved data was not found. New list created.");

            return new List<Task>(TaskToSaveAndLoadConverter.ConvertLinesAfterLoading(File.ReadAllLines(_fileName)));
        }
    }
}
