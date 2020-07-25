namespace BillSplitter.Models.InteractionLevel
{
    public class InteractionLevelPosition
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int QuantityNumerator { get; set; } = 0;
        public int QuantityDenomenator { get; set; } = 1;
        public bool Selected { get; set; } = false;

        public Position ToPosition(int? billId = null)
        {
            return new Position
            {
                Name = Name,
                Price = Price,
                Quantity = QuantityNumerator,
                BillId = billId
            };
        }
    }
}