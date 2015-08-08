namespace RestoreLib
{
    public interface IFile
    {
        string FullName { get; }
        IFile CopyTo(string path);
        void MoveTo(string path);
        void Delete();
    }
}