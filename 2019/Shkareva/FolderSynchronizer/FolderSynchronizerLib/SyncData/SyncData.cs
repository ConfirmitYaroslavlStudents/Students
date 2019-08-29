using System;
using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncData : IEquatable<SyncData>
    {
        public Dictionary<string, string> FilesToCopy;
        public Dictionary<string, string> FilesToUpdate;
        public Dictionary<string, string> FilesToDelete;
        public ILog Log;

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
        }

        public bool Equals(SyncData other)
        {
            bool filesToCopy = CompareDictionary(other.FilesToCopy, FilesToCopy);
            bool filesToUpdate = CompareDictionary(other.FilesToUpdate, FilesToUpdate);
            bool filesToDelete = CompareDictionary(other.FilesToDelete, FilesToDelete);
            return filesToCopy && filesToUpdate && filesToDelete;
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
