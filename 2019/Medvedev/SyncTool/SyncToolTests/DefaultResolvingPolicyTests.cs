using System;
using Sync.Resolutions;
using Sync.ResolvingPolicies;
using Sync.Wrappers;
using Xunit;

namespace Sync.Tests
{
    public class DefaultResolvingPolicyTests
    {
        [Fact]
        public void DeleteConflict_Resolve_Returns_DeleteResolution()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            var slaveFile = slave.CreateFile("a", new FileAttributes(2, DateTime.MaxValue));

            var conflict = new Conflict(null, slaveFile);

            var policy = new DefaultResolvingPolicy(master, slave);

            Assert.IsType<DeleteResolution>(policy.Resolve(conflict));
        }
    }
}