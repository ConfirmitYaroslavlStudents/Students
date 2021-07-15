using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
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
