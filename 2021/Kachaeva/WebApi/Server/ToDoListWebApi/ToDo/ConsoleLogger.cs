using System;

namespace ToDo
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
        }
    }
}
