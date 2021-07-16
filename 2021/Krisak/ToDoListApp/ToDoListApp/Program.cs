using System;
using ToDoList;
using System.Xml.Serialization;

namespace ToDoListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var ToDo = new ToDoConsole();
            ToDo.DoWork();
        }
    }
}
