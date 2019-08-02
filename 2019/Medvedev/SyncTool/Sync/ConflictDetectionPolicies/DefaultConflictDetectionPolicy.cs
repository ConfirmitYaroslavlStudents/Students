using System;
using System.Collections.Generic;
using Sync.Wrappers;

namespace Sync
{
    public class DefaultConflictDetectionPolicy : IConflictDetectionPolicy
    {
        private IComparer<IFileSystemElementInfoWrapper> Comparer { get; }

        public DefaultConflictDetectionPolicy(IComparer<IFileSystemElementInfoWrapper> comparer)
        {
            Comparer = comparer;
        }

        public Conflict GetConflict(IFileSystemElementInfoWrapper first, IFileSystemElementInfoWrapper second)
        {
            if (first == null || second == null)
                return new Conflict(first, second);

            if (first.ElementType != second.ElementType)
                throw new ArgumentException("Attributes of first and second elements  are not equal");

            var comparision = Comparer.Compare(first, second);

            if (comparision < 0)
                return new Conflict(first, second);
            if (comparision > 0)
                return new Conflict(second, first);
            return null;
        }

        public bool MakesConflict(IFileSystemElementInfoWrapper first, IFileSystemElementInfoWrapper second)
        {
            if (first == null || second == null)
                return true;

            if (first.ElementType != second.ElementType)
                throw new ArgumentException("Attributes of first and second elements  are not equal");

            return Comparer.Compare(first, second) != 0;
        }
    }
}