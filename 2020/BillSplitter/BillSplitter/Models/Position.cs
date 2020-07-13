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
        public List<Customer> Customers { get; private set; }

        public void AddCustomer(Customer customer)
        {
            if (Customers == null) Customers = new List<Customer>(); 
            
            Customers.Add(customer);
        }
    }
}
