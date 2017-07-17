using System.IO;

namespace Mp3UtilConsole.Actions
{
    public class TagAction : IActionStrategy
    {
        public void Process(Mp3File mp3File)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(mp3File.FullName);
            if (nameWithoutExtension == null)
            {
                return;
            }

            int ind = nameWithoutExtension.IndexOf('-');
            string artist = "", 
                   title = "";

            if (ind != -1)
            {
                artist = nameWithoutExtension.Substring(0, ind - 1).Trim();
                title = nameWithoutExtension
                    .Substring(ind + 1, nameWithoutExtension.Length - ind - 1).Trim();
            }
            else
            {
                artist = nameWithoutExtension.Trim();
            }

            mp3File.Artist = artist;
            mp3File.Title = title;
            mp3File.Save();
        }
    }
}