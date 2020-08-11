using BillSplitter.Models;
using System.Collections.Generic;
using System.Linq;
using BillSplitter.Calculators;

namespace BillSplitter.Data
{
    public class CustomerBillBuilder
    {
        public List<Position> Build(Customer customer)
        {
            var orders = customer.Orders;

            return orders.Select(order => new Position()
                {
                    Price = new OrderPriceCalculator().CalculatePrice(order),
                    Id = order.Position.Id,
                    Name = order.Position.Name
                })
                .ToList();
        }
    }
}
