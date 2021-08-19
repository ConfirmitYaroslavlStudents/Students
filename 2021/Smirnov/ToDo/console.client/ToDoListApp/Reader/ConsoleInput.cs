using System;

namespace ToDoListApp.Reader
{
    public class ConsoleInput: IConsoleInput
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
