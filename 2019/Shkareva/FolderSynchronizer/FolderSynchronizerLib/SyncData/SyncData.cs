using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncData
    {
        public Dictionary<string, string> FilesToCopy;
        public Dictionary<string, string> FilesToUpdate;
        public List<string> FilesToDelete;
        public string LogFlag;
        public bool NoDeleteFlag;

        public SyncData()
        {
            FilesToCopy = new Dictionary<string, string>();
            FilesToUpdate = new Dictionary<string, string>();
            FilesToDelete = new List<string>();
        }

        public SyncData
           (Dictionary<string,string> copy,
            Dictionary<string,string> update,
            List<string> delete)
        {
            FilesToCopy = copy;
            FilesToUpdate = update;
            FilesToDelete = delete;
            LogFlag = "summary";
            NoDeleteFlag = false;
        }
    }
}
