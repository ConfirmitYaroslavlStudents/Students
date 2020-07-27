using System.Linq;
using BillSplitter.Models;

namespace BillSplitter.Data
{
    public class CustomerDbAccessor
    {
        private BillContext _context;

        public CustomerDbAccessor(BillContext context)
        {
            _context = context;
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            _context.SaveChanges();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _context.Customer.FirstOrDefault(x => x.Id == customerId);
        }
    }
}