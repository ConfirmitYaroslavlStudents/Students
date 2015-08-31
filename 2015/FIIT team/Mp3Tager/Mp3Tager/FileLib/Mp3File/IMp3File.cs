namespace FileLib
{
    public interface IMp3File
    {
        Mp3Tags Tags { get; }

        string FullName { get; set; }

        void Save();

        IMp3File CopyTo(string path);

        void MoveTo(string path);

        void Delete();
    }
}