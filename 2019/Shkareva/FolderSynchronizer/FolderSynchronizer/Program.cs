using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace FolderSynchronizer
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length<2)
            {
                throw new Exception();
            }

            var Master = LoadFolder(args[0]);
            var Slave = LoadFolder(args[1]);
            var Flags = new HashSet<string>();

            if (args.Length > 2)
            {
                for (int i=2; i < args.Length; i++)
                {
                    Flags.Add(args[i]);
                }
            }

            Synchronizer.Synchronize(Master, Slave);
        }

        public static Folder LoadFolder(string address)
        {
            if (!Directory.Exists(address))
            {
                throw new DirectoryNotFoundException();
            }

            var folder = new Folder(address);
            folder.internalFoldersPaths = Directory.GetDirectories(address).ToList<string>();

            if (folder.internalFoldersPaths.Count != 0)
            {
                foreach (string dirPath in folder.internalFoldersPaths)
                {
                    folder.internalFolders.Add(LoadFolder(dirPath));
                }
            }

            folder.FilesPaths = Directory.GetFiles(address).ToList();
            return folder;
        }
        
    }
}
