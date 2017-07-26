using System.Collections.Generic;
using System.IO;

namespace Mp3UtilLib.FileSystem
{
    public interface IFileSystem
    {
        bool Exists(string path);
        void Move(string source, string dest);
        IEnumerable<string> GetFiles(string directory, string searchPattern, SearchOption searchOption);
        IEnumerable<string> GetFilesFromCurrentDirectory(string searchPattern, bool recursive);
    }
}