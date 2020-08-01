using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BillSplitter.Models;

namespace BillSplitter.Data
{
    public class CustomersDbAccessor
    {
        private BillContext _context;

        public CustomersDbAccessor(BillContext context)
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

        public void DeleteById(int customerId)
        {
            var toDelete = _context.Orders.Where(order => order.CustomerId == customerId);
            _context.Orders.RemoveRange(toDelete);

            _context.Customer.Remove(GetCustomerById(customerId));

            _context.SaveChanges();
        }

      
        public bool DbContains(int customerId)
        {
            return GetCustomerById(customerId) != null;
        }

    }
}