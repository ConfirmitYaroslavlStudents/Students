namespace Mp3Lib
{
    public interface IMp3File
    {
        void MoveTo(string newPath);
        void Save();
        string Path { get; }
        bool Exists { get; }
        string DirectoryName { get; }
        TagBase Tag { get; }
    }
}
