using System;
using RomaCalculator;
using RomaCalculator.KindsOfOperators;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var Roma = new Calculator();
            var result = Roma.Calculate("V", "III", new Addition());
            Console.ReadLine();
        }
    }
}
