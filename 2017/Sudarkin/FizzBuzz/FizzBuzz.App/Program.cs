using System;

namespace FizzBuzz.App
{
    internal class Program
    {
        private static void Main()
        {
            FizzBuzz fizzBuzz = new FizzBuzz();

            fizzBuzz.AddRule(2, "Banana");
            fizzBuzz.AddRule(17, "Orange");
            fizzBuzz.AddRule(4, "Grape");

            for (int i = 1; i <= 100; i++)
            {
                Console.WriteLine(
                    fizzBuzz.GetStringRepresentationFor(i));
            }

            Console.ReadKey();
        }
    }
}