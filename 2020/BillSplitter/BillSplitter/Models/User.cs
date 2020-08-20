using System.Collections.Generic;

namespace BillSplitter.Models
{
    public class User
    {
        public  int Id { get; set; }
        public virtual List<Bill> Bills { get; set; }
        public virtual List<Customer> Customers { get; set; }
        public string Name { get; set; }
        public string Provider { get; set; } = "LoginProvider";
    }
}
