using FolderSynchronizerLib;
using System.Collections.Generic;

namespace Tests
{
    public class FolderSet
    {
        public List<FolderPair> FolderList;
        public bool NoDeleteFlag;
        public string Loglevel;

        public FolderSet SetNoDeleteFlag(bool noDeleteFlag)
        {
            NoDeleteFlag = noDeleteFlag;
            return this;
        }

        public FolderSet SetLogLevel(string logLevel)
        {
            Loglevel = logLevel;
            return this;
        }


    }
}