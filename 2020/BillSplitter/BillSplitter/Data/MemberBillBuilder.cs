using BillSplitter.Models;
using System.Collections.Generic;
using System.Linq;
using BillSplitter.Calculators;

namespace BillSplitter.Data
{
    public class MemberBillBuilder
    {
        public List<Position> Build(Member member)
        {
            var orders = member.Orders;

            return orders.Select(order => new Position()
                {
                    Price = new OrderPriceCalculator().CalculatePrice(order),
                    Id = order.Position.Id,
                    Name = order.Position.Name,
                    ManagingMember = order.Position.ManagingMember
                })
                .ToList();
        }

       
    }
}
