using System;
using Sync.Resolutions;
using Sync.ResolvingPolicies;
using Sync.Wrappers;
using Xunit;

namespace Sync.Tests
{
    public class NoDeleteResolvingPolicyTests
    {
        [Fact]
        public void UpdateConflict_Resolve_Returns_UpdateResolution()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            var masterFile = master.CreateFile("a", new FileAttributes(1, DateTime.MinValue));
            var slaveFile = slave.CreateFile("a", new FileAttributes(2, DateTime.MaxValue));

            var conflict = new Conflict(masterFile, slaveFile);

            var policy = new DefaultResolvingPolicy(slave, master);

            Assert.IsType<UpdateResolution>(policy.Resolve(conflict));
        }

        [Fact]
        public void CopyConflict_Resolve_Returns_CopyResolution()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            var masterFile = master.CreateFile("a", new FileAttributes(1, DateTime.MinValue));

            var conflict = new Conflict(masterFile, null);

            var policy = new DefaultResolvingPolicy(master, slave);

            Assert.IsType<CopyResolution>(policy.Resolve(conflict));
        }
    }
}