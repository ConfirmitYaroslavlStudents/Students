using BillSplitter.Models;
using System.Collections.Generic;
using System.Linq;

namespace BillSplitter.Data
{
    public class CustomerBillBuilder
    {
        public List<Position> Build(Customer customer)
        {
            return customer
                .Orders
                .Select(order => new Position
                {
                    Name = order.Position.Name, 
                    Price = order.Position.Price * (decimal)order.Quantity
                })
                .ToList();
        }
    }
}
