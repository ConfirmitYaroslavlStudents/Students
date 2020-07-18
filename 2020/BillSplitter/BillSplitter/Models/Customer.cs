using System.Collections.Generic;

namespace BillSplitter.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        
        public int BillId { get; set; }
        public List<Order> Orders { get; set; }
    }
}
