using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Refactoring.StatementTools
{
    [DataContract]
    public class Statement
    {
        [DataMember] public string Name;
        [DataMember] public double TotalAmount;
        [DataMember] public int FrequentRenterPoints;
        [DataMember] public Dictionary<string, double> Movies;

        public Statement(Customer customer)
        {
            Movies = new Dictionary<string, double>();
            TotalAmount = 0;
            FrequentRenterPoints = 0;
            Name = customer.Name;

            foreach (var rental in customer.Rentals)
            {
                var priceOfMovie = rental.GetPrice();

                FrequentRenterPoints += rental.GetFrequentPoints();
                Movies[rental.Movie.Title] = priceOfMovie;
                TotalAmount += priceOfMovie;
            }
        }

        public void GetStatement(IStatementData data, IStatementFormater formatter)
        {
            if (data != null)
                formatter.GetStatement(data.SetData, this); 
            else
                formatter.GetStatement(null, this);
        }
    }
}