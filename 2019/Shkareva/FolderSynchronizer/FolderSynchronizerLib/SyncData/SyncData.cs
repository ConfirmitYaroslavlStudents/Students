using System;
using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncData : IEquatable<SyncData>
    {
        public Dictionary<string, string> FilesToCopy;
        public Dictionary<string, string> FilesToUpdate;
        public Dictionary<string, string> FilesToDelete;
        public string LogFlag;

        public SyncData()
        {
            FilesToCopy = new Dictionary<string, string>();
            FilesToUpdate = new Dictionary<string, string>();
            FilesToDelete = new Dictionary<string, string>();
        }

        public SyncData
           (Dictionary<string,string> filesToCopy,
            Dictionary<string,string> filesToUpdate,
            Dictionary<string, string> filesToDelete)
        {
            FilesToCopy = filesToCopy;
            FilesToUpdate = filesToUpdate;
            FilesToDelete = filesToDelete;
            LogFlag = "summary";
        }

        public bool Equals(SyncData other)
        {
            bool level = (LogFlag == other.LogFlag);
            bool toCopy = CompareDictionary(other.FilesToCopy, FilesToCopy);
            bool toUpdate = CompareDictionary(other.FilesToUpdate, FilesToUpdate);
            bool toDelete = CompareDictionary(other.FilesToDelete, FilesToDelete);
            return level && toCopy && toUpdate && toDelete;
        }

        private bool CompareDictionary(Dictionary<string, string> aDictionary, Dictionary<string,string> bDictionary)
        {
            foreach (KeyValuePair<string, string> pair in aDictionary)
            {
                if (!bDictionary.ContainsKey(pair.Key))
                {
                    return false;
                }
                if (bDictionary[pair.Key] != pair.Value)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
