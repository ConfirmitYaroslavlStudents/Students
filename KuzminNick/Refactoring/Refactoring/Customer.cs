using System.Collections.Generic;

namespace VideoService
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
            set;
        }

        public string Statement()
        {
            var totalRental = 0.0;
            var frequentRenterPoints = 0;
            var result = "Учет аренды для " + Name + "\n";
            foreach (var rental in Rentals)
            {
                var valueOfCurrentRental = rental.GetRental();

                frequentRenterPoints += rental.GetFrequentPoints();

                result += "\t" + rental.Movie.Title + "\t" + valueOfCurrentRental + "\n";
                totalRental += valueOfCurrentRental;
            }

            result += "Сумма задолженности составляет " + totalRental + "\n";
            result += "Вы заработали " + frequentRenterPoints + " за активность";
            return result;
        }
    }
}
