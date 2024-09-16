using System.Linq;
using BillSplitter.Models;

namespace BillSplitter.Calculators
{
    public class OrderPriceCalculator
    {
        public decimal CalculatePrice(Order order)
        {
            if (order.Quantity == null)
                return CalculateEquallySharedPrice(order);

            var onePiecePrice = order.Position.Price;

            return (decimal)(order.Quantity * onePiecePrice);
        }

        private decimal CalculateEquallySharedPrice(Order order)
        {
            var onePiecePrice = order.Position.Price;
            var overallPrice = onePiecePrice * order.Position.Quantity;

            var equallySharedOrders = order.Position.Orders.Where(o => o.Quantity == null);
            var specificallySharedOrders = order.Position.Orders.Where(o => o.Quantity != null);

            var equallySharedPrice =
                overallPrice - specificallySharedOrders.Sum(o => o.Quantity * onePiecePrice);

            return (decimal)equallySharedPrice / equallySharedOrders.ToList().Count;
        }
    }
}