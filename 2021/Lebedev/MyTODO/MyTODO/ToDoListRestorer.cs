using System.IO;
using System.Collections.Generic;

namespace MyTODO
{
    public class ToDoListRestorer
    {
        private FileInfo _file;

        public ToDoListRestorer(FileInfo file)
        {
            _file = file;
        }

        public void Save(ToDoList list)
        {
            using var writer = new StreamWriter(_file.FullName);
            foreach (var each in list)
            {
                writer.WriteLine(each);
            }
        }

        public IEnumerable<ToDoItem> Read()
        {
            if (_file == null || !_file.Exists)
                return null;
            var items = new List<ToDoItem>();
            using var reader = new StreamReader(_file.FullName);
            while (!reader.EndOfStream)
            {
                items.Add(new ToDoItem(reader.ReadLine(), 
                                   !string.IsNullOrEmpty(reader.ReadLine()), 
                                   !string.IsNullOrEmpty(reader.ReadLine())));
            }
            return items;
        }
    }
}
