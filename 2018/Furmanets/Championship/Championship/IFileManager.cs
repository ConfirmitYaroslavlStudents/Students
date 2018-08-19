namespace Championship
{
    public interface IFileManager
    {
        void WriteToFile(Tournament tournament);
        Tournament ReadFromFile();
        void DeleteFile();
    }
}
