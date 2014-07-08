using System.Collections.Generic;

namespace RefactoringDemo.Steps.Step1
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
                double thisAmount = 0;

                switch (rental.Movie.PriceCode)
                {
                    case Movie.REGULAR:
                        thisAmount += 2;
                        if (rental.DaysRented > 2)
                            thisAmount += (rental.DaysRented - 2)*1.5;
                        break;

                    case Movie.NEW_RELEASE:
                        thisAmount += rental.DaysRented*3;
                        break;

                    case Movie.CHILDRENS:
                        thisAmount += 1.5;
                        if (rental.DaysRented > 3)
                            thisAmount += (rental.DaysRented - 3) * 1.5;
                        break;
                }

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
    }
}