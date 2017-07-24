using System.Threading;

namespace HomeWork3
{
    public class MP3Renamer:IMP3Renamer
    {
        public void Rename(MP3File file)
        {
            file.FileName = "New Name";
            Thread.Sleep(2);
        }
    }
}
