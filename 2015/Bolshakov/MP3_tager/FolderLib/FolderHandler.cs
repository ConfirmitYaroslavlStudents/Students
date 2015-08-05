using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderLib
{
    public class FolderHandler
    {
        public List<string> GetAllMp3s(string path)
        {
            if(!Directory.Exists(path))
                throw new FileNotFoundException();
            var mp3s = Directory.GetFiles(path, "*.mp3");
            return mp3s.ToList();
        }
    }
}
