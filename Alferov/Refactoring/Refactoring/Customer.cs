using System.Collections.Generic;
using System.Text;

namespace Refactoring
{
    public class Customer
    {
        public string Name { get; private set; }
        public List<Rental> Rentals { get; private set; }

        public Customer(string name)
        {
            Name = name;
            Rentals = new List<Rental>();
        }

        public Customer(string name, IEnumerable<Rental> rentals)
        {
            Name = name;
            Rentals = new List<Rental>(rentals);
        }

        public string GetStatement()
        {
            double totalAmount = 0;
            int frequentRenterPoints = 0;
            
            var result = new StringBuilder(string.Format("Учет аренды для {0}\n", Name));
            
            foreach (var rental in Rentals)
            {
                double thisAmount = rental.GetCharge();
                frequentRenterPoints += rental.GetFrequentPoints();

                result.AppendFormat("\t{0}\t{1}\n", rental.Movie.Title, thisAmount);
                totalAmount += thisAmount;
            }

            result.AppendFormat("Сумма задолженности составляет {0}\n", totalAmount);
            result.AppendFormat("Вы заработали {0} за активность", frequentRenterPoints);
            return result.ToString();
        }
    }
}