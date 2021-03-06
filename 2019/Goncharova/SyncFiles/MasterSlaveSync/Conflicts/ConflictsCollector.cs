﻿using MasterSlaveSync.Conflicts;
using System.IO.Abstractions;
using System.Linq;

namespace MasterSlaveSync
{
    public class ConflictsCollector
    {
        private readonly ConflictsRetriever conflictsRetriever = new ConflictsRetriever();

        public ConflictsCollection CollectConflicts(IDirectoryInfo master, IDirectoryInfo slave)
        {
            var directoryElementsInBothMasterAndSlave = from m in master.EnumerateDirectories()
                                                        join s in slave.EnumerateDirectories()
                                                        on m.Name equals s.Name
                                                        select new { Master = m, Slave = s };

            var result = conflictsRetriever.GetConflicts(master, slave);

            foreach (var directoriesPair in directoryElementsInBothMasterAndSlave)
            {
                result = result.Concat(CollectConflicts(directoriesPair.Master, directoriesPair.Slave));
            }

            return result;

        }

    }
}
