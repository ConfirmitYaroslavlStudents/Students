using System.IO;
using Newtonsoft.Json;

namespace ToDo
{
    public class FileLoaderSaver : ILoaderSaver
    {
        private readonly string _fileName;

        public FileLoaderSaver(string fileName)
        {
            _fileName = fileName;
        }

        public ToDoList Load()
        {
            if (!File.Exists(_fileName))
                return new ToDoList();
            return JsonConvert.DeserializeObject<ToDoList>(File.ReadAllText(_fileName));
            
        }

        public void Save (ToDoList toDoList)
        {
            if (toDoList.Count == 0)
            {
                if(File.Exists(_fileName))
                    File.Delete(_fileName);
            }
            else
                File.WriteAllText(_fileName, JsonConvert.SerializeObject(toDoList));
        }
    }
}
