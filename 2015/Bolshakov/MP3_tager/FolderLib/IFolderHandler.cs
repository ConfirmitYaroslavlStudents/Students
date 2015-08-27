using System.Collections.Generic;

namespace FolderLib
{
    public interface IFolderHandler
    {
        List<string> GetAllMp3s(string path);
    }
}