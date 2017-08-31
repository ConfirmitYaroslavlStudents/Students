namespace RenamerLib
{
    public interface IMP3File
    {
        string Artist { set; get; }
        string Title { set; get; }
        string FilePath { set; get; }
        void Move(string path);
        void Save();
    }
}