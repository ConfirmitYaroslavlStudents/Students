using System.Collections.Generic;
using System.IO;

namespace FolderBackuperLib
{
    public class FileSystemSource : ISource
    {

        public FileSystemSource(string source)
        {
            SourceFolder = source;
            DirectoryNames = new List<string>();
            FileNames = new List<string>();
        }

        public string SourceFolder { get; private set; }
        public List<string> DirectoryNames { get; set; }
        public List<string> FileNames { get; set; }

        public IEnumerable<string> GetDirectories()
        {
            return Directory.GetDirectories(SourceFolder, "*", SearchOption.AllDirectories);
        }

        public IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(SourceFolder, "*", SearchOption.AllDirectories);
        }

        public bool Exists(string filename)
        {
            return Directory.Exists(filename);
        }

        public void CreateDirectory(string directoryName)
        {
            Directory.CreateDirectory(directoryName);
        }

        public void Copy(string source, string destination, bool overwrite)
        {
            File.Copy(source, destination, overwrite);
        }
    }
}
