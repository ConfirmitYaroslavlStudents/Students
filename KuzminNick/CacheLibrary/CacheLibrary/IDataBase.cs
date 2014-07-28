namespace CacheLibraryWithoutTimers
{
    public interface IDataBase<T>
    {
        T GetItemById(string id);
    }
}
