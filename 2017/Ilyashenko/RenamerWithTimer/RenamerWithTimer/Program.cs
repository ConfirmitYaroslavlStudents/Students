using System;
using ProcessorsLib;

namespace RenamerWithTimer
{
    class Program
    {
        static void Main(string[] args)
        {
            var processor = new FileProcessor();
            processor.Process(true);

            Console.ReadKey();
        }
    }
}
