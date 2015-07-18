using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3Lib
{
    class Program
    {
        static void Main(string[] args)
        {
            Mp3Lib app = new Mp3Lib(args);
            if (args.Length == 0)
            {
                Console.WriteLine( "maloargov");
               app.ShowHelp();
               Environment.Exit(0);
            }

            app.ExecuteCommand();
 
        }
    }
}
