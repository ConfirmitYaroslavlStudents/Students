using System;
using System.IO;
using System.Collections.Generic;

namespace MyTODO
{
    public class ToDoListReducer
    {
        static public void Save(FileInfo file, ToDoList list)
        {
            using var writer = new StreamWriter(file.FullName);
            foreach (var each in list)
            {
                writer.WriteLine("{0}\n{1}", each.Name, each.State);
            }
        }

        static public IEnumerable<ToDoItem> Read(FileInfo file)
        {
            if (file == null || !file.Exists)
                return null;
            List<ToDoItem> items = new List<ToDoItem>();
            using (StreamReader reader = new StreamReader(file.FullName))
            {
                while (!reader.EndOfStream)
                {
                    items.Add(new ToDoItem(reader.ReadLine(), int.Parse(reader.ReadLine())));
                }
            }
            return items;
        }
    }
}
