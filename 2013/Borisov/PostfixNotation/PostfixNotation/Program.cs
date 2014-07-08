using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostfixNotation
{
    class Program
    {
        static void Main(string[] args)
        {
            PostfixNotation example = new PostfixNotation();

            Console.WriteLine(example.MakePostfixNotation("a+(b-c)*d"));
            Console.WriteLine(example.MakePostfixNotation("3+4*2/(1-5)^2"));
            Console.WriteLine(example.MakePostfixNotation("a+b-c"));
            Console.WriteLine(example.MakePostfixNotation("89+45^12"));
            Console.WriteLine(example.MakePostfixNotation("alpha+gamma^omega+12*16"));

            Console.ReadKey();
        }
    }
}
