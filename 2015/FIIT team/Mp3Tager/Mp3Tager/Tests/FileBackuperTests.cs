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
            using (var backup = new FileBackuper(new FakeMp3File(new Mp3Tags(), @"D:\music\audioq.mp3", new FakeUniquePathCreator())))
            {
                var actual = backup.RestoreFromBackup();
                Assert.IsTrue(actual);
            }            
        }
    }
}
