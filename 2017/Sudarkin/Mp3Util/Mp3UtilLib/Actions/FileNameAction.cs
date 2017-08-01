using System.IO;

namespace Mp3UtilLib.Actions
{
    public class FileNameAction : IActionStrategy
    {
        public void Process(AudioFile audioFile)
        {
            string newPath = Path.Combine(Path.GetDirectoryName(audioFile.FullName),
                $"{audioFile.Artist} - {audioFile.Title}.mp3");

            audioFile.MoveTo(newPath);
        }
    }
}