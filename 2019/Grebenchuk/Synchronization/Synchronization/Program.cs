using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Synchronization
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo directory = new DirectoryInfo(@"C:\Users\Пользователь\Desktop\1");
            var papki = directory.GetDirectories();
            var files = directory.GetFiles();
        }
    }
}
