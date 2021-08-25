using System.IO;
using System.Collections.Generic;
using Ne

namespace MyTODO
{
    public class ToDoFileManager
    {
        private FileInfo _file;

        public ToDoFileManager(FileInfo file)
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
                items.Add(new ToDoItem(items.Count,reader.ReadLine(), 
                                   !string.IsNullOrEmpty(reader.ReadLine()), 
                                   !string.IsNullOrEmpty(reader.ReadLine())));
            }
            return items;
        }
    }
}
