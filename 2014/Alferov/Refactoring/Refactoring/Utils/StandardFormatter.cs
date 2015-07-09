using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Refactoring.Utils
{
    public class StandardFormatter : ICustomerFormatter, ICustomerDeserializer
    {
        public void Serialize(SerializedData data, Customer customer)
        {
            var result = new StringBuilder(string.Format("Учет аренды для {0}\n", customer.Name));

            foreach (var movie in customer.Movies)
            {
                result.AppendFormat("\t{0}\t{1}\n", movie.Key, movie.Value);
            }

            result.AppendFormat("Сумма задолженности составляет {0}\n", customer.TotalAmount);
            result.AppendFormat("Вы заработали {0} за активность", customer.FrequentRenterPoints);
            data.StandardData = result.ToString();
        }

        public Customer Deserialize(SerializedData serializedData)
        {
            string standardData = serializedData.StandardData;
            string name = Regex.Match(standardData, @"\b\w+\n", RegexOptions.Compiled).ToString().TrimEnd('\n');
            var customer = new Customer(name);

            var movieTitles = Regex.Matches(standardData, @"\t(.*?)\t", RegexOptions.Compiled);
            var moviePrices = Regex.Matches(standardData, @"\t\d+[,.]?(\d+)?\n", RegexOptions.Compiled);

            if (movieTitles.Count != moviePrices.Count)
            {
                throw new ArgumentException("data");
            }

            for (int i = 0; i < movieTitles.Count; ++i)
            {
                customer.Movies.Add(movieTitles[i].ToString().Trim('\t'), Convert.ToDouble(moviePrices[i].ToString().Trim('\t', '\n')));
            }

            string totalAmounAsString = Regex.Match(standardData, @" \d+[,.]?(\d+)?\n", RegexOptions.Compiled).ToString().TrimEnd('\n');
            customer.TotalAmount = Convert.ToDouble(totalAmounAsString);
            customer.FrequentRenterPoints = Convert.ToInt32(Regex.Match(standardData, @" (\d+) ", RegexOptions.Compiled).ToString());

            return customer;
        }
    }
}