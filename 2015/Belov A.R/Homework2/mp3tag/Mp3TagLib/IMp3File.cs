namespace Mp3TagLib
{
    public interface IMp3File
    {
        string Name { get; }
        void SetTags(Mp3Tags tags);
        void Save();
    }
}
