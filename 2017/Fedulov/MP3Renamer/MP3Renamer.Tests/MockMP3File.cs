using System;
using RenamerLib;

namespace MP3Renamer.Tests
{
    public class MockMp3File : IMP3File
    {
        public string Artist { set; get; }
        public string Title { set; get; }
        public string FilePath { set; get; }
        public string FileName { set; get; }

        public readonly IFileManager FileManager;

        public MockMp3File(string path, IFileManager fileManager)
        {
            FilePath = path;

            var splittedFilePath = FilePath.Split(new char[] { '/' });
            FileName = splittedFilePath[splittedFilePath.Length - 1];

            FileManager = fileManager;
        }

        public void Move(string path)
        {
            string oldPath = FilePath;

            FilePath = path;
            var splittedFilePath = FilePath.Split(new char[] { '/' });
            FileName = splittedFilePath[splittedFilePath.Length - 1];

            FileManager.Move(oldPath, FilePath);
        }

        public void Save()
        {
            string[] fileNameParts = FileName.Split(new string[] { " - ", "." }, StringSplitOptions.RemoveEmptyEntries);

            Artist = fileNameParts[0];
            Title = fileNameParts[1];
        }
    }
}