using System;


namespace RefactorLibrary
{
    public class Movie
    {
        private readonly string _title;


        public Movie(string title, Price priceCode)
        {
            PriceCode = priceCode;
            _title = title;
        }

        public string Title
        {
            get { return _title; } 
            private set
            {
                if (value == null) throw new ArgumentNullException("value");
                Title = value;
            }
        }


        public Price PriceCode { get; set; }

        internal double GetCharge(int daysRented)
        {
            return PriceCode.GetCharge(daysRented);
        }

        internal int GetBonusProfit(int daysRented)
        {
            return PriceCode.GetBonusProfit(daysRented);
        }
    }
}
