using System;
using System.IO;

namespace MusicFileRenameLibrary
{
    public class FileWorker
    {

        public string[] GetFiles(string directoryPath, string searchPattern, bool recursive)
        {
            if (!Directory.Exists(directoryPath))
                throw new ArgumentException("Папка не найдена");

            SearchOption searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            return Directory.GetFiles(directoryPath, searchPattern, searchOption);
        }

        public void GetFileTags(FileShell file)
        {
            if (!File.Exists(file.FullFilePath))
                throw new ArgumentException("Файл " + file + " не существует");
            var fileTags = TagLib.File.Create(file.FullFilePath);
            file.TagArtist = fileTags.Tag.Performers[0];
            file.TagTitle = fileTags.Tag.Title;
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
