using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public interface ILogger
    {
        public void Recording(string message);
        public string TakeData();
    }
}
