using System;
using Sync.Wrappers;
using Xunit;

namespace Sync.Tests
{
    public class FileWrapperTests
    {
        [Fact]
        public void Equal_EqualFiles_IgnoreCase_True()
        {
            var dir1 = new DirectoryWrapper("master");
            var file1 = dir1.CreateFile("a", new FileAttributes(1, DateTime.MaxValue));

            var dir2 = new DirectoryWrapper("MaStEr");
            var file2 = dir2.CreateFile("A", new FileAttributes(2, DateTime.MinValue));

            Assert.True(file1.Equals(file2));
        }
    }
}