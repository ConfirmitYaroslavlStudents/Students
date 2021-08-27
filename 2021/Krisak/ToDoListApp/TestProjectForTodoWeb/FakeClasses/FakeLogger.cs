using System;
using ToDoLibrary.Loggers;

namespace TestProjectForToDoLibrary
{
    public class FakeLogger: ILogger
    {
        public void Log(Exception e) { }
    }
}