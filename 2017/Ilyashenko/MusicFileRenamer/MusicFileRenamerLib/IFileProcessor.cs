namespace MusicFileRenamerLib
{
    public interface IFileProcessor
    {
        void MakeTags(Mp3File file);
        void MakeFilename(Mp3File file);
    }
}
