using System.Collections.Generic;
using System.Linq;
using Sync.ConflictDetectionPolicies;
using Sync.Wrappers;

namespace Sync
{
    public class ConflictSeeker
    {
        public ConflictSeeker(IConflictDetectionPolicy policy)
        {
            ConflictDetectionPolicy = policy;
        }

        private IConflictDetectionPolicy ConflictDetectionPolicy { get; }

        public List<Conflict> GetConflicts(DirectoryWrapper masterDirectory, DirectoryWrapper slaveDirectory)
        {
            var left = LeftJoin(masterDirectory.EnumerateDirectories(), slaveDirectory.EnumerateDirectories())
                .Union(LeftJoin(masterDirectory.EnumerateFiles(), slaveDirectory.EnumerateFiles()));

            var right = RightJoin(masterDirectory.EnumerateDirectories(), slaveDirectory.EnumerateDirectories())
                .Union(RightJoin(masterDirectory.EnumerateFiles(), slaveDirectory.EnumerateFiles()));

            return left.Union(right).ToHashSet().ToList();
        }

        private IEnumerable<Conflict> RightJoin(IEnumerable<IFileSystemElementWrapper> left,
            IEnumerable<IFileSystemElementWrapper> right)
        {
            return from x in right
                join y in left
                    on x.Name equals y.Name into
                    temp
                from z in temp.DefaultIfEmpty()
                where ConflictDetectionPolicy.MakesConflict(z, x)
                select ConflictDetectionPolicy.GetConflict(z, x);
        }

        private IEnumerable<Conflict> LeftJoin(IEnumerable<IFileSystemElementWrapper> left,
            IEnumerable<IFileSystemElementWrapper> right)
        {
            return from x in left
                join y in right
                    on x.Name equals y.Name into
                    temp
                from z in temp.DefaultIfEmpty()
                where ConflictDetectionPolicy.MakesConflict(x, z)
                select ConflictDetectionPolicy.GetConflict(x, z);
        }
    }
}