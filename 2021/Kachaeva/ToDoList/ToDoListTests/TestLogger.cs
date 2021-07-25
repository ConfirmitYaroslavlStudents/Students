using System.Collections.Generic;

namespace ToDo
{
    public class TestLogger : ILogger
    {
        public List<string> Messages { get; private set; }

        public TestLogger()
        {
            Messages = new List<string>();
        }

        public void Log(string message)
        {
            Messages.Add(message);
        }
    }
}