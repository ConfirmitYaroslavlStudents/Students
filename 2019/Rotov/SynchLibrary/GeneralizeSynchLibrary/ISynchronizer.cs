namespace GeneralizeSynchLibrary
{
    public interface ISynchronizer
    {
        SynchReport Synchronize(FileWrapperCollection collection);
    }
}
