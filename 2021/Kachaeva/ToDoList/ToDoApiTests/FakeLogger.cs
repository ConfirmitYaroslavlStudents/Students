using System.Collections.Generic;
using ToDo;

namespace ToDoApiTests
{
    public class FakeLogger : ILogger
    {
        public List<string> Messages { get; private set; }

        public FakeLogger()
        {
            Messages = new List<string>();
        }

        public void Log(string message)
        {
            Messages.Add(message);
        }
    }
}