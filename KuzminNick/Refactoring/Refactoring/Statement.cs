using System.IO;
using System.Runtime.Serialization.Json;
using Refactoring;

namespace VideoService
{
    public interface IStatement
    {
        string GetStatement(Customer customer);
    }

    public class StringStatement : IStatement
    {
        private readonly StringStatementBuilder _statementBuilder = new StringStatementBuilder(); 
        public string GetStatement(Customer customer)
        {
            var totalRental = 0.0;
            var frequentRenterPoints = 0;
            var result = _statementBuilder.GetStringOfCustomerName(customer.Name);
            foreach (var rental in customer.Rentals)
            {
                var valueOfCurrentRental = rental.GetRental();

                frequentRenterPoints += rental.GetFrequentPoints();

                result += _statementBuilder.GetStringOfRentalForCurrentMovie(rental.Movie.Title, valueOfCurrentRental);
                totalRental += valueOfCurrentRental;
            }

            result += _statementBuilder.GetStringOfTotalRental(totalRental);
            result += _statementBuilder.GetStringOfFrequentRenterPoints(frequentRenterPoints);
            return result;
        }
    }

    public class JsonStatement : IStatement
    {
        public string GetStatement(Customer customer)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof (Customer));
                serializer.WriteObject(stream, customer);
                stream.Position = 0;
                using (var streamReader = new StreamReader(stream))
                {
                    var serializedObjectInStringFormat = streamReader.ReadToEnd();
                    return serializedObjectInStringFormat;
                }
            }
        }
    }
}
