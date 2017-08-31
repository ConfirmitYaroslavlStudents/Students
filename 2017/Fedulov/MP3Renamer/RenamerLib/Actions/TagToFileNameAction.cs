using System.IO;

namespace RenamerLib.Actions
{
    public class TagToFileNameAction : IAction
    {
        public void Process(IMP3File mp3File)
        {
            string directory = Path.GetDirectoryName(mp3File.FilePath);
            string newPath = directory + "\\" + mp3File.Artist + " - " + mp3File.Title + ".mp3";
            mp3File.Move(newPath);
        }
    }
}