using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sync
{
    public class ConflictSeeker
    {
        private IConflictDetectionPolicy ConflictDetectionPolicy { get; }

        public ConflictSeeker(IConflictDetectionPolicy policy)
        {
            ConflictDetectionPolicy = policy;
        }

        public List<Conflict> GetConflicts(DirectoryInfo masterDirectory, DirectoryInfo slaveDirectory)
        {
            var left = (
                from x in masterDirectory.GetContainment()
                join y in slaveDirectory.GetContainment()
                    on new {x.Name, Attribute = x.ElementType} equals new {y.Name, Attribute = y.ElementType} into
                    temp
                from z in temp.DefaultIfEmpty()
                where ConflictDetectionPolicy.MakesConflict(x, z)
                select ConflictDetectionPolicy.GetConflict(x, z)
            ).ToList();

            var right = (
                from x in slaveDirectory.GetContainment()
                join y in masterDirectory.GetContainment()
                    on new {x.Name, Attribute = x.ElementType} equals new {y.Name, Attribute = y.ElementType} into
                    temp
                from z in temp.DefaultIfEmpty()
                where ConflictDetectionPolicy.MakesConflict(z, x)
                select ConflictDetectionPolicy.GetConflict(z, x)
            ).ToList();

            return left.Union(right).ToHashSet().ToList();
        }
    }
}