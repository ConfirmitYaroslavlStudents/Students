using System.Collections.Generic;
using System.Linq;
using BillSplitter.Models;
using BillSplitter.Models.InteractionLevel;

namespace BillSplitter.Data
{
    public class OrdersDbAccessor
    {
        private readonly BillContext _context;
        
        public OrdersDbAccessor(BillContext context)
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
            _context.AddRange(positions
                .Select(pos => new Order
                {
                    CustomerId = customer.Id,
                    PositionId = pos.Id
                }));
            _context.SaveChanges();
        }

        public void DeleteByUserAndPosition(int userId, int positionId)
        {
            var order = _context.Orders.FirstOrDefault(o => (o.Customer.UserId==userId && o.PositionId==positionId ));
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}