using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListLib
{
    public class Task
    {
        public string Name { get; set; }
        public int Status { get; set; }

        public Task(string name, int status)
        {
            Name = name;
            Status = status;
        }

        public string Print()
        {
            var str = Name + ' ' + Status;
            return str;
        }
    }
}
