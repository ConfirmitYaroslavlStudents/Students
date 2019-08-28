using SyncLib;
using System;
using Xunit;

namespace SyncTest
{
    public class ConflictType
    {
        //тесты мои тестики((
        [Fact]
        public void FileConflictTypes_All()
        {
            FileChecker fileChecker = new FileChecker(Environment.CurrentDirectory + "\\master", Environment.CurrentDirectory + "\\slave");

            var check1 = fileChecker.GetTypeConflict("\\file1.txt");

            Assert.Equal(FileConflictType.NoConflict, check1);

            var check2 = fileChecker.GetTypeConflict("\\file2.txt");

            Assert.Equal(FileConflictType.DifferentContent, check2);

            var check3 = fileChecker.GetTypeConflict("\\file3.txt");

            Assert.Equal(FileConflictType.NoExistConflict, check3);

        }
        [Fact]
        public void GeDirectoryConflictTypes_All()
        {
            DirectoryChecker fileChecker = new DirectoryChecker(Environment.CurrentDirectory + "\\slave");

            var check1 = fileChecker.GetTypeConflict("\\dir1");

            Assert.Equal(DirectoryConflictType.ExistConflict, check1);

            var check2 = fileChecker.GetTypeConflict("\\dir2");

            Assert.Equal(DirectoryConflictType.NoExistConflict, check2);

        }
    }
}
