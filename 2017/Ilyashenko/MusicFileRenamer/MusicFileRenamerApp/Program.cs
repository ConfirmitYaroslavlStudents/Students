using System;
using System.IO;
using MusicFileRenamerLib;

namespace MusicFileRenamerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var directory = Directory.GetCurrentDirectory();
            try
            {
                var renamer = new Renamer(args, directory, new FilenameMaker(), new TagMaker());

                foreach (var fPath in renamer.GetFilePaths())
                    renamer.Rename(new Mp3File(fPath));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
