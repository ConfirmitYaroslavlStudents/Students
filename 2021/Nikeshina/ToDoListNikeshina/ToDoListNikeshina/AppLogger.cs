using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class AppLogger : ILogger
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
