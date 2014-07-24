using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Refactoring.Tests
{
    [TestClass]
    public class RefactoringTests
    {
        [TestMethod]
        public void GetStatement_WhenCustomerHaveNoRentals_ShouldPass()
        {
            const string customerName = "Romnaka";

            var customer = new Customer(customerName);
            var sb = new StringBuilder(string.Format("Учет аренды для {0}\n", customerName));

            sb.AppendFormat("Сумма задолженности составляет {0}\n", 0);
            sb.AppendFormat("Вы заработали {0} за активность", 0);

            string expected = sb.ToString();
            string actual = customer.GetStatement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStatement_WhenCustomerHaveMultipleRentalsDaysRentedMoreThanDefaultValues_ShouldPass()
        {
            const string customerName = "Romnaka";
            var regularMovie = new RegularMovie("Harry Potter");
            var childrensMovie = new ChildrensMovie("The Lion King");
            var newReleaseMovie = new NewReleaseMovie("Van Helsing");

            var customer = new Customer(customerName, new []
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
            string actual = customer.GetStatement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStatement_WhenCustomerHaveMultipleRentalsDaysRentedLessThanDefaultValues_ShouldPass()
        {
            const string customerName = "Romnaka";
            var regularMovie = new RegularMovie("Harry Potter");
            var childrensMovie = new ChildrensMovie("The Lion King");
            var newReleaseMovie = new NewReleaseMovie("Van Helsing");

            var customer = new Customer(customerName, new[]
            {
                new Rental(regularMovie, 2),
                new Rental(childrensMovie, 3),
                new Rental(newReleaseMovie, 1)
            });

            var sb = new StringBuilder(string.Format("Учет аренды для {0}\n", customerName));
            sb.AppendFormat("\t{0}\t{1}\n", regularMovie.Title, 2);
            sb.AppendFormat("\t{0}\t{1}\n", childrensMovie.Title, 1.5);
            sb.AppendFormat("\t{0}\t{1}\n", newReleaseMovie.Title, 3);
            sb.AppendFormat("Сумма задолженности составляет {0}\n", 6.5);
            sb.AppendFormat("Вы заработали {0} за активность", 3);

            string expected = sb.ToString();
            string actual = customer.GetStatement();

            Assert.AreEqual(expected, actual);
        }
    }
}
