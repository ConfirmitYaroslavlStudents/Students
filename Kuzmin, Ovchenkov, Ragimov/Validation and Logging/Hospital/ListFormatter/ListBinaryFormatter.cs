using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ListFormatter
{
    public class ListBinaryFormatter<T> : IListBinaryFormatter<T>
    {
        public byte[] Serialize(IList<T> data)
        {
            var binFmt = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                binFmt.Serialize(ms, data);
                //LOGGING
                //WHAT ABOUT TRY/CATCH??
                return ms.ToArray();
            }
        }

        public IList<T> Deserialize(byte[] serializedData)
        {
            var binFmt = new BinaryFormatter();
            using (var ms = new MemoryStream(serializedData))
            {
                return binFmt.Deserialize(ms) as List<T>;
            }
        }
    }
}