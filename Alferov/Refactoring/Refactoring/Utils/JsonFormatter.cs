using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Refactoring.Utils
{
    public class JsonFormatter : ICustomerFormatter
    {
        private readonly Type[] _serializableTypes =
        {
            typeof (Rental), typeof (Movie), typeof (RegularMovie),
            typeof (ChildrensMovie), typeof (NewReleaseMovie)
        };

        public string Serialize(Customer customer)
        {
            using (var stream = new MemoryStream())
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof (Customer), _serializableTypes);
                jsonSerializer.WriteObject(stream, customer);
                stream.Position = 0;
                var sr = new StreamReader(stream);
                return sr.ReadToEnd();
            }
        }

        public Customer Deserialize(string serializedData)
        {
            using (var stream = new MemoryStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(serializedData);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var dataContractSerializer = new DataContractJsonSerializer(typeof (Customer), _serializableTypes);

                return dataContractSerializer.ReadObject(stream) as Customer;
            }
        }
    }
}