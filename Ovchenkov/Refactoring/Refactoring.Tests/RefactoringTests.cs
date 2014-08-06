using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using Xunit;
using Xunit.Extensions;

namespace Refactoring.Tests
{
    public class RefactoringTests
    {
        public Dictionary<string, double> GetMoviesWithPrice(Rental[] rentals)
        {
            var movies = new Dictionary<string, double>();

            foreach (var rental in rentals)
            {
                var priceOfMovie = rental.GetPrice();
                movies[rental.Movie.Title] = priceOfMovie;
            }
            return movies;
        }

        public bool CheckStatement(Statement statement, Dictionary<string, double> movies, string name, double totalAmount, int frequencyPoints)
        {
            return statement.Movies.SequenceEqual(movies) && statement.Name == name && statement.TotalAmount.CompareTo(totalAmount) == 0 &&
                   statement.FrequentRenterPoints == frequencyPoints;
        }

        public bool CheckStringStatement(string statement, Dictionary<string, double> movies, string name, double totalAmount, int frequencyPoints)
        {
            var result = String.Format("Учет аренды для {0}{1}", name, Environment.NewLine);

            foreach (var movie in movies)
            {
                result += String.Format("\t {0} \t {1} {2}", movie.Key,
                    movie.Value.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            }

            result += String.Format("Сумма задолженности составляет {0}{1}", totalAmount.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            result += String.Format("Вы заработали {0} за активность", frequencyPoints.ToString(CultureInfo.InvariantCulture));

            return (statement == result);
        }

        public bool CheckJsonStatement(string statement, Dictionary<string, double> movies, string name, double totalAmount, int frequencyPoints)
        {
            using (Stream stream = new MemoryStream())
            {
                var data = Encoding.UTF8.GetBytes(statement);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var dataContractSerializer = new DataContractJsonSerializer(typeof(Statement));

                var customer = dataContractSerializer.ReadObject(stream) as Statement;

                return (CheckStatement(customer, movies, name, totalAmount, frequencyPoints));
            }
        }

        [Fact]
        public void Statement_WhenCustomerHaveNoRentals_ShouldPass()
        {
            const string customerName = "John Rambo";
            const double expectedTotalAmount = 0;
            const int expectedFrequentRenterPoints = 0;

            var customer = new Customer(customerName);
            var statement = new Statement(customer);

            Assert.True(CheckStatement(statement, new Dictionary<string, double>(), customerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(CheckStringStatement(statement.GetStatement(new StringStatement()), new Dictionary<string, double>(), customerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(CheckJsonStatement(statement.GetStatement(new JsonStatement()), new Dictionary<string, double>(), customerName, expectedTotalAmount, expectedFrequentRenterPoints));
        }

        [Theory]
        [InlineData(0, 0, 0, 3.5, 3)]
        [InlineData(1, 1, 1, 6.5, 3)]
        [InlineData(3, 1, 1, 8, 3)]
        [InlineData(1, 4, 1, 8, 3)]
        [InlineData(1, 1, 2, 9.5, 4)]
        [InlineData(5, 4, 3, 18.5, 4)]
        [InlineData(2, 3, 1, 6.5, 3)]
        public void Statement__WhenCustomerHaveMultipleRentals_Shouldpass(int regularMovieDays, int childrensMovieDays, int newReleaseMovieDays,
            double expectedTotalAmount, int expectedFrequentRenterPoints)
        {
            const string customerName = "John Rambo";

            var newReleaseMovie = new Movie { PriceCode = TypeOfMovie.NewRelease, Title = "Rembo : XX" };
            var regularMovie = new Movie { PriceCode = TypeOfMovie.Regular, Title = "Rembo :First Blood" };
            var childrensMovie = new Movie { PriceCode = TypeOfMovie.Childrens, Title = "Rembo : Baby edition" };
            var customerRentals = new[]
            {
                new Rental(regularMovie, regularMovieDays),
                new Rental(childrensMovie, childrensMovieDays),
                new Rental(newReleaseMovie, newReleaseMovieDays)
            };

            var customer = new Customer(customerName, customerRentals);
            var statement = new Statement(customer);

            Assert.True(CheckStatement(statement, GetMoviesWithPrice(customerRentals), customerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(CheckStringStatement(statement.GetStatement(new StringStatement()), GetMoviesWithPrice(customerRentals), customerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(CheckJsonStatement(statement.GetStatement(new JsonStatement()), GetMoviesWithPrice(customerRentals), customerName, expectedTotalAmount, expectedFrequentRenterPoints));
        }

        [Fact]
        public void Statement_WhenCustomerHaveSingleRental_Shouldpass()
        {
            const string customerName = "John Rambo";
            const int daysRented = 3;
            const double expectedTotalAmount = 9;
            const int expectedFrequentRenterPoints = 2;
           
            var movie = new Movie { PriceCode = TypeOfMovie.NewRelease, Title = "Rembo : XX" };
            var customerRentals = new Rental[1];
            customerRentals[0] = new Rental(movie, daysRented);

            var customer = new Customer(customerName, new Rental(movie, daysRented));
            var statement = new Statement(customer);

            Assert.True(CheckStatement(statement, GetMoviesWithPrice(customerRentals), customerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(CheckStringStatement(statement.GetStatement(new StringStatement()), GetMoviesWithPrice(customerRentals), customerName, expectedTotalAmount, expectedFrequentRenterPoints));
            Assert.True(CheckJsonStatement(statement.GetStatement(new JsonStatement()), GetMoviesWithPrice(customerRentals), customerName, expectedTotalAmount, expectedFrequentRenterPoints));
        }
    }
}
