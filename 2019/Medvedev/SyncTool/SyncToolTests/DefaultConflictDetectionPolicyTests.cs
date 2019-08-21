using System;
using Sync.Comparers;
using Sync.ConflictDetectionPolicies;
using Sync.Wrappers;
using Xunit;

namespace Sync.Tests
{
    public class DefaultConflictDetectionPolicyTests
    {
        [Fact]
        public void NoConflicts_MakesConflict_ReturnsFalse()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            var attr = new FileAttributes(1, DateTime.MaxValue);

            var first = master.CreateFile("a", attr);
            var second = slave.CreateFile("a", attr);

            var policy = new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer());

            Assert.False(policy.ConflictExists(first, second));
        }

        [Fact]
        public void NoConflicts_GetConflict_ReturnsNull()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            var attr = new FileAttributes(1, DateTime.MaxValue);

            var first = master.CreateFile("a", attr);
            var second = slave.CreateFile("a", attr);

            var policy = new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer());

            Assert.Null(policy.GetConflict(first, second));
        }

        [Fact]
        public void ConflictExists_MakesConflict_ReturnsTrue()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            var attrFirst = new FileAttributes(1, DateTime.MaxValue);
            var attrSecond = new FileAttributes(2, DateTime.MaxValue);

            var first = master.CreateFile("a", attrFirst);
            var second = slave.CreateFile("a", attrSecond);

            var policy = new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer());

            Assert.True(policy.ConflictExists(first, second));
        }

        [Fact]
        public void ConflictExists_GetConflict_ReturnsNotNull()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            var attrFirst = new FileAttributes(1, DateTime.MaxValue);
            var attrSecond = new FileAttributes(2, DateTime.MaxValue);

            var first = master.CreateFile("a", attrFirst);
            var second = slave.CreateFile("a", attrSecond);

            var policy = new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer());

            Assert.NotNull(policy.GetConflict(first, second));
        }

        [Fact]
        public void DifferentFileSystemElements_MakesConflict_ThrowsArgumentException()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            var attrFirst = new FileAttributes(1, DateTime.MaxValue);

            var first = master.CreateFile("a", attrFirst);
            var second = slave.CreateDirectory("a");

            var policy = new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer());

            Assert.Throws<ArgumentException>(() => policy.ConflictExists(first, second));
        }

        [Fact]
        public void DifferentFileSystemElements_GetConflict_ThrowsArgumentException()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            var attrFirst = new FileAttributes(1, DateTime.MaxValue);

            var first = master.CreateFile("a", attrFirst);
            var second = slave.CreateDirectory("a");

            var policy = new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer());

            Assert.Throws<ArgumentException>(() => policy.GetConflict(first, second));
        }

        [Fact]
        public void NullNotNull_MakesConflict_ReturnsTrue()
        {
            var slave = new DirectoryWrapper("slave");

            var attrSecond = new FileAttributes(2, DateTime.MaxValue);

            var second = slave.CreateFile("a", attrSecond);

            var policy = new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer());

            Assert.True(policy.ConflictExists(null, second));
        }

        [Fact]
        public void NullNotNull_GetConflict_ReturnsNotNull()
        {
            var slave = new DirectoryWrapper("slave");

            var attrSecond = new FileAttributes(2, DateTime.MaxValue);

            var second = slave.CreateFile("a", attrSecond);

            var policy = new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer());

            Assert.NotNull(policy.GetConflict(null, second));
        }

        [Fact]
        public void NotNullNull_MakesConflict_ReturnsTrue()
        {
            var master = new DirectoryWrapper("master");

            var attrFirst = new FileAttributes(1, DateTime.MaxValue);

            var first = master.CreateFile("a", attrFirst);

            var policy = new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer());

            Assert.True(policy.ConflictExists(first, null));
        }

        [Fact]
        public void NotNullNull_GetConflict_ReturnsNotNull()
        {
            var master = new DirectoryWrapper("master");

            var attrFirst = new FileAttributes(1, DateTime.MaxValue);

            var first = master.CreateFile("a", attrFirst);

            var policy = new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer());

            Assert.NotNull(policy.GetConflict(first, null));
        }

        [Fact]
        public void FirstBetterThanSecond_GetConflict_ReturnsFirstSecond()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            var attrFirst = new FileAttributes(2, DateTime.MaxValue);
            var attrSecond = new FileAttributes(1, DateTime.MaxValue);

            var first = master.CreateFile("a", attrFirst);
            var second = slave.CreateFile("a", attrSecond);

            var policy = new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer());

            var conflict = policy.GetConflict(first, second);

            Assert.Equal(first, conflict.Source);
            Assert.Equal(second, conflict.Destination);
        }

        [Fact]
        public void SecondBetterThanFirst_GetConflict_ReturnsSecondFirst()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            var attrFirst = new FileAttributes(1, DateTime.MaxValue);
            var attrSecond = new FileAttributes(2, DateTime.MaxValue);

            var first = master.CreateFile("a", attrFirst);
            var second = slave.CreateFile("a", attrSecond);

            var policy = new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer());

            var conflict = policy.GetConflict(first, second);

            Assert.Equal(second, conflict.Source);
            Assert.Equal(first, conflict.Destination);
        }

        [Fact]
        public void NullNotNull_GetConflict_ReturnsNullNotNull()
        {
            var slave = new DirectoryWrapper("slave");

            var attrSecond = new FileAttributes(2, DateTime.MaxValue);

            var second = slave.CreateFile("a", attrSecond);

            var policy = new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer());

            var conflict = policy.GetConflict(null, second);

            Assert.Null(conflict.Source);
            Assert.NotNull(conflict.Destination);
        }

        [Fact]
        public void NotNullNull_GetConflict_ReturnsNotNullNull()
        {
            var master = new DirectoryWrapper("master");

            var attrFirst = new FileAttributes(1, DateTime.MaxValue);

            var first = master.CreateFile("a", attrFirst);

            var policy = new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer());

            var conflict = policy.GetConflict(first, null);

            Assert.NotNull(conflict.Source);
            Assert.Null(conflict.Destination);
        }
    }
}