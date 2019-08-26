using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneralizeSynchLibrary
{
    public class NoRemoveSynchronizer : ISynchronizer
    {

        public NoRemoveSynchronizer() { }
        public SynchResult Synchronize(FileWrapperCollection collection)
        {
            List<Tuple<FileWrapper, FileWrapper>> replaceList = new List<Tuple<FileWrapper, FileWrapper>>();
            List<Tuple<FileWrapper, string>> copyList = new List<Tuple<FileWrapper, string>>();
            var enumerator = collection.GetListCopy();
            for (int i = 0; i < collection.Count; i++)
            {
                var current = enumerator.Where(x => x.Priority == i);
                var root = current.FirstOrDefault().Root;
                var intersection = enumerator.Where(x => x.Priority > i);
                var copy = from file in enumerator
                           where !current.Contains(file)
                           select new Tuple<FileWrapper, string>(file, root);
                var replace = from file in intersection
                              from anotherFile in current
                              where file.Equals(anotherFile)
                              select new Tuple<FileWrapper, FileWrapper>(anotherFile, file);
                replaceList.AddRange(replace);
                copyList.AddRange(copy);
            }
            return new SynchResult(null, replaceList, copyList);
        }
    }
}
