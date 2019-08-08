using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Linq;

namespace Synchronization
{
    public class Synchronization
    {
        private string logLevel = "summary";
        private bool nodelete = true;
        Dictionary<string, string> deleted;
        Dictionary<string, string> copied;
        Dictionary<string, string> updated;

        DirectoryInfo _master;
        DirectoryInfo _slave;



        public Synchronization(string Master, string Slave)
        {
            DirectoryInfo _master = new DirectoryInfo(Master);
            DirectoryInfo _slave = new DirectoryInfo(Slave);

            deleted = new Dictionary<string, string>();
            copied = new Dictionary<string, string>();
            updated = new Dictionary<string, string>();

            Merge(_master, _slave);
        }

        public bool Nodelete
        {
            get
            {
                return nodelete;
            }

            set
            {
                nodelete = value;
            }
        }

        public string LogLevel
        {
            get
            {
                return logLevel;
            }
            set
            {
                if (value == "summary" || value == "silent" || value == "verbose")
                    logLevel = value;
                else throw new ArgumentException();
            }
        }

        public void Logging()
        {
            StreamWriter writer = new StreamWriter(Path.Combine(_master.FullName, "logging.txt"));
            switch (logLevel)
            {
                case "verbose":
                    {
                        foreach (var c in deleted)
                        {
                            writer.WriteLine(c.Key + "  was deleted from  " + c.Value);
                                }
                        foreach (var c in copied)
                        {
                            writer.WriteLine(c.Key + "  was copied to  " + c.Value);
                        }
                        foreach (var c in updated)
                        {
                            writer.WriteLine(c.Key + "  was updated by  " + c.Value);
                        }
                        break;
                    }
                case "summary":
                    {
                        writer.WriteLine("deleted  " + deleted.Count + "  files");
                        writer.WriteLine("copied  " + copied.Count + "  files");
                        writer.WriteLine("updated  " + updated.Count + "  files");
                        break;
                    }
                case "silent":
                    {
                        writer.WriteLine("No information");
                        break;
                    }
            }
            writer.Close();

        }

        public void Merge(DirectoryInfo master, DirectoryInfo slave)
        {
            Dictionary<string, DirectoryInfo> foldersMaster = master.GetDirectories().ToDictionary(f => f.Name, f => f);
            Dictionary<string, DirectoryInfo> folderSlave = slave.GetDirectories().ToDictionary(f => f.Name, f => f);

            Dictionary<string, FileInfo> filesMaster = master.GetFiles().ToDictionary(f => f.Name, f => f);
            Dictionary<string, FileInfo> filesSlave = slave.GetFiles().ToDictionary(f => f.Name, f => f);


            foreach (var c in foldersMaster.Keys)
            {
                if (!folderSlave.ContainsKey(c))
                {
                    CopyAll(foldersMaster[c], new DirectoryInfo(Path.Combine(slave.FullName, c)));
                }
                else
                {
                    Merge(foldersMaster[c], folderSlave[c]);
                    folderSlave.Remove(c);
                }
            }

            foreach (var c in folderSlave.Keys)
            {
                if (!foldersMaster.ContainsKey(c))
                {
                    CopyAll(folderSlave[c], new DirectoryInfo(Path.Combine(master.FullName, c)));
                }
            }

            foreach (var c in filesMaster.Keys)
            {
                if (!filesSlave.ContainsKey(c))
                {
                    File.Copy(filesMaster[c].FullName, Path.Combine(slave.FullName, c));
                }
                else
                {
                    if (filesMaster[c].LastWriteTimeUtc < filesSlave[c].LastWriteTimeUtc)
                    {
                        filesMaster[c].Replace(filesSlave[c].FullName, null, true);
                        updated.Add(filesMaster[c].FullName, filesSlave[c].FullName);
                    }
                    else
                    {
                        filesSlave[c].Replace(filesMaster[c].FullName, null, true);
                        updated.Add(filesSlave[c].FullName, filesMaster[c].FullName);
                    }

                    filesSlave.Remove(c);
                }
            }

            foreach (var c in filesSlave.Keys)
            {
                if (!filesMaster.ContainsKey(c))
                {
                    if (!nodelete)
                    {
                        deleted.Add(filesSlave[c].FullName, filesSlave[c].DirectoryName);
                        filesSlave[c].Delete();
                    }
                }
            }
        }



        private void CopyAll(DirectoryInfo fromFolder, DirectoryInfo toFolder)
        {
            if (Directory.Exists(toFolder.FullName) == false)
            {
                Directory.CreateDirectory(toFolder.FullName);
            }

            foreach (FileInfo fi in fromFolder.GetFiles())
            {
                fi.CopyTo(Path.Combine(toFolder.ToString(), fi.Name), true);

                copied.Add(fi.FullName, toFolder.FullName);
            }

            foreach (DirectoryInfo subDirection in fromFolder.GetDirectories())
            {
                DirectoryInfo nextSubDirection = toFolder.CreateSubdirectory(subDirection.Name);
                CopyAll(subDirection, nextSubDirection);
            }
        }
    }
}