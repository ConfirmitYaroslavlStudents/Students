using System;

namespace Matrix.Writers
{
    public class FileWriter : IWriter
    {
        public void Write(string value)
        {
            Console.Write("<FILE: " + value + ">");
        }

        public void WriteLine()
        {
            Console.WriteLine("<FILE>");
        }

        public void Dispose()
        {
        }
    }
}