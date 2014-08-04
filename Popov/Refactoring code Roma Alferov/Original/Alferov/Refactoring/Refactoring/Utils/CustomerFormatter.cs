using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;

namespace Refactoring.Utils
{
    public abstract class CustomerFormatter
    {
        public abstract string Serialize(Customer customer);
    }

    public class StringFormatter : CustomerFormatter
    {
        public override string Serialize(Customer customer)
        {
            var result = new StringBuilder(string.Format("Учет аренды для {0}\n", customer.Name));

            foreach (var movie in customer.Movies)
            {
                result.AppendFormat("\t{0}\t{1}\n", movie.Key, movie.Value);
            }

            result.AppendFormat("Сумма задолженности составляет {0}\n", customer.TotalAmount);
            result.AppendFormat("Вы заработали {0} за активность", customer.FrequentRenterPoints);
            return result.ToString();
        }
    }

    public class JsonFormatter : CustomerFormatter
    {
        public override string Serialize(Customer customer)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(customer);
        }
    }

    public class XmlFormatter : CustomerFormatter
    {
        public override string Serialize(Customer customer)
        {
            var serializer = new DataContractSerializer(typeof(Customer));

            using (var sw = new StringWriter())
            {
                using (var writer = new XmlTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;
                    serializer.WriteObject(writer, customer);
                    writer.Flush();
                    return sw.ToString();
                }
            }
        }
    }
}
