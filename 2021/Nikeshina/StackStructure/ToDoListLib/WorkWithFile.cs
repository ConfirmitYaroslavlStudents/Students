using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToDoListLib
{
    public class WorkWithFile
    {
        private StringBuilder sb;
        public WorkWithFile()
        {
            sb = new StringBuilder();
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
            List<Task> tasks = new List<Task>();
            using (var sr = new StreamReader("Tasks.txt"))
            {
                var str = sr.ReadLine();
                while (str != null && str != "")
                {
                    var text = str.Substring(0, str.Length - 2);
                    var status = str.Substring(str.Length - 1, 1);
                    tasks.Add(new Task(text, int.Parse(status)));
                    str = sr.ReadLine();
                }
            }
            return tasks;
        }
    }
}
