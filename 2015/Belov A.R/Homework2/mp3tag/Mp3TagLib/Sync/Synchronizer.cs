using System;
using System.Collections.Generic;
using Mp3TagLib.Plan;

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
            OperationList = new List<SyncOperation>();
            _syncRule = rule;
        }
      
        public List<IMp3File> ModifiedFiles { get;private set; }
        public List<SyncOperation> OperationList { get; private set; }

        public Dictionary<string, string> ErrorFiles { get;private set; }

        public void Sync(IEnumerable<IMp3File> files,Mask mask)
        {
            foreach (var mp3File in files)
            {
                Sync(mp3File,mask,_syncRule.Clone());
            }
        }

        public void Sync(IMp3File file, Mask mask, ISyncRule rule)
        {
            var errorFlag = true;
            _tager.CurrentFile = file;
            foreach (var operation in rule.OperationsList)
            {
                if (operation.Call(mask, _tager,file))
                {
                    OperationList.Add(operation);
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

        public void Sync(SyncPlan plan)
        {
            foreach (var item in plan)
            {
                if (_tager.Load(item.FilePath))
                Sync(_tager.CurrentFile,item.Mask,item.Rule);
                else
                {
                    ErrorFiles.Add(item.FilePath, "load error");
                }
            }
        }
        public void SaveLast()
        {
            if(OperationList.Count!=0)
            OperationList[OperationList.Count-1].Save();
        }

        public void RestoreLast()
        {
            if (OperationList.Count != 0)
                OperationList[OperationList.Count - 1].Cancel();
        }

        public void Save()
        {
            foreach (var operation in OperationList)
            {
                try
                {
                    operation.Save();
                }
                catch (Exception e)
                {
                      
                    ErrorFiles.Add(operation.File.Name,e.Message);
                }
            }
        }
        public void Restore()
        {
            foreach (var operation in OperationList)
            {
               
                    operation.Cancel();  
            }
        }
        public void Redo()
        {
            foreach (var operation in OperationList)
            {

                operation.Redo();
            }
        }
    }
}
