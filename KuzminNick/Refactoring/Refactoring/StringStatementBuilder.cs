namespace Refactoring
{
    public class StringStatementBuilder
    {
        public string GetStringOfCustomerName(string name)
        {
            return string.Format("Учет аренды для {0} \r\n", name);
        }

        public string GetStringOfRentalForCurrentMovie(string movieTitle, double valueOfCurrentMovie)
        {
            return string.Format("\t {0} \t {1} \r\n", movieTitle, valueOfCurrentMovie);
        }

        public string GetStringOfTotalRental(double totalRental)
        {
            return string.Format("Сумма задолженности составляет {0} \r\n", totalRental);
        }

        public string GetStringOfFrequentRenterPoints(int frequentRenterPoints)
        {
            return string.Format("Вы заработали {0} за активность", frequentRenterPoints);
        }
    }
}
