using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Refactoring.Utils
{
    public class XmlFormatter : ICustomerFormatter
    {
        public string Serialize(Customer customer)
        {
            var serializer = new DataContractSerializer(typeof(Customer));

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
            using (Stream stream = new MemoryStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(serializedData);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var dataContractSerializer = new DataContractSerializer(typeof(Customer));

                return dataContractSerializer.ReadObject(stream) as Customer;
            }
        }
    }
}
