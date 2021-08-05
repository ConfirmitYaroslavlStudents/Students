using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ToDoListNikeshina
{
    public enum StatusOfTask
    {
        Todo = 0,
        InProgress = 1,
        Done=2
    }
    public class Task
    {
        public string Name { get; private set; }
        public StatusOfTask Status { get;private set; }

        public Task(string name, int status)
        {
            Name = name;
            Status = (StatusOfTask)status;
        }

        public void ChangeStatus()
        {
            if (Status == StatusOfTask.Todo)
                Status =(StatusOfTask) 1;
            else if(Status == StatusOfTask.InProgress)
                Status = (StatusOfTask)2;
        }

        public void ChangeName(string newName)
        {
            Name = newName;
        }
        public string StringFormat()
        {
            var str = Name + " " + Status;
            return str;
        }

        public override bool Equals(object obj)
        {
            var other = (Task) obj;

            return this.Name == other.Name &&
                this.Status == other.Status;
        }
    }
}
