using System;
using DataStructures;

namespace RunNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 3, 4 });

            foreach (var x in list)
                Console.WriteLine(x);
        }
    }
}
