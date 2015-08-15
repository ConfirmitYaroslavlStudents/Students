using System.IO;
using FileLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class FileBackuperTests
    {
        [TestMethod]
        public void Backup_Successful()
        {
            var backup = new FileBackuper();

            var tempFile = backup.MakeBackup(new FakeMp3File(new Mp3Tags(), @"D:\music\audio.mp3"));
            Assert.AreEqual(tempFile.FullName, Path.GetTempPath() + @"audio.mp3");

            backup.RestoreFromBackup();
            Assert.AreEqual(tempFile.FullName, @"D:\music\audio.mp3");
        }
    }
}
