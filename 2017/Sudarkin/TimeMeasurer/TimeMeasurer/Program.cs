using System;

namespace TimeMeasurer
{
    internal class Program
    {
        private static void Main()
        {
            new Processor().Process();

            Console.ReadKey();
        }
    }
}