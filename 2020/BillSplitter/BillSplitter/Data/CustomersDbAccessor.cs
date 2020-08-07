using System.Linq;
using BillSplitter.Models;

namespace BillSplitter.Data
{
    public class CustomersDbAccessor
    {
        private readonly BillContext _context;

        public CustomersDbAccessor(BillContext context)
        {
            _context = context;
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _context.Customers.FirstOrDefault(x => x.Id == customerId);
        }

        public void DeleteById(int customerId)
        {
            var toDelete = _context.Orders.Where(order => order.CustomerId == customerId);
            _context.Orders.RemoveRange(toDelete);

            _context.Customers.Remove(GetCustomerById(customerId));

            _context.SaveChanges();
        }
    }
}