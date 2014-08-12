using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FilmService
{
    [DataContract]
    public class DataStore : IEquatable<DataStore>
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public Dictionary<string, double> RentalsData { get; private set; }
        [DataMember]
        public double TotalAmount { get; private set; }
        [DataMember]
        public int FrequentRenterPoints { get; private set; }

        public DataStore(string name, Dictionary<string, double> rentalsData, double totalAmount, int frequentRenterPoints)
        {
            Name = name;
            RentalsData = rentalsData;
            TotalAmount = totalAmount;
            FrequentRenterPoints = frequentRenterPoints;
        }
        public DataStore()
        {
            RentalsData = new Dictionary<string, double>();
        }

        public override bool Equals(object obj)
        {
            if (obj is DataStore)
            {
                return Equals(obj as DataStore);
            }
            return false;
        }
        public bool Equals(DataStore other)
        {
            if (!Name.Equals(other.Name) ||
                TotalAmount != other.TotalAmount ||
                FrequentRenterPoints != other.FrequentRenterPoints)
            {
                return false;
            }
            foreach (var item in RentalsData)
            {
                if (!other.RentalsData.ContainsKey(item.Key) ||
                    RentalsData[item.Key] != other.RentalsData[item.Key])
                {
                    return false;
                }
            }
            return true;
        }

        public void FormDataStore(string name, List<Rental> rentals)
        {
            Name = name;
            foreach (var rental in rentals)
            {
                var thisAmount = rental.Movie.CurrentCalculator.Calculate(rental.DaysRented);
                RentalsData[rental.Movie.Title] = thisAmount;
                FrequentRenterPoints += rental.Movie.CurrentCalculator.GetPoints();
                TotalAmount += thisAmount;
            }
        }
    }
}
