using System;

namespace RefreshingCacheLibrary
{
    //IDataStorage, implement resharper's tips
    public interface ICanGetValue<TKey, TValue>
    {
        TValue GetValue(TKey key, DateTime now);
        bool Contains(TKey key);
    }
}
