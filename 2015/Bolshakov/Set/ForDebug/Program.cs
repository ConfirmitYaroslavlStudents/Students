using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SetLib;

namespace ForDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            var set = new Set<int>();
            set.Add(5);
            set.Add(6);
            set.Add(7);
            set.Add(8);
            set.Add(9);
            set.Add(10);
            set.Add(10);
            set.Add(9);
            set.Add(8);
            foreach (var item in set)
            {
                set.Add(2);
                Console.Write(item);
            }

            Console.ReadLine();
        }
    }
}
