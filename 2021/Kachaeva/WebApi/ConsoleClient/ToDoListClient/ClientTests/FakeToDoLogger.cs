using System.Collections.Generic;
using ToDoClient;

namespace ClientTests
{
    public class FakeToDoLogger : IToDoLogger
    {
        public List<string> Messages { get; private set; }

        public FakeToDoLogger()
        {
            Messages = new List<string>();
        }

        public void Log(string message)
        {
            Messages.Add(message);
        }
    }
}