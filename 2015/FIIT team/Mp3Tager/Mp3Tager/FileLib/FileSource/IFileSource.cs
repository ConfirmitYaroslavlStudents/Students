using System.Collections.Generic;

namespace FileLib.FileSource
{
    interface IFileSource
    {
        IEnumerable<IMp3File> GetFiles(string path);
    }
}
