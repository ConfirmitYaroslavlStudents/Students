using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillSplitter.Models
{
    public class Position
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public List<Customer> Customers { get; }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }
    }
}
