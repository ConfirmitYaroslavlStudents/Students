using System;
using System.Collections.Generic;
using System.Linq;
using Sync.Comparers;
using Sync.ConflictDetectionPolicies;
using Sync.Resolutions;
using Sync.ResolvingPolicies;
using Sync.Wrappers;
using Xunit;

namespace Sync.Tests
{
    public class ResolverTests
    {
        [Fact]
        public void GetConflictsResolution_DefaultResolvingPolicy_MultipleConflicts()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            var masterFile1 = master.CreateDirectory("a").CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            var masterFile2 = master.CreateFile("2", new FileAttributes(3, DateTime.MaxValue));

            var slaveDirA = slave.CreateDirectory("a");
            var slaveFile1 = slaveDirA.CreateFile("1", new FileAttributes(2, DateTime.MaxValue));
            var slaveFile2 = slaveDirA.CreateFile("2", new FileAttributes(1, DateTime.MaxValue));

            var collector = new ConflictsCollector(master, slave, new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var conflicts = collector.GetConflicts();

            var resolver = new Resolver(new ResolvingPolicies.DefaultResolvingPolicy(master, slave));

            var expected = new HashSet<IResolution>
            {
                new UpdateResolution(slaveFile1, masterFile1),
                new DeleteResolution(slaveFile2),
                new CopyResolution(masterFile2, @"slave\2")
            };

            var actual = resolver.GetConflictsResolutions(conflicts).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetConflictsResolution_NoDeleteResolvingPolicy_MultipleConflicts()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            var masterFile1 = master.CreateDirectory("a").CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            var masterFile2 = master.CreateFile("2", new FileAttributes(3, DateTime.MaxValue));

            var slaveDirA = slave.CreateDirectory("a");
            var slaveFile1 = slaveDirA.CreateFile("1", new FileAttributes(2, DateTime.MaxValue));
            var slaveFile2 = slaveDirA.CreateFile("2", new FileAttributes(1, DateTime.MaxValue));

            var collector = new ConflictsCollector(master, slave, new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var conflicts = collector.GetConflicts();

            var resolver = new Resolver(new NoDeleteResolvingPolicy(master, slave));

            var expected = new HashSet<IResolution>
            {
                new UpdateResolution(slaveFile1, masterFile1),
                new CopyResolution(masterFile2, @"slave\2")
            };

            var actual = resolver.GetConflictsResolutions(conflicts).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}