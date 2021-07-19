using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListProject
{
    public class ConsoleWriterReader : IWriterReader
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
