using System.Collections.Generic;
using System.Linq;


namespace RefactorLibrary
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
            Rentals = new List<Rental>();
        }

        public Customer(string name, List<Rental> rentals) : this(name)
        {
            Rentals = rentals;
        }

        //модификатор доступа set и оформление
        public string Name
        {
            get; 
            set;
        }

        public List<Rental> Rentals
        {
            get; 
            private set;
        }

        //Customer, Rental и Movie не должны уметь считать, SRP
        internal double GetTotalCharge()
        {
            return Rentals.Sum(rental => rental.GetCharge());
        }

        internal double GetBonusProfit()
        {
            return Rentals.Sum(rental => rental.GetBonusProfit());
        }
    }
}