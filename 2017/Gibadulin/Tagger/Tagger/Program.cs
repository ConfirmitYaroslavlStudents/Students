using System;
using DirLib;
using ParseInputLib;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagger
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine(); //(path) (mask) (-n|-t) [-r]
            var inputData = ParseInput.Parse(input);
            Dir.ChangeFiles(inputData);        
        }
    }
}
