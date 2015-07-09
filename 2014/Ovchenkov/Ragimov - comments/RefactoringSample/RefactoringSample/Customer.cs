using System.Collections.Generic;

namespace RefactoringSample
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
            Rentals = new List<Rental>();
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
    }
}