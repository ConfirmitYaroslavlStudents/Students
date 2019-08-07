using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SynchLibrary;

namespace SyncTest
{
    [TestClass]
    public class UnitTest1
    {
        string Master = @".\master";
        string Slave = @".\slave";

        public void Prepare()
        {
            if (!Directory.Exists(Master))
                Directory.CreateDirectory(Master);
            else
                Directory.Delete(Master, true);
            if (!Directory.Exists(Slave))
                Directory.CreateDirectory(Slave);
            else
                Directory.Delete(Slave, true);
        }

        [TestMethod]
        public void FilesAreNotReplaced()
        {
            Prepare();
            var in_master = Path.Combine(Master, "file.txt");
            var in_slave = Path.Combine(Slave, "file.txt");
            using (StreamWriter r = new StreamWriter(in_master))
                r.WriteLine(1);
            using (StreamWriter r = new StreamWriter(in_slave))
                r.WriteLine(1);
            var start = (new FileInfo(in_slave)).LastWriteTime;
            Sync cl = new Sync(Master, Slave, false, 0);
            cl.Synchronization();
            var control = (new FileInfo(in_slave)).LastWriteTime;
            Prepare();
            Assert.AreEqual(start, control);
        }

        [TestMethod]

        public void SwapWork()
        {
            Prepare();
            var in_master = Path.Combine(Master, "file.txt");
            var in_slave = Path.Combine(Slave, "file.txt");
            using (StreamWriter r = new StreamWriter(in_master))
                r.WriteLine(1);
            var sync = new Sync(Master, Slave, false, 0);
            sync.Synchronization();
            var res = File.Exists(in_slave);
            Prepare();
            Assert.IsTrue(res);
        }


        [TestMethod]

        public void RemoveWork()
        {
            Prepare();
            var in_slave = Path.Combine(Slave, "file.txt");
            using (StreamWriter r = new StreamWriter(in_slave))
                r.WriteLine(1);
            var sync = new Sync(Master, Slave, true, 0);
            sync.Synchronization();
            var res = !File.Exists(in_slave);
            Prepare();
            Assert.IsTrue(res);
        }

        [TestMethod]

        public void ReplaceWork()
        {
            Prepare();
            var in_master = Path.Combine(Master, "file.txt");
            var in_slave = Path.Combine(Slave, "file.txt");
            using (StreamWriter r = new StreamWriter(in_master))
                r.WriteLine(1);
            using (StreamWriter r = new StreamWriter(in_slave))
                r.WriteLine(2);
            var sync = new Sync(Master, Slave, false, 0);
            sync.Synchronization();
            int res = 0;
            using(StreamReader r = new StreamReader(in_slave))
            {
                res = Convert.ToInt32(r.ReadToEnd());
            }
            Prepare();
            Assert.AreEqual(1, res);
        }
    }
}
