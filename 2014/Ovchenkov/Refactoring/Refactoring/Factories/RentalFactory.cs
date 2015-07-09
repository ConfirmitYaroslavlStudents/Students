using System.Collections.Generic;

namespace Refactoring.Factories
{
    public static class RentalFactory
    {
        public static string[] Titles { get; set; }
        public static TypeOfMovie[] PriceCodesOfMovies { get; set; }
        public static int[] DaysRented { get; set; }

        public static void GetRentalInformation(string title, TypeOfMovie priceCodesOfMovie, int daysRented)
        {
            Titles = new[] { title };
            PriceCodesOfMovies = new[] { priceCodesOfMovie };
            DaysRented = new[] { daysRented };
        }

        public static void GetRentalInformation(string[] titles, TypeOfMovie[] priceCodesOfMovies, int[] daysRented)
        {
            Titles = titles;
            PriceCodesOfMovies = priceCodesOfMovies;
            DaysRented = daysRented;
        }

        private static Movie BuildMovie(TypeOfMovie priceCode, string title)
        {
            return new Movie { PriceCode = priceCode, Title = title };
        }

        private static Rental BuildRental(TypeOfMovie priceCode, string title, int daysRented)
        {
            return new Rental {Movie = BuildMovie(priceCode, title), DaysRented = daysRented};
        }

        public static Rental[] Build()
        {
            List<Rental> rentals = null;
            if ((Titles.Length == PriceCodesOfMovies.Length) && (PriceCodesOfMovies.Length == DaysRented.Length))
            {
                 rentals = new List<Rental>();
                for (var i = 0; i < Titles.Length; ++i)
                {
                    rentals.Add(BuildRental(PriceCodesOfMovies[i], Titles[i], DaysRented[i]));
                }
            }

            return rentals != null ? rentals.ToArray() : new Rental[] {};
        }
    }
}
