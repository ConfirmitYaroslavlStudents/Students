using System.Collections.Generic;

namespace BillSplitter.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public virtual List<Position> Positions { get; set; } = new List<Position>();
        public virtual List<Customer> Customers { get; set; } = new List<Customer>();
        public string Url { get; }
    }
}
