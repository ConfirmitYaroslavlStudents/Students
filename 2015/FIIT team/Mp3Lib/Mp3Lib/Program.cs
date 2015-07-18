using System;

namespace Mp3Lib
{
    class Program
    {
        static void Main(string[] args)
        {
            Mp3Lib app = new Mp3Lib(args);
            if (args.Length == 0)
            {      
               app.ShowHelp();
               Environment.Exit(0);
            }

            app.ExecuteCommand();

            

        }
    }
}
