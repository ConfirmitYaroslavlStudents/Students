using Microsoft.VisualStudio.TestTools.UnitTesting;
using DirectorySynchronizationApp;
using System.IO;

namespace DirectorySynchronizationTests
{
    [TestClass]
    public class SynchronizationTests
    {
        public void DeleteDirectories()
        {
            if (Directory.Exists("master"))
                Directory.Delete("master", true);
            if (Directory.Exists("slave"))
                Directory.Delete("slave", true);
        }

        [TestMethod]
        public void DeleteSynchronization_GetTwoSameDirectories_NothingChancged()
        {
            DeleteDirectories();
            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");
            Directory.CreateDirectory(@"master\a");
            Directory.CreateDirectory(@"slave\a");
            File.Create(@"master\1").Close();
            File.Create(@"slave\1").Close();

            var sync = new Synch(master, slave, Enums.Flags.Nothing);
            sync.Synchronization();

            Assert.AreEqual(0, sync.log.ShowNumberOfCopiedFiles());
            Assert.AreEqual(0, sync.log.ShowNumberOfUpdatedFiles());
            Assert.AreEqual(0, sync.log.ShowNumberOfDeletedFiles());

        }

        [TestMethod]
        public void DeleteSynchronization_GetTwodifferentDirectories_OneFileCopied()
        {
            DeleteDirectories();
            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");
            Directory.CreateDirectory(@"master\a");
            Directory.CreateDirectory(@"slave\a");
            File.Create(@"master\1").Close();

            var sync = new Synch(master, slave, Enums.Flags.Nothing);
            sync.Synchronization();

            Assert.AreEqual(1, sync.log.ShowNumberOfCopiedFiles());
        }

        [TestMethod]
        public void NoDeleteSynchronization_GetTwodDfferentDirectories_OneFileCopied()
        {
            DeleteDirectories();
            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");
            Directory.CreateDirectory(@"master\a");
            Directory.CreateDirectory(@"slave\a");
            File.Create(@"master\1").Close();

            var sync = new Synch(master, slave, Enums.Flags.NoDelete);
            sync.Synchronization();

            Assert.AreEqual(1, sync.log.ShowNumberOfCopiedFiles());
        }


        [TestMethod]
        public void NoDeleteSynchronization_GetTwodDfferentDirectories_NothingChanged()
        {
            DeleteDirectories();
            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");
            Directory.CreateDirectory(@"master\a");
            Directory.CreateDirectory(@"slave\a");
            File.Create(@"master\1").Close();
            File.Create(@"slave\1").Close();

            var sync = new Synch(master, slave, Enums.Flags.NoDelete);
            sync.Synchronization();

            Assert.AreEqual(0, sync.log.ShowNumberOfCopiedFiles());
            Assert.AreEqual(0, sync.log.ShowNumberOfUpdatedFiles());
            Assert.AreEqual(0, sync.log.ShowNumberOfDeletedFiles());
        }

    }
}
