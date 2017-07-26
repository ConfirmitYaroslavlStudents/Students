using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenamerLib
{
    public interface IAction
    {
        void Process(MP3File mp3File);
    }

    class TagToFileNameAction : IAction
    {
        public void Process(MP3File mp3File)
        {
            string directory = Path.GetDirectoryName(mp3File.FilePath);
            string newPath = directory + "\\" + mp3File.Artist + " - " + mp3File.Title + ".mp3";
            mp3File.Move(newPath);
        }
    }

    class FileNameToTagAction : IAction
    {
        public void Process(MP3File mp3File)
        {
            string fileName = Path.GetFileNameWithoutExtension(mp3File.FilePath);
            if(fileName == null)
                throw new IOException("MP3 file should have a name");

            string[] fileParts = fileName.Split(new string[] {"-"}, StringSplitOptions.RemoveEmptyEntries);
            if (fileParts.Length < 2)
                throw new IOException("File name does not contain enough information");
            if (fileParts.Length > 2)
                throw new IOException("File name contains extra information");

            mp3File.Artist = fileParts[0];
            mp3File.Title = fileParts[1];
            mp3File.Save();
        }
    }

    public enum AllowedActions
    {
        toFileName,
        toTag
    }
}
