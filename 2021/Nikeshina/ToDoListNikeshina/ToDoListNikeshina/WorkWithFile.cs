using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToDoListNikeshina
{
    public class WorkWithFile
    {
        public ILogger _logger;
        public WorkWithFile() { }
        public WorkWithFile(ILogger logger)
        {
            this._logger = logger;
        }
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
                _logger.WriteLine(Messages.ToDoListIsEmpty());
               
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

    public interface ILogger
    {
        public void WriteLine(string message);
        public string ReadLine();
    }

    public class TestLogger : ILogger
    {
        public List<string> Messages = new List<string>();
        public List<string> inputStrings = new List<string>();


        public TestLogger() { }

        public TestLogger(List<string> input)
        {
            inputStrings = input;
        }

        public string ReadLine()
        {
            var current=inputStrings[0];
            inputStrings.RemoveAt(0);
            return current;
        }

        public void WriteLine(string msg)
        {
            Messages.Add(msg);
        }
    }

    public class AppLogger : ILogger
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
