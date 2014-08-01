using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Refactoring.Utils
{
    public class StringFormatter : ICustomerFormatter
    {
        public string Serialize(Customer customer)
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

        public Customer Deserialize(string serializedData)
        {
            string name = Regex.Match(serializedData, @"\b\w+\n", RegexOptions.Compiled).ToString().TrimEnd('\n');
            var customer = new Customer(name);

            var movieTitles = Regex.Matches(serializedData, @"\t(.*?)\t", RegexOptions.Compiled);
            var moviePrices = Regex.Matches(serializedData, @"\t\d+[,.]?(\d+)?\n", RegexOptions.Compiled);

            if (movieTitles.Count != moviePrices.Count)
            {
                throw new ArgumentException("data");
            }

            for (int i = 0; i < movieTitles.Count; ++i)
            {
                customer.Movies.Add(movieTitles[i].ToString().Trim('\t'), Convert.ToDouble(moviePrices[i].ToString().Trim('\t', '\n')));
            }

            var totalAmounAsString = Regex.Match(serializedData, @" \d+[,.]?(\d+)?\n", RegexOptions.Compiled).ToString().TrimEnd('\n');
            customer.TotalAmount = Convert.ToDouble(totalAmounAsString);
            customer.FrequentRenterPoints = Convert.ToInt32(Regex.Match(serializedData, @" (\d+) ", RegexOptions.Compiled).ToString());
     
            return customer;
        }
    }
}
