namespace RefreshingCacheLibrary
{
    public interface IDataStorage<in TKey, out TValue>
    {
        TValue GetValue(TKey key);
        bool Contains(TKey key);
    }
}
