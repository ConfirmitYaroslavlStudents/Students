namespace FileLib
{
    public class UniquePathCreator : BaseUniquePathCreator
    {
        protected override bool Exists(string path)
        {
            return System.IO.File.Exists(path);
        }
    }
}
