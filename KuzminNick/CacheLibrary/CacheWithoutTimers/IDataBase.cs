using CacheLibrary;

namespace CacheLibraryWithoutTimers
{
    public interface IDataBase<T>
    {
        Element<T> GetItemByIdentifier(string identifier);
    }
}
