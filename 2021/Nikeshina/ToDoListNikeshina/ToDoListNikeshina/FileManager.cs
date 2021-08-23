using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToDoListNikeshina
{
    public  class FileManager
    {
        private  int _countNotesInProgress;

        public void Save(List<Task> list)
        {
            using (var sw = new StreamWriter(FileConfig.FilePath))
            {
                foreach (var task in list)
                    sw.WriteLine(task.StringFormat());
            }
        }

        public KeyValuePair<List<Task>,int> Load()
        {
            if (!File.Exists("Tasks.txt"))
                return new KeyValuePair<List<Task>, int>(new List<Task>(),-1);

            List<Task> tasks = new List<Task>();
            var idcount = 0;
            using (var sr = new StreamReader(FileConfig.FilePath))
            {
                var str = sr.ReadLine();
                while (str!=null && str != "")
                {
                    var words = str.Split(' ');
                    int id = GetTaskId(words);
                    if (id > idcount)
                        idcount = id;
                    tasks.Add(new Task(GetName(words), GetStatus(words),id));
                    str = sr.ReadLine();
                }
            }
            return new KeyValuePair<List<Task>, int>(tasks,idcount);
        }

        private int GetTaskId(string [] words)
        {
            return int.Parse(words[0]);
        }

        private StatusOfTask GetStatus(string[] words)
        {
            var count = words.Length;
            var currentTaskStatus = words[count - 1];

            if (currentTaskStatus == "InProgress")
            {
                _countNotesInProgress++;
                return StatusOfTask.InProgress;
            }
            else if (currentTaskStatus == "Todo")
                return StatusOfTask.Todo;
            else
                return StatusOfTask.Done;

        }

        public int GetCountOfTaskInProgress() => _countNotesInProgress;

        private string GetName(string[] words)
        {
            var count = words.Length;
            var sb = new StringBuilder();

            for (int i = 1; i < count - 2; i++)
                sb.Append(words[i] + " ");

            sb.Append(words[count - 2]);
            return sb.ToString();
        }
    }
}
