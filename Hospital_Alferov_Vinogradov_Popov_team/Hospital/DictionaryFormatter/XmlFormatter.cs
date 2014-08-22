using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace DictionaryFormatter
{
    public class XmlFormatter<TKey, TValue> : IDictionaryFormatter<TKey, TValue>
    {
        private readonly Type[] _serializableTypes =
        {
            typeof (TKey), typeof (TValue)
        };

        public string Serialize(Dictionary<TKey, TValue> data)
        {
            var serializer = new DataContractSerializer(typeof(Dictionary<TKey, TValue>), _serializableTypes);

            using (var sw = new StringWriter())
            using (var writer = new XmlTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                serializer.WriteObject(writer, data);
                writer.Flush();
                return sw.ToString();
            }
        }

        public Dictionary<TKey, TValue> Deserialize(string serializedData)
        {
            using (var stream = new MemoryStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(serializedData);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var dataContractSerializer = new DataContractSerializer(typeof(Dictionary<TKey, TValue>), _serializableTypes);

                return dataContractSerializer.ReadObject(stream) as Dictionary<TKey, TValue>;
            }
        }
    }
}
