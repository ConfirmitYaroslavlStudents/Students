using System.Linq;
using BillSplitter.Models;

namespace BillSplitter.Data
{
    public class CustomerRepository
    {
        private readonly BillContext _context;

        public CustomerRepository(BillContext context)
        {
            _context = context;
        }

        public void AddC(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public Customer GetById(int customerId)
        {
            return _context.Customers.FirstOrDefault(x => x.Id == customerId);
        }

        public void DeleteById(int customerId)
        {
            var toDelete = _context.Orders.Where(order => order.CustomerId == customerId);
            _context.Orders.RemoveRange(toDelete);

            _context.Customers.Remove(GetById(customerId));

        }
    }
}