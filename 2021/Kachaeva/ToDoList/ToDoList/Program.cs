using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ToDoListProject
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var fileName = "ToDoList.txt";
            var fileWorkHandler = new FileWorkHandler(fileName);
            var consoleWriterReader = new ConsoleWriterReader();
            var controller = new Controller(fileWorkHandler, consoleWriterReader);
            controller.HandleUsersInput();
        }
    }
}
