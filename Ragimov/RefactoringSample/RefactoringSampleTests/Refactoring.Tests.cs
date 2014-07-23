using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefactoringSample;

namespace RefactoringSampleTests
{
    [TestClass]
    public class RefactoringTests
    {
        [TestMethod]
        public void Customer_RentedNothing_Shouldpass()
        {
            const string name = "Nemo";
            var expectedstatement = String.Format("Учет аренды для {0}{1}",name, Environment.NewLine);
            expectedstatement += String.Format("Сумма задолженности составляет {0}{1}", 0.ToString(CultureInfo.InvariantCulture),Environment.NewLine);
            expectedstatement += string.Format("Вы заработали {0} за активность",0.ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual(expectedstatement, new Customer(name).Statement());
        }

        [TestMethod]
        public void Customer_RentedSingleMovie_Shouldpass()
        {
            const string name = "Nemo";

            var customerNemo = new Customer(name);
            var mrNobody = new Rental(new PriceRegular(), "Mr.Nobody");
            mrNobody.DaysRented = 1;
            customerNemo.Rentals.Add(mrNobody);

            var expectedstatement = String.Format("Учет аренды для {0}{1}", name, Environment.NewLine);
            expectedstatement += String.Format("\t {0} \t {1} {2}", mrNobody.Movie.Title,2.ToString(CultureInfo.InvariantCulture), Environment.NewLine);

            expectedstatement += String.Format("Сумма задолженности составляет {0}{1}", 2.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            expectedstatement += String.Format("Вы заработали {0} за активность", 1.ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual(expectedstatement, customerNemo.Statement());
        }

        [TestMethod]
        public void Customer_RentedMultipleMovies_Shouldpass()
        {
            const string name = "Nemo";

            var customerNemo = new Customer(name);
            var mrNobody = new Rental(new PriceRegular(), "Mr.Nobody");
            mrNobody.DaysRented = 5;
            customerNemo.Rentals.Add(mrNobody);

            var movie2 = new Rental(new PriceNewRelease(), "Another movie for Nemo");
            movie2.DaysRented = 10;
            customerNemo.Rentals.Add(movie2);

            var movie3 = new Rental(new PriceChildren(), "Third Movie for Nemo");
            movie3.DaysRented = 23;
            customerNemo.Rentals.Add(movie3);

            var expectedstatement = String.Format("Учет аренды для {0}{1}", name, Environment.NewLine);

            expectedstatement += String.Format("\t {0} \t {1} {2}", mrNobody.Movie.Title,6.5.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            expectedstatement += String.Format("\t {0} \t {1} {2}", movie2.Movie.Title, 30.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            expectedstatement += String.Format("\t {0} \t {1} {2}", movie3.Movie.Title, 31.5.ToString(CultureInfo.InvariantCulture), Environment.NewLine);

            expectedstatement += String.Format("Сумма задолженности составляет {0}{1}", 68.ToString(CultureInfo.InvariantCulture), Environment.NewLine);
            expectedstatement += String.Format("Вы заработали {0} за активность", 4.ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual(expectedstatement, customerNemo.Statement());
        }
    }
}
