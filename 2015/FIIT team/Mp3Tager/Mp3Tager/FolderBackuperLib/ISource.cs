using System.Collections.Generic;

namespace FolderBackuperLib
{
    public interface ISource
    {
        string SourceFolder { get; }

        List<string> DirectoryNames { get; }

        List<string> FileNames { get; }

        IEnumerable<string> GetDirectories();

        IEnumerable<string> GetFiles();

        bool Exists(string filename);

        void CreateDirectory(string directoryName);

        void Copy(string source, string destination, bool overwrite);
    }
}