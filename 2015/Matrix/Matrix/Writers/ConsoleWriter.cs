using System;

namespace Matrix.Writers
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string value)
        {
            Console.Write(value);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void Dispose()
        {
        }
    }
}