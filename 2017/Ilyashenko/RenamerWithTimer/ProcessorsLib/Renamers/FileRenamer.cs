using System.IO;

namespace RenamersLib
{
    public class FileRenamer : IFileRenamer
    {
        public void Rename(Mp3File file)
        {
            file.Move(Path.GetDirectoryName(file.Path) + "New" + Path.GetFileName(file.Path));
        }
    }
}
