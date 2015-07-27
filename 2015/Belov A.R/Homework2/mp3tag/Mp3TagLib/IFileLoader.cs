namespace Mp3TagLib
{
    public interface IFileLoader
    {
        bool FileExist(string path);
        IMp3File Load(string path);
    }
}
