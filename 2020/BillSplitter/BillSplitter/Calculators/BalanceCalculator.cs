using BillSplitter.Models;
using System.Collections.Generic;
using System.Linq;

namespace BillSplitter.Calculators
{
    public class BalanceCalculator
    {
        private OrderPriceCalculator _calculator;

        public BalanceCalculator(OrderPriceCalculator calculator)
        {
            _calculator = calculator;
        }

        public Dictionary<string, decimal> CalculateBalances(Member member)
        {
            Dictionary<string, decimal> payments = new Dictionary<string, decimal>();

            foreach (var order in member.Orders)
            {
                var position = order.Position;
                var positionManager = position.ManagingMember;

                if (!payments.ContainsKey(positionManager.Name))
                    payments.Add(positionManager.Name, 0);

                payments[order.Position.ManagingMember.Name] += _calculator.CalculatePrice(order);
            }

            foreach (var position in member.ManagedPositions)
            {
                foreach (var order in position.Orders)
                {
                    if (!payments.ContainsKey(order.Member.Name))
                        payments.Add(order.Member.Name, 0);

                    payments[order.Member.Name] -= _calculator.CalculatePrice(order);
                }
            }

            return payments;
        }

        public Dictionary<string, decimal> CalculateDebts(Member member)
        {
            var balances = CalculateBalances(member);

            var debts = balances.Where(balance => balance.Value < 0)
                .ToDictionary(
                    balance => balance.Key,
                    balance => -balance.Value);

            return debts;
        }

        public Dictionary<string, decimal> CalculatePayments(Member member)
        {
            var balances = CalculateBalances(member);

            var payments = balances.Where(balance => balance.Value > 0)
                .ToDictionary(
                    balance => balance.Key,
                    balance => balance.Value);

            return payments;
        }
    }
}
