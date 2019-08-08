using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Synchronization
{
    class Program
    {
        public static void ClearDir(string path)
        {
            if (Directory.Exists("Master"))
            {
                DirectoryInfo Dir;
                Dir = new DirectoryInfo("Master");
                var subDir = Dir.GetDirectories();
                foreach (var c in subDir)
                {
                    var files = c.GetFiles();
                    foreach (var f in files)
                        f.Delete();
                    c.Delete();
                }
                var file = Dir.GetFiles();
                foreach (var f in file)
                    f.Delete();
                Directory.Delete("Master");
            }
        }
        static void Main(string[] args)
        {
            DirectoryInfo masterDir;
            DirectoryInfo slaveDir;
            ClearDir("Master");
            masterDir = Directory.CreateDirectory("Master");
            File.Create(Path.Combine(masterDir.FullName, "1.txt"));
            File.Create(Path.Combine(masterDir.FullName, "2m.txt"));

            DirectoryInfo masterSubDir = masterDir.CreateSubdirectory("Folder1m");
            File.Create(Path.Combine(masterSubDir.FullName, "3m.txt"));


            ClearDir("Slave");
            slaveDir = Directory.CreateDirectory("Slave");
            File.Create(Path.Combine(slaveDir.FullName, "1.txt"));
            File.Create(Path.Combine(slaveDir.FullName, "3s.txt"));

            DirectoryInfo slaveSubDir = slaveDir.CreateSubdirectory("Folder2s");
            File.Create(Path.Combine(masterSubDir.FullName, "2s.txt"));
            Synchronization synchronization = new Synchronization(masterDir.FullName, slaveDir.FullName);
        }
    }
}
