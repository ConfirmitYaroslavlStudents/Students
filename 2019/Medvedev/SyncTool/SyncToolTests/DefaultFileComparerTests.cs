using System;
using Sync.Comparers;
using Sync.Wrappers;
using Xunit;

namespace Sync.Tests
{
    public class DefaultFileComparerTests
    {
        [Fact]
        public void EqualFilesAttributes_ComparerReturnsEquals()
        {
            var attribute = new FileAttributes(1, DateTime.MinValue);
            var dir = new DirectoryWrapper("master");

            var first = dir.CreateFile("a", attribute);
            var second = dir.CreateFile("b", attribute);

            var comparer = new DefaultFileComparer();

            Assert.True(comparer.Compare(first, second) == 0);
        }

        [Fact]
        public void FirstNullSecondNotNull_ComparerReturnsGreater()
        {
            var attribute = new FileAttributes(1, DateTime.MinValue);
            var dir = new DirectoryWrapper("master");

            FileWrapper first = null;
            var second = dir.CreateFile("b", attribute);

            var comparer = new DefaultFileComparer();

            Assert.True(comparer.Compare(first, second) > 0);
        }

        [Fact]
        public void FirstNotNullSecondNull_ComparerReturnsLess()
        {
            var attribute = new FileAttributes(1, DateTime.MinValue);
            var dir = new DirectoryWrapper("master");

            var first = dir.CreateFile("a", attribute);
            FileWrapper second = null;

            var comparer = new DefaultFileComparer();

            Assert.True(comparer.Compare(first, second) < 0);
        }

        [Fact]
        public void FirstSizeGreaterThanSecondSize_WriteTimesEquals_ComparerReturnsLess()
        {
            var dir = new DirectoryWrapper("master");

            var first = dir.CreateFile("a", new FileAttributes(2, DateTime.MinValue));
            var second = dir.CreateFile("b", new FileAttributes(1, DateTime.MinValue));

            var comparer = new DefaultFileComparer();

            Assert.True(comparer.Compare(first, second) < 0);
        }

        [Fact]
        public void FirstSizeLessThanSecondSize_WriteTimesEquals_ComparerReturnsLess()
        {
            var dir = new DirectoryWrapper("master");

            var first = dir.CreateFile("a", new FileAttributes(1, DateTime.MinValue));
            var second = dir.CreateFile("b", new FileAttributes(2, DateTime.MinValue));

            var comparer = new DefaultFileComparer();

            Assert.True(comparer.Compare(first, second) > 0);
        }

        [Fact]
        public void FirstLastWriteTimeGreaterThanSecondLastWriteTime_SizesEquals_ComparerReturnsLess()
        {
            var dir = new DirectoryWrapper("master");

            var first = dir.CreateFile("a", new FileAttributes(1, DateTime.MaxValue));
            var second = dir.CreateFile("b", new FileAttributes(1, DateTime.MinValue));

            var comparer = new DefaultFileComparer();

            Assert.True(comparer.Compare(first, second) < 0);
        }

        [Fact]
        public void FirstLastWriteTimeLessThanSecondLastWriteTime_SizesEquals_ComparerReturnsGrater()
        {
            var dir = new DirectoryWrapper("master");

            var first = dir.CreateFile("a", new FileAttributes(1, DateTime.MinValue));
            var second = dir.CreateFile("b", new FileAttributes(1, DateTime.MaxValue));

            var comparer = new DefaultFileComparer();

            Assert.True(comparer.Compare(first, second) > 0);
        }
    }
}