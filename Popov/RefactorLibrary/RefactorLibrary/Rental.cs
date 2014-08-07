

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
            set;
        }

        public int DaysRented
        {
            get; 
            set;
        }

        
        public int GetBonusProfit()
        {
            return Movie.GetBonusProfit(DaysRented);
        }

        public double GetCharge()
        {
            return Movie.GetCharge(DaysRented);
        }
    }
}