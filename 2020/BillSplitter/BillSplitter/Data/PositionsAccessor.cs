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

        public Position GetPositionById(int positionId)
        {
            return _context.Position.FirstOrDefault(x => x.Id == positionId);
        }

        public bool DbContains(int positionId)
        {
            return GetPositionById(positionId) != null;
        }
    }
}