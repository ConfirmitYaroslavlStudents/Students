using System;
using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncData : IEquatable<SyncData>
    {
        public readonly Dictionary<string, string> FilesToCopy;
        public readonly Dictionary<string, string> FilesToUpdate;
        public readonly Dictionary<string, string> FilesToDelete;       

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
