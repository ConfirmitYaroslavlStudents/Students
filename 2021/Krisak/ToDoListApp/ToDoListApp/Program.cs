using System;
using ToDoList;
using System.Xml.Serialization;

namespace Todo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                var ToDo = new ToDoConsole();
                ToDo.DoWork();
            }
            else
            {
                var ToDo = new ToDoCMD();
                ToDo.DoWork(args);
            }
        }
    }
}
