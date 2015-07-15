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
            var sourceArr = new int[1000];

            for (int i = 0; i < 1000; i++)
            {
                sourceArr[i] = i;
                set.Add(i);
            }

            var resultArr = new int[1000];
            set.CopyTo(resultArr, 0);

            Console.ReadLine();
        }
    }
}
