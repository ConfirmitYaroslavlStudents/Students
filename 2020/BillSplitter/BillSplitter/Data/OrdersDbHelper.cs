using System.Collections.Generic;
using BillSplitter.Models;
using BillSplitter.Models.InteractionLevel;

namespace BillSplitter.Data
{
    public class OrdersDbHelper
    {
        private BillContext _context;
        
        public OrdersDbHelper(BillContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void AddOrders(Customer customer, List<InteractionLevelPosition> positions)
        {
            foreach (var pos in positions)
            {
                if (pos.Selected && 1.0 * pos.QuantityNumerator / pos.QuantityDenomenator > double.Epsilon)
                {
                    var order = new Order
                    {
                        CustomerId = customer.Id,
                        PositionId = pos.Id,
                        Quantity = 1.0 * pos.QuantityNumerator / pos.QuantityDenomenator
                    };

                    AddOrder(order);
                }
            }
        }
    }
}