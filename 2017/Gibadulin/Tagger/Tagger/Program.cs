using System;
using TaggerLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagger
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputData = ParseInput.Parse(args);
            Dir.ChangeFiles(inputData);
        }
    }
}
