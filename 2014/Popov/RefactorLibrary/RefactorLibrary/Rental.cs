

namespace RefactorLibrary
{
    public class Rental
    {
        public Rental(Movie movie, int daysRented)
        {
            Movie = movie;
            DaysRented = daysRented;
        }

        public Movie Movie
        {
            get; 
            private set;
        }

        public int DaysRented
        {
            get; 
            private set;
        }

        
        internal int GetBonusProfit()
        {
            return Movie.GetBonusProfit(DaysRented);
        }

        internal double GetCharge()
        {
            return Movie.GetCharge(DaysRented);
        }
    }
}