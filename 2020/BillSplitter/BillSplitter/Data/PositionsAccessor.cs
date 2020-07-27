using System.Collections.Generic;
using System.Linq;
using BillSplitter.Models;

namespace BillSplitter.Data
{
    public class PositionsAccessor
    {
        private BillContext _context;

        public PositionsAccessor(BillContext context)
        {
            _context = context;
        }

        public void AddPosition(Position pos)
        {
            _context.Position.Add(pos);
            _context.SaveChanges();
        }

        public Position GetPositionById(int id)
        {
            return _context.Position.FirstOrDefault(pos => pos.Id == id);
        }
    }
}