namespace RefreshingCache
{
    public interface IDataStorage<in TKey, out TValue>
    {
        TValue GetData(TKey key);
    }
}
