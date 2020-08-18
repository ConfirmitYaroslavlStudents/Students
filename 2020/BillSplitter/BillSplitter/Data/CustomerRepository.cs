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
            var toDelete = _context.Set<Order>().Where(order => order.CustomerId == customerId);
            _context.Set<Order>().RemoveRange(toDelete);

            var customer = GetById(customerId);
            var admin = customer.Bill.Customers.Find(c => c.Role == "Admin");

            foreach (var position in customer.ManagedPositions)
            {
                position.ManagingCustomerId = admin.Id;
                position.ManagingCustomer = admin;
            }

            _context.Set<Customer>().Remove(customer);
        }
    }
}