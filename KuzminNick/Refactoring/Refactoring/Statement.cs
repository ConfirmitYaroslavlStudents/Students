using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Refactoring;

namespace VideoService
{
    public interface IStatement
    {
        StringBuilder GetStatement(Customer customer);
    }

    public class StringStatement : IStatement
    {
        private readonly StatementBuilder _statementBuilder = new StatementBuilder(); 
        public StringBuilder GetStatement(Customer customer)
        {
            var totalRental = 0.0;
            var frequentRenterPoints = 0;
            var result = _statementBuilder.GetStringOfCustomerName(customer.Name);
            foreach (var rental in customer.Rentals)
            {
                var valueOfCurrentRental = rental.GetRental();

                frequentRenterPoints += rental.GetFrequentPoints();

                result.Append(_statementBuilder.GetStringOfRentalForCurrentMovie(rental.Movie.Title, valueOfCurrentRental));
                totalRental += valueOfCurrentRental;
            }

            result.Append(_statementBuilder.GetStringOfTotalRental(totalRental));
            result.Append(_statementBuilder.GetStringOfFrequentRenterPoints(frequentRenterPoints));
            return result;
        }
    }

    public class JsonStatement : IStatement
    {
        public StringBuilder GetStatement(Customer customer)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof (Customer));
                serializer.WriteObject(stream, customer);
                stream.Position = 0;
                using (var streamReader = new StreamReader(stream))
                {
                    var serializedObjectInStringFormat = streamReader.ReadToEnd();
                    return new StringBuilder(serializedObjectInStringFormat);
                }
            }
        }
    }
}
