using System;
using System.IO;

namespace RenamerLib.Actions
{
    public class FileNameToTagAction : IAction
    {
        public void Process(IMP3File mp3File)
        {
            string fileName = Path.GetFileNameWithoutExtension(mp3File.FilePath);
            if (fileName == null)
                throw new IOException("MP3 file should have a name");

            string[] fileParts = fileName.Split(new string[] { " - ", "." }, StringSplitOptions.RemoveEmptyEntries);
            if (fileParts.Length < 2)
                throw new IOException("File name does not contain enough information");
            if (fileParts.Length > 2)
                throw new IOException("File name contains extra information");

            mp3File.Artist = fileParts[0];
            mp3File.Title = fileParts[1];
            mp3File.Save();
        }
    }
}