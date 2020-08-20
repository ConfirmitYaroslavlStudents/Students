using System.Linq;
using BillSplitter.Models;
using Microsoft.EntityFrameworkCore;

namespace BillSplitter.Data
{
    public class OrderRepository
    {
        private readonly DbContext _context;
        
        public OrderRepository(DbContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            _context.Set<Order>().Add(order);
        }

        public void DeleteByUserAndPosition(int userId, int positionId)
        {
            var order = _context.Set<Order>().FirstOrDefault(o => (o.Member.UserId == userId && o.PositionId == positionId ));
            if(order != null)
                _context.Set<Order>().Remove(order);
        }
    }
}