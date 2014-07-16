namespace RefreshingCache
{
    public interface IComputer<TKey, TValue>
    {
        TValue GetData(TKey key);
    }
}
