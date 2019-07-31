using System;
using System.Collections.Generic;
using System.IO;

namespace FolderSync
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter master folder adress:");
                var masterFolderAdress = Console.ReadLine();
                Console.WriteLine("Enter slave folder adress:");
                var slaveFolderAdress = Console.ReadLine();
                Console.WriteLine("Enter command:");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "sync":
                        DirectoryInfo masterDirectory = new DirectoryInfo(masterFolderAdress);
                        DirectoryInfo slaveDirectory = new DirectoryInfo(slaveFolderAdress);

                        List<FileInfo> masterListOfFiles = new List<FileInfo>();
                        foreach (var item in masterDirectory.GetFiles())
                        {
                            masterListOfFiles.Add(item);
                        }   

                        List<FileInfo> slaveListOfFiles = new List<FileInfo>();
                        foreach (var item in slaveDirectory.GetFiles())
                        {
                            slaveListOfFiles.Add(item);
                        }

                        var tupleListsMasterAndSlave = Synchronization(masterListOfFiles, slaveListOfFiles);

                        foreach (var file in tupleListsMasterAndSlave.Item1)
                        {
                            File.Copy(file.FullName, Path.Combine(masterFolderAdress,file.Name), true);
                        }
                        foreach (var file in tupleListsMasterAndSlave.Item2)
                        {
                            File.Copy(file.FullName, Path.Combine(slaveFolderAdress, file.Name), true);
                        }
                        Console.WriteLine("Synchronization complete");
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("There is no such command! Please retry");
                        break;
                }
            }
        }
        public static Tuple<List<FileInfo>, List<FileInfo>> Synchronization(List<FileInfo> master, List<FileInfo> slave)
        {
            List<FileInfo> filesInMasterNotInSlave = new List<FileInfo>();
            List<FileInfo> filesInSlaveNotInMaster = new List<FileInfo>();

            if (master.Count == 0)
            {
                master = slave;
                return new Tuple<List<FileInfo>, List<FileInfo>>(master, slave);
            }
            if (slave.Count == 0)
            {
                slave = master;
                return new Tuple<List<FileInfo>, List<FileInfo>>(master, slave);
            }
            if (master.Count != 0 && slave.Count != 0)
            {
                foreach (var file in slave)
                {
                    if (!master.Contains(file))
                        filesInSlaveNotInMaster.Add(file);
                }

                foreach (var file in master)
                {
                    if (!slave.Contains(file))
                        filesInMasterNotInSlave.Add(file);
                }
                if (filesInMasterNotInSlave.Count == 0 && filesInSlaveNotInMaster.Count != 0)
                {
                    master.Clear();
                    foreach (var file in slave)
                        master.Add(file);
                }
                else
                {
                    slave.Clear();
                    foreach (var file in master)
                        slave.Add(file);
                }
            }
            return new Tuple<List<FileInfo>, List<FileInfo>>(master, slave);
        }
    }
}
/*
C:\dir1
C:\dir2
sync
 */