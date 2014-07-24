using System;


namespace RefactoringDemo.Steps.Step1
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
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                Title = value;
            }
        }


        public Price PriceCode { get; set; }

        public double GetCharge(int daysRented)
        {
            return PriceCode.GetCharge(daysRented);
        }

        public int GetFrequentRenterPoints(int daysRented)
        {
            return PriceCode.GetFrequentRenterPoints(daysRented);
        }
    }
}
