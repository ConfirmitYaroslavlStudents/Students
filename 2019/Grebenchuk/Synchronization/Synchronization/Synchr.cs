using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Linq;

namespace Synchronization
{
    public class Synchr
    {
        private DirectoryInfo _master;
        private DirectoryInfo _slave;

        public Synchr(string master, string slave)
        {
            _master = new DirectoryInfo(master);
            _slave = new DirectoryInfo(slave);
        }

        public void Merge ()
        {
            foreach(var item in _slave.GetFiles())
            {
                    File.Delete(item.FullName);
            }

            foreach (var item in _master.GetFiles())
            {
                item.CopyTo(_slave.FullName + "\\" + item.Name);          
            }

        }

        public void NoDeleteMerge()
        {
            HashSet<string> slaveFiles = new HashSet<string>();
            Directory.GetFiles(_slave.FullName).ToList().ForEach(f => slaveFiles.Add(Path.GetFileName(f)));

            foreach (var item in _master.GetFiles())
            {
                if (!slaveFiles.Contains(item.Name))
                {
                    item.CopyTo(_slave.FullName + "\\" + item.Name);
                }
                else
                {
                    File.Replace(_slave.FullName + "\\" + item.Name, item.FullName, item.FullName);
                }
            }
        }
    }
}