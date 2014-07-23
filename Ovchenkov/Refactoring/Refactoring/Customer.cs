using System.Collections.Generic;
using System.Globalization;

namespace Refactoring
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
            Rentals = new List<Rental>();
        }

        public string Name
        {
            get;
            set;
        }

        public List<Rental> Rentals
        {
            get;
            private set;
        }

        public void AddRental(Rental rental)
        {
            Rentals.Add(rental);
        }

        public string Statement()
        {
            double totalAmount = 0;
            int frequentRenterPoints = 0;
            string result = "Учет аренды для " + Name + "\n";

            foreach (var rental in Rentals)
            {
                var thisAmount = rental.GetPrice();

                frequentRenterPoints += rental.GetFrequentPoints();

                result += "\t" + rental.Movie.Title + "\t" + thisAmount.ToString(CultureInfo.InvariantCulture) + "\n";
                totalAmount += thisAmount;
            }

            result += "Сумма задолженности составляет " + totalAmount.ToString(CultureInfo.InvariantCulture) + "\n";
            result += "Вы заработали " + frequentRenterPoints.ToString(CultureInfo.InvariantCulture) + " за активность";
            return result;
        }
    }
}