namespace Cache
{
    //сделать то, что советует решарпер
    public interface IGettingValue<TKey, TValue>
    {
        TValue this[TKey key] { get; }
    }

    public interface IGetNumberIncludeCache
    {
        int NumberIncludeCahce { get; set; }
        int NumberIncludeStorage { get; set;  }
    }

    public interface ICheckCantainsKeyInCache<Tkey>
    {
        bool ContainsInCache(Tkey key);
    }

    public interface IMakeElementsInCacheOld
    {
        void MakeElementsOld(int number);
    }
}
