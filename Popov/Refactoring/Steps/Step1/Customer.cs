using System.Collections.Generic;
using System.Linq;

namespace RefactoringDemo.Steps.Step1
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
            Rentals = new List<Rental>();
        }

        public Customer(string name, List<Rental> rentals) : this(name)
        {
            Rentals = rentals;
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
            var result = "Учет аренды для " + Name + "\n";
            foreach (var rental in Rentals)
            {
                result += "\t" + rental.Movie.Title + "\t" + rental.GetCharge() + "\n";
            }

            result += "Сумма задолженности составляет " + GetTotalCharge() + "\n";
            result += "Вы заработали " + GetProfit() + " за активность";
            return result;
        }

        private double GetTotalCharge()
        {
            return Rentals.Sum(rental => rental.GetCharge());
        }
        private double GetProfit()
        {
            return Rentals.Sum(rental => rental.GetProfit());
        }
    }
}