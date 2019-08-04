using Sync.Comparers;
using Sync.Wrappers;
using Xunit;

namespace Sync.Tests
{
    public class DefaultDirectoryComparerTests
    {
        [Fact]
        public void EqualNames_ComparerReturnsEquals()
        {
            var first = new DirectoryWrapper("master");
            var second = new DirectoryWrapper("MaStEr");

            var comparer = new DefaultDirectoryComparer();

            Assert.True(comparer.Compare(first, second) == 0);
        }

        [Fact]
        public void NullNotNull_ComparerReturns_Greater()
        {
            var second = new DirectoryWrapper("MaStEr");

            var comparer = new DefaultDirectoryComparer();

            Assert.True(comparer.Compare(null, second) > 0);
        }

        [Fact]
        public void NotNullNull_ComparerReturns_Less()
        {
            var first = new DirectoryWrapper("master");

            var comparer = new DefaultDirectoryComparer();

            Assert.True(comparer.Compare(first, null) < 0);
        }

        [Fact]
        public void FirstNameLexicographicallyLessThanSecondName_ComparerReturns_Less()
        {
            var first = new DirectoryWrapper("AbB");
            var second = new DirectoryWrapper("abc");

            var comparer = new DefaultDirectoryComparer();

            Assert.True(comparer.Compare(first, second) < 0);
        }

        [Fact]
        public void FirstNameLexicographicallyGreaterThanSecondName_ComparerReturns_Greater()
        {
            var first = new DirectoryWrapper("abc");
            var second = new DirectoryWrapper("AbB");

            var comparer = new DefaultDirectoryComparer();

            Assert.True(comparer.Compare(first, second) > 0);
        }
    }
}