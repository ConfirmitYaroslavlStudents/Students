using System.Collections.Generic;
using FolderBackuperLib;

namespace Tests.Fakes
{
    class FakeFileSystemSource : ISource
    {
        public string SourceFolder { get; private set; }
        public List<string> DirectoryNames { get; set; }
        public List<string> FileNames { get; set; }

        public FakeFileSystemSource(string source)
        {
            SourceFolder = source;
            DirectoryNames = new List<string>();
            FileNames = new List<string>();
        }

        public IEnumerable<string> GetDirectories()
        {
            return new[] { @"D:\source\folderOne", @"D:\source\folderOne\FolderTwo" };
        }

        public IEnumerable<string> GetFiles()
        {
            return new[] { @"D:\source\folderOne\Text.txt", @"D:\source\folderOne\FolderTwo\Nice - one.mp3" };
        }

        public bool Exists(string filename)
        {
            return false;
        }

        public void CreateDirectory(string directoryName)
        {
            
        }

        public void Copy(string source, string destination, bool overwrite)
        {
           
        }
    }
}
