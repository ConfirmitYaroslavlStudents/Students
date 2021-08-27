using System;
using ToDoLibrary.Loggers;

namespace TestProjectForToDoLibrary
{
    public class FakeLogger: ILogger
    {
        public Exception @Exception;

        public void Log(Exception e)
            => @Exception = e;
    }
}