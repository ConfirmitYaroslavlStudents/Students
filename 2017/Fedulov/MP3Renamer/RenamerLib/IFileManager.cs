using System.Collections.Generic;
using System.IO;

namespace RenamerLib
{
    public interface IFileManager
    {
        bool Exist(string path);
        void Move(string source, string dest);
        IEnumerable<IMP3File> GetFiles(string searchPattern, SearchOption searchOption);
    }
}