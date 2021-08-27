using System.Collections.Generic;
using System.IO;

namespace ToDoLibrary.SaveAndLoad
{
    public class FileSaver: ISaveTasks
    {
        private readonly string _fileName;

        public FileSaver(string fileName)
            => _fileName = fileName;

        public void Save(List<Task> tasks)
        => File.WriteAllLines(_fileName, TasksToSaveAndLoadConverter.ConvertTasksToSave(tasks));
    }
}