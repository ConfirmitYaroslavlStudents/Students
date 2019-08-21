using System;
using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class SyncData : IEquatable<SyncData>
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

        public bool Equals(SyncData other)
        {
            bool flag = (NoDeleteFlag == other.NoDeleteFlag);
            bool level = (LogFlag == other.LogFlag);
            bool toCopy = CompareDictionary(other.FilesToCopy, FilesToCopy);
            bool toUpdate = CompareDictionary(other.FilesToUpdate, FilesToUpdate);
            bool toDelete = new HashSet<string>(FilesToDelete).SetEquals(other.FilesToDelete);
            return flag && level && toCopy && toUpdate && toDelete;
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
