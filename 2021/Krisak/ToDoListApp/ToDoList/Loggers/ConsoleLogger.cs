using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Loggers
{
    public class ConsoleLogger: ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
