using System;

namespace ToDoConsole
{
    public static class CommandReader
    {
        public static string[] GetCommand()
            => Console.ReadLine().Split(' ');
    }
}