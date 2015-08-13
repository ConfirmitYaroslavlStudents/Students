using System.Collections.Generic;

namespace FileLib
{
    public interface ISource
    {
        string SourceFolder { get; }

        IEnumerable<string> GetFileNames();
        IEnumerable<IMp3File> GetFiles();

    }
}
