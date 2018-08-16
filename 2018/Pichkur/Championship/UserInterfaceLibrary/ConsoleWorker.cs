using System;

namespace Championship
{
    public class ConsoleWorker : IDataInputWorker
    {
        public string InputString()
        {
            return Console.ReadLine();
        }

        public void WriteLineMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteMessage(string message)
        {
            Console.Write(message);
        }
    }
}
