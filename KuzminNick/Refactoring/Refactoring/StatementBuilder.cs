using System.Text;

namespace Refactoring
{
    public class StatementBuilder
    {
        public StringBuilder GetStringOfCustomerName(string name)
        {
            return new StringBuilder("Учет аренды для " + name + "\r\n");
        }

        public StringBuilder GetStringOfRentalForCurrentMovie(string movieTitle, double valueOfCurrentMovie)
        {
            return new StringBuilder("\t" + movieTitle + "\t" + valueOfCurrentMovie + "\r\n");
        }

        public StringBuilder GetStringOfTotalRental(double totalRental)
        {
            return new StringBuilder("Сумма задолженности составляет " + totalRental + "\r\n");
        }

        public StringBuilder GetStringOfFrequentRenterPoints(int frequentRenterPoints)
        {
            return new StringBuilder("Вы заработали " + frequentRenterPoints + " за активность");
        }
    }
}
