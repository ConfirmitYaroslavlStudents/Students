using BillSplitter.Data;
using BillSplitter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillSplitter.Controllers.Finder
{
    public interface IFinder
    {
        IEnumerable<int> Find(BillContext context, int id);
    }
}
