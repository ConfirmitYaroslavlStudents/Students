using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VideoService
{
    [DataContract]
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
            Rentals = new List<Rental>();
        }

        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public List<Rental> Rentals { get; set; }

        public string GetStatement(IStatement statement)
        {
            return statement.GetStatement(this);
        }
    }
}
