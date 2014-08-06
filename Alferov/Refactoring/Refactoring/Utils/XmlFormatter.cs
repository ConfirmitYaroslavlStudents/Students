using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Refactoring.Utils
{
    public class XmlFormatter : ICustomerFormatter
    {
        private readonly Type[] _serializableTypes =
        {
            typeof (Rental), typeof (Movie), typeof (RegularMovie),
            typeof (ChildrensMovie), typeof (NewReleaseMovie)
        };

        public string Serialize(Customer customer)
        {
            var serializer = new DataContractSerializer(typeof (Customer), _serializableTypes);

            using (var sw = new StringWriter())
            using (var writer = new XmlTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                serializer.WriteObject(writer, customer);
                writer.Flush();
                return sw.ToString();
            }
        }

        public Customer Deserialize(string serializedData)
        {
            using (var stream = new MemoryStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(serializedData);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var dataContractSerializer = new DataContractSerializer(typeof (Customer), _serializableTypes);

                return dataContractSerializer.ReadObject(stream) as Customer;
            }
        }
    }
}