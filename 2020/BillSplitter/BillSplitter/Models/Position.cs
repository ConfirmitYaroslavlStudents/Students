using System.Collections.Generic;

namespace BillSplitter.Models
{
    public class Position
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
        public double Quantity { get; set; }
    }
}
