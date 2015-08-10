using FolderBackuperLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class FolderBackuperTests
    {
        [TestMethod]
        public void BackupTest_Successful()
        {
            var backup = new FolderBackuper(new FakeFileSystemSource(@"D:\source"), new FakeFileSystemSource(@"Z:\BackupFolder"));

            backup.MakeBackup();

            Assert.AreEqual(@"Z:\BackupFolder\folderOne\Text.txt", backup._destination.FileNames[0]);
            Assert.AreEqual(@"Z:\BackupFolder\folderOne\FolderTwo\Nice - one.mp3", backup._destination.FileNames[1]);
        }
    }
}
