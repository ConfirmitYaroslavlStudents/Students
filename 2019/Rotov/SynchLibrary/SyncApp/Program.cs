using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynchLibrary;

namespace SyncApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = @"";
            var b = @"";
            var worker = new Merge(a, b);
            worker.Working();
        }
    }
}
