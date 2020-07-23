using BillSplitter.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BillSplitter.Controllers.Finder
{
    public class BillFinder : IFinder
    {
        public IEnumerable<int> Find(BillContext context, int customerId)
        {
            context.Bill.Load();
            context.Position.Load();
            context.Orders.Load();
            context.Customer.Load();

            var customer = context.Customer.Find(customerId);
            var order = customer.Orders.First();
            var position = context.Position.Find(order.PositionId);

            return new List<int> { context.Bill.FirstOrDefault(x => x.Positions.Contains(position)).Id };
        }
    }
}
