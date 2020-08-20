namespace BillSplitter.Models
{
    public class Order
    {
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        public int Id { get; set; }
        public decimal? Quantity { get; set; }
    }
}
