using Refactoring.Utils;
using Xunit;
using Xunit.Extensions;

namespace Refactoring.Tests
{
    public class RefactoringTests
    {
        [Fact]
        public void GetStatement_WhenCustomerHaveNoRentals_ShouldPass()
        {
            var customer = new Customer("Romnaka");
            const double expectedTotalAmount = 0;
            const int expectedFrequentRenterPoints = 0;

            customer.GetStatement(new StringFormatter());

            Assert.Equal(expectedTotalAmount, customer.TotalAmount);
            Assert.Equal(expectedFrequentRenterPoints, customer.FrequentRenterPoints);
        }

        [Theory]
        [InlineData(5, 4, 3, 18.5, 4)]
        [InlineData(2, 3, 1, 6.5, 3)]
        public void GetStatement_WhenCustomerHaveMultipleRentals_ShouldPass(int regularMovieDays, int childrensMovieDays, int newReleaseMovieDays,
            double expectedTotalAmount, int expectedFrequentRenterPoints)
        {
            const string customerName = "Romnaka";
            var regularMovie = new RegularMovie("Harry Potter");
            var childrensMovie = new ChildrensMovie("The Lion King");
            var newReleaseMovie = new NewReleaseMovie("Van Helsing");

            var customer = new Customer(customerName, new[]
            {
                new Rental(regularMovie, regularMovieDays),
                new Rental(childrensMovie, childrensMovieDays),
                new Rental(newReleaseMovie, newReleaseMovieDays)
            });

            customer.GetStatement(new StringFormatter());

            Assert.Equal(expectedTotalAmount, customer.TotalAmount);
            Assert.Equal(expectedFrequentRenterPoints, customer.FrequentRenterPoints);
        }

        [Fact]
        public void StringFormatter_WhenCustomerHaveMultipleRentals_ShouldPass()
        {
            const string customerName = "Romnaka";
            var regularMovie = new RegularMovie("Harry Potter");
            var childrensMovie = new ChildrensMovie("The Lion King");
            var newReleaseMovie = new NewReleaseMovie("Van Helsing");

            var customer = new Customer(customerName, new[]
            {
                new Rental(regularMovie, 5),
                new Rental(childrensMovie, 4),
                new Rental(newReleaseMovie, 3)
            });

            var stringFormatter = new StringFormatter();
            string serializedData = customer.GetStatement(stringFormatter);
            Customer actual = stringFormatter.Deserialize(serializedData);

            Assert.True(customer.Equals(actual));
        }

        [Fact]
        public void JsonFormatter_WhenCustomerHaveMultipleRentals_ShouldPass()
        {
            const string customerName = "Romnaka";
            var regularMovie = new RegularMovie("Harry Potter");
            var childrensMovie = new ChildrensMovie("The Lion King");
            var newReleaseMovie = new NewReleaseMovie("Van Helsing");

            var customer = new Customer(customerName, new[]
            {
                new Rental(regularMovie, 5),
                new Rental(childrensMovie, 4),
                new Rental(newReleaseMovie, 3)
            });

            var jsonFormatter = new JsonFormatter();
            string serializedData = customer.GetStatement(jsonFormatter);
            Customer actual = jsonFormatter.Deserialize(serializedData);

            Assert.True(customer.Equals(actual));
        }

        [Fact]
        public void XmlFormatter_WhenCustomerHaveMultipleRentals_ShouldPass()
        {
            const string customerName = "Romnaka";
            var regularMovie = new RegularMovie("Harry Potter");
            var childrensMovie = new ChildrensMovie("The Lion King");
            var newReleaseMovie = new NewReleaseMovie("Van Helsing");

            var customer = new Customer(customerName, new[]
            {
                new Rental(regularMovie, 5),
                new Rental(childrensMovie, 4),
                new Rental(newReleaseMovie, 3)
            });

            var xmlFormatter = new XmlFormatter();
            string serializedData = customer.GetStatement(xmlFormatter);
            Customer actual = xmlFormatter.Deserialize(serializedData);

            Assert.True(customer.Equals(actual));
        }
    }
}
