using System;
using System.Collections.Generic;

namespace Mp3TagLib.Sync
{
    public class Synchronizer
    {
        private readonly Tager _tager;
        private readonly ISyncRule _syncRule;
        public Synchronizer(Tager tager):this(tager,new DefaultSyncRule())
        {
        }
        public Synchronizer(Tager tager,ISyncRule rule)
        {
            _tager = tager;
            ModifiedFiles = new List<IMp3File>();
            ErrorFiles = new Dictionary<string, string>();
            _syncRule = rule;
        }
        public List<IMp3File> ModifiedFiles { get;private set; }

        public Dictionary<string, string> ErrorFiles { get;private set; }

        public void Sync(IEnumerable<IMp3File> files,Mask mask)
        {
            foreach (var mp3File in files)
            {
                Sync(mp3File,mask,_syncRule);
            }
        }

        public void Sync(IMp3File file, Mask mask, ISyncRule rule)
        {
            var errorFlag = true;
            _tager.CurrentFile = file;
            foreach (var func in rule.OperationsList)
            {
                if (func(mask, _tager))
                {
                    ModifiedFiles.Add(_tager.CurrentFile);
                    errorFlag = false;
                    break;
                }
            }
            if (errorFlag)
            {
                ErrorFiles.Add(_tager.CurrentFile.Name, "Can't sync this file");
            }
        }
        public void Save()
        {
            foreach (var modifiedFile in ModifiedFiles)
            {
                try
                {
                    modifiedFile.Save();
                }
                catch (Exception e)
                {
                      
                    ErrorFiles.Add(modifiedFile.Name,e.Message);
                }
            }
            ModifiedFiles.Clear();
        }
    }
}
