using System;
using FizzBuzzLib;

namespace FizzBuzzApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var fizzBuzz = new FizzBuzz(new string[] { "11 Banana", "17 Orange"});
            fizzBuzz.AddRule(2, "Hello");
            fizzBuzz.AddRule(7, "World");

            for (int i = 1; i <= 100; i++)
                Console.WriteLine(fizzBuzz.Process(i));

            Console.ReadKey();
        }
    }
}
