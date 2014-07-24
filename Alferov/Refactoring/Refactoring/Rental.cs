namespace Refactoring
{
    public class Rental
    {
        public Movie Movie { get; private set; }

        public int DaysRented { get; private set; }

        public Rental(Movie movie, int daysRented)
        {
            Movie = movie;
            DaysRented = daysRented;
        }

        public double GetCharge()
        {
            return Movie.GetPrice(DaysRented);
        }

        public int GetFrequentPoints()
        {
            var result = 1;

            if (Movie is NewReleaseMovie && DaysRented > 1)
            {
                ++result;
            }

            return result;
        }
    }
}