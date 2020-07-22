using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillSplitter.Models
{
    public class SummaryCustomerInfo
    {
        public Customer Customer { get; set; }
        public decimal Sum { get; set; }
    }
}
