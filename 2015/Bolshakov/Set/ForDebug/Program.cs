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
            var tree = new Tree<List<int>>();
            var a = new List<int>();
            var b = new List<int>();
            var d = new List<int>();
            var c = new List<int>();

            tree.Add(a);
            tree.Add(b);
            tree.Add(d);

            var result = tree.Remove(c);
            var smt = 3;
        }
    }
}
