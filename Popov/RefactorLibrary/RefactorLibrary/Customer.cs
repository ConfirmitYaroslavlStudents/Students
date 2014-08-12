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