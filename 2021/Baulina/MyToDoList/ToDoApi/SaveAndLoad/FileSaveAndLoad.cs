using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ToDoListLib;

namespace ToDoApi.SaveAndLoad
{
    public class FileSaveAndLoad : IListSaveAndLoad
    {
        private readonly string _path = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "ToDoList.txt");

        public IEnumerable<ToDoItem> LoadTheList()
        {
            if (!File.Exists(_path)) return new List<ToDoItem>();
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<ToDoItem[]>(json);
        }

        public void SaveTheList(IEnumerable<ToDoItem> toDoList)
        {
            var arrayToSerialize = toDoList.ToArray();
            var json = JsonSerializer.Serialize(arrayToSerialize);
            File.WriteAllText(_path, json);
        }
    }
}
