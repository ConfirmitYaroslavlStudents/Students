using BillSplitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillSplitter.Calculators
{
    public class DebtsCalculator
    {
        public Dictionary<string, decimal> CalculateDebts(Member member, List<Position> memberBill)
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
