using System.Linq;

namespace GeneralizeSynchLibrary
{
    public class NoRemoveSynchronizer : BaseSynchronizer
    {
        public override ILogger Synchronize(FileSystemHandler handler)
        {
            handler.MoveIntersectionFiles(Intersection);
            handler.SwapFiles(MasterWithoutSlave , SlaveWithoutMaster);
            return handler._logger;
        }
    }
}
