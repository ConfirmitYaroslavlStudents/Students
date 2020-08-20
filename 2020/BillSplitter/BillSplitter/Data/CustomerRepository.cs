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

        public void UpdateCustomerRoleById(int customerId, string role)
        {
            var customer = GetById(customerId);
            customer.Role = role;
        }

        public void DeleteById(int customerId)
        {
            var customer = GetById(customerId);

            var orders = customer.Orders;

            _context.Set<Order>().RemoveRange(orders);
            _context.Set<Customer>().Remove(customer);
        }
    }
}