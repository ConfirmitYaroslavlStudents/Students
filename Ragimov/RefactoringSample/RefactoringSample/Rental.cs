namespace RefactoringSample
{
    public class Rental
    {
        public Rental(PriceType priceType,string title)
        {
            Movie = new Movie(priceType,title);
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

        public int GetFrequentPoints()
        {
            return Movie.PriceType.GetFrequentPoints(DaysRented);
        }

        public double GetCharge()
        {
            return Movie.PriceType.GetPrice(DaysRented);
        }
    }
}