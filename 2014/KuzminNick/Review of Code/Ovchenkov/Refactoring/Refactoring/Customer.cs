using System.Collections.Generic;

namespace Refactoring
{
    public class Customer
    {
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

        //Change constructors to 'Factory' pattern or use redirection to constructor with max amount of parameters
        public Customer(string name)
        {
            Name = name;
            Rentals = new List<Rental>();
        }

        public Customer(string name, Rental rental)
        {
            Name = name;
            Rentals = new List<Rental>();
            AddRental(rental);
        }

        public Customer(string name, IEnumerable<Rental> rentals)
        {
            Name = name;
            Rentals = new List<Rental>(rentals);
        }

        public void AddRental(Rental rental)
        {
            Rentals.Add(rental);
        }
    }
}