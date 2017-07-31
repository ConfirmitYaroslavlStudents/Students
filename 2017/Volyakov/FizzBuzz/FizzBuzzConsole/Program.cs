using System;
using FizzBuzzLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzzConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            FizzBuzz fizzBuzz = new FizzBuzz();

            fizzBuzz.AddDivisor(6, "Vazz");
            fizzBuzz.AddDivisor(7, "Tazz");
            fizzBuzz.AddDivisor(17, "Kavkazz");

            for (int i = 1; i <= 100; i++)
            {
                Console.WriteLine(
                    fizzBuzz.GetReaction(i));
            }

            Console.ReadKey();
        }
    }
}
