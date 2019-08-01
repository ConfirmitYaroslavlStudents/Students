using System.Collections.Generic;

namespace FolderSynchronizer
{
    class Folder
    {
        public string Path;
        public List<string> shortFilesNames;
        public List<string> FilesPaths;
        public List<Folder> internalFolders;
        public List<string> internalFoldersPaths;

        public Folder(string address)
        {
            Path = address;
        }

        public Folder() { }
    }
}
