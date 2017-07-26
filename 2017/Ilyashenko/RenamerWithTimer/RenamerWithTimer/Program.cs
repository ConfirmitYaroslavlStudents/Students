using System;
using RenamersLib;
using System.Collections.Generic;

namespace RenamerWithTimer
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = new List<Mp3File>() { new Mp3File("FirstSample.mp3"), new Mp3File("SecondSample.mp3"), new Mp3File("ThirdSample.mp3")};

            foreach (var file in files)
            {
                var oldName = file.Path;
                var renamer = new RenamerWithTimeCounter(new RenamerWithPermissionCheck(new FileRenamer(), new PermissionChecker(), UserRole.Administrator));
                renamer.Rename(file);
                Console.WriteLine($"Файл {oldName} переименован в {file.Path} за {renamer.Elapsed}");
            }

            Console.ReadKey();
        }
    }
}
