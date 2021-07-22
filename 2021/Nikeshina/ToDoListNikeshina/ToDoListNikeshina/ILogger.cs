using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public interface ILogger
    {
        public void WriteLine(string message);
        public string ReadLine();
    }
}
