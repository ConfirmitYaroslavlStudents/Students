using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackWithUseArray
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Stack<int> Example = new Stack<int>();
            Example.Push(124);
            Example.Push(605);
            Example.Push(89);
            Example.Push(1);
            int length = Example.Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(Example.Pop());
            }
            Example.Push(194);
            Console.WriteLine(Example.Peek());
            Stack<string> SecondExample = new Stack<string>();
            SecondExample.Push("know");
            SecondExample.Push("don`t");
            SecondExample.Push("i");
            SecondExample.Push("no?");
            SecondExample.Push("yes?");
            length = SecondExample.Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(SecondExample.Pop());
            }
            Stack<string> ThirdExample = new Stack<string>();
            ThirdExample.Push("!!!");
            ThirdExample.Push("Go");
            ThirdExample.Push(" 3 ");
            ThirdExample.Push(" 2 ");
            ThirdExample.Push(" 1 ");
            foreach (string t in ThirdExample)
            {
                Console.WriteLine(t);
            }

            Stack<int> Example105 = new Stack<int>();
            Example105.Push(2);
            Example105.Push(4);
            Example105.Push(8);
            Example105.Push(16);
            foreach (int t in Example105)
            {
                Console.WriteLine(t);
            }
            Console.ReadKey();
        }
    }
}
