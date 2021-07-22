using System;
using ToDoList;
using System.Xml.Serialization;

namespace Todo
{
    class Program
    {
        static void Main(string[] args)
        {
            var ToDo = new ToDoConsole();

            if (args.Length == 0)
                ToDo.DoWork();
            else ToDo.DoWork(args);
        }
    }
}
