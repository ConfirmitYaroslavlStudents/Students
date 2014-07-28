using System.Collections.Generic;

namespace FilmService.KindsOfGenerators
{
    public class StatementGeneratorString:StatementGenerator
    {
        public override string Generate(string name, List<Rental> rentals)
        {
            double totalAmount = 0;
            var frequentRenterPoints = 0;
            var result = "Учет аренды для " + name + "\n";
            foreach (var rental in rentals)
            {
                var thisAmount = rental.Movie.CurrentCalculator.Calculate(rental.DaysRented);

                frequentRenterPoints += rental.Movie.CurrentCalculator.GetPoints();

                result += "\t" + rental.Movie.Title + "\t" + thisAmount + "\n";
                totalAmount += thisAmount;
            }

            result += "Сумма задолженности составляет " + totalAmount + "\n";
            result += "Вы заработали " + frequentRenterPoints + " за активность";
            return result;
        }
    }
}
