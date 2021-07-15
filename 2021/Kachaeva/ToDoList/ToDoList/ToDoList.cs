using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    [Serializable]
    public class ToDoList
    {
        private List<Task> tasks = new List<Task>();
        public int Count { get { return tasks.Count; } }

        public Task this[int index]
        {
            get { return tasks[index]; }
        }

        public void Add(string task)
        {
            tasks.Add(new Task(task));
        }

        public void Remove(int number)
        {
            tasks.RemoveAt(number);
        }

        public override string ToString()
        {
            StringBuilder toDoList = new StringBuilder();
            for (var i = 0; i < tasks.Count; i++)
            {
                toDoList.Append(i + 1 + ". " + tasks[i].text);
                if (tasks[i].isDone)
                    toDoList.AppendLine("  [v]");
                else
                    toDoList.AppendLine("  [ ]");
            }
            return toDoList.ToString();
        }
    }
}