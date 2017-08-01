using System;
using System.Collections.Generic;

namespace FizzBuzz.App
{
    internal class Program
    {
        private static void Main()
        {
            FizzBuzz fizzBuzz = new FizzBuzz(new Dictionary<int, string>
            {
                { 2, "Banana" },
                { 17, "Orange" },
                { 4, "Grape" }
            });


            FizzBuzzProcessor processor = new FizzBuzzProcessor(fizzBuzz);

            foreach (string item in processor.Process(1, 100))
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}