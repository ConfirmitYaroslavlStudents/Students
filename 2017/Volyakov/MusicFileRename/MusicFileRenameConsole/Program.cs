using System;
using MusicFileRenameLibrary;

namespace MusicFileRename
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var renamer = new MusicFileRenamerShell(args);
                renamer.RenameMusicFiles();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
