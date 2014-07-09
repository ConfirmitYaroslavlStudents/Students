using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new Stack<string>();
            stack.Push("test");
            var result = stack.Pop();
            Console.WriteLine(result);
        }
    }
}
