namespace CacheLibrary
{
    public interface IDataBase<T>
    {
        T GetItemById(string id);
    }
}
