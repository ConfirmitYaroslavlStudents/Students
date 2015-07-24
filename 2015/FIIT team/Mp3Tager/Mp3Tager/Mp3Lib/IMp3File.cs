namespace Mp3Lib
{
    public interface IMp3File
    {
        void MoveTo(string newPath);
        void Save();
        string Path { get; }
        string DirectoryName { get; }
        TagBase Tag { get; }
    }
}
