namespace RefactoringSample
{
    public class Movie
    {
        public Movie(PriceType priceType,string title)
        {
            PriceType = priceType;
            Title = title;
        }

        public PriceType PriceType { get; private set; }

        public string Title
        {
            get;
            set;
        }
    }
}
