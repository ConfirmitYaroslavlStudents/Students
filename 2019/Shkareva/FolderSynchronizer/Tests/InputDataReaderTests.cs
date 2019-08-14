using Xunit;
using FolderSynchronizerLib;

namespace Tests
{
    public class InputDataReaderTests
    {
        [Theory]
        [InlineData("C:\\", "D:\\", "--no-delete", "-loglevel", "silent")]
        [InlineData("C:\\", "D:\\", "--no-delete")]
        [InlineData("C:\\", "D:\\", "-loglevel", "silent", "--no-delete")]
        [InlineData("C:\\", "D:\\")]
        [InlineData("C:\\", "D:\\","-loglevel", "verbose")]
        public void CreateInputValidArgs(params string[] args)
        {
            var input = new InputDataReader(new TestFolderPathChecker()).Read(args);
        }

        [Theory]
        [InlineData("apple", "|tree:\\", "--no-delete", "-loglevel", "silent")]
        [InlineData("C:\\", "D:\\", "--nodelete")]
        [InlineData("C:\\", "D:\\", "-loglevel", "medium", "--no-delete")]
        [InlineData("W<C:\\", "WD>:\\")]
        [InlineData("C:\\", "D:\\", "loglevel", "verbose")]
        [InlineData("C:\\", "D:\\",  "-loglevel", "--no-delete", "silent")]

        public void CreateInputInvalidArgs(params string[] args)
        {
            Assert.Throws<SyncException>(() => {var input = new InputDataReader(new TestFolderPathChecker()).Read(args); });
        }

    }
}
