using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace VideoService
{
    [DataContract]
    public class Customer
    {
        private List<Rental> _rentals;

        public Customer(string name)
        {
            Name = name;
            Rentals = new List<Rental>();
            RentalsInformation = new Dictionary<string, int>();
        }

        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public Dictionary<string, int> RentalsInformation
        {
            get; set; 
        }

        public List<Rental> Rentals
        {
            get { return _rentals; }
            set
            {
                _rentals = value;
                if (value == null) return;
                foreach (var rental in value)
                {
                    RentalsInformation.Add(rental.Movie.Title, rental.DaysRented);
                }
            }
        }

        public StringBuilder GetStatement(IStatement statement)
        {
            return statement.GetStatement(this);
        }
    }
}
