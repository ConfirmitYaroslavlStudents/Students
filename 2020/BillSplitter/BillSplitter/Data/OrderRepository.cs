using System.Linq;
using BillSplitter.Models;

namespace BillSplitter.Data
{
    public class OrderRepository
    {
        private readonly BillContext _context;
        
        public OrderRepository(BillContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void DeleteByUserAndPosition(int userId, int positionId)
        {
            var order = _context.Orders.FirstOrDefault(o => (o.Customer.UserId==userId && o.PositionId==positionId ));
            if(order!=null)
                _context.Orders.Remove(order);
        }
    }
}