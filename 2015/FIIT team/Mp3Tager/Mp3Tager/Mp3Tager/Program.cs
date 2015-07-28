using System;

namespace Mp3Tager
{
    class Program
    {
        //[TODO] 1. backups of source files + auto restore
        //[TODO] 2. change tags by file name
        // changetags <pathToFile> <mask>
        // {trackNumber}. {artist} - {song}
        // {trackNumber}{artist} - {song}
        // 1. Pugacheva - Arlekino.mp3
        static void Main(string[] args)
        {
            try
            {
                var app = new Application();
                app.Execute(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
