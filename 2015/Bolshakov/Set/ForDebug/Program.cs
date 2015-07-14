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
            var a = new SortedSet<int>();
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
                Console.Write("{0} ",item);
                break;
            }
            foreach (var item in set)
            {
                Console.Write("{0} ", item);
                //break;
            }

            Console.ReadLine();
        }
    }
}
