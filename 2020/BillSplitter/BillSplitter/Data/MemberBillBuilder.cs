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

        public Dictionary<string, decimal> CountPayments(Member member, List<Position> memberBill)
        {

            Dictionary<string, decimal> payments = new Dictionary<string, decimal>();
            foreach (var pos in memberBill)
            {
                if (!payments.ContainsKey(pos.ManagingMember.Name))
                    payments.Add(pos.ManagingMember.Name, 0);

                payments[pos.ManagingMember.Name] += pos.Price;
            }
         

            foreach (var position in member.ManagedPositions)
            {
                foreach (var order in position.Orders)
                {

                    if (!payments.ContainsKey(order.Member.Name))
                        payments.Add(order.Member.Name, 0);

                    payments[order.Member.Name] -= new Calculators.OrderPriceCalculator().CalculatePrice(order);
                }
            }
            return payments;
        }
    }
}
