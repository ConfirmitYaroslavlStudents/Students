using System;
using System.IO;

namespace Mp3Tager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var app = new Mp3Lib.Application();
                app.Execute(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            


        }
    }
}
