using BillSplitter.Data;
using BillSplitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillSplitter.Controllers.Calculator
{
    public class CustomerCalculator : ICalculator
    {
        public Tuple<List<Position>, decimal> Calculate(BillContext context, int customerId)
        {
            var customer = context.Customer.FirstOrDefault(x => x.Id == customerId);
            var positions = new List<Position>();

            var sum = 0m;
            foreach (var order in customer.Orders)
            {
                var posId = order.PositionId;
                var position = context.Position.FirstOrDefault(x => x.Id == posId);

                var customerPrice = position.Price * (decimal)order.Quantity;
                positions.Add(new Position { Name = position.Name, Price = customerPrice });

                sum += customerPrice;
            }

            return new Tuple<List<Position>, decimal>(positions, sum);
        }
    }
}
