using System;
using System.Collections.Generic;

namespace GeneralizeSynchLibrary
{
    public class SynchReport
    {
        List<FileWrapper> RemoveList;
        List<Tuple<FileWrapper, FileWrapper>> ReplaceList;

        public SynchReport(List<FileWrapper> removeList, List<Tuple<FileWrapper, FileWrapper>> replaceList)
        {
            RemoveList = removeList;
            ReplaceList = replaceList;
        }

        public List<Tuple<FileWrapper, FileWrapper>> GetReplaceList()
        {
            return new List<Tuple<FileWrapper, FileWrapper>>(ReplaceList);
        }

        public List<FileWrapper> GetRemoveList()
        {
            return new List<FileWrapper>(RemoveList);
        }

        public ILogger ApplyReport(ILogger logger)
        {
            if (ReplaceList != null)
                foreach (var item in ReplaceList)
                    if(FileHandler.AreNotEqual(item.Item1, item.Item2))
                        FileHandler.Replace(item, logger);
            if (RemoveList != null)
                foreach (var item in RemoveList)
                    FileHandler.Remove(item, logger);
            return logger;
        }
    }
}
