using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SynchLibrary;
using System.IO;

namespace SyncApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var master = @".\master";
            var slave = @".\slave";
            var synchronization = new Sync(master, slave, false, 2);
            synchronization.Synchronization();
        }
    }
}
