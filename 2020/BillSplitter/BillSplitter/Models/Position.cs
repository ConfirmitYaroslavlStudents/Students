using System.Collections.Generic;
using BillSplitter.Models.InteractionLevel;

namespace BillSplitter.Models
{
    public class Position
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public virtual List<Order> Orders { get; set; }
        public int Quantity { get; set; }

        public InteractionLevelPosition GetInteractionLevelPosition()
        {
            return new InteractionLevelPosition
            {
                Id = Id,
                Name = Name,
                Price = Price,
                QuantityNumerator = Quantity
            };
        }
    }
}
