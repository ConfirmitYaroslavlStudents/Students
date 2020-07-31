namespace BillSplitter.Models
{
    public class Order
    {
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
        public int CustomerId { get; set; }
        public int Id { get; set; }
        public double Quantity { get; set; }
    }
}
