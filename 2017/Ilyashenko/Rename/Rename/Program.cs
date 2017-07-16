using System;
using System.IO;

namespace Rename
{
    class Program
    {
        static void Main(string[] args)
        {
            var directory = Directory.GetCurrentDirectory();
            try
            {
                var renamer = new FileRenamer(directory);
                renamer.Rename(args);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Один или несколько аргументов программы были введены неверно. Проверьте правильность введённых значений.");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
