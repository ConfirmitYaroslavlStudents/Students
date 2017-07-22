using System;
using Var1Processor = TimeMeasurer.Variant1.Processor;
using Var2Processor = TimeMeasurer.Variant2.Processor;

namespace TimeMeasurer
{
    internal class Program
    {
        private static void Main()
        {
            new Var1Processor().Process();
            Console.WriteLine();
            new Var2Processor().Process();

            Console.ReadKey();
        }
    }
}