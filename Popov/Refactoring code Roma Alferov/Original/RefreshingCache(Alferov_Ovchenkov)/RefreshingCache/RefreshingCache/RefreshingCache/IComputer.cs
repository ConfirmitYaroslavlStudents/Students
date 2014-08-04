namespace RefreshingCache
{
    public interface IComputer<in TKey, out TValue>
    {
        TValue GetData(TKey key);
    }
}
