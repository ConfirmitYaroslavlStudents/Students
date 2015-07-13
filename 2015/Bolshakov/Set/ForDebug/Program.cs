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
            var set = new SortedSet<int>();
            var tree = new Tree<int>();
            var rnd = new Random();

            for (int i = 0; i < rnd.Next(100); i++)
            {
                var num = rnd.Next(100);
                set.Add(num);
                tree.Add(num);
            }

            var arr = new int[set.Count];
            set.CopyTo(arr);

            var toDelete = rnd.Next(set.Count);

            for (int i = 0; i < toDelete; i++)
            {
                tree.Remove(arr[i]);
            }
        }
    }
}
