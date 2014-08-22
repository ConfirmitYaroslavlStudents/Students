using System.Collections.Generic;

namespace DictionaryFormatter
{
    public interface IDictionaryFormatter<TKey, TValue>
    {
        string Serialize(Dictionary<TKey, TValue> data);
        Dictionary<TKey, TValue> Deserialize(string serializedData);
    }
}
