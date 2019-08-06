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
            var a = @"C:\Users\PISYA\Desktop\master\";
            var b = @"C:\Users\PISYA\Desktop\slave\";
            var worker = new Sync(a, b);
            worker.Synchronization();
        }
    }
}
