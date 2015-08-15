using System;

namespace CommandCreation
{
    public class ConsoleWriter: IWriter
    {
        public void Write(string value)
        {
            Console.Write(value);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}
