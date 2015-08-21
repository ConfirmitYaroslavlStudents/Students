using System.Collections.Generic;

namespace FileLib
{
    public interface IDirectory
    {
        string SourceFolder { get; }

        IEnumerable<IMp3File> GetFiles();
    }
}
