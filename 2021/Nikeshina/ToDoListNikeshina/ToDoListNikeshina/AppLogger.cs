using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class AppLogger : ILogger
    {

        public string TakeData()
        {
            return Console.ReadLine();
        }

        public void Recording(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
