using System;
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
            using (var sw = new StreamWriter("Tasks.txt"))
            {
                foreach (var task in list)
                    sw.WriteLine(task.PrintTask());
            }
        }

        public List<Task> Load()
        {
            if (!File.Exists("Tasks.txt"))
                return new List<Task>();

            List<Task> tasks = new List<Task>();
            using (var sr = new StreamReader("Tasks.txt"))
            {
                var str = sr.ReadLine();
                while (str != null && str != "")
                {
                    var words = str.Split(' ');
                    tasks.Add(new Task(GetName(words), GetStatus(words)));
                    str = sr.ReadLine();
                }
            }
            return tasks;
        }

        private int GetStatus(string[] words)
        {
            var count = words.Length;
            int currentTaskStatus = int.Parse(words[count - 1]);

            if (currentTaskStatus == 1)
                _countNotesInProgress++;

            return int.Parse(words[count - 1]);
        }

        public int GetCountOfTaskInProgress() => _countNotesInProgress;

        private string GetName(string[] words)
        {
            var count = words.Length;
            var sb = new StringBuilder();

            for (int i = 0; i < count - 2; i++)
                sb.Append(words[i] + " ");

            sb.Append(words[count - 2]);
            return sb.ToString();
        }
    }
}
