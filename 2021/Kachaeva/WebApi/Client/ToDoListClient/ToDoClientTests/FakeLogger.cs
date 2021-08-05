using System.Collections.Generic;
using ToDoClient;

namespace ToDoClientTests
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