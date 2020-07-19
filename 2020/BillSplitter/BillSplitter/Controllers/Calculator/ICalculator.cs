using BillSplitter.Data;
using BillSplitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillSplitter.Controllers.Calculator
{
    public interface ICalculator
    {
        Tuple<List<Position>, decimal> Calculate(BillContext context, int customerId);
    }
}
