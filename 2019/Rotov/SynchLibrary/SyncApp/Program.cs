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
            var worker = new Merge(args[0], args[1]);
            worker.Working();
        }
    }
}
