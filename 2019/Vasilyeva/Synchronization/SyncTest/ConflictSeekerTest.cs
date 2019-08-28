using SyncLib;
using System;
using Xunit;

namespace SyncTest
{
    public class ConflictSeekerTest
    {
        [Fact]
        public void GetMasterConflicts_AllTypesConflict()
        {
            var seeker = new DefaultConflictSeeker(Environment.CurrentDirectory + "\\master", Environment.CurrentDirectory + "\\slave");

            var masterConflict = seeker.GetMasterConflict();

            Assert.Equal(3, masterConflict.Count);

            Assert.True(masterConflict[0] is DifferentContentConflict);
            Assert.True(masterConflict[1] is NoExistFileConflict);
            Assert.True(masterConflict[2] is NoExistDirectoryConflict);
        }

        [Fact]
        public void GetSlaveConflicts_ByDefaultSeeker_AllTypes()
        {
            var seeker = new DefaultConflictSeeker(Environment.CurrentDirectory + "\\master", Environment.CurrentDirectory + "\\slave");

            var slaveConflict = seeker.GetSlaveConflicts();

            Assert.Equal(2, slaveConflict.Count);

            Assert.True(slaveConflict[0] is NoExistFileConflict);
            Assert.True(slaveConflict[1] is NoExistDirectoryConflict);
        }

        [Fact]
        public void GetSlaveConflicts_ByRemoveSeeker_AllTypes()
        {
            var seeker = new RemoveConflictSeeker(Environment.CurrentDirectory + "\\master", Environment.CurrentDirectory + "\\slave");

            var slaveConflict = seeker.GetSlaveConflicts();

            Assert.Equal(2, slaveConflict.Count);

            Assert.True(slaveConflict[0] is ExistFileConflict);
            Assert.True(slaveConflict[1] is ExistDirectoryConflict);
        }
    }
}
