using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToDoListNikeshina
{
    public class WorkWithFile
    {
        public void Write(List<Task> list)
        {
            using (var sw = new StreamWriter("Tasks.txt"))
            {
                foreach (var task in list)
                    sw.WriteLine(task.Print());
            }
        }
        public List<Task> Read()
        {
            if (!File.Exists("Tasks.txt"))
            {
                MessageWriter.ToDoListIsEmpty();
                return new List<Task>();
            }
            
            List<Task> tasks = new List<Task>();
            using (var sr = new StreamReader("Tasks.txt"))
            {
                var str = sr.ReadLine();
                while (str != null && str != "")
                {
                    var words = str.Split(' ');
                    tasks.Add(new Task(GetName(words),GetStatus(words)));
                    str = sr.ReadLine();
                }
            }
            return tasks;
        }
        private bool GetStatus(string[] words)
        {
            var count = words.Length;
            if (words[count - 1] == "true")
                return true;

            return false;
        }

        private string GetName(string[] words)
        {
            var count = words.Length;
            var sb = new StringBuilder();
            for(int i=0;i<count-1;i++)
            {
                sb.Append(words[i] + " ");
            }
            return sb.ToString();
        }
    } 
}
