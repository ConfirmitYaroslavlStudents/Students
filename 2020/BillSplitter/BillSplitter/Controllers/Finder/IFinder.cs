using BillSplitter.Data;
using System.Collections.Generic;

namespace BillSplitter.Controllers.Finder
{
    public interface IFinder
    {
        IEnumerable<int> Find(BillContext context, int id);
    }
}
