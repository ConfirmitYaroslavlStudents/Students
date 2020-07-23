using System.Linq;
using BillSplitter.Models;

namespace BillSplitter.Data
{
    public class CustomerDbHelper
    {
        private BillContext _context;

        public CustomerDbHelper(BillContext context)
        {
            _context = context;
        }

        public void AddCustomer(Customer customer)
        {
            _context.Add(customer);
            _context.SaveChanges();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _context.Customer.FirstOrDefault(x => x.Id == customerId);
        }
    }
}