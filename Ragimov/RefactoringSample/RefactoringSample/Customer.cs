using System;
using System.Collections.Generic;
using System.Globalization;

namespace RefactoringSample
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

        public string Statement()
        {
            double totalAmount = 0;
            var frequentRenterPoints = 0;
            var result = String.Format("Учет аренды для {0}{1}", Name,Environment.NewLine);

            foreach (var rental in Rentals)
            {
                var thisAmount = rental.GetCharge();

                frequentRenterPoints += rental.GetFrequentPoints();

                result += String.Format("\t {0} \t {1} {2}",rental.Movie.Title,thisAmount.ToString(CultureInfo.InvariantCulture),Environment.NewLine);
                totalAmount += thisAmount;
            }

            result += String.Format("Сумма задолженности составляет {0}{1}", totalAmount.ToString(CultureInfo.InvariantCulture),Environment.NewLine);
            result += String.Format("Вы заработали {0} за активность", frequentRenterPoints.ToString(CultureInfo.InvariantCulture));
            return result;
        }
    }
}