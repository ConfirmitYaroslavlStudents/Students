using System;
using System.Collections.Generic;

namespace GeneralizeSynchLibrary
{
    public class SynchResult
    {
        List<FileWrapper> RemoveList = new List<FileWrapper>();
        List<Tuple<FileWrapper, FileWrapper>> ReplaceList = new List<Tuple<FileWrapper, FileWrapper>>();
        List<Tuple<FileWrapper, string>> CopyList = new List<Tuple<FileWrapper, string>>();
        FileHandler _fileHandler = new FileHandler();

        public SynchResult(List<FileWrapper> removeList, List<Tuple<FileWrapper, FileWrapper>> replaceList, List<Tuple<FileWrapper, string>> copyList)
        {
            RemoveList = removeList;
            ReplaceList = replaceList;
            CopyList = copyList;
        }

        public List<Tuple<FileWrapper, FileWrapper>> GetReplaceList()
        {
            return new List<Tuple<FileWrapper, FileWrapper>>(ReplaceList);
        }

        public List<FileWrapper> GetRemoveList()
        {
            return new List<FileWrapper>(RemoveList);
        }

        public List<Tuple<FileWrapper, string>> GetCopyList()
        {
            return new List<Tuple<FileWrapper, string>>(CopyList);
        }

        public ILogger ApplyResult(ILogger logger)
        {
            if (ReplaceList != null)
                foreach (var item in ReplaceList)
                    if (_fileHandler.AreNotEqual(item.Item1, item.Item2))
                        _fileHandler.Replace(item, logger);
            if (RemoveList != null)
                foreach (var item in RemoveList)
                    _fileHandler.Remove(item, logger);
            if (CopyList != null)
                foreach (var item in CopyList)
                    _fileHandler.Copy(item, logger);
            return logger;
        }
    }
}
