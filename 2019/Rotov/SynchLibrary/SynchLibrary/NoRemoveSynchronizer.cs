using System.Linq;

namespace SynchLibrary
{
    public class NoRemoveSynchronizer : ISynchronizer
    {
        public ILogger Synchronize(string master , string slave , ILogger logger)
        {
            FileSystemHandler handler = new FileSystemHandler(master , slave , logger);
            var listMaster = handler.GetListOfFiles(master);
            var listSlave = handler.GetListOfFiles(slave);
            var intersection = listMaster.Intersect(listSlave);
            var masterWithoutSlave = listMaster.Except(listSlave);
            var slaveWithoutMaster = listSlave.Except(listMaster);
            handler.MoveIntersectionFiles(intersection);
            handler.SwapFiles(masterWithoutSlave , slaveWithoutMaster);
            return handler._logger;
        }
    }
}
