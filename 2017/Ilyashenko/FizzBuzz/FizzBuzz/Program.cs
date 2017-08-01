using System;
using FizzBuzzLib;

namespace FizzBuzzApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var fizzBuzz = new FizzBuzz(new Rule[] 
            {
                new Rule(11, "Banana"),
                new Rule(17, "Apple")
            });

            fizzBuzz.AddRule(2, "Hello");
            fizzBuzz.AddRule(7, "World");

            for (int i = 1; i <= 100; i++)
                Console.WriteLine(fizzBuzz.Process(i));

            Console.ReadKey();
        }
    }
}
