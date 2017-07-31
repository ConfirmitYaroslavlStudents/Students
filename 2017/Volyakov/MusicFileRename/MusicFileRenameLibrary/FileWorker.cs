using System;
using System.IO;

namespace MusicFileRenameLibrary
{
    public class FileWorker
    {
        private ArgsShell _args;

        public FileWorker(ArgsShell args)
        {
            _args = args;
        }

        public string[] GetFiles()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            SearchOption searchOption = _args.Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            return Directory.GetFiles(currentDirectory, _args.Pattern, searchOption);
        }

        public string GetFileTitleTag(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException("Файл " + filePath + " не существует");

            var fileTags = TagLib.File.Create(filePath);

            return fileTags.Tag.Title;
        }

        public string GetFileArtistTag(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException("Файл " + filePath + " не существует");

            var fileTags = TagLib.File.Create(filePath);

            return fileTags.Tag.Performers[0];
        }

        public void SaveFile(string oldFilePath, FileShell newFile)
        {
            if (oldFilePath != newFile.Path)
                File.Move(oldFilePath, newFile.Path);
            var newFileTags = TagLib.File.Create(newFile.Path);
            newFileTags.Tag.Performers = new string[] { newFile.TagArtist };
            newFileTags.Tag.Title = newFile.TagTitle;
            newFileTags.Save();
        }
    }
}
