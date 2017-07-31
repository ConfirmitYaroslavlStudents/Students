using System;
using System.IO;
using RenamerLibrary;

namespace RenamerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var directory = Directory.GetCurrentDirectory();
            try
            {
                var renamer = new Renamer(directory);
                renamer.Rename(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
