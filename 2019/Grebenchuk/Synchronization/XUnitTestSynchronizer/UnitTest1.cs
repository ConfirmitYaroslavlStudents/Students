using System;
using Xunit;
using System.IO;

namespace XUnitTestSynchronizer
{
    public class UnitTest1
    {
        DirectoryInfo masterDir;
        DirectoryInfo slaveDir;
        public void Prepare()
        {
            if (Directory.Exists("Master"))
                Directory.Delete("Master");
            masterDir = Directory.CreateDirectory("Master");
            File.Create(Path.Combine(masterDir.FullName, "1.txt"));
            File.Create(Path.Combine(masterDir.FullName, "2m.txt"));

            DirectoryInfo masterSubDir =  masterDir.CreateSubdirectory("Folder1m");
            File.Create(Path.Combine(masterSubDir.FullName, "3m.txt"));


            if (Directory.Exists("Slave"))
                Directory.Delete("Slave");
            slaveDir = Directory.CreateDirectory("Master");
            File.Create(Path.Combine(slaveDir.FullName, "1.txt"));
            File.Create(Path.Combine(slaveDir.FullName, "3s.txt"));

            DirectoryInfo slaveSubDir = slaveDir.CreateSubdirectory("Folder2s");
            File.Create(Path.Combine(masterSubDir.FullName, "2s.txt"));
        }
        [Fact]
        public void Test1()
        {
            Prepare();
           // Synchronization 
        }
    }
}
