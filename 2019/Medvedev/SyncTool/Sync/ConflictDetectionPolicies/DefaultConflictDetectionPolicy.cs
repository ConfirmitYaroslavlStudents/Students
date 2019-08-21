using System;
using System.Collections.Generic;
using Sync.Wrappers;

namespace Sync.ConflictDetectionPolicies
{
    public class DefaultConflictDetectionPolicy : IConflictDetectionPolicy
    {
        public DefaultConflictDetectionPolicy(IComparer<IFileSystemElementWrapper> comparer)
        {
            Comparer = comparer;
        }

        private IComparer<IFileSystemElementWrapper> Comparer { get; }

        public Conflict GetConflict(IFileSystemElementWrapper first, IFileSystemElementWrapper second)
        {
            if (first == null || second == null)
                return new Conflict(first, second);

            if (first.ElementType != second.ElementType)
                throw new ArgumentException("Types of first and second elements are not equal");

            var comparision = Comparer.Compare(first, second);

            if (comparision < 0)
                return new Conflict(first, second);
            if (comparision > 0)
                return new Conflict(second, first);
            return null;
        }

        public bool ConflictExists(IFileSystemElementWrapper first, IFileSystemElementWrapper second)
        {
            if (first == null || second == null)
                return true;

            if (first.ElementType != second.ElementType)
                throw new ArgumentException("Attributes of first and second elements  are not equal");

            return Comparer.Compare(first, second) != 0;
        }
    }
}