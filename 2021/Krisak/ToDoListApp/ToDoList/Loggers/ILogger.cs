using System;

namespace ToDoLibrary.Loggers
{
    public interface ILogger
    {
        public void Log(Exception e);
    }
}
