using TagLib;

namespace Mp3Lib
{
    // TODO: we already have IFile
    public interface IMp3File
    {
        string Path { get; }

        IMp3Tags Mp3Tags { get; }

        void MoveTo(string path);

        void Save();
    }
}
