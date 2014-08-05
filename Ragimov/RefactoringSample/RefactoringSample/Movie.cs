namespace RefactoringSample
{
    public class Movie
    {
        public Movie(PriceProvider priceType,string title)
        {
            PriceType = priceType;
            Title = title;
        }

        public PriceProvider PriceType { get; private set; }

        public string Title
        {
            get;
            set;
        }
    }
}
