using System.IO;

namespace Mp3UtilLib.Actions
{
    public class TagAction : IActionStrategy
    {
        public void Process(AudioFile audioFile)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(audioFile.FullName);
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

            audioFile.Artist = artist;
            audioFile.Title = title;
            audioFile.Save();
        }
    }
}