namespace Refactoring
{
    public enum TypeOfMovie
    {
        Childrens, Regular, NewRelease
    }

    public class Movie
    {
        private Price _price;

        public string Title
        {
            get;
            set;
        }

        public TypeOfMovie PriceCode
        {
            get { return _price.GetPriceCode(); }
            set
            {
                // Violate the principle of 'single responsibility'.
                // It's necessary to change logic of program and use abstracts classes or interface
                switch (value)
                {
                    case TypeOfMovie.Regular:
                        _price = new RegularPrice();
                        break;
                    case TypeOfMovie.Childrens:
                        _price = new ChildrensPrice();
                        break;
                    case TypeOfMovie.NewRelease:
                        _price = new NewReleasePrice();
                        break;
                }
            }
        }

        public double GetPrice(int daysRented)
        {
            return _price.GetPrice(daysRented);
        }

        public int GetFrequentPoints(int daysRented)
        {
            return _price.GetFrequentPoints(daysRented);
        }
    }
}
