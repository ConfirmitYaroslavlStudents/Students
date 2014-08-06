using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FilmService
{
    [DataContract]
    public class DataForStatement:IEquatable<DataForStatement>
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Dictionary<string, double> RentalsData { get; set; }
        [DataMember]
        public double TotalAmount { get; set; }
        [DataMember]
        public int FrequentRenterPoints { get; set; }

        public DataForStatement()
        {
            Name = "";
            RentalsData = new Dictionary<string, double>();
            TotalAmount = 0;
            FrequentRenterPoints = 0;
        }

        public static bool operator ==(DataForStatement a, DataForStatement b)
        {
            return true;
        }

        public static bool operator !=(DataForStatement a, DataForStatement b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is DataForStatement)
            {
                return Equals(obj as DataForStatement);
            }
            return false;
        }

        public bool Equals(DataForStatement other)
        {
            var f1 = Name.Equals(other.Name);
            var f2 = TotalAmount == other.TotalAmount;
            var f3 = FrequentRenterPoints == other.FrequentRenterPoints;
            if (!f1 || !f2 || !f3)
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
    }
}
