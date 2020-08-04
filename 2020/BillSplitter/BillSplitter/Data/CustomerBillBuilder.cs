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
                Price = order.Position.Price*order.Position.Quantity / order.Position.Orders.Count,
                Id = order.Position.Id,
                Name = order.Position.Name
            }).ToList();
        }
    }
}
