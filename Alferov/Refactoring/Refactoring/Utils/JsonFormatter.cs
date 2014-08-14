using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Refactoring.Utils
{
    public class JsonFormatter : ICustomerFormatter, ICustomerDeserializer
    {
        private readonly Type[] _serializableTypes =
        {
            typeof (Rental), typeof (Movie), typeof (RegularMovie),
            typeof (ChildrensMovie), typeof (NewReleaseMovie)
        };

        public void Serialize(SerializedData data, Customer customer)
        {
            using (var stream = new MemoryStream())
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(Customer), _serializableTypes);
                jsonSerializer.WriteObject(stream, customer);
                stream.Position = 0;
                var sr = new StreamReader(stream);
                data.JsonData = sr.ReadToEnd();
            }
        }

        public Customer Deserialize(SerializedData serializedData)
        {
            using (var stream = new MemoryStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(serializedData.JsonData);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var dataContractSerializer = new DataContractJsonSerializer(typeof(Customer), _serializableTypes);

                return dataContractSerializer.ReadObject(stream) as Customer;
            }
        }
    }
}