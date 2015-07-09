using System.Runtime.Serialization;

namespace Refactoring
{
    [DataContract]
    public class Rental
    {
        [DataMember]
        public Movie Movie { get; private set; }

        [DataMember]
        public int DaysRented { get; private set; }

        public Rental(Movie movie, int daysRented)
        {
            Movie = movie;
            DaysRented = daysRented;
        }
        
        private Rental() {}

        public double GetCharge()
        {
            return Movie.GetPrice(DaysRented);
        }

        public int GetFrequentPoints()
        {
            int result = 1;

            if (Movie is NewReleaseMovie && DaysRented > 1)
            {
                ++result;
            }

            return result;
        }
    }
}