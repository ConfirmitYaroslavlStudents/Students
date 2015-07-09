namespace RefactoringSample
{
    public class RentalBuilder
    {
        public Rental Build(Movie movie)
        {
            return new Rental(movie);
        }
        public Rental Build(Movie movie, int daysRented)
        {
            return new Rental(movie) { DaysRented = daysRented };
        }
        public Rental Build(PriceProvider priceType, string title, int daysRented)
        {
            return new Rental(new Movie(priceType, title)) { DaysRented = daysRented };
        }
    }
}
