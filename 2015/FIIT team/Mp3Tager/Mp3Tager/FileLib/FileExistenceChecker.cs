namespace FileLib
{
    public class FileExistenceChecker : BaseFileExistenceChecker
    {
        protected override bool Exists(string path)
        {
            var system = new FileSystemSource("");
            system.Exists();

            return System.IO.File.Exists(path);
        }
    }
}
