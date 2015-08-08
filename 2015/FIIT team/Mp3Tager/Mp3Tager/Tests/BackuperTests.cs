using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestoreLib;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class BackuperTests
    {
        [TestMethod]
        public void Backup_Successful()
        {
            var backup = new Restore();

            var tempFile = backup.MakeBackup(new FakeFile(@"D:\music\audio.mp3"));
            Assert.AreEqual(tempFile.FullName, Path.GetTempPath() + @"audio.mp3");

            backup.RestoreFromBackup();
            Assert.AreEqual(tempFile.FullName, @"D:\music\audio.mp3");
        }
    }
}
