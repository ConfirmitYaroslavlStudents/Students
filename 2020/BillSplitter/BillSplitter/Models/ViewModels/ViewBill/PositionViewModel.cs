namespace BillSplitter.Models.ViewModels.ViewBill
{
    public class PositionViewModel
    {
        public PositionViewModel(Position position)
        {
            Id = position.Id;
            Name = position.Name;
            OriginalPrice = position.Price;
            Quantity = position.Quantity;
            ActualPrice = 0;
            Selected = false;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal ActualPrice { get; set; }
        public bool Selected { get; set; }
    }
}