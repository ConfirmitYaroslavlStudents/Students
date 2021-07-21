using System.IO;
using System.Collections.Generic;

namespace MyTODO
{
    public class ToDoListReducer
    {
        public static void Save(FileInfo file, ToDoList list)
        {
            using var writer = new StreamWriter(file.FullName);
            foreach (var each in list)
            {
                writer.WriteLine("{0}\n{1}", each.Name, each.State);
            }
        }

        public static IEnumerable<ToDoItem> Read(FileInfo file)
        {
            if (!(file is {Exists: true}))
                return null;
            var items = new List<ToDoItem>();
            using var reader = new StreamReader(file.FullName);
            while (!reader.EndOfStream)
            {
                items.Add(new ToDoItem(reader.ReadLine(), int.Parse(reader.ReadLine())));
            }
            return items;
        }
    }
}
