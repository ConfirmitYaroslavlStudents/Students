using System.IO;

namespace Mp3UtilLib.Actions
{
    public class FileNameAction : IActionStrategy
    {
        public void Process(Mp3File mp3File)
        {
            mp3File.MoveTo(
                Path.Combine(Path.GetDirectoryName(mp3File.FullName),
                    $"{mp3File.Artist} - {mp3File.Title}.mp3"));
        }
    }
}