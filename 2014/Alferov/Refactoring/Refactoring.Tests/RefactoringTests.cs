using Refactoring.Utils;
using Xunit;
using Xunit.Extensions;

namespace Refactoring.Tests
{
    public class RefactoringTests
    {
        private static class CustomerFactory
        {
            public static Customer GetCustomer()
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

                return customer;
            }
        }

        [Fact]
        public void GetStatement_WhenCustomerHaveNoRentals_ShouldPass()
        {
            var customer = new Customer("Romnaka");
            const double expectedTotalAmount = 0;
            const int expectedFrequentRenterPoints = 0;

            customer.GetStatement(new StandardFormatter());

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

            customer.GetStatement(new StandardFormatter());

            Assert.Equal(expectedTotalAmount, customer.TotalAmount);
            Assert.Equal(expectedFrequentRenterPoints, customer.FrequentRenterPoints);
        }

        [Fact]
        public void StandardFormatter_WhenCustomerHaveMultipleRentals_ShouldPass()
        {
            var customer = CustomerFactory.GetCustomer();
            var standardFormatter = new StandardFormatter();
            var serializedData = customer.GetStatement(standardFormatter);
            Customer actual = standardFormatter.Deserialize(serializedData);

            Assert.True(customer.Equals(actual));
        }

        [Fact]
        public void JsonFormatter_WhenCustomerHaveMultipleRentals_ShouldPass()
        {
            var customer = CustomerFactory.GetCustomer();
            var jsonFormatter = new JsonFormatter();
            var serializedData = customer.GetStatement(jsonFormatter);
            Customer actual = jsonFormatter.Deserialize(serializedData);

            Assert.True(customer.Equals(actual));
        }

        [Fact]
        public void XmlFormatter_WhenCustomerHaveMultipleRentals_ShouldPass()
        {
            var customer = CustomerFactory.GetCustomer();
            var xmlFormatter = new XmlFormatter();
            var serializedData = customer.GetStatement(xmlFormatter);
            Customer actual = xmlFormatter.Deserialize(serializedData);

            Assert.True(customer.Equals(actual));
        }

        [Fact]
        public void CompositeFormatter_WhenCustomerHaveMultipleRentals_ShouldPass()
        {
            var jsonFormatter = new JsonFormatter();
            var xmlFormatter = new XmlFormatter();
            var standardFormatter = new StandardFormatter();
            var compositeFormatter = new CompositeFormatter();
            
            compositeFormatter.AddFormatter(new JsonFormatter());
            compositeFormatter.AddFormatter(new StandardFormatter());
            compositeFormatter.AddFormatter(new XmlFormatter());

            var customer = CustomerFactory.GetCustomer();
            var serializedData = customer.GetStatement(compositeFormatter);
            Customer actualFromJson = jsonFormatter.Deserialize(serializedData);
            Customer actualFromXml = xmlFormatter.Deserialize(serializedData);
            Customer actualFromStandard = standardFormatter.Deserialize(serializedData);

            Assert.True(customer.Equals(actualFromJson));
            Assert.True(customer.Equals(actualFromXml));
            Assert.True(customer.Equals(actualFromStandard));
        }
    }
}
