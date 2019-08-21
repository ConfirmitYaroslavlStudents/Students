using System;
using System.Collections.Generic;
using System.Linq;
namespace GeneralizeSynchLibrary
{
    public class RemoveSynchronizer : ISynchronizer
    {

        public RemoveSynchronizer() { }
        public SynchReport Synchronize(FileWrapperCollection collection)
        {
            List<FileWrapper> removeList = new List<FileWrapper>();
            List<Tuple<FileWrapper, FileWrapper>> replaceList = new List<Tuple<FileWrapper, FileWrapper>>();
            var enumerator = collection.GetListCopy();
            for(int i = 0; i < collection.Count; i++)
            {
                var current = enumerator.Where(x => x.Priority == i);
                var intersection = enumerator.Where(x => x.Priority > i);
                var remove = intersection.Except(current);
                var replace = from file in intersection
                              from anotherFile in current
                              where file.Equals(anotherFile)
                              select new Tuple<FileWrapper, FileWrapper>(anotherFile, file);
                removeList.AddRange(remove);
                replaceList.AddRange(replace);
            }
            return new SynchReport(removeList, replaceList);
        }
    }
}
