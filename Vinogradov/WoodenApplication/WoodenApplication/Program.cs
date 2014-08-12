using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Forest;

namespace WoodenApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var qweerty = new HashSet<int>(new int[] { 1, 2, 3 });
            qweerty.Remove(4);
            Console.ReadLine();
        }
    }
}
