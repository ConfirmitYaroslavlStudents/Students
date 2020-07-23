using BillSplitter.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BillSplitter.Controllers.Finder
{
    public class CustomerFinder : IFinder
    {
        public IEnumerable<int> Find(BillContext context, int billId)
        {
            context.Bill.Load();
            context.Customer.Load();
            context.Position.Load();
            context.Orders.Load();

            var bill = context.Bill.FirstOrDefault(x => x.Id == billId);

            var customersId = (from o in context.Orders.ToList()
                               where bill.Positions.FirstOrDefault(x => x.Id == o.PositionId) != null
                               select o.CustomerId).Distinct();

            return customersId;
        }
    }
}
