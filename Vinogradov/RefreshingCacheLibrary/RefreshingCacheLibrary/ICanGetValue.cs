using System;

namespace RefreshingCacheLibrary
{
    public interface ICanGetValue<TKey, TValue>
    {
        TValue GetValue(TKey key, DateTime now);
        bool Contains(TKey key);
    }
}
