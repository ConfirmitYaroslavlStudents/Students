using System;

namespace HomeWork3
{
    class Program
    {
        static void Main(string[] args)
        {
            MP3File file = new MP3File();
            User currentUser = new User();
            var Renamer = new RenamerWithTimer(new RenamerWithPermissionCheck(
                new MP3Renamer(), currentUser));
            Renamer.Rename(file);
            Console.WriteLine("{0} {1}", file.FileName, Renamer.Elapsed);
            Console.ReadLine();
        }
    }
}
