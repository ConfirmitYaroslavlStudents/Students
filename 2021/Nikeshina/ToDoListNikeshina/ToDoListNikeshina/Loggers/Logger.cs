using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class Logger:ILogger
    {
        public void Log(string message) => Console.WriteLine(message);
    }
}
