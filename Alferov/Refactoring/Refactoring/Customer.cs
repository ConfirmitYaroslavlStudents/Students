using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Refactoring.Utils;

namespace Refactoring
{
    [DataContract]
    public class Customer
    {
        [XmlIgnore]
        [ScriptIgnore]
        public List<Rental> Rentals { get; private set; }

        [DataMember]
        public Dictionary<string, double> Movies { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public double TotalAmount { get; private set; }

        [DataMember]
        public int FrequentRenterPoints { get; private set; }

        private Customer(){}

        public Customer(string name)
        {
            Name = name;
            Rentals = new List<Rental>();
            Movies = new Dictionary<string, double>();
        }

        public Customer(string name, IEnumerable<Rental> rentals)
        {
            Name = name;
            Rentals = new List<Rental>(rentals);
            Movies = new Dictionary<string, double>();
        }

        public string GetStatement(CustomerFormatter formatter)
        {
            foreach (var rental in Rentals)
            {
                double thisAmount = rental.GetCharge();
                FrequentRenterPoints += rental.GetFrequentPoints();
                Movies.Add(rental.Movie.Title, thisAmount);
                TotalAmount += thisAmount;
            }

            return formatter.Serialize(this);
        }
    }
}