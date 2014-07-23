namespace RefreshingCacheLibrary
{
    public interface ICanGetValue<TKey, TValue>
    {
        TValue GetValue(TKey key);
    }
}
