namespace BillSplitter.Models
{
    public class ViewBillPositionModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal OriginalPrce { get; set; }
        public decimal ActualPrice { get; set; }
        public bool Selected { get; set; }
    }
}