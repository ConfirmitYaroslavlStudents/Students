using System;

namespace ToDoClient
{
    public class ConsoleReader : IToDoReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}