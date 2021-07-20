﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MyToDoList;

namespace ToDoApp
{
    static class DataHandler
    {
        private static readonly string Path = System.IO.Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "ToDoList.txt");

        public static void SaveToFile(ToDoList list)
        {
            var arrayToSerialize = list.ToArray();
            var json = JsonSerializer.Serialize(arrayToSerialize);
            File.WriteAllText(Path, json);
        }

        public static IEnumerable<ToDoItem> LoadFromFile()
        {
            if (!File.Exists(Path)) return new List<ToDoItem>();
            var json = File.ReadAllText(Path);
            return JsonSerializer.Deserialize<ToDoItem[]>(json);

        }
    }
}
