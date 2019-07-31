using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SyncTool
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo(@"D:\dir2");

            foreach (var d in dir.EnumerateDirectories())
            {
                Console.WriteLine(d.Name);
            }
        }
    }
}
