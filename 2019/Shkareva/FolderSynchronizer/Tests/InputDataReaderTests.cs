using System;
using Xunit;
using FolderSynchronizerLib;
using System.Collections.Generic;
using System.Collections;

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
            var input = new InputDataReader().Read(args);
        }

        [Theory]
        [InlineData("C:\\", "QW:\\", "--no-delete", "-loglevel", "silent")]
        [InlineData("C:\\", "D:\\", "--nodelete")]
        [InlineData("C:\\", "D:\\", "-loglevel", "medium", "--no-delete")]
        [InlineData("WC:\\", "WD:\\")]
        [InlineData("C:\\", "D:\\", "loglevel", "verbose")]
        public void CreateInputInvalidArgs(params string[] args)
        {
            Assert.Throws<SyncException>(() => {var input = new InputDataReader().Read(args); });
        }

    }
}
