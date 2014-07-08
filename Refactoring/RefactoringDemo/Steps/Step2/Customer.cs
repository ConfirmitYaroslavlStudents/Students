using System.Collections.Generic;

namespace RefactoringDemo.Steps.Step2
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
            int frequentRenterPoints = 0;
            string result = "Учет аренды для " + Name + "\n";
            foreach (var rental in Rentals)
            {
                var thisAmount = GetAmountFoRental(rental);

                frequentRenterPoints++;
                if (rental.Movie.PriceCode == Movie.NEW_RELEASE && rental.DaysRented > 1)
                    frequentRenterPoints++;

                result += "\t" + rental.Movie.Title + "\t" + thisAmount + "\n";
                totalAmount += thisAmount;
            }

            result += "Сумма задолженности составляет " + totalAmount + "\n";
            result += "Вы заработали " + frequentRenterPoints + " за активность";
            return result;
        }

        private static double GetAmountFoRental(Rental rental)
        {
            double result = 0;

            switch (rental.Movie.PriceCode)
            {
                case Movie.REGULAR:
                    result += 2;
                    if (rental.DaysRented > 2)
                        result += (rental.DaysRented - 2)*1.5;
                    break;

                case Movie.NEW_RELEASE:
                    result += rental.DaysRented*3;
                    break;

                case Movie.CHILDRENS:
                    result += 1.5;
                    if (rental.DaysRented > 3)
                        result += (rental.DaysRented - 3)*1.5;
                    break;
            }
            return result;
        }
    }
}