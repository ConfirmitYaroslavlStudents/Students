namespace MusicFileRenamerLib
{
    public interface IFileSystem
    {
        bool Exists(string path);
        void Move(string sourceFileName, string destFileName);
        void SetTags(Mp3File file);
    }
}
