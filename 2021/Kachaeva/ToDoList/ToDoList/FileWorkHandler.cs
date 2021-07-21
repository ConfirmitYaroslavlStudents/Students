using System.IO;

namespace ToDoListProject
{
    public class FileWorkHandler : IToDoListLoaderSaver
    {
        private readonly string _fileName;

        public FileWorkHandler(string fileName)
        {
            _fileName = fileName;
        }

        public ToDoList Load()
        {
            if (!File.Exists(_fileName))
                return new ToDoList();

            var toDoList = new ToDoList();
            var lines = File.ReadLines(_fileName);
            foreach(var line in lines)
            {
                var task = Parse(line);
                toDoList.Add(task);
            }
            return toDoList;
        }

        private Task Parse(string line)
        {
            var textStartIndex = line.IndexOf('.') + 2;
            var textEndIndex = line.IndexOf('[') - 2;
            var text = line[textStartIndex..textEndIndex];
            var isDone = false;
            if (line[textEndIndex + 3] == 'v')
                isDone = true;
            return new Task(text, isDone);
        }

        public void Save (ToDoList toDoList)
        {
            if (toDoList.Count != 0)
                File.WriteAllText(_fileName, toDoList.ToString());
        }
    }
}
