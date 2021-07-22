using System;

namespace ToDo
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
