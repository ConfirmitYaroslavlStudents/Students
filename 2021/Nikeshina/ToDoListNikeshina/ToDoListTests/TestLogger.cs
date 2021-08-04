using System;
using System.Collections.Generic;
using ToDoListNikeshina;

namespace ToDoListTests
{
    class TestLogger : ILogger
    {
        public List<string> Messages = new List<string>();

        public void Log(string msg)
        {
            Messages.Add(msg);
        }
    }
}
