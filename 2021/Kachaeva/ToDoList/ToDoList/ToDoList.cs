﻿using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    public class ToDoList
    {
        private readonly List<Task> _tasks = new List<Task>();
        public int Count { get { return _tasks.Count; } }

        public Task this[int index]
        {
            get { return _tasks[index]; }
        }

        public void Add(Task task)
        {
            _tasks.Add(task);
        }

        public void Remove(int number)
        {
            _tasks.RemoveAt(number);
        }

        public override string ToString()
        {
            StringBuilder toDoList = new StringBuilder();
            for (var i = 0; i < _tasks.Count; i++)
                toDoList.AppendLine(i + 1 + ". " + _tasks[i].ToString());
            return toDoList.ToString();
        }
    }
}