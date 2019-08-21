using System.Collections.Generic;

namespace FolderSynchronizerLib
{
    public class FolderSet
    {
        public readonly List<FolderPair> FolderList;
        public readonly bool NoDeleteFlag;
        public readonly string Loglevel;

        public FolderSet(InputData input)
        {
            var folderWorker = new FolderWorker();

            foreach (var path in input.FoldersPaths)
            {
                var folderPair = new FolderPair();
                folderPair.New = folderWorker.LoadFolder(path);
                folderPair.Old = folderWorker.LoadSerializedFolder(path);
                FolderList.Add(folderPair);
            }
                        
            NoDeleteFlag = input.NoDeleteFlag;
            Loglevel = input.LogFlag;
        }

        public FolderSet() { }
    }
}
