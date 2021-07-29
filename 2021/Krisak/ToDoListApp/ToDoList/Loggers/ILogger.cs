using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Loggers
{
   public interface ILogger
    {
        public void Log(string message);
    }
}
