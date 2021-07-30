using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MyToDoList
{
    public class FileManager
    {
        private readonly string _path = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "ToDoList.txt");

        public void SaveToFile(ToDoList list)
        {
            var arrayToSerialize = list.ToArray();
            var json = JsonSerializer.Serialize(arrayToSerialize);
            File.WriteAllText(_path, json);
        }

        public IEnumerable<ToDoItem> LoadFromFile()
        {
            if (!File.Exists(_path)) return new List<ToDoItem>();
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<ToDoItem[]>(json);
        }
    }
}
