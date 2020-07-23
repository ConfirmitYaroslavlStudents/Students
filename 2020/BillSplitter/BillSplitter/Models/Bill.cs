using System.Collections.Generic;

namespace BillSplitter.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public virtual List<Position> Positions { get; set; }
        public virtual List<Customer> Customers { get; set; }
        public string Url { get; }
    }
}
