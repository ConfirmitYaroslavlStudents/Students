namespace FileLib
{
    // TODO: we already have IFile
    public interface IMp3File
    {        

        Mp3Tags Tags { get; }

        string FullName { get; }

        void Save();

        IMp3File CopyTo(string path);

        void MoveTo(BaseFileExistenceChecker checker, string path);

        void Delete();

        
    }
}
