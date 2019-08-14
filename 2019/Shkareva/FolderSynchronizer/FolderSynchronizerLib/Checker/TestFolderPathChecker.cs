using System.IO;

namespace FolderSynchronizerLib
{
    public class TestFolderPathChecker : IChecker
    {
        public bool IsValid(string path)
        {
            var invalidChars = Path.GetInvalidPathChars();

            foreach(char c in invalidChars)
            {
                if (path.Contains(c.ToString()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
