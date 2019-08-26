namespace GeneralizeSynchLibrary
{
    public interface ISynchronizer
    {
        SynchResult Synchronize(FileWrapperCollection collection);
    }
}
