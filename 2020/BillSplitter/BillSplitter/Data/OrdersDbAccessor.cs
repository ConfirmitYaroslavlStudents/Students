using System.Linq;
using BillSplitter.Models;

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

        public void DeleteByUserAndPosition(int userId, int positionId)
        {
            var order = _context.Orders.FirstOrDefault(o => (o.Customer.UserId==userId && o.PositionId==positionId ));
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}