using System.Collections.Generic;

namespace Mp3Lib
{
    public interface ISource
    {
        string SourceFolder { get; }

        IEnumerable<string> GetFileNames();
        IEnumerable<IMp3File> GetFiles();

    }
}
