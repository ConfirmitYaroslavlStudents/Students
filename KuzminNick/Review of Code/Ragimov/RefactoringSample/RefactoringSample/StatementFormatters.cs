using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RefactoringSample
{
    // As one of the variants: to create an interface and implement in classes 'StringStatement' and 'JsonStatement' 
    //which is consistent with the principle of "open/closed"
    public class StringStatement
    {
        public string Result;
        public StringStatement(Statement statement)
        {
            var customer = statement.Customer;
            var result = new StringBuilder();
            result.Append(String.Format("Учет аренды для {0}{1}", customer.Name, Environment.NewLine));

            foreach (var movie in statement.MoviePrices.Keys)
            {
                result.Append(String.Format("\t {0} \t {1} {2}", movie, statement.MoviePrices[movie].ToString(CultureInfo.InvariantCulture), Environment.NewLine));
            }

            result.Append(String.Format("Сумма задолженности составляет {0}{1}", statement.TotalAmount.ToString(CultureInfo.InvariantCulture), Environment.NewLine));
            result.Append(String.Format("Вы заработали {0} за активность", statement.FrequentRenterPoints.ToString(CultureInfo.InvariantCulture)));
            Result = result.ToString();
        }
    }

    public class JsonStatement
    {
        public MemoryStream Result;
        public JsonStatement(Statement statement)
        {
            var stream = new MemoryStream();
            // change 'ser' to 'serializer'
            var ser = new DataContractJsonSerializer(typeof(Statement));
            ser.WriteObject(stream, statement);

            Result = stream;
        }
    }
}
