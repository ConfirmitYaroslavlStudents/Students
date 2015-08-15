namespace FileLib
{
    public class FileExistenceChecker : BaseFileExistenceChecker
    {
        protected override bool Exists(string path)
        {
            return System.IO.File.Exists(path);
        }
    }
}
