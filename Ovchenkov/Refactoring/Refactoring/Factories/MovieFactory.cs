using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class MovieFactory
    {
        public static Movie Build(TypeOfMovie priceCode, string title)
        {
            return new Movie { PriceCode = priceCode, Title = title };
        }
    }
}
