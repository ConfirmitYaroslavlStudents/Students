using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class RentalFactory
    {
        public static Rental Build(Movie movie, int daysRented)
        {
            return new Rental(movie, daysRented);
        }

        public static Rental Build(TypeOfMovie priceCode, string title, int daysRented)
        {
            return new Rental(MovieFactory.Build(priceCode, title), daysRented);
        }
    }
}
