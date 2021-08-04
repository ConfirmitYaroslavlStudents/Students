using System;

namespace ToDoListClient
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
