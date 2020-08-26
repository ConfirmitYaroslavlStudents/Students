using System.Collections.Generic;
using System.Linq;
using BillSplitter.Models;

namespace BillSplitter.Calculators
{
    public class MemberBillBuilder
    {
        public OrderPriceCalculator Calculator { get; }

        public MemberBillBuilder(OrderPriceCalculator calculator)
        {
            Calculator = calculator;
        }

        public List<Position> Build(Member member)
        {
            var orders = member.Orders;

            return orders.Select(order => new Position
                {
                    Price = Calculator.CalculatePrice(order),
                    Id = order.Position.Id,
                    Name = order.Position.Name,
                    ManagingMember = order.Position.ManagingMember
                })
                .ToList();
        }
    }
}
