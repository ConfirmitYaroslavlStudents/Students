using System;
using System.Collections.Generic;
using System.Data;

namespace ToDoListLib
{
    public class ToDoList
    {
        private List<Task> _taskList;

        public ToDoList()
        {
            _taskList = new List<Task>();
        }
        public ToDoList(List<Task> taskList)
        {
            _taskList = taskList;
        }

        public int Count
        {
            get
            {
                return _taskList.Count;
            }
        }
       
        public void AddTask(string name, string description, TaskStatus status)
        {
            var task = new Task(name, description, status);

            if (!_taskList.Contains(task))
                _taskList.Add(new Task(name, description, status));
            else
                throw new InvalidOperationException(@$"Task ""{name}"" has already been added");
        }

        public Task GetTask(string name)
        {
            var taskIndex = GetTaskIndex(name);

            if (taskIndex >= 0)
                return _taskList[taskIndex];
            else
                throw new InvalidOperationException(@$"Task ""{name}"" not found");
        }
        private int GetTaskIndex(string name)
        {
            return _taskList.FindIndex(x => x.Name.Contains(name));
        }
        public void DeleteTask(string name)
        {
            _taskList.RemoveAt(GetTaskIndex(name));
        }
        public List<Task> GetAllTask()
        {
            return new List<Task>(_taskList);
        }       
    }
}
