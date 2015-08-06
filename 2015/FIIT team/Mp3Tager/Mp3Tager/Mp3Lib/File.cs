namespace Mp3Lib
{
    public class File : IFile
    {
        public bool Exists(string path)
        {
            return System.IO.File.Exists(path);
        }
    }
}
