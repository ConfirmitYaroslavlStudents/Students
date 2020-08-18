using System.Collections.Generic;

namespace BillSplitter.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int BillId { get; set; }
        public virtual Bill Bill { get; set; }
        public virtual List<Order> Orders { get; set; } = new List<Order>();
        public string Role { get; set; }
    }
}
