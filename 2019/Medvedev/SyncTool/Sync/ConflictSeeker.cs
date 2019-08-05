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
            var left = (
                from x in masterDirectory.EnumerateContainment()
                join y in slaveDirectory.EnumerateContainment()
                    on new {x.Name, Type = x.ElementType} equals new {y.Name, Type = y.ElementType} into
                    temp
                from z in temp.DefaultIfEmpty()
                where ConflictDetectionPolicy.MakesConflict(x, z)
                select ConflictDetectionPolicy.GetConflict(x, z)
            ).ToList();

            var right = (
                from x in slaveDirectory.EnumerateContainment()
                join y in masterDirectory.EnumerateContainment()
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