using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RenamerLib;
using System.Threading.Tasks;

namespace MP3Renamer.Tests
{
    class MockFileManager : IFileManager
    {
        public Dictionary<string, IMP3File> files;

        public MockFileManager()
        {
            files = new Dictionary<string, IMP3File>();
        }

        public MockFileManager(Dictionary<string, IMP3File> files)
        {
            this.files = files;
        }

        public void AddFile(string name, IMP3File file) => files.Add(name, file);

        public bool Exist(string path) => files.ContainsKey(path);

        public void Move(string source, string dest)
        {
            files.Add(dest, files[source]);
            files.Remove(source);
        }

        public IEnumerable<IMP3File> GetFiles(string searchPattern, 
            SearchOption searchOption) => files.Values.ToArray();
    }

    public class MockMP3File : IMP3File
    {
        public string Artist { set; get; }
        public string Title { set; get; }
        public string FilePath { set; get; }
        public string FileName { set; get; }

        public readonly IFileManager fileManager;

        public MockMP3File(string path, IFileManager fileManager)
        {
            FilePath = path;

            var splittedFilePath = FilePath.Split(new char[] { '/' });
            FileName = splittedFilePath[splittedFilePath.Length - 1];

            this.fileManager = fileManager;
        }

        public void Move(string path)
        {
            string oldPath = FilePath;

            FilePath = path;
            var splittedFilePath = FilePath.Split(new char[] { '/' });
            FileName = splittedFilePath[splittedFilePath.Length - 1];

            fileManager.Move(oldPath, FilePath);
        }

        public void Save()
        {
            string[] fileNameParts = FileName.Split(new string[] { " - ", "." }, StringSplitOptions.RemoveEmptyEntries);
            
            Artist = fileNameParts[0];
            Title = fileNameParts[1];
        }
    }
}
