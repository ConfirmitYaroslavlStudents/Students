using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class Task
    {
        public string Name { get; private set; }
        public bool Status { get;private set; }

        public Task(string name, bool status)
        {
            Name = name;
            Status = status;
        }

        public void ChangeStatus()
        {
            Status = !Status;
        }

        public void ChangeName(string newName)
        {
            Name = newName;
        }
        public override string ToString()
        {
            var str = Name + " " + Status;
            return str;
        }
    }
}
