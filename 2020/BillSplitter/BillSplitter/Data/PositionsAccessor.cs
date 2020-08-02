using System;
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

        public void UpdateById(int positionId, Position positionData)
        {
            positionData.Id = positionId;
            _context.Position.Update(positionData);
            _context.SaveChanges();
        }
        public bool DbContains(int positionId)
        {
            return GetPositionById(positionId) != null;
        }

        public void DeleteById(int positionId)
        {
            _context.Position.Remove(GetPositionById(positionId));
            _context.SaveChanges();
        }
    }
}