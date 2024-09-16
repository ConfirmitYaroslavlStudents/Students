using System.Linq;
using BillSplitter.Models;
using Microsoft.EntityFrameworkCore;

namespace BillSplitter.Data
{
    public class CustomerRepository
    {
        private readonly DbContext _context;

        public CustomerRepository(DbContext context)
        {
            _context = context;
        }

        public void Add(Customer customer)
        {
            _context.Set<Customer>().Add(customer);
        }

        public Customer GetById(int customerId)
        {
            return _context.Set<Customer>().FirstOrDefault(x => x.Id == customerId);
        }

        public void DeleteById(int customerId)
        {
            var toDelete = _context.Set<Order>().Where(order => order.CustomerId == customerId);
            _context.Set<Order>().RemoveRange(toDelete);

            _context.Set<Customer>().Remove(GetById(customerId));
        }
    }
}