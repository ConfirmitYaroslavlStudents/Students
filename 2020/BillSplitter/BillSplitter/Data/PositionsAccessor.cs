using System.Collections.Generic;
using System.Linq;
using BillSplitter.Models;

namespace BillSplitter.Data
{
    public class PositionsDbAccessor
    {
        private BillContext _context;

        public PositionsDbAccessor(BillContext context)
        {
            _context = context;
        }

        public void AddPosition(Position pos)
        {
            _context.Position.Add(pos);
            _context.SaveChanges();
        }

    }
}