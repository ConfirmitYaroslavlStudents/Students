using System.Collections.Generic;

namespace ListFormatter
{
    public interface IListBinaryFormatter<T>
    {
        byte[] Serialize(IList<T> data);
        IList<T> Deserialize(byte[] serializedData);
    }
}