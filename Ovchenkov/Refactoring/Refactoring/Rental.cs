namespace Refactoring
{
    public class Rental
    {
        public Movie Movie
        {
            get;
            set;
        }

        public int DaysRented
        {
            get;
            set;
        }

        public double GetPrice()
        {
            return Movie.GetPrice(DaysRented);
        }

        public int GetFrequentPoints()
        {
            return Movie.GetFrequentPoints(DaysRented);
        }
    }
}