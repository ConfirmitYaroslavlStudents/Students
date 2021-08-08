using System;

namespace ToDoClient
{
    public class ConsoleLogger : IToDoLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
        }
    }
}