using BillSplitter.Data;
using BillSplitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillSplitter.Controllers.Finder
{
    public class CustomerFinder : IFinder
    {
        public IEnumerable<Customer> Find(BillContext context, int billId)
        {


            return null;
        }
    }
}
