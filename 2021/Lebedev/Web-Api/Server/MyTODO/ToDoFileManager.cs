using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

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
            writer.WriteLine(JsonConvert.SerializeObject(list));
            writer.Close();
        }

        public IEnumerable<ToDoItem> Read()
        {
            if (_file == null || !_file.Exists)
                return null;
            var items = new List<ToDoItem>();
            using var reader = new StreamReader(_file.FullName);
            if (!reader.EndOfStream)
                items = new List<ToDoItem>(JsonConvert.DeserializeObject<List<ToDoItem>>(reader.ReadLine()));
            return items;
        }
    }
}
