using System.Collections.Generic;

namespace FolderSynchronizer
{
    public class SyncData
    {
        public Dictionary<string, string> FilesToCopy;
        public List<string> FilesToDelete;
        public string LogFlag;
        public bool NoDeleteFlag;

        public SyncData()
        {
            FilesToCopy = new Dictionary<string, string>();
            FilesToDelete = new List<string>();
            NoDeleteFlag = false;
            LogFlag = "summary";
        }
    }
}
