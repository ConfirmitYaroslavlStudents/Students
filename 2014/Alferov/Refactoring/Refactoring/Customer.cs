using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Refactoring.Utils;

namespace Refactoring
{
    [DataContract]
    public class Customer : IEquatable<Customer>
    { 
        [DataMember]
        public List<Rental> Rentals { get; private set; }

        [DataMember]
        public Dictionary<string, double> Movies { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public double TotalAmount { get; set; }

        [DataMember]
        public int FrequentRenterPoints { get; set; }

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
        
        private Customer() {}

        public bool Equals(Customer other)
        {
            const double eps = 0.000001;

            if (Name != other.Name ||
                Math.Abs(TotalAmount - other.TotalAmount) > eps ||
                Math.Abs(FrequentRenterPoints - other.FrequentRenterPoints) > eps)
            {
                return false;
            }

            if (other.Movies.Count != Movies.Count)
            {
                return false;
            }

            foreach (string key in Movies.Keys)
            {
                if (!other.Movies.ContainsKey(key) || Math.Abs(other.Movies[key] - Movies[key]) > eps)
                {
                    return false;
                }
            }

            return true;
        }

        public SerializedData GetStatement(ICustomerFormatter formatter)
        {
            foreach (Rental rental in Rentals)
            {
                double thisAmount = rental.GetCharge();
                FrequentRenterPoints += rental.GetFrequentPoints();
                Movies.Add(rental.Movie.Title, thisAmount);
                TotalAmount += thisAmount;
            }

            var serializedData = new SerializedData();
            formatter.Serialize(serializedData, this);

            return serializedData;
        }
    }
}