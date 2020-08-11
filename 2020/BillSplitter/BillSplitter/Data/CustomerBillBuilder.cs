using BillSplitter.Models;
using System.Collections.Generic;
using System.Linq;

namespace BillSplitter.Data
{
    public class CustomerBillBuilder
    {
        public List<Position> Build(Customer customer)
        {
            var orders = customer.Orders;

            return orders.Select(order => new Position()
                {
                    Price = CalculatePrice(order),
                    Id = order.Position.Id,
                    Name = order.Position.Name
                })
                .ToList();
        }

        private decimal CalculatePrice(Order order)
        {
            if (order.Quantity == null)
                return CalculateEquallySharedPrice(order);

            var onePiecePrice = order.Position.Price;
            var overallPrice = onePiecePrice * order.Position.Quantity;

            return (decimal)(order.Quantity * overallPrice);
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
