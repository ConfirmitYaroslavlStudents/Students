using System.Text;
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

            var sb = new StringBuilder(string.Format("Учет аренды для {0}\n", customerName));
            sb.AppendFormat("\t{0}\t{1}\n", regularMovie.Title, 6.5);
            sb.AppendFormat("\t{0}\t{1}\n", childrensMovie.Title, 3);
            sb.AppendFormat("\t{0}\t{1}\n", newReleaseMovie.Title, 9);
            sb.AppendFormat("Сумма задолженности составляет {0}\n", 18.5);
            sb.AppendFormat("Вы заработали {0} за активность", 4);

            string expected = sb.ToString();
            string actual = customer.GetStatement(new StringFormatter());

            Assert.Equal(expected, actual);
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

            const string expected = "{\"Movies\":{\"Harry Potter\":6.5,\"The Lion King\":3,\"Van Helsing\":9},\"Name\":\"Romnaka\",\"TotalAmount\":18.5,\"FrequentRenterPoints\":4}";
            string actual = customer.GetStatement(new JsonFormatter());

            Assert.Equal(expected, actual);
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

            const string expected = "<Customer xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.datacontract.org/2004/07/Refactoring\">\r\n  <FrequentRenterPoints>4</FrequentRenterPoints>\r\n  <Movies xmlns:d2p1=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\">\r\n    <d2p1:KeyValueOfstringdouble>\r\n      <d2p1:Key>Harry Potter</d2p1:Key>\r\n      <d2p1:Value>6.5</d2p1:Value>\r\n    </d2p1:KeyValueOfstringdouble>\r\n    <d2p1:KeyValueOfstringdouble>\r\n      <d2p1:Key>The Lion King</d2p1:Key>\r\n      <d2p1:Value>3</d2p1:Value>\r\n    </d2p1:KeyValueOfstringdouble>\r\n    <d2p1:KeyValueOfstringdouble>\r\n      <d2p1:Key>Van Helsing</d2p1:Key>\r\n      <d2p1:Value>9</d2p1:Value>\r\n    </d2p1:KeyValueOfstringdouble>\r\n  </Movies>\r\n  <Name>Romnaka</Name>\r\n  <TotalAmount>18.5</TotalAmount>\r\n</Customer>";
            string actual = customer.GetStatement(new XmlFormatter());

            Assert.Equal(expected, actual);
        }
    }
}
