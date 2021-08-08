using System.IO;
using Newtonsoft.Json;

namespace ToDoApiDependencies
{
    public class FileLoaderAndSaver : ILoaderAndSaver
    {
        private readonly string _fileName;

        public FileLoaderAndSaver(string fileName)
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
                File.Delete(_fileName);
            else
                File.WriteAllText(_fileName, JsonConvert.SerializeObject(toDoList));
        }
    }
}
