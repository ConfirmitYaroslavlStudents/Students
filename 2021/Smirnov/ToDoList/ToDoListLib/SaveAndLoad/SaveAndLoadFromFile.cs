using System.Collections.Generic;
using System.IO;
using ToDoListLib.Models;

namespace ToDoListLib.SaveAndLoad
{
    public class SaveAndLoadFromFile : ISaveAndLoad
    {
        private const string FilePath = "tasks.txt";

        public IEnumerable<Task> Load()
        {
            var toDoList = new List<Task>();

            if (!File.Exists(FilePath))
            {
                return toDoList;
            }

            using var streamReader = new StreamReader(FilePath);
            for (var line = streamReader.ReadLine(); line != null; line = streamReader.ReadLine())
            {
                toDoList.Add(new Task
                {
                    Id = long.Parse(line),
                    Description = streamReader.ReadLine(),
                    Status = (TaskStatus) int.Parse(streamReader.ReadLine())
                });
            }

            return toDoList;
        }

        public void Save(IEnumerable<Task> toDoList)
        {
            using var streamWriter = new StreamWriter(FilePath);
            foreach (var task in toDoList)
            {
                streamWriter.WriteLine(task.Id);
                streamWriter.WriteLine(task.Description);
                streamWriter.WriteLine((int) task.Status);
            }
        }
    }
}
