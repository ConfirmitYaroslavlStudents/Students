namespace SynchLibrary
{
    public class RemoveSynchronizer : BaseSynchronizer
    {
        public override ILogger Synchronize(FileSystemHandler handler)
        {
            handler.MoveIntersectionFiles(Intersection);
            handler.RemoveDisapperedFiles(SlaveWithoutMaster);
            return handler._logger;
        }
    }
}
